using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Reposiroties;
using Testify.API.TokenHelper;
using Testify.DAL.Models;
using Testify.API.DTOs;

namespace Testify.API.Controllers
{
    [Route("Access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        UserRepository UserRepo;
        AuthHelper authHelper;
        public AccessController(IConfiguration configuration)
        {
            authHelper = new AuthHelper(configuration);
        }

        [HttpGet("Login")]
        public async Task<UserLoginWithToken> LoginReturnToken(string username, string passwordHash)
        {
            UserRepo = new UserRepository();
            UserLoginWithToken uLT = new UserLoginWithToken();
            User u = await UserRepo.GetByKeyAndPassword(username, passwordHash);
            if (u != null && u.PasswordHash != "-1")
            {
                RefreshTokenRepository tokenRepo = new RefreshTokenRepository();

                var token = tokenRepo.GetTokenByUserId(u.Id.ToString());

                if (token != null)
                {
                    uLT.Token = token.ToString();
                    uLT.Id = u.Id;
                    uLT.UserName = u.UserName;
                    uLT.FullName = u.FullName;
                    uLT.IsLoginSucces = true;
                    uLT.LevelId = u.LevelId;
                    return uLT;
                }
                else
                {
                    var tokenModel = authHelper.GenerateJWTToken(u);
                    if (tokenModel != null)
                    {

                        RefreshToken refreshToken = new RefreshToken();
                        refreshToken.CreatedAt = tokenModel.RegisterDate;
                        refreshToken.IsRevoked = false;
                        refreshToken.UserId = tokenModel.UserId;
                        refreshToken.ExpiryDate = tokenModel.Expried;
                        refreshToken.Token = tokenModel.Token;

                        if (tokenRepo.AddToken(refreshToken) == true)
                        {
                            uLT.Token = tokenModel.Token;
                            uLT.Id = u.Id;
                            uLT.UserName = u.UserName;
                            uLT.FullName = u.FullName;
                            uLT.IsLoginSucces = true;
                            uLT.LevelId = u.LevelId;
                            return uLT;
                        }

                    }
                }

                //uLT.Id = u.Id;
                //uLT.UserName = u.UserName;
                //uLT.FullName = u.FullName;
                //uLT.IsLoginSucces = true; 
                //uLT.LevelId = u.LevelId;      
                //uLT.Token = authHelper.GenerateJWTToken(u).Token;
                //return uLT;
            }
            uLT.Id = null;
            uLT.IsLoginSucces = false;
            return uLT;


        }
        [HttpGet("CheckToken")]
        public async Task<UserLoginWithToken> CheckAndGetToken(string token)
        {
            UserLoginWithToken dataTrans = new UserLoginWithToken();
            UserRepo = new UserRepository();
            RefreshTokenRepository tokenRepo = new RefreshTokenRepository();
            string uId = await tokenRepo.GetUserIdByToken(token);

            if (string.IsNullOrEmpty(uId))
            {
                dataTrans.IsLoginSucces = false;
                return dataTrans;
            }

            var usr = await UserRepo.GetByidUser(uId);
            if (usr != null)
            {
                bool loginSuc = tokenRepo.CheckTokenExpried(token);
                dataTrans.Token = token;
                dataTrans.UserName = usr.UserName;
                dataTrans.LevelId = usr.LevelId;
                dataTrans.FullName = usr.FullName;
                dataTrans.Id = usr.Id;
                dataTrans.IsLoginSucces = loginSuc;
                return dataTrans;
            }
            return null;

        }


        [HttpPost("Register")]
        public async Task<bool> RegisterUser(User usr)
        {
            UserRepo = new UserRepository();
            var user = UserRepo.FindUserExistByKeyWord(usr.Email);
            if (user != null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
    }
}
