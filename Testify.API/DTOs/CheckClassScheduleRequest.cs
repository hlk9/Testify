using Testify.DAL.ViewModels;

namespace Testify.API.DTOs;

public class CheckClassScheduleRequest
{
    public List<ClassWithUser> DataList { get; set; }
    public int ScheduleId { get; set; }
}