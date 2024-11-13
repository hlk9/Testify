public class LogService
{
    private readonly ILogger<LogService> _logger;

    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogError(string message, Exception exception)
    {
        _logger.LogError(exception, message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }
}
