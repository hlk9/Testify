using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("ExamActivityLog")]
    public class ExamActivityLogController : ControllerBase
    {

        private readonly ExamActicityLogRepository _repo;
        public ExamActivityLogController()
        {
            _repo = new ExamActicityLogRepository();
        }

        [HttpGet("GetAllByExamId")]
        public List<ExamActivityLog> GetAllByExamId(int id)
        {
           return _repo.GetAllByExamId(id);
        }

        [HttpGet("GetAllByUidAndEid")]
        public List<ExamActivityLog> GetAllByUidAndEid(Guid Uid,int Eid,int examScheduleId)
        {
            return _repo.GetAllByUserIdAndExamIdSheduleId(Uid, Eid, examScheduleId);
        }

        [HttpPost("AddExamLog")]
        public bool AddExLog([FromBody] ExamActivityLog log)
        {
            return _repo.AddExamL(log);
        }

        [HttpGet("GetAllByUserId")]
        public List<ExamActivityLog> GetAllByUserId(Guid id)
        {
            return _repo.GetAllByUserId(id);
        }

        [HttpGet("GetById")]
        public ExamActivityLog GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
