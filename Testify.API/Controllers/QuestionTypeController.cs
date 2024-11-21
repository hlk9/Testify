using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("QuestionType")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        QuestionTypeReposiroty _repo;

        public QuestionTypeController()
        {
            _repo = new QuestionTypeReposiroty();
        }

        [HttpGet("Get-All-Question-Type")]
        public async Task<ActionResult<List<QuestionType>>> GetAllQuestionTypes()
        {
            var lstQuestionType = await _repo.GetAllTypes();
            return Ok(lstQuestionType);
        }

        [HttpGet("Get-Question-Type-By-Id")]
        public async Task<ActionResult<QuestionType>> GetQuestionTypeById(int id)
        {
            var QuestionType = await _repo.GetTypeById(id);
            return Ok(QuestionType);
        }

        [HttpPost("Create-Question-Type")]
        public async Task<ActionResult<QuestionType>> Create(QuestionType questionType)
        {
            var createQuestionType = await _repo.CreateType(questionType);
            return Ok(createQuestionType);
        }

        [HttpPut("Update-Question-Type")]
        public async Task<ActionResult<QuestionType>> Update(QuestionType questionType)
        {
            var updateQuestionType = await _repo.UpdateType(questionType);
            return Ok(updateQuestionType);
        }

        [HttpDelete("Delete-Question-Type")]
        public async Task<ActionResult<QuestionType>> Delete(int id)
        {
            var deleteQuestionType = await _repo.DeleteType(id);
            return Ok(deleteQuestionType);
        }
    }
}
