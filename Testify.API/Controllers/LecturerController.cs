using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Net.Http;
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
            var lstLecturer = await _repo.GetAllLecturer(keyWord, isActive);
            return Ok(lstLecturer);
        }

        [HttpGet("Get-score")]
        public async Task<ActionResult<List<ScoreStatistics>>> GetAllScore(int idsub, int idexam)
        {
            var lstScore = await _repo.GetScore(idsub, idexam);
            return Ok(lstScore);
        }

        [HttpGet("Get-score2")]
        public async Task<ActionResult<List<ScoreStatistics>>> GetAllScore2(Guid idlec, int idclass)
        {
            var lstScore2 = await _repo.GetScore2(idlec, idclass);
            return Ok(lstScore2);
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

        [HttpPut("Update-Forgot-Password")]
        public async Task<ActionResult<User>> UpdateForgotPass(User user)
        {
            var updateLecturer = await _repo.UpdateForgotPass(user);
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


        [HttpGet("Get-All-List-Exam-By-StudentId")]

        public async Task<ActionResult<List<SubmissionWithName>>> GetExamStudentId(Guid studentId)
        {
            var lstExam = await _repo.GetListExamOfStudent(studentId);
            return Ok(lstExam);
        }


        [HttpDelete("Delete-Lecturer")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteLecturer = await _repo.DeleteLecturer(id);

            if (deleteLecturer.Success)
            {
                return NoContent();
            }

            return BadRequest(new
            {
                ErrorCode = deleteLecturer.ErrorCode,
                Message = deleteLecturer.Message
            });
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
                   worksheetsU.Cells[rowU, 7].Value == null ||
                   worksheetsU.Cells[rowU, 9].Value == null)
                {
                    continue;
                }

                User user = new DAL.Models.User();
                user.FullName = worksheetsU.Cells[rowU, 1].Value.ToString();
                user.UserName = worksheetsU.Cells[rowU, 2].Value.ToString();
                user.PhoneNumber = worksheetsU.Cells[rowU, 3].Value.ToString();
                user.DateOfBirth = DateTime.Parse(worksheetsU.Cells[rowU, 3].Value.ToString());
                user.Address = worksheetsU.Cells[rowU, 4].Value.ToString();
                user.Email = worksheetsU.Cells[rowU, 5].Value.ToString();
                user.PasswordHash = worksheetsU.Cells[rowU, 7].Value.ToString();
                user.Sex = Convert.ToBoolean(worksheetsU.Cells[rowU, 9].Value.ToString());
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

        [HttpGet("Get-ClassByTeacher")]
        public async Task<List<Class>> GetByTeacher(Guid idLec)
        {
            return await _repo.GetAllClassByLecturer(idLec);
        }

        [HttpGet("Get-Score-Class-By-Teacher")]
        public async Task<ActionResult<List<ClassesWithLecturer>>> Check(Guid teacherId, int classId)
        {
            return await _repo.GetScore2(teacherId, classId);

        }

        [HttpGet("Get-All-Count-Student-By-UserId")]
        public async Task<int> GetCountStudent(Guid userId)
        {
            var count = await _repo.GetCountStudentByUserId(userId);
            return count;
        }

        [HttpGet("Confirm-Email")]
        public async Task<User> ConfirmEmail(string email)
        {
            var confirm = await _repo.ConfirmEmail(email);
            return confirm;
        }
    }
}
