using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Testify.DAL.Reposiroties
{
    public class ExamReponsitory
    {
        private readonly TestifyDbContext _context;


        public ExamReponsitory()
        {

            _context = new TestifyDbContext();
        }

        public async  Task<List<Exam>> GetAllExam(string? textSearch, bool isActive)
        {
            var qr = _context.Exams.AsQueryable();

            if (isActive)
            {
                qr = qr.Where(x => x.Status == 1);
            }

            if (!string.IsNullOrEmpty(textSearch))
            {
                qr = qr.Where(c =>
                        c.Name.ToLower().Contains(textSearch.Trim().ToLower()));

            }
            return await qr.ToListAsync();

        }

        public async Task<Exam> GetByIdExam(int id)
        {
            var a = await _context.Exams.FindAsync(id);
            return a;
        }

        public async Task<List<ExamWhitExamDetail>> GetAllExamWhitExamDetail(string? textSearch, bool isActive)
        {
            var filteredExam = await GetAllExam(textSearch, isActive);
            var data = await (from _ex in _context.Exams
                              join _exdt in _context.ExamDetails
                              on _ex.Id equals _exdt.ExamId 
                              join _exdtqs in _context.ExamDetailQuestions
                              on _exdt.Id equals _exdtqs.ExamDetailId
                              join _qs in _context.Questions
                              on _exdtqs.QuestionId equals _qs.Id
                              select new ExamWhitExamDetail
                              {
                                  Id = _ex.Id,
                                  Name = _ex.Name,
                                  IdExamDetail = _exdt.Id,
                                  IdExamDetailQues = _exdtqs.Id,
                                  NumberOfQuestions = _ex.NumberOfQuestions,
                                  Status = _ex.Status,
                                  MaximmumMark = _ex.MaximmumMark,
                                  PassMark = _ex.PassMark,
                                  Duration = _ex.Duration,
                                  CreateDate = _exdt.CreateDate,
                                  CreateBy = _exdt.CreateBy,
                                  UpdateBy = _exdt.UpdateBy,
                                  UpdateDate = _exdt.UpdateDate,
                                  Point = _exdtqs.Point,
                                  Description = _ex.Description,
                                  
                              }
                              ).ToListAsync();
            return data;
        }

        public async Task<Exam> AddExam(Exam exam)
        {
            try
            {
                var addExam = _context.Exams.Add(exam).Entity;
                await _context.SaveChangesAsync();
                return addExam;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Exam> UpdateExam(Exam exam)
        {
            try
            {
                var objUpdateExam = await _context.Exams.FindAsync(exam.Id);

                

                objUpdateExam.Name = exam.Name;
                objUpdateExam.NumberOfQuestions = exam.NumberOfQuestions;
                objUpdateExam.Status = exam.Status;
                objUpdateExam.Duration = exam.Duration;
                objUpdateExam.Description = exam.Description;
                objUpdateExam.MaximmumMark = exam.MaximmumMark;
                objUpdateExam.PassMark = exam.PassMark; 
                objUpdateExam.SubjectId = exam.SubjectId;

                var updateExam = _context.Exams.Update(objUpdateExam).Entity;
                await _context.SaveChangesAsync();
                return updateExam;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Exam> DeleteExam(int id)
        {
            try
            {
                var objDeleteExam = await _context.Exams.FindAsync(id);

                _context.Exams.Remove(objDeleteExam);
                await _context.SaveChangesAsync();
                return objDeleteExam;
            }
            catch
            {
                return null;
            }
        }

       

        public async Task<List<Exam>> GetAllActicve()
        {
            return _context.Exams.Where(x=>x.Status == 1).ToList();
        }

        public async Task<List<Exam>> GetAllActicveOfSubject(int subjectId)
        {
            return _context.Exams.Where(x => x.Status == 1&&x.SubjectId==subjectId).ToList();
        }
        
    }
}
