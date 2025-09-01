using Core.DTOs.Study.Create;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            } catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
