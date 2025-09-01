using Core;
using Core.DTOs.Study.Create;
using Core.DTOs.Study.Edit;
using Core.Interfaces.Services;
using Core.Models;
using System.Numerics;

namespace APIs.Services
{
    public class StudyService(IUnitOfWork unitOfWork) : IStudyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private static uint? ParseId(string id)
        {
            if (!uint.TryParse(id, out var parsedId))
                return null;

            return parsedId;
        }

        public async Task<StudyCreateResponse?> CreateStudy(string patientPersonId, StudyCreateRequest request)
        {
            uint parsedPatientPersonId = ParseId(patientPersonId) ?? throw new ArgumentException("Invalid patient perosn id");

            var person = await _unitOfWork.Persons.GetById(parsedPatientPersonId, ["Patient"]) ?? throw new NullReferenceException($"Patient with person id {parsedPatientPersonId} not found!");
            var doctor = await _unitOfWork.Doctors.GetById(request.DoctorId, ["Person"]) ?? throw new NullReferenceException($"Doctor with id {request.DoctorId} not found!");
            var service = await _unitOfWork.Services.GetById(request.ServiceId) ?? throw new NullReferenceException($"Patient with id {request.ServiceId} not found!");

            var study = new Study
            {
                PatientId = person!.Patient!.Id,
                DoctorId = (int)request.DoctorId,
                ServiceId = (int)request.ServiceId,
            };

            var createdStudy = await _unitOfWork.Studies.Add(study);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException("Could not add study to the database");

            return new StudyCreateResponse
            {
                PatientPersonId = (uint)person.Patient.Id,
                PatientName = $"{person.FirstName} {person.LastName}",
                PatientMobileNumber = person.MobileNumber,
                PatientDateOfBirth = person.DateOfBirth,
                ServiceId = (uint)service.Id,
                ServiceType = service.Type,
                ServiceDescription = service.Description,
                ServiceCurrencyCode = service.CurrencyCode,
                ServiceCost = service.Cost,
                StudyStatus = createdStudy!.Status,
                StudyCreationDate = createdStudy.CreatedAt,
                DoctorName = $"{doctor.Person.FirstName} {doctor.Person.LastName}",
            };
        }

        public async Task<bool> CompleteStudy(string studyId, string doctorPersonId)
        {
            uint parsedDoctorPersonId = ParseId(doctorPersonId) ?? throw new ArgumentException("Invalid doctor person id");

            var person = await _unitOfWork.Persons.GetById(parsedDoctorPersonId, ["Doctor"]) ?? throw new NullReferenceException($"Doctor with person id {parsedDoctorPersonId} not found!");

            uint parsedStudyId = ParseId(studyId) ?? throw new ArgumentException("Invalid study id");

            var study = await _unitOfWork.Studies.GetById(parsedStudyId) ?? throw new NullReferenceException($"Study with id: {parsedStudyId} not found!");

            if (study.DoctorId != person!.Doctor!.Id)
                throw new UnauthorizedAccessException();

            study.Status = "completed";

            _unitOfWork.Studies.Update(study);
            int affectedRows = _unitOfWork.Complete();

            return affectedRows == 1;
        }

        public async Task<StudyCreateResponse?> EditStudy(string studyId, StudyEditRequest request, string patientPersonId)
        {
            uint parsedStudyId = ParseId(studyId) ?? throw new ArgumentException("Invalid study id");

            var study = await _unitOfWork.Studies.GetById(parsedStudyId, ["Patient.Person"]) ?? throw new NullReferenceException($"Study with id: {parsedStudyId} not found!");

            uint parsedPatientPersonId = ParseId(patientPersonId) ?? throw new ArgumentException("Invalid patient person id");

            if(study.Patient.Person.Id != parsedPatientPersonId)
                throw new UnauthorizedAccessException();

            if (request.DoctorId is not null)
                study.DoctorId = (int)request.DoctorId;

            if(request.ServiceId is not null)
                study.ServiceId = (int)request.ServiceId;

            _unitOfWork.Studies.Update(study);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException("Could not edit study data");

            var updatedStudy = await _unitOfWork.Studies.GetById(parsedStudyId, ["Doctor.Person", "Patient.Person", "Service"]);

            return new StudyCreateResponse
            {
                PatientPersonId = (uint)updatedStudy!.Patient.Person.Id,
                PatientName = $"{updatedStudy.Patient.Person.FirstName} {updatedStudy.Patient.Person.LastName}",
                PatientMobileNumber = updatedStudy!.Patient.Person.MobileNumber,
                PatientDateOfBirth = updatedStudy!.Patient.Person.DateOfBirth,
                ServiceId = (uint)updatedStudy.Service.Id,
                ServiceType = updatedStudy.Service.Type,
                ServiceDescription = updatedStudy.Service.Description,
                ServiceCurrencyCode = updatedStudy.Service.CurrencyCode,
                ServiceCost = updatedStudy.Service.Cost,
                StudyStatus = updatedStudy.Status,
                StudyCreationDate = updatedStudy.CreatedAt,
                DoctorName = $"{updatedStudy.Doctor.Person.FirstName} {updatedStudy.Doctor.Person.LastName}",
            };
        }

        public async Task<bool> CancelStudy(string studyId, string patientPersonId)
        {
            uint parsedStudyId = ParseId(studyId) ?? throw new ArgumentException("Invalid study id");

            var study = await _unitOfWork.Studies.GetById(parsedStudyId, ["Patient.Person"]) ?? throw new NullReferenceException($"Study with id: {parsedStudyId} not found!");

            uint parsedPatientPersonId = ParseId(patientPersonId) ?? throw new ArgumentException("Invalid patient personId id");

            if (study.Patient.Person.Id != parsedPatientPersonId)
                throw new UnauthorizedAccessException();

            if (study.Status == "completed")
                throw new ArgumentException("Can't cancel completed study");

            _unitOfWork.Studies.Delete(study);
            int affectedRows = _unitOfWork.Complete();

            return affectedRows == 1;
        }
    }
}
