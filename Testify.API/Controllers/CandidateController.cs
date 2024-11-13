using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        CandidateRepository _repo;
        public CandidateController()
        {
            _repo = new CandidateRepository();
        }

        [HttpGet("Get-All-Candidate")]
        public async Task<ActionResult<List<User>>> GetlAllCandidate()
        {
            var lstCandidate = await _repo.GetAllCandidate();
            return Ok(lstCandidate);
        }

        [HttpGet("Get-Candidate-By-Id")]
        public async Task<ActionResult<User>> GetCandidateById(Guid id)
        {
            var candidate = await _repo.GetCandidateById(id);
            return Ok(candidate);
        }

        [HttpPost("Create-Candidate")]
        public async Task<ActionResult<User>> Create(User user)
        {
            var createCandidate = await _repo.CreateCandidate(user);
            return Ok(createCandidate);
        }
        [HttpPut("Update-Candidate")]
        public async Task<ActionResult<User>> Update(User user)
        {
            var updateCandidate = await _repo.UpdateCandidate(user);
            return Ok(updateCandidate);
        }

        [HttpDelete("Delete-Candidate")]
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            var deleteCandidate = await _repo.DeleteCandidate(id);
            return Ok(deleteCandidate);
        }
    }
}
