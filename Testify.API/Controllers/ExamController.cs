using Microsoft.AspNetCore.Mvc;
using Testify.API.DTOs;
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

        [HttpGet("Get-ExamBySubject")]
        public async Task<List<Exam>> GetBySubject(int id)
        {
            return await _respon.GetAllActicveOfSubject(id);
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
        [HttpPut("Delete-Exam")]    
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

        [HttpGet("Get-InfoBasic")]
        public async Task<List<ExamWhitQusetion>> GetInfoBasic()
        {
            List<ExamWhitQusetion> listResult = new List<ExamWhitQusetion>();
            SubjectRepository subjectRepository = new SubjectRepository();

            var lstExam = await _respon.GetAllActicve();
            var lstSub = await subjectRepository.GetAllSubject(null, true);

            foreach (var item in lstExam)
            {
                listResult.Add(new ExamWhitQusetion
                {
                    Id = item.Id,
                    Description = item.Description,
                    ExamName = item.Name,
                    Status = item.Status,
                    SubjectId = item.SubjectId,
                    SubjectName = lstSub.FirstOrDefault(x => x.Id == item.SubjectId).Name,
                    Duration = item.Duration,
                    NumberOfQuestion = item.NumberOfQuestions,
                    NumberOfRepeat = item.NumberOfRepeat,


                });
            }
            return listResult;

        }

        [HttpGet("Get-Count-Exam-By-UserId")]
        public async Task<ActionResult<int>> GetCountExamByUserId(Guid userId)
        {
            var count = await _respon.GetCountExamByUserId(userId);
            return Ok(count);
        }

        [HttpGet("Get-Exams-By-UserId")]
        public async Task<ActionResult<List<Exam>>> GetExamsByUserId(Guid UserId)
        {
            var lst = await _respon.GetExamsByUserId(UserId);
            return Ok(lst);
        }

        [HttpGet("Score-Distribution-By-Exam")]
        public async Task<ActionResult<List<ScoreDistribution>>> ScoreDistributionByExam(int ExamId)
        {
            var lst = await _respon.ScoreDistributionByExam(ExamId);
            return Ok(lst);
        }
    }
}
