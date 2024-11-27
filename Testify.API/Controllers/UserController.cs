using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepos;
        private readonly LevelRepository levelRepos;
        public UserController()
        {
            userRepos = new UserRepository();
            levelRepos = new LevelRepository();
        }

        [HttpPost("Register-Student")]
        public Task<bool> RegisterStudent([FromBody] User user)
        {
            var a = userRepos.AddUser(user);

            if (a != null)
            {

                return Task.FromResult(true);
            }
            return Task.FromResult(false);


        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var lstUser = await userRepos.GetAllUsers();
            return Ok(lstUser);
        }

        [HttpGet("Get-By-idUser")]
        public async Task<ActionResult<List<User>>> GetByidUser(Guid id)
        {
            var idUser = await userRepos.GetByidUserSendMail(id);
            return Ok(idUser);
        }

        [HttpPost("create-user")]
        public async Task<ActionResult<User>> CreateAccount(User user)
        {
            var newU = await userRepos.AddUser(user);
            return Ok(newU);
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<User>> UpdateAccount(User user)
        {
            var editUser = await userRepos.UpdateUser(user);
            return Ok(editUser);
        }

        [HttpDelete("delete-user")]
        public async Task<ActionResult<User>> DeleteAccount(Guid id)
        {
            var deleteU = await userRepos.DeleteUser(id);
            return Ok(deleteU);
        }

        [HttpPost("Import-Excel-User")]
        public async Task<ActionResult<int>> UploadFile(IFormFile file, [FromForm] int levelId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Chưa chọn file excel!");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);

            var lstUser = await userRepos.GetAllUsers();
            var worksheetsU = package.Workbook.Worksheets[0];

            var userFailCount = 0;
            int totalrowU = worksheetsU.Dimension.Rows;

            for (int rowU = 2; rowU <= totalrowU; rowU++)
            {
                if (worksheetsU.Cells[rowU, 1].Value == null ||
                    worksheetsU.Cells[rowU, 2].Value == null ||
                    worksheetsU.Cells[rowU, 3].Value == null ||
                    worksheetsU.Cells[rowU, 4].Value == null ||
                    worksheetsU.Cells[rowU, 5].Value == null ||
                    worksheetsU.Cells[rowU, 6].Value == null ||
                    worksheetsU.Cells[rowU, 7].Value == null
                    )
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.UserName.Trim().ToLower().Equals(worksheetsU.Cells[rowU, 2].Value.ToString().Trim().ToLower())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.PhoneNumber.Trim().Equals(worksheetsU.Cells[rowU, 4].Value.ToString().Trim())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.Email.Trim().Equals(worksheetsU.Cells[rowU, 6].Value.ToString())))
                {
                    userFailCount++;
                    continue;
                }

                //else if()
                User u = new User();
                u.FullName = worksheetsU.Cells[rowU, 1].Value.ToString();
                u.UserName = worksheetsU.Cells[rowU, 2].Value.ToString();
                u.DateOfBirth = DateTime.Parse(worksheetsU.Cells[rowU, 3].Value.ToString());
                u.PhoneNumber = worksheetsU.Cells[rowU, 4].Value.ToString();
                u.Address = worksheetsU.Cells[rowU, 5].Value.ToString();
                u.Email = worksheetsU.Cells[rowU, 6].Value.ToString();
                u.PasswordHash = worksheetsU.Cells[rowU, 7].Value.ToString();
                u.LevelId = levelId;


                var successAddUser = await userRepos.AddUser(u);




            }

            return userFailCount++;
        }

        [HttpGet("Get-Users-With-Status-One")]
        public async Task<List<User>> UsersWithStatusOne(int classId, string? searchValue)
        {
            var usersWithStatusOne = await userRepos.GetUsersWithStatusOne(classId, searchValue);
            return usersWithStatusOne;
        }

        [HttpGet("Get-Users-With-Status-Two")]
        public async Task<List<User>> UsersWithStatusTwo(int classId)
        {
            var usersWithStatusTwo = await userRepos.GetUsersWithStatusTwo(classId);
            return usersWithStatusTwo;
        }

        [HttpGet("Check-Email-Or-Phone")]
        public async Task<ActionResult> CheckEmailOrPhone(string email, string phoneNumber, string userName)
        {
            bool result = await userRepos.CheckEmailOrPhone(email, phoneNumber, userName);
            return Ok(result);
        }

        [HttpGet("Export-Account-By-LevelId")]
        public async Task<ActionResult> ExportAccountByLevelId(int levelId)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            if (levelId != 0 && levelId != null)
            {
                var users = await userRepos.GetUsersByLevelId(levelId);

                var worksheet = package.Workbook.Worksheets.Add("Tài khoản");

                worksheet.Cells.Style.Font.Name = "Times New Roman";
                System.Drawing.Color customColor = System.Drawing.Color.FromArgb(41, 166, 154);

                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 25;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 25;
                worksheet.Column(6).Width = 25;
                worksheet.Column(7).Width = 25;
                worksheet.Column(8).Width = 25;

                worksheet.Row(1).Height = 30;
                worksheet.Cells[1, 1].Value = "Full Name";
                worksheet.Cells[1, 2].Value = "Username";
                worksheet.Cells[1, 3].Value = "Username";
                worksheet.Cells[1, 4].Value = "Date of Birth";
                worksheet.Cells[1, 5].Value = "Phone Number";
                worksheet.Cells[1, 6].Value = "Address";
                worksheet.Cells[1, 7].Value = "Email";
                worksheet.Cells[1, 8].Value = "Sex";

                for (int i = 1; i <= 8; i++)
                {
                    worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(customColor);
                    worksheet.Cells[1, i].Style.WrapText = true;
                    worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Cells[1, i].Style.Font.Size = 12;
                }

                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];

                    worksheet.Cells[i + 2, 1].Value = user.FullName;
                    worksheet.Cells[i + 2, 2].Value = user.UserName;
                    worksheet.Cells[i + 2, 3].Value = user.PasswordHash;
                    worksheet.Cells[i + 2, 4].Value = user.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 5].Value = user.PhoneNumber;
                    worksheet.Cells[i + 2, 6].Value = user.Address;
                    worksheet.Cells[i + 2, 7].Value = user.Email;
                    worksheet.Cells[i + 2, 8].Value = user.Sex ? "Male" : "Female";
                }

                var excelByBytes = package.GetAsByteArray();

                return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Danh_Sach_Tai_Khoan.xlsx");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
