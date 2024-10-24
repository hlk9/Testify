﻿using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("Lecturer")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        LecturerRepository _repo;
        public LecturerController()
        {
            _repo = new LecturerRepository();
        }

        [HttpGet("Get-All-Lecturer")]
        public async Task<ActionResult<List<User>>> GetlAllLecturer(string? keyWord, bool isActive)
        {
            var lstLecturer = await _repo.GetAllLecturer(keyWord,isActive);
            return Ok(lstLecturer);
        }

        [HttpGet("Get-score")]
        public async Task<ActionResult<List<ScoreStatistics>>> GetAllScore( int idsub, int idexam)
        {
            var lstScore = await _repo.GetScore(idsub,idexam);
            return Ok(lstScore);
        }
        [HttpGet("Get-Lecturer-By-Id")]
        public async Task<ActionResult<User>> GetLecturerById(Guid id)
        {
            var Lecturer = await _repo.GetLecturerById(id);
            return Ok(Lecturer);
        }

        [HttpPost("Create-Lecturer")]
        public async Task<ActionResult<User>> Create(User user)
        {
            var createLecturer = await _repo.CreateLecturer(user);
            return Ok(createLecturer);
        }

        [HttpPost("Create-Students")]
        public async Task<ActionResult<User>> CreateStu(User user)
        {
            var createStu = await _repo.CreateStudent(user);
            return Ok(createStu);
        }
        [HttpPut("Update-Lecturer")]
        public async Task<ActionResult<User>> Update(User user)
        {
            var updateLecturer = await _repo.UpdateLecturer(user);
            return Ok(updateLecturer);
        }

        [HttpGet("Get-All-Teacher")]
        public async Task<ActionResult<List<User>>> GetlAllTeacher()
        {
            var lstTeacher = await _repo.GetAllTeacher();
            return Ok(lstTeacher);
        }

        [HttpGet("Get-All-Student")]
        public async Task<ActionResult<List<User>>> GetlAllStudent()
        {
            var lstStudent = await _repo.GetAllStudent();
            return Ok(lstStudent);
        }


        [HttpDelete("Delete-Lecturer")]
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            var deleteLecturer = await _repo.DeleteLecturer(id);
            return Ok(deleteLecturer);
        }


        [HttpPost("Import-Excel-User")]
        public async Task<ActionResult<List<User>>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Chưa chọn file import");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var result = await ProcessExcelFile(stream);
            return Ok(result.Value);
        }


        private async Task<ActionResult<List<User>>> ProcessExcelFile(Stream stream)
        {

            var lstUser = new List<User>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);

            var worksheetsU = package.Workbook.Worksheets[0];

            for (int rowU = 2; rowU <= worksheetsU.Dimension.Rows; rowU++)
            {
                if (worksheetsU.Cells[rowU, 1].Value == null ||
                   worksheetsU.Cells[rowU, 2].Value == null ||
                   worksheetsU.Cells[rowU, 3].Value == null ||
                   worksheetsU.Cells[rowU, 4].Value == null ||
                   worksheetsU.Cells[rowU, 5].Value == null ||
                   worksheetsU.Cells[rowU, 6].Value == null ||
                   worksheetsU.Cells[rowU, 7].Value == null)
                {
                    continue;
                }

                User user = new DAL.Models.User();
               user.FullName = worksheetsU.Cells[rowU, 1].Value.ToString();
               user.UserName = worksheetsU.Cells[rowU, 1].Value.ToString();
               user.PhoneNumber = worksheetsU.Cells[rowU, 1].Value.ToString();
               user.DateOfBirth = DateTime.Parse( worksheetsU.Cells[rowU, 1].Value.ToString());
               user.Address = worksheetsU.Cells[rowU, 1].Value.ToString();
               user.Email = worksheetsU.Cells[rowU, 1].Value.ToString();
               user.PasswordHash = worksheetsU.Cells[rowU, 1].Value.ToString();
            }
            return lstUser;
        }

        [HttpPost("Create-User-In-Import-Excel")]
        public async Task<ActionResult> CreateAccountInExcel(List<User> lstAccount)
        {
            if (lstAccount.Count > 0 && lstAccount != null)
            {
                foreach (var item in lstAccount)
                {
                    User u = new User();
                    u.FullName = item.FullName;
                    u.UserName = item.UserName;
                    u.PhoneNumber = item.PhoneNumber;
                    u.DateOfBirth = item.DateOfBirth;
                    u.Address = item.Address;
                    u.Email = item.Email;
                    u.PasswordHash = item.PasswordHash;

                    var objU = await _repo.CreateStudent(u);
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
