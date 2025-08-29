using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Edit;
using Core.DTOs.Patient.Register;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var response = await _patientService.GetPatientDetails(id, email!);

                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost("register")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientRegisterResponse>> AddPatient(PatientRegisterRequest request)
        {
            try
            {
                var response = await _patientService.AddPatient(request);

                return Created(string.Empty, response);
            }
            catch (InvalidOperationException)
            { 
                return BadRequest("Could not add this patient!");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("edit")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientEditResponse>> EditPatientInfo(PatientEditRequest request)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var response = await _patientService.EditPatient(request, email!);

                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not update the pateint info!");
            }
        }
    }
}
