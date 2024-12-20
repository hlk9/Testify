﻿using Microsoft.EntityFrameworkCore;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class CandidateRepository
    {
        TestifyDbContext _context;
        public CandidateRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<User>> GetAllCandidate()
        {
            //return await _context.Users.ToListAsync();
            return await _context.Users.Where(x => x.LevelId == 4).ToListAsync();

        }

        public async Task<User> GetCandidateById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateCandidate(User user)
        {
            try
            {
                // gán luôn LevelId = 4 để khi thêm mặc định lv = 4 => là thí sinh
                user.LevelId = 4;
                var addCandidate = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addCandidate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> UpdateCandidate(User user)
        {
            try
            {
                var updateCandidate = await _context.Users.FindAsync(user.Id);

                updateCandidate.FullName = user.FullName;
                updateCandidate.UserName = user.UserName;
                updateCandidate.DateOfBirth = user.DateOfBirth;
                updateCandidate.PhoneNumber = user.PhoneNumber;
                updateCandidate.Address = user.Address;
                updateCandidate.PasswordHash = user.PasswordHash;
                updateCandidate.AvatarUrl = user.AvatarUrl;
                updateCandidate.LastLogin = user.LastLogin;
                updateCandidate.Email = user.Email;
                updateCandidate.Status = user.Status;
                updateCandidate.LevelId = 4;
                //updateCandidate.Level = user.Level;


                var objCandidate = _context.Users.Update(updateCandidate).Entity;
                await _context.SaveChangesAsync();
                return objCandidate;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<User> DeleteCandidate(Guid id)
        {
            try
            {
                var objCandidate = await _context.Users.FindAsync(id);

                _context.Users.Remove(objCandidate);
                await _context.SaveChangesAsync();
                return objCandidate;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
