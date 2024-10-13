using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.DAL.Reposiroties
{
    public class LecturerRepository
    {
        TestifyDbContext _context;
        public LecturerRepository()
        {
            _context = new TestifyDbContext();
        }

        public async Task<List<User>> GetAllLecturer(string? textSearch, bool isActive)
        {
            //return await _context.Users.ToListAsync();
            if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == false)
            {
                return await _context.Users.ToListAsync();
            }
            else if ((string.IsNullOrEmpty(textSearch) || textSearch.Length == 0) && isActive == true)
            {
                return await _context.Users.Where(x => x.Status == 1).ToListAsync();
            }
            else if ((textSearch != null || textSearch != "") && isActive == true)
            {
                return await _context.Users.Where(x => x.UserName.ToLower().Contains(textSearch.Trim().ToLower())
                || x.FullName.ToLower().Contains(textSearch.Trim().ToLower())
                || x.PhoneNumber.ToLower().Contains(textSearch.Trim().ToLower())
                || x.Email.ToLower().Contains(textSearch.Trim().ToLower())
                || x.Address.ToLower().Contains(textSearch.Trim().ToLower()) && x.Status == 1).ToListAsync();
            }
            else
            {
                return await _context.Users.Where(x => x.FullName.ToLower().Contains(textSearch.Trim().ToLower())).ToListAsync();
            }
        }

        public async Task<List<User>> GetAllTeacher()
        {
            return await _context.Users.Where(x => x.LevelId == 3).ToListAsync();
        }
        public async Task<User> GetLecturerById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateLecturer(User user)
        {
            try
            {
                user.LevelId = 3;
                var addLecturer = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addLecturer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateLecturer(User user)
        {
            try
            {
                var updateLecturer = await _context.Users.FindAsync(user.Id);

                updateLecturer.FullName = user.FullName;
                updateLecturer.UserName = user.UserName;
                updateLecturer.DateOfBirth = user.DateOfBirth;
                updateLecturer.PhoneNumber = user.PhoneNumber;
                updateLecturer.Address = user.Address;
                updateLecturer.PasswordHash = user.PasswordHash;
                updateLecturer.AvatarUrl = user.AvatarUrl;
                updateLecturer.LastLogin = user.LastLogin;
                updateLecturer.Email = user.Email;
                updateLecturer.Status = user.Status;
                updateLecturer.LevelId = 3;
                //updateCandidate.Level = user.Level;


                var objLecturer = _context.Users.Update(updateLecturer).Entity;
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public async Task<User> DeleteLecturer(Guid id)
        {
            try
            {
                var objLecturer = await _context.Users.FindAsync(id);

                _context.Users.Remove(objLecturer);
                await _context.SaveChangesAsync();
                return objLecturer;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<ScoreStatistics>> GetScore(int IDSubject, int IDExam)
        {


            var data = await (from u in _context.Users
                              join sub in _context.Submissions
                              on u.Id equals sub.UserId
                              from e in _context.Exams
                              join s in _context.Subjects
                              on e.SubjectId equals s.Id
                              where (
                              s.Id == IDSubject && e.Id == IDExam

                              )
                              select new ScoreStatistics
                              {
                                  UserID = u.Id,
                                  FullName = u.FullName,
                                  SubjectId = s.Id,
                                  SubjectName = s.Name,
                                  ExamId = e.Id,
                                  ExamName = e.Name,
                                  SubmissionId = sub.Id,
                                  Score = sub.TotalMark

                              }).ToListAsync();
            return data;
        }

    }
}
