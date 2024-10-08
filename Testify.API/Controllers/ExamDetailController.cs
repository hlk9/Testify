﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

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
    }
}
