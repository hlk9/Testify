using Blazored.LocalStorage;
using System.Threading.Tasks;

public class TokenService
{
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SetTokenAsync(string token)
    {
        await _localStorage.SetItemAsync("token", token);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("token");
    }

    public async Task RemoveTokenAsync()
    {
        await _localStorage.RemoveItemAsync("token");
    }
}
