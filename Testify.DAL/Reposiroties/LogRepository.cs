using Testify.DAL.Context;
using Testify.DAL.Models;
namespace Testify.DAL.Reposiroties;

public class LogRepository
{
    private readonly TestifyDbContext _context;

    public LogRepository()
    {
        _context = new TestifyDbContext();
    }
    
    public IEnumerable<LogEntity> GetAllLogs()
    {
        return _context.Logs.ToList();
    }
    
    public LogEntity GetLogById(int id)
    {
        return _context.Logs.FirstOrDefault(log => log.Id == id);
    }
    
    public IEnumerable<LogEntity> GetLogsByLevel(string level)
    {
        return _context.Logs.Where(log => log.Level == level).ToList();
    }
    
    public IEnumerable<LogEntity> GetLogsByDateRange(DateTime startDate, DateTime endDate)
    {
        return _context.Logs
            .Where(log => log.TimeStamp >= startDate && log.TimeStamp <= endDate)
            .OrderBy(log => log.TimeStamp)
            .ToList();
    }
    
    public IEnumerable<LogEntity> GetLogsByMessageGuid(string guid)
    {
        return _context.Logs
            .Where(log => log.Message.Contains(guid))
            .ToList();
    }

    // Xóa log theo ID
    public void DeleteLog(int id)
    {
        var log = _context.Logs.FirstOrDefault(log => log.Id == id);
        if (log != null)
        {
            _context.Logs.Remove(log);
            _context.SaveChanges();
        }
    }

    // Thêm log mới (nếu cần)
    public void AddLog(LogEntity logEntity)
    {
        _context.Logs.Add(logEntity);
        _context.SaveChanges();
    }
}
