using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class ExamActicityLogRepository
    {
        private readonly TestifyDbContext _context;
        public ExamActicityLogRepository()
        {
            _context = new TestifyDbContext();
        }

        public bool AddExamL(ExamActivityLog log)
        {
            try
            {
                _context.ExamActivityLogs.Add(log);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ExamActivityLog GetById(int id) 
        {
            try
            {
                return _context.ExamActivityLogs.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public List<ExamActivityLog> GetAllByUserId(Guid userId)
        {
            try
            {
                return _context.ExamActivityLogs.Where(x => x.UserId == userId).OrderByDescending(x=>x.ActionTime).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<ExamActivityLog> GetAllByExamId(int examId)
        {
            try
            {
                return _context.ExamActivityLogs.Where(x => x.ExamId == examId).OrderByDescending(x => x.ActionTime).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<ExamActivityLog> GetAllByUserIdAndExamId( Guid uId, int examId)
        {
            try
            {
                return _context.ExamActivityLogs.Where(x => x.ExamId == examId&&x.UserId == uId).OrderByDescending(x => x.ActionTime).ToList();
            }
            catch
            {
                return null;
            }
        }

    }
}
