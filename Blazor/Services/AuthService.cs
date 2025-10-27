using Blazor.Pages;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Shared.Enums;

namespace Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient httpClient , IJSRuntime js)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task<LoginResult> LoginAsync(LoginDTO model)
        {
            try
            {
                //var jsonToSend = JsonSerializer.Serialize(model);
                //Console.WriteLine(jsonToSend);

                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var token = json.GetProperty("token").GetString();

                    // ذخیره توکن در localStorage
                    await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

                    return new LoginResult { IsSuccess = true, Token = token };
                }

                var errorJson = await response.Content.ReadFromJsonAsync<JsonElement>();
                var errorMessage = errorJson.GetProperty("error").GetString();

                return new LoginResult { IsSuccess = false, ErrorMessage = errorMessage };
            }
            catch (Exception ex)
            {
                return new LoginResult { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }


        public async Task<RegisterResult> RegisterAsync(RegisterDTO model)
        {
            try
            {
                model.Role = UserRole.Admin;
                var response = await _httpClient.PostAsJsonAsync("api/Auth/register", model);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var message = json.GetProperty("message").GetString();

                    return new RegisterResult
                    {
                        IsSuccess = true,
                        Message = message
                    };
                }

                // خطای سرور
                var errorJson = await response.Content.ReadFromJsonAsync<JsonElement>();
                var errorMessage = errorJson.TryGetProperty("error", out var e)
                    ? e.GetString()
                    : "Registration failed.";

                return new RegisterResult
                {
                    IsSuccess = false,
                    ErrorMessage = errorMessage
                };
            }
            catch (Exception ex)
            {
                return new RegisterResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }

    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }

public class RegisterResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
}
}

