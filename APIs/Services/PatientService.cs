using Core;
using Core.Models;
using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Register;
using Core.Interfaces.Services;
using Core.DTOs.Patient.Edit;
using System.Reflection.Metadata.Ecma335;

namespace APIs.Services
{
    public class PatientService(IUnitOfWork unitOfWork) : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PatientDetailsResponse?> GetPatientDetails(uint personId, string email)
        {
            var person = await _unitOfWork.Persons.GetByEmail(email);

            if (person == null || person.Id != personId)
                throw new UnauthorizedAccessException();

            var patientData = await _unitOfWork.PatientData.GetByPersonId(personId) ?? throw new NullReferenceException("Patient not found!");

            var dto = new PatientDetailsResponse
            {
                PersonId = patientData.PersonId,
                PatientName = $"{patientData.FirstName} {patientData.LastName}",
                MobileNumber = patientData.MobileNumber,
                DateOfBirth = patientData.DateOfBirth,
                Gender = patientData.Gender,
                SocialSecurityNumber = patientData.SocialSecurityNumber,
                PatientId = patientData.PatientId,
                IsVip = patientData.IsVip,
                IsActive = patientData.IsActive,
                StudyId = patientData.StudyId,
                StudyCreatedAt = patientData.StudyCreatedAt,
                StudyUpdatedAt = patientData.StudyUpdatedAt,
                ServiceId = patientData.ServiceId,
                ServiceType = patientData.ServiceType,
                ServiceDescription = patientData.ServiceDescription,
                ServiceCost = patientData.ServiceCost,
                ServiceCurrency = patientData.ServiceCurrency,
                DoctorId = patientData.DoctorId,
                DoctorName = $"{patientData.DoctorFirstName} {patientData.DoctorLastName}"
            };

            return dto;
        }

        public async Task<PatientRegisterResponse?> AddPatient(PatientRegisterRequest request)
        {
            var patient = new Patient
            {
                PersonId = (int)request.PersonId,
                IsVip = request.IsVip,
            };

            await _unitOfWork.Patients.Add(patient);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException();

            var person = await _unitOfWork.Persons.GetById(request.PersonId, ["Patient"]);

            var response = new PatientRegisterResponse
            {
                PersonId = person!.Id,
                Email = person!.Email!,
                Name = $"{person.FirstName} {person.LastName}",
                MobileNumber = person!.MobileNumber,
                Gender = person!.Gender,
                SocialSecurityNumber = person!.SocialSecurityNumber,
                DateOfBirth = person!.DateOfBirth,
                IsVip = person!.Patient!.IsVip,
                IsActive = person!.Patient.IsActive,
            };

            return response;
        }

        public async Task<PatientEditResponse?> EditPatient(PatientEditRequest request, string email)
        {
            var person = await _unitOfWork.Persons.GetByEmail(email) ?? throw new UnauthorizedAccessException();
                
            if (!string.IsNullOrEmpty(request.FirstName))
                person.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                person.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.MobileNumber))
            {
                var mobileNumberFlag = await _unitOfWork.Persons.GetByMobileNumber(request.MobileNumber);

                if (mobileNumberFlag != null)
                    throw new ArgumentException("This mobile number already exists");
                else
                    person.MobileNumber = request.MobileNumber;
            }
                

            if (!string.IsNullOrEmpty(request.Gender))
                person.Gender = request.Gender;

            if (request.DateOfBirth.HasValue)
                person.DateOfBirth = request.DateOfBirth.Value;

            _unitOfWork.Persons.Update(person);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException();

            return new PatientEditResponse
            {
                Name = $"{person.FirstName} {person.LastName}",
                MobileNumber = person.MobileNumber,
                Gender = person.Gender,
                SocialSecurityNumber = person.SocialSecurityNumber,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
            };
        }

    }
}
