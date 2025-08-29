using Core;
using Core.Models;
using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Register;
using Core.Interfaces.Services;

namespace APIs.Services
{
    public class PatientService(IUnitOfWork unitOfWork) : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PatientDetailsResponse?> GetPatientDetails(uint personId)
        {
            var patientData = await _unitOfWork.PatientData.GetByPersonId(personId);

            if (patientData == null)
                return null;

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
                return null;

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

    }
}
