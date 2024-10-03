using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        ExamRepository repo;
        public ExamController()
        {
            repo = new ExamRepository();
        }
        [HttpGet("Get-Active")]
        public List<Exam> GetAllActicve()
        {
            return repo.GetAllActive();
        }
    }
}
