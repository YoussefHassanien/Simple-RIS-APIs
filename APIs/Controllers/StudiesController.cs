using Core.DTOs.Study.Create;
using Core.DTOs.Study.Edit;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudiesController(IStudyService studyService) : ControllerBase
    {
        private readonly IStudyService _studyService = studyService;

        [HttpPost]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<StudyCreateResponse>> CreateStudy(StudyCreateRequest request)
        {
            try
            {
                var patientPersonId = User.FindFirst("id")?.Value!;

                var response = await _studyService.CreateStudy(patientPersonId, request);

                return Created(string.Empty, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("complete/{studyId}")]
        [Authorize(Roles = "doctor")]
        public async Task<ActionResult> CompleteStudy(string studyId)
        {
            try
            {
                var doctorPersonId = User.FindFirst("id")?.Value!;

                var response = await _studyService.CompleteStudy(studyId, doctorPersonId);

                return response == true ? Ok("Study status become completed") : StatusCode(500);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
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

        [HttpPatch("{studyId}")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult<StudyCreateResponse>> EditStudy(string studyId, StudyEditRequest request)
        {
            try
            {
                var patientPersonId = User.FindFirst("id")?.Value!;

                var response = await _studyService.EditStudy(studyId, request, patientPersonId);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("cancel/{studyId}")]
        [Authorize(Roles = "patient")]
        public async Task<ActionResult> CancelStudy(string studyId)
        {
            try
            {
                var patientPersonId = User.FindFirst("id")?.Value!;

                var response = await _studyService.CancelStudy(studyId, patientPersonId);

                return response == true ? NoContent() : StatusCode(500);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
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

    }
}
