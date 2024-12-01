using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("QuestionLevel")]
    [ApiController]
    public class QuestionLevelController : ControllerBase
    {
        QuestionLevelReposiroty _repo;

        public QuestionLevelController()
        {
            _repo = new QuestionLevelReposiroty();
        }

        [HttpGet("Get-All-Question-Level")]
        public async Task<ActionResult<List<QuestionLevel>>> GetAllQuestionTypes(string? keyword)
        {
            var lstQuestionLevel = await _repo.GetAllLevels(keyword);
            return Ok(lstQuestionLevel);
        }


        [HttpGet("Get-Question-Level-By-Id")]
        public async Task<ActionResult<QuestionLevel>> GetQuestionTypeById(int id)
        {
            var QuestionLevel = await _repo.GetLevelById(id);
            return Ok(QuestionLevel);
        }

        [HttpPost("Create-Question-Level")]
        public async Task<ActionResult<QuestionLevel>> Create(QuestionLevel QuestionLevel)
        {
            var createQuestionType = await _repo.CreateLevel(QuestionLevel);
            return Ok(createQuestionType);
        }

        [HttpPut("Update-Question-Level")]
        public async Task<ActionResult<QuestionLevel>> Update(QuestionLevel QuestionLevel)
        {
            var updateQuestionType = await _repo.UpdateLevel(QuestionLevel);
            return Ok(updateQuestionType);
        }

        [HttpDelete("Delete-Question-Level")]
        public async Task<ActionResult<QuestionLevel>> Delete(int id)
        {
            var deleteQuestionType = await _repo.DeleteLevel(id);
            return Ok(deleteQuestionType);
        }
    }
}
