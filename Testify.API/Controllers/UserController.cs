using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
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

        [HttpGet("Export-Excel-Template-Account")]
        public async Task<ActionResult> ExportTemplateAccount()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var package = new ExcelPackage();

            var worksheetA = package.Workbook.Worksheets.Add("Tài Khoản");

            worksheetA.Cells.Style.Font.Name = "Times New Roman";
            System.Drawing.Color customColor = System.Drawing.Color.FromArgb(41, 166, 154); //màu chính của web
            System.Drawing.Color customColorYellow = System.Drawing.Color.Yellow;

            worksheetA.Column(1).Width = 35;
            worksheetA.Column(2).Width = 20;
            worksheetA.Column(3).Width = 30;
            worksheetA.Column(4).Width = 25;
            worksheetA.Column(5).Width = 45;
            worksheetA.Column(6).Width = 20;
            worksheetA.Column(7).Width = 30;
            worksheetA.Column(8).Width = 30;
            worksheetA.Column(11).Width = 25;

            worksheetA.Row(1).Height = 40;

            worksheetA.Cells[1, 1].Value = "Họ và tên";
            worksheetA.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 1].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 1].Style.Font.Bold = true;
            worksheetA.Cells[1, 1].Style.Font.Size = 12;

            worksheetA.Cells[1, 2].Value = "Ngày sinh";
            worksheetA.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 2].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 2].Style.Font.Bold = true;
            worksheetA.Cells[1, 2].Style.Font.Size = 12;

            worksheetA.Cells[1, 3].Value = "Email";
            worksheetA.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 3].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 3].Style.Font.Bold = true;
            worksheetA.Cells[1, 3].Style.Font.Size = 12;

            worksheetA.Cells[1, 4].Value = "Số điện thoại";
            worksheetA.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 4].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 4].Style.Font.Bold = true;
            worksheetA.Cells[1, 4].Style.Font.Size = 12;

            worksheetA.Cells[1, 5].Value = "Địa chỉ";
            worksheetA.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 5].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 5].Style.Font.Bold = true;
            worksheetA.Cells[1, 5].Style.Font.Size = 12;

            worksheetA.Cells[1, 6].Value = "Giới tính";
            worksheetA.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 6].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 6].Style.Font.Bold = true;
            worksheetA.Cells[1, 6].Style.Font.Size = 12;

            worksheetA.Cells[1, 7].Value = "Tên tài khoản";
            worksheetA.Cells[1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 7].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 7].Style.Font.Bold = true;
            worksheetA.Cells[1, 7].Style.Font.Size = 12;

            worksheetA.Cells[1, 8].Value = "Mật khẩu";
            worksheetA.Cells[1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 8].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 8].Style.Font.Bold = true;
            worksheetA.Cells[1, 8].Style.Font.Size = 12;

            worksheetA.Cells[1, 11].Value = "Ví dụ giới tính";
            worksheetA.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 11].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 11].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 11].Style.Font.Bold = true;
            worksheetA.Cells[1, 11].Style.Font.Size = 12;

            worksheetA.Cells[2, 1].Value = "Nguyễn Văn A";
            worksheetA.Cells[2, 2].Value = "05/01/2004";
            worksheetA.Cells[2, 3].Value = "nva@gmail.com";
            worksheetA.Cells[2, 4].Value = "0912888999";
            worksheetA.Cells[2, 5].Value = "Trịnh Văn Bô - Phương Canh - Hà Nội";
            worksheetA.Cells[2, 6].Value = 1;
            worksheetA.Cells[2, 7].Value = "nva";
            worksheetA.Cells[2, 8].Value = "123123";
            worksheetA.Cells[2, 11].Value = "Nam điền - 1";
            worksheetA.Cells[3, 11].Value = "Nữ điền - 0";

            var excelByBytes = package.GetAsByteArray();

            return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template_Account_LVT.xlsx");
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

            var lstUserTemp = new List<User>();
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
                    worksheetsU.Cells[rowU, 7].Value == null ||
                    worksheetsU.Cells[rowU, 8].Value == null
                    )
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.UserName.Trim().ToLower().Equals(worksheetsU.Cells[rowU, 7].Value.ToString().Trim().ToLower())) || lstUserTemp.Any(x => x.UserName.Trim().ToLower().Equals(worksheetsU.Cells[rowU, 7].Value.ToString().Trim().ToLower())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.PhoneNumber.Trim().Equals(worksheetsU.Cells[rowU, 4].Value.ToString().Trim())) || lstUserTemp.Any(x => x.PhoneNumber.Trim().Equals(worksheetsU.Cells[rowU, 4].Value.ToString().Trim())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.Email.Trim().Equals(worksheetsU.Cells[rowU, 3].Value.ToString().Trim().ToLower())) || lstUserTemp.Any(x => x.Email.Trim().Equals(worksheetsU.Cells[rowU, 3].Value.ToString().Trim().ToLower())))
                {
                    userFailCount++;
                    continue;
                }
                //else if (Convert.ToInt32(worksheetsU.Cells[rowU, 6].Value.ToString().Trim()) != 1 ||Convert.ToInt32(worksheetsU.Cells[rowU, 6].Value.ToString().Trim()) != 0)
                //{
                //    userFailCount++;
                //    continue;
                //}

                MD5 md5 = MD5.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(worksheetsU.Cells[rowU, 8].Value.ToString().Trim());
                byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                md5.Clear();

                string cellValue = worksheetsU.Cells[rowU, 2].Value.ToString().Trim();

                string dateFormat = "dd/MM/yyyy";

                if (DateTime.TryParseExact(cellValue, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    cellValue = parsedDate.ToString("yyyy/MM/dd");
                }
                    User u = new User();
                u.FullName = worksheetsU.Cells[rowU, 1].Value.ToString().Trim();
                u.DateOfBirth = DateTime.Parse(cellValue);
                u.Email = worksheetsU.Cells[rowU, 3].Value.ToString().Trim();
                u.PhoneNumber = "0" + worksheetsU.Cells[rowU, 4].Value.ToString().Trim();
                u.Address = worksheetsU.Cells[rowU, 5].Value.ToString().Trim();
                u.Sex = worksheetsU.Cells[rowU, 6].Value.ToString().Trim() == "1" ? true : false;
                u.UserName = worksheetsU.Cells[rowU, 7].Value.ToString().Trim();
                u.PasswordHash = sb.ToString();
                u.LevelId = levelId;
                u.Status = 1;

                var successAddUser = await userRepos.AddUser(u);
                if(successAddUser != null)
                {
                    lstUserTemp.Add(u);
                }
            }
            lstUserTemp.Clear();
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
        public async Task<ActionResult> CheckEmailOrPhone(string email, string phoneNumber, string userName, Guid? userId)
        {
            bool result = await userRepos.CheckEmailOrPhone(email, phoneNumber, userName, userId);
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
                System.Drawing.Color customColor = System.Drawing.Color.FromArgb(41, 166, 154); //màu chính của web
                System.Drawing.Color answerCorrect = System.Drawing.Color.FromArgb(112, 173, 71);

                worksheet.Column(1).Width = 35;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 30;
                worksheet.Column(4).Width = 25;
                worksheet.Column(5).Width = 45;
                worksheet.Column(6).Width = 20;

                worksheet.Row(1).Height = 30;

                worksheet.Cells[1, 1].Value = "Họ và tên";
                worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 1].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 12;

                worksheet.Cells[1, 2].Value = "Ngày sinh";
                worksheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 2].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 2].Style.Font.Bold = true;
                worksheet.Cells[1, 2].Style.Font.Size = 12;

                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 3].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 3].Style.Font.Size = 12;

                worksheet.Cells[1, 4].Value = "Số điện thoại";
                worksheet.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 4].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 4].Style.Font.Bold = true;
                worksheet.Cells[1, 4].Style.Font.Size = 12;

                worksheet.Cells[1, 5].Value = "Địa chỉ";
                worksheet.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 5].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 5].Style.Font.Bold = true;
                worksheet.Cells[1, 5].Style.Font.Size = 12;

                worksheet.Cells[1, 6].Value = "Giới tính";
                worksheet.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(customColor);
                worksheet.Cells[1, 6].Style.WrapText = true; // Cho phép chữ tự xuống dòng
                worksheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
                worksheet.Cells[1, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
                worksheet.Cells[1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                worksheet.Cells[1, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[1, 6].Style.Font.Bold = true;
                worksheet.Cells[1, 6].Style.Font.Size = 12;

                worksheet.Cells[1, 1].Value = "Họ và tên";
                worksheet.Cells[1, 2].Value = "Ngày sinh";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Số điện thoại";
                worksheet.Cells[1, 5].Value = "Địa chỉ";
                worksheet.Cells[1, 6].Value = "Giới tính";

                for (int i = 1; i <= 6; i++)
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
                    worksheet.Cells[i + 2, 2].Value = user.DateOfBirth.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 3].Value = user.Email;
                    worksheet.Cells[i + 2, 4].Value = user.PhoneNumber.ToString();
                    worksheet.Cells[i + 2, 5].Value = user.Address;
                    worksheet.Cells[i + 2, 6].Value = user.Sex ? "Nam" : "Nữ";
                }

                var excelByBytes = package.GetAsByteArray();

                return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"List_Account.xlsx");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
