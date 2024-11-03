using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("ExamDetailQuestion")]
    [ApiController]
    public class ExamDetailQuestionController : ControllerBase
    {
        ExamDetailQuestionRepository _respon;
        public ExamDetailQuestionController()
        {
            _respon = new ExamDetailQuestionRepository();
        }
        [HttpGet("Get-ExamDetailQuestion-By-ExamDetailID")]
        public async Task<ActionResult<List<ExamDetailQuestion>>> GetExamDetailQuestionByExamId(int examDetailID)
        {
            var objExamId = await _respon.GetAllByExamDetailId(examDetailID);
            return Ok(objExamId);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ExamDetailQuestion>> CreateExamDetailQuestion(ExamDetailQuestion examDetailQuestion)
        {
            var obj = await _respon.Create(examDetailQuestion);
            return Ok(obj);
        }
    }
}
