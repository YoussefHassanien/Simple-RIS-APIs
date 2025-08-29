using Core.DTOs.Patient.Details;
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
    }
}
