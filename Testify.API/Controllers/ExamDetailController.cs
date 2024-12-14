using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("ExamDetail")]
    [ApiController]
    public class ExamDetailController : ControllerBase
    {
        ExamDetailRepository _respon;
        public ExamDetailController()
        {
            _respon = new ExamDetailRepository();
        }

        [HttpGet("Get-ExamDetail")]
        public async Task<ActionResult<List<ExamDetail>>> GetAll()
        {
            var lstExamDetail = await _respon.GetAllExamDetail();
            return Ok(lstExamDetail);
        }

        [HttpGet("Get-ExamDetail-id")]
        public async Task<ActionResult<ExamDetail>> GetExamDetailId(int id)
        {
            var objExDetail = await _respon.GetExamDetailId(id);
            return Ok(objExDetail);
        }

        [HttpGet("Get-ExamDetail-By-ExamID")]
        public async Task<ActionResult<List<ExamDetail>>> GetExamDetailByExamId(int examId)
        {
            var objExamId = await _respon.GetExamDetailByExamId(examId);
            return Ok(objExamId);
        }

        [HttpPost("Create-Exam-Detail")]
        public async Task<ActionResult<ExamDetail>> Create(ExamDetail examDetail)
        {
            var obj = await _respon.CreateExamDetail(examDetail);
            return Ok(obj);
        }

        [HttpDelete("Delete-ExamDetail")]
        public async Task<ActionResult> DeleteExamDetail(int id)
        {
            var deleteEx = await _respon.DeleteExamDetail(id);

            if (deleteEx.Success)
            {
                return NoContent();
            }

            return BadRequest(new
            {
                ErrorCode = deleteEx.ErrorCode,
                Message = deleteEx.Message
            });
        }

        [HttpPut("Update-satus")]
        public async Task<ActionResult<ExamDetail>> UpdateStatus(ExamDetail examDetail)
        {
            var obj = await _respon.UpdateStatus(examDetail);
            return Ok(obj);
        }

        [HttpGet("CheckTrungCodeDT")]
        public async Task<IActionResult> ChekTrungCodeDT(string code, int? idExam, int idExamDetail)
        {
            var isDuplicate = await _respon.IsExamDetailCodeExist(code, idExam, idExamDetail); 
            return Ok(isDuplicate);
        }



    }
}
