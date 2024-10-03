using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("Exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        ExamReponsitory _respon;
        public ExamController()
        {
            _respon = new ExamReponsitory();
        }
        [HttpGet("Get-Active")]
        public async Task<List<Exam>> GetAllActicve()
        {
            return await _respon.GetAllActicve();
        }

        [HttpGet("Get-Exams")]
        public async Task<ActionResult<List<ExamWhitExamDetail>>> GetAll(string? keyword, bool isActive)
        {
            var lstEx = await _respon.GetAllExamWhitExamDetail(keyword, isActive);
            return Ok(lstEx);
        }
        [HttpGet("get-exams-by-id")]
        public async Task<ActionResult<Exam>> GetByIdExam(int id)
        {
            var objEx = await _respon.GetByIdExam(id);
            return Ok(objEx);
        }
        [HttpPost("Add-Exam")]
        public async Task<ActionResult<Exam>> CreateExam(Exam r)
        {
            var addExam = await _respon.AddExam(r);
            return Ok(addExam);
        }
        [HttpDelete("Delete-Exam")]
        public async Task<ActionResult<Exam>> DeleteClass(int id)
        {
            var deleteEx = await _respon.DeleteExam(id);
            return Ok(deleteEx);
        }
        [HttpPut("Update-Exam")]
        public async Task<ActionResult<Exam>> UpdateClass(Exam c)
        {
            var updateEx = await _respon.UpdateExam(c);
            return Ok(updateEx);
        }




        [HttpPost("add-questions-to-exam")]
        public async Task<IActionResult> AddQuestionsToExam(int examId, [FromBody] List<Question> questions)
        {
            try
            {
                await _respon.AddQuestionsToExamAsync(examId, questions);
                return Ok(new { message = "Questions added to exam successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
