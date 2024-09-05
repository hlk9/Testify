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
    public class UserRepository 
    {
        TestifyDbContext _context;
        public UserRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUsersId(int id) 
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateUser(User user) 
        {
            try
            {
                var create = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var obj = await _context.Users.FindAsync(user.Id);
                obj.FullName = user.FullName;
                obj.UserName = user.UserName;
                obj.DateOfBirth = user.DateOfBirth;
                obj.PhoneNumber = user.PhoneNumber;
                obj.Address = user.Address;
                obj.Email = user.Email;
                obj.PasswordHash = user.PasswordHash;
                obj.AvatarUrl = user.AvatarUrl;
                obj.LastLogin = user.LastLogin;
                obj.Status = user.Status;
                
                var update = _context.Users.Update(obj).Entity;
                await _context.SaveChangesAsync();
                return update;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> DeleteUser(int id)
        {
            try
            {
                var delete = await _context.Users.FindAsync(id);

                _context.Users.Remove(delete);
                await _context.SaveChangesAsync();
                return delete;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
