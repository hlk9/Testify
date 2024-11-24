using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("AnswerSubmission")]
    [ApiController]
    public class AnswerSubmissionController : ControllerBase
    {
        AnswerSubmissionReposiroty _repo;

        public AnswerSubmissionController()
        {
            _repo = new AnswerSubmissionReposiroty();
        }

        [HttpPost("Create-AnswerSubmission")]
        public async Task<ActionResult<AnswerSubmission>> Create(AnswerSubmission newAnswerSubmission)
        {
            var obj = await _repo.Create(newAnswerSubmission);
            return Ok(obj);
        }

        [HttpGet("Get-AnswerById")]
        public  List<AnswerSubmission> GetAnswerById(int id)
        {
            var obj =  _repo.GetAnswersByIdSubmission(id);
            return obj;
        }
    }
}
