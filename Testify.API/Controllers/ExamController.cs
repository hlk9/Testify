﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
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
                    SubjectName = lstSub.FirstOrDefault(x=> x.Id == item.SubjectId).Name,
                    Duration = item.Duration,
                    NumberOfQuestion = item.NumberOfQuestions

                });
            }
            return listResult;

        }



    }
}
