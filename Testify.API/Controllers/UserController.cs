using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

namespace Testify.API.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepos;
        public UserController()
        {
            userRepos = new UserRepository();
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
        public async Task<ActionResult<int>> UploadFile(IFormFile file,[FromForm] int levelId)
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
                else if (lstUser.Any(x => x.UserName.Trim().ToLower().Equals(worksheetsU.Cells[rowU,2].Value.ToString().Trim().ToLower())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.PhoneNumber.Trim().Equals(worksheetsU.Cells[rowU,4].Value.ToString().Trim())))
                {
                    userFailCount++;
                    continue;
                }
                else if (lstUser.Any(x => x.Email.Trim().Equals(worksheetsU.Cells[rowU,6].Value.ToString())))
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
        public async Task<List<User>> UsersWithStatus(int classId)
        {
            var usersWithStatusOne = await userRepos.GetUsersWithStatusOne(classId);
            return usersWithStatusOne;
        }
    }
}
