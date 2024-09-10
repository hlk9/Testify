﻿using Microsoft.EntityFrameworkCore;
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
        private readonly TestifyDbContext _context;
        public UserRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByidUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> AddUser(User user)
        {
            try
            {
                var addUser = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addUser;
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
                var objUpdateUser = await _context.Users.FindAsync(user.Id);

                objUpdateUser.FullName = user.FullName;
                objUpdateUser.UserName = user.UserName;
                objUpdateUser.DateOfBirth = user.DateOfBirth;
                objUpdateUser.PhoneNumber = user.PhoneNumber;
                objUpdateUser.Address = user.Address;
                objUpdateUser.Email = user.Email;
                objUpdateUser.PasswordHash = user.PasswordHash;
                objUpdateUser.AvatarUrl = user.AvatarUrl;

                var updateUser = _context.Users.Update(objUpdateUser).Entity;
                await _context.SaveChangesAsync();
                return updateUser;
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> DeleteUser(int id)
        {
            try
            {
                var objDeleteUser = await _context.Users.FindAsync(id);

                _context.Users.Remove(objDeleteUser);
                await _context.SaveChangesAsync();
                return objDeleteUser;
            }
            catch
            {
                return null;
            }
        }
    }
}
