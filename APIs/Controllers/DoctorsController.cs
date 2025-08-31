using APIs.Services;
using Core.DTOs.Doctor.Edit;
using Core.DTOs.Doctor.Register;
using Core.DTOs.Doctor.Studies;
using Core.Interfaces.Services;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController(IDoctorService doctorService) : ControllerBase
    {
        private readonly IDoctorService _doctorService = doctorService;

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public async Task<ActionResult<DoctorRegisterResponse>> AddDoctor(DoctorRegisterRequest request)
        {
            try
            {
                var response = await _doctorService.AddDoctor(request);

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
                return BadRequest("Could not add this doctor!");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Authorize(Roles = "doctor")]
        public async Task<ActionResult<DoctorEditResponse?>> EditDoctorInfo(DoctorEditRequest request)
        {
            try
            {
                string id = User.FindFirst("id")?.Value!;

                var response = await _doctorService.EditDoctor(request, id);

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
                return StatusCode(500, "Could not update the doctor info!");
            }
        }

        [HttpGet("studies")]
        [Authorize(Roles = "doctor")]
        public async Task<ActionResult<List<DoctorStudiesResponse>>>GetDoctorStudies(uint page = 1, uint limit = 10)
        {
            try
            {
                string id = User.FindFirst("id")?.Value!;

                var response = await _doctorService.GetDoctorStudies(id, page, limit);

                return Ok(response);
            }
            catch(ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting doctor studies");
            }
        }
    }
}
