using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Edit;
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

        [HttpGet]
        [Authorize(Roles= "patient")]
        public async Task<ActionResult<PatientDetailsResponse>> GetPatinetDetails()
        {
            try
            {
                string id = User.FindFirst("id")?.Value!;

                var response = await _patientService.GetPatientDetails(id);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("all/details")]
        [AllowAnonymous]
        public async Task<ActionResult<PatientDetailsResponse>> GetPatinetsDetails(uint page = 1, uint limit = 10)
        {
            try
            {

                var response = await _patientService.GetPatientsDetails(page, limit);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientRegisterResponse>> AddPatient(PatientRegisterRequest request)
        {
            try
            {
                var response = await _patientService.AddPatient(request);

                return Created(string.Empty, response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<PatientEditResponse>> EditPatientInfo(PatientEditRequest request)
        {
            try
            {
                string id = User.FindFirst("id")?.Value!;

                var response = await _patientService.EditPatient(request, id);

                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "Database error");
            }
            catch (Exception)
            {
                return StatusCode(500, "Could not update the pateint info!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult> DeletePatient(string id)
        {
            try
            {
                bool response = await _patientService.DeletePatient(id);

                return response == true ? NoContent() : BadRequest($"Could not delete patient with person id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
