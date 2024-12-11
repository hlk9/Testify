using Testify.DAL.Models;

namespace Testify.Web.Services;

public class LogUserService
{
    private readonly HttpClient _httpClient;

    public LogUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gửi yêu cầu đến API để lấy danh sách logs chứa GUID trong Message.
    /// </summary>
    /// <param name="guid">GUID cần tìm</param>
    /// <returns>Danh sách logs hoặc null nếu không tìm thấy</returns>
    public async Task<List<LogEntity>> GetLogsByGuidAsync(string guid)
    {
        if (string.IsNullOrWhiteSpace(guid))
        {
            throw new ArgumentException("GUID không được để trống.", nameof(guid));
        }

        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<LogEntity>>($"LogUser/by-guid?guid={guid}");
            return response;
        }
        catch (Exception e)
        {
            return null;
        }
       

      

        // Xử lý khi không tìm thấy hoặc lỗi từ server
        return null;
    }
}