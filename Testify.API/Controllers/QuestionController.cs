using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;


namespace Testify.API.Controllers
{
    [Route("Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        QuestionReposiroty _repoQuestion;

        public QuestionController()
        {
            _repoQuestion = new QuestionReposiroty();
        }

        [HttpGet("Get-All-Questions")]
        public async Task<ActionResult<List<Question>>> GetlAllQuestions()
        {
            var lstQuestion = await _repoQuestion.GetAllQuestions();
            return Ok(lstQuestion);        
        }

        [HttpGet("Get-Question-By-Id")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await _repoQuestion.GetQuestionById(id);
            return Ok(question);
        }

        [HttpPost("Create-Question")]
        public async Task<ActionResult<Question>> Create(Question question)
        {
            var createQuestion = await _repoQuestion.CreateQuestion(question);
            return Ok(createQuestion);
        }

        [HttpPut("Update-Question")]
        public async Task<ActionResult<Question>> Update(Question question)
        {
            var updateQuestion = await _repoQuestion.UpdateQuestion(question);
            return Ok(updateQuestion);
        }

        [HttpPut("Update-Status")]
        public async Task<ActionResult<Question>> UpdateStatus(int questionId, string? status)
        {
            var updateStatus = await _repoQuestion.UpdateStatusQuestion(questionId, status);
            return Ok(updateStatus);
        }

        [HttpDelete("Delete-Question")]
        public async Task<ActionResult<Question>> Delete(int id)
        {
            var deleteQuestion = await _repoQuestion.DeleteQuestion(id);
            return Ok(deleteQuestion);
        }
    }
}
