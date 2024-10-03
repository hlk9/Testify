using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class ExamRepository
    {
        TestifyDbContext _context;
        public ExamRepository()
        {
            _context = new TestifyDbContext();

        }

        public List<Exam> GetAllActive()
        {
            return _context.Exams.Where(x=>x.Status==1).ToList();
        }



    }
}
