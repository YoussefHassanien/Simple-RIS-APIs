using Core;
using Core.DTOs.Doctor.Register;
using Core.DTOs.Doctor.Edit;
using Core.Interfaces.Services;
using Core.Models;
using Core.DTOs.Doctor.Studies;

namespace APIs.Services
{
    public class DoctorService(IUnitOfWork unitOfWork) : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private static uint? ParseId(string id)
        {
            if (!uint.TryParse(id, out var parsedId))
                return null;

            return parsedId;
        }

        public async Task<DoctorRegisterResponse?> AddDoctor(DoctorRegisterRequest request)
        {
            var person = await _unitOfWork.Persons.GetById(request.PersonId, ["Doctor"]) ?? throw new NullReferenceException($"Person with id: {request.PersonId} not found!");
                

            if (person.Doctor is not null)
                throw new ArgumentException("This doctor already exists");

            var doctor = new Doctor
            {
                PersonId = (int)request.PersonId,
                Salary = request.Salary,
                CurrencyCode = request.CurrencyCode is not null ? request.CurrencyCode : "EGP",
                Expertise = request.Expertise
            };

            await _unitOfWork.Doctors.Add(doctor);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException();

            return new DoctorRegisterResponse
            {
                PersonId = person.Id,
                Email = person.Email!,
                Name = $"{person.FirstName} {person.LastName}",
                MobileNumber = person.MobileNumber,
                Gender = person.Gender,
                SocialSecurityNumber = person.SocialSecurityNumber,
                DateOfBirth = person.DateOfBirth,
                Salary = doctor.Salary,
                CurrencyCode = doctor.CurrencyCode,
                Expertise = doctor.Expertise
            };
        }

        public async Task<DoctorEditResponse?> EditDoctor(DoctorEditRequest request, string id)
        {
            uint? personId = ParseId(id) ?? throw new ArgumentException("Invalid id");

            var person = await _unitOfWork.Persons.GetById((uint)personId!) ?? throw new NullReferenceException($"Doctor with person id: {id} not found!");

            if (!string.IsNullOrEmpty(request.FirstName))
                person.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                person.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.MobileNumber))
            {
                var mobileNumberFlag = await _unitOfWork.Persons.GetByMobileNumber(request.MobileNumber);

                if (mobileNumberFlag is not null)
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

            return new DoctorEditResponse
            {
                Name = $"{person.FirstName} {person.LastName}",
                MobileNumber = person.MobileNumber,
                Gender = person.Gender,
                SocialSecurityNumber = person.SocialSecurityNumber,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
            };
        }

        public async Task<List<DoctorStudiesResponse>> GetDoctorStudies(string id, uint page = 1, uint limit = 10)
        {
            uint? personId = ParseId(id) ?? throw new ArgumentException("Invalid id");

            var person = await _unitOfWork.Persons.GetById((uint)personId!, ["Doctor"]) ?? throw new NullReferenceException($"Doctor with person id: {id} not found!");

            if (person.Doctor is null)
                throw new NullReferenceException("Doctor not found!");

            var studies = await _unitOfWork.Studies.GetByDoctorId((uint)person.Doctor!.Id, page, limit, ["Service", "Patient", "Patient.Person"]);

            var response = new List<DoctorStudiesResponse>();

            foreach ( var study in studies)
            {
                response.Add(new DoctorStudiesResponse
                {
                    PatientPersonId = (uint)study!.Patient.PersonId,
                    PatientName = $"{study.Patient.Person.FirstName} {study.Patient.Person.LastName}",
                    PatientMobileNumber = study.Patient.Person.MobileNumber,
                    PatientDateOfBirth = study.Patient.Person.DateOfBirth,
                    ServiceId = (uint)study.ServiceId,
                    ServiceType = study.Service.Type,
                    ServiceDescription = study.Service.Description,
                    ServiceCurrencyCode = study.Service.CurrencyCode,
                    ServiceCost = study.Service.Cost,
                    StudyStatus = study.Status,
                    StudyDate = study.UpdatedAt
                });
            }

            return response;
        }
    }
}
