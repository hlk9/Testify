using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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

        [HttpDelete("Delete")]
        public async Task<ActionResult<ExamDetailQuestion>> DeleteExamDetailQuestionsByExamDetailId(int idExamDetail)
        {
            var isDeleted = await _respon.DeleteExamDetailQuestionByExamDetailID(idExamDetail);

            return Ok(isDeleted);
        }

        [HttpGet("Get-Question-By-ExamDetailID")]
        public async Task<ActionResult<List<QuestionInExam>>> GetAllQuestionByExamDetailID(int examdetailID)
        {
            var objGetAll = await _respon.GetQuestionByExamDetailID(examdetailID);
            return Ok(objGetAll);
        }

        [HttpGet("Get-Question-By-ExamDetailID-Not")]
        public async Task<ActionResult<List<QuestionInExam>>> GetAllQuestionByExamDetailID_NOT(int examdetailID, int SubjectId, string? textSearch)
        {
            string decodedContent = "";
            if (!string.IsNullOrEmpty(textSearch) || !string.IsNullOrWhiteSpace(textSearch))
            {
                decodedContent = Uri.UnescapeDataString(textSearch);
            }
            var objGetAll = await _respon.GetQuestionByExamDetailID_NOT(examdetailID,  SubjectId);

            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                objGetAll = objGetAll
                    .Where(x => x.Content.Contains(decodedContent, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(objGetAll);
        }

        [HttpGet("Get-Question-By-ExamDetailID-NotAndLevel")]
        public async Task<ActionResult<List<QuestionInExam>>> GetAllQuestionByExamDetailID_NOTAndLevel(int examdetailID, int levelID, int SubjectId, string? textSearch)
        {
            string decodedContent = "";
            if (!string.IsNullOrEmpty(textSearch) || !string.IsNullOrWhiteSpace(textSearch))
            {
                decodedContent = Uri.UnescapeDataString(textSearch);
            }
            var objGetAll = await _respon.GetQuestionByExamDetailID_NOTAndLevel(examdetailID, levelID ,  SubjectId);
            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                objGetAll = objGetAll
                    .Where(x => x.Content.Contains(decodedContent, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return Ok(objGetAll);
        }

        [HttpPost("AddListQuestionToExam_New")]
        public bool AddListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            return _respon.AddListQuestionToExam(data, idExamDetail);
        }

       


        [HttpPost("Remove-ListQuestionToExam")]
        public bool RemoveFromListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            return _respon.RemoveFromListQuestionToExam(data, idExamDetail);
        }

    }
}
