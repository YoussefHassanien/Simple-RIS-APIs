using Core;
using Core.DTOs.Patient.Details;
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

    }
}
