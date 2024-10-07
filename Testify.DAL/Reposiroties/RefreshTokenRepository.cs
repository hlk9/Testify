using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class RefreshTokenRepository
    {
        TestifyDbContext _context;
        public RefreshTokenRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<string> GetUserIdByToken(string token)
        {
            var usr = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(token.Replace("\"","")));

            if (usr != null)
            {
                return  usr.UserId.ToString();
            }
            return null;

        }

        public string GetTokenByUserId(string id)
        {
            var usr = _context.RefreshTokens.FirstOrDefault(x => x.UserId == Guid.Parse(id) && x.ExpiryDate > DateTime.Now);

            if (usr != null)
            {
                return usr.Token.ToString();
            }
            return null;

        }

        public bool AddToken(RefreshToken token)
        {
            try
            {
                _context.RefreshTokens.Add(token);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckExistUserIdByToken(string id)
        {
            var usr = _context.RefreshTokens.FirstOrDefault(x => x.UserId == Guid.Parse(id));

            if (usr != null)
            {
                return true;
            }
            return false;

        }

        public bool CheckTokenExpried(string token)
        {
            string a = token.Replace("\"", "");
            var token1 = _context.RefreshTokens.FirstOrDefault(x => x.Token == token.Replace("\"", ""));
            var tok = _context.RefreshTokens.FirstOrDefault(x =>  x.Token == token.Replace("\"","") && x.ExpiryDate>DateTime.UtcNow);

            if (tok != null)
                return true;
            return false;

        }

    }
}
