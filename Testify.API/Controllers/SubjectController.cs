﻿using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("Subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        SubjectRepository _repo;
        public SubjectController()
        {
            _repo = new SubjectRepository();
        }

        [HttpGet("get-all-subject")]
        public async Task<ActionResult<List<Subject>>> GetAll(string? keyWord, bool isActive)
        {
            var lstSubject = await _repo.GetAllSubject(keyWord, isActive);
            return Ok(lstSubject);
        }

        [HttpGet("get-subject-by-id")]
        public async Task<ActionResult<Subject>> GetByIdSub(int id)
        {
            var objSubject = await _repo.GetSubjectById(id);
            return Ok(objSubject);
        }

        [HttpPost("create-subject")]
        public async Task<ActionResult<Subject>> Create(Subject subject)
        {
            var addSubject = await _repo.CreateSubject(subject);
            return Ok(addSubject);
        }

        [HttpPut("update-subject")]
        public async Task<ActionResult<Subject>> Update(Subject subject)
        {
            var updateSubject = await _repo.UpdateSubject(subject);
            return Ok(updateSubject);
        }

        [HttpDelete("delete-subject")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteSubject = await _repo.DeleteSubject(id);

            if (deleteSubject.Success)
            {
                return NoContent();
            }

            return BadRequest(new
            {
                ErrorCode = deleteSubject.ErrorCode,
                Message = deleteSubject.Message
            });
        }

        [HttpGet("Get-Count-By-UserId")]
        public async Task<ActionResult<int>> GetCountByUserId(Guid userId)
        {
            var count = await _repo.GetCountSubjectByUserId(userId);
            return Ok(count);
        }

        [HttpGet("Score-Distribution-By-Subject")]
        public async Task<ActionResult<ScoreDistributionBy>> ScoreDistributionBySubject(int subjectId)
        {
            var lst = await _repo.ScoreDistributionBySubject(subjectId);
            return Ok(lst);
        }

        [HttpGet("get-all-by-subjectId")]
        public async Task<List<SubmissionViewModel>> GetAllSubmissionsAsync(int? subjectId, string? textSearch, Guid? usersID, int? classId, string? startTime, string? endTime)
        {
            return await _repo.GetSubmissionDetails(subjectId, textSearch, usersID, classId, startTime, endTime);
        }
    }
}
