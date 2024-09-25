using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        TestifyDbContext context;
        [HttpGet("Get-Classes")]
        public List<Class> GetAllClassOfOrganization()
        {
            context = new TestifyDbContext();
         
            return context.Classes.ToList();

        }

        [HttpGet("Get-Class-ByName")]
        public List<Class> GetClassByName(string keyword, int organizationId)
        {
            context = new TestifyDbContext();
            return context.Classes.Where(x => x.Name.Contains(keyword)).ToList();

        }

        [HttpPost("Add-Class")]
        public bool AddClass(Class cls)
        {
            context = new TestifyDbContext();
            context.Classes.Add(cls);
            return context.SaveChanges() > 0;
        }

        [HttpDelete("Delete-Class")]
        public bool DeleteClass(int classId)
        {
            context = new TestifyDbContext();


            try
            {
                var cls = context.Classes.FirstOrDefault(x => x.Id == classId);
                context.Classes.Remove(cls);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
          

        }

        [HttpPut("Update-Class")]
        public bool UpdateClass(Class classUpdate)
        {
            context = new TestifyDbContext();
            var classOrigin = context.Classes.Find(classUpdate.Id);
            if (classOrigin != null)
            {
                classOrigin.Name = classUpdate.Name;               
                classOrigin.Description = classUpdate.Description;
                classOrigin.Status = classUpdate.Status;
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
