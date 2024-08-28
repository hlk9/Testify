using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        AnswerReposiroty _repoAnswer;

        public AnswerController()
        {
            _repoAnswer = new AnswerReposiroty();
        }

        [HttpGet("Get-All-Answers")]
        public async Task<ActionResult<List<Answer>>> GetAllAnswers()
        {
            var lstAnswers = await _repoAnswer.GetAllAnswers();
            return Ok(lstAnswers);
        }

        [HttpGet("Get-All-Answer-By-QuestionId")]
        public async Task<ActionResult<List<Answer>>> GetAllAnswerByQuestionId(int questionId)
        {
            var lstAnswersByQuestionId = await _repoAnswer.GetAllAnswerByQuestionId(questionId);
            return Ok(lstAnswersByQuestionId);
        }

        [HttpGet("Get-Answer-By-Id")]
        public async Task<ActionResult<Answer>> GetAnswerById(int id)
        {
            var objAnswer = await _repoAnswer.GetAnSwerById(id);
            return Ok(objAnswer);
        }

        [HttpPost("Create-Answer")]
        public async Task<ActionResult<Answer>> Create(Answer answer)
        {
            var createAnswer = await _repoAnswer.CreateAnswer(answer);
            return Ok(createAnswer);
        }

        [HttpPut("Update-Answer")]
        public async Task<ActionResult<Answer>> Update(Answer answer)
        {
            var updateAnswer = await _repoAnswer.UpdateAnswer(answer);
            return Ok(updateAnswer);
        }

        [HttpDelete("Delete-Answer")]
        public async Task<ActionResult<Answer>> Delete(int id)
        {
            var deleteAnswer = await _repoAnswer.DeleteAnswer(id);
            return Ok(deleteAnswer);
        }
    }
}
