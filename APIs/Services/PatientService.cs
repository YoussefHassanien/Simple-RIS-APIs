using Core;
using Core.Models;
using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Register;
using Core.Interfaces.Services;
using Core.DTOs.Patient.Edit;

namespace APIs.Services
{
    public class PatientService(IUnitOfWork unitOfWork) : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private static uint? ParseId(string id)
        {
            if (!uint.TryParse(id, out var parsedId))
                return null;

            return parsedId;
        }

        public async Task<PatientDetailsResponse?> GetPatientDetails(string id)
        {
            uint? personId = ParseId(id) ?? throw new ArgumentException("Invalid id");
                
            var patientData = await _unitOfWork.PatientData.GetByPersonId((uint)personId);

            if (patientData is null || !(bool)patientData.IsActive!)
                throw new UnauthorizedAccessException();

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
                DoctorName = patientData.DoctorFirstName is not null && patientData.DoctorLastName is not null ? $"{patientData.DoctorFirstName} {patientData.DoctorLastName}" : null,
            };

            return dto;
        }

        public async Task<List<PatientDetailsResponse>> GetPatientsDetails(uint page = 1, uint limit = 10)
        {

            var patienstData = await _unitOfWork.PatientData.GetAll(page, limit);

            var response = new List<PatientDetailsResponse>();

            foreach (var patient in patienstData)
            {
                response.Add(new PatientDetailsResponse
                {
                    PersonId = patient.PersonId,
                    PatientName = $"{patient.FirstName} {patient.LastName}",
                    MobileNumber = patient.MobileNumber,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    SocialSecurityNumber = patient.SocialSecurityNumber,
                    PatientId = patient.PatientId,
                    IsVip = patient.IsVip,
                    IsActive = patient.IsActive,
                    StudyId = patient.StudyId,
                    StudyCreatedAt = patient.StudyCreatedAt,
                    StudyUpdatedAt = patient.StudyUpdatedAt,
                    ServiceId = patient.ServiceId,
                    ServiceType = patient.ServiceType,
                    ServiceDescription = patient.ServiceDescription,
                    ServiceCost = patient.ServiceCost,
                    ServiceCurrency = patient.ServiceCurrency,
                    DoctorId = patient.DoctorId,
                    DoctorName = patient.DoctorFirstName is not null && patient.DoctorLastName is not null ? $"{patient.DoctorFirstName} {patient.DoctorLastName}" : null,
                });
            }

            return response;
        }

        public async Task<PatientRegisterResponse?> AddPatient(PatientRegisterRequest request)
        {
            var person = await _unitOfWork.Persons.GetById(request.PersonId, ["Patient"]) ?? throw new NullReferenceException($"Person with id: {request.PersonId} not found!");

            if (person.Patient is not null)
                throw new ArgumentException("This patient already exists");

            var patient = new Patient
            {
                PersonId = (int)request.PersonId,
                IsVip = request.IsVip,
            };

            await _unitOfWork.Patients.Add(patient);
            int affectedRows = _unitOfWork.Complete();

            if (affectedRows != 1)
                throw new InvalidOperationException();

            return new PatientRegisterResponse
            {
                PersonId = person!.Id,
                Email = person!.Email!,
                Name = $"{person.FirstName} {person.LastName}",
                MobileNumber = person!.MobileNumber,
                Gender = person!.Gender,
                SocialSecurityNumber = person!.SocialSecurityNumber,
                DateOfBirth = person!.DateOfBirth,
                IsVip = patient.IsVip,
            };

            
        }

        public async Task<PatientEditResponse?> EditPatient(PatientEditRequest request, string id)
        {
            uint? personId = ParseId(id) ?? throw new ArgumentException("Invalid id");

            var person = await _unitOfWork.Persons.GetById((uint)personId!) ?? throw new NullReferenceException($"Patient with person id: {id} not found!"); 
                
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

        public async Task<bool> DeletePatient(string id)
        {
            uint? personId = ParseId(id) ?? throw new ArgumentException("Invalid id");

            var person = await _unitOfWork.Persons.GetById((uint)personId, ["Patient"]) ?? throw new NullReferenceException();

            var deletedPatient = new Patient
            {
                PersonId = person.Id,
                IsVip = person.Patient!.IsVip,
                IsActive = false,
                UpdatedAt = DateTime.UtcNow,
            };

            _unitOfWork.Patients.Update(deletedPatient);
            int affectedRows = _unitOfWork.Complete();

            return affectedRows == 2;
            
                
        }

    }
}
