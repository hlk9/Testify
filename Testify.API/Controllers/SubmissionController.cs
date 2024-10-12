using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("submission")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        SubmissionReposiroty _repo;

        public SubmissionController()
        {
            _repo = new SubmissionReposiroty();
        }

        [HttpGet("Get-All-Submission")]
        public async Task<ActionResult<List<Submission>>> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet("Get-By-Id")]
        public async Task<ActionResult<Submission>> GetById(int id)
        {
            return Ok(await _repo.GetById(id));
        }

        [HttpPost("Create-Submission")]
        public async Task<ActionResult<Submission>> Create(Submission submission)
        {
            var obj = await _repo.Create(submission);
            return Ok(obj);
        }
    }
}
