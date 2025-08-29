using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Register;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        private readonly IPatientService _patientService = patientService;

        [HttpGet("details/{id}")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientDetailsResponse>> GetPatinetDetails(uint id)
        {
            var response = await _patientService.GetPatientDetails(id);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("register")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientRegisterResponse>> AddPatient(PatientRegisterRequest request)
        {
            var response = await _patientService.AddPatient(request);

            if (response is null)
                return BadRequest("Could not add this patient!");

            return Created(string.Empty, response);
        }
    }
}
