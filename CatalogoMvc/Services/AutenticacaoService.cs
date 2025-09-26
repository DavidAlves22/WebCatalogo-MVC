using CatalogoMvc.Models.Autenticacao;
using CatalogoMvc.Services.Interfaces;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace CatalogoMvc.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private const string apiEndPoint = "api/auth/";
        private readonly JsonSerializerOptions _options;
        public readonly IHttpClientFactory _clientFactory;

        public AutenticacaoService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<LoginRetornoViewModel> Login(LoginViewModel loginViewModel)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");
            var login = JsonSerializer.Serialize(loginViewModel);
            StringContent content = new StringContent(login, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint + "login", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<LoginRetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<RetornoViewModel> Register(RegisterViewModel model)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");
            var novoUsuario = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(novoUsuario, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint + "register", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<RetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<LoginRetornoViewModel> RefreshToken(TokenViewModel tokenModel)
        {            
            var client = _clientFactory.CreateClient("AutenticaApi");
            var token = JsonSerializer.Serialize(tokenModel);
            StringContent content = new StringContent(token, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint + "refresh - token", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<LoginRetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<RetornoViewModel> Revoke(string username)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");

            using (var response = await client.PostAsync(apiEndPoint + "revoke" + "/" + username, null))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<LoginRetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<RetornoViewModel> CreateRole(string role)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");

            var requestBody = new
            {
                Role = role
            };

            var novaRole = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(novaRole, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint + "create-role", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<LoginRetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<RetornoViewModel> AddUserToRole(string email, string role)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");

            var requestBody = new
            {
                Email = email,
                Role = role
            };

            var associarRole = JsonSerializer.Serialize(requestBody);
            StringContent content = new StringContent(associarRole, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint + "add-user-role", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var loginResponse = await JsonSerializer.DeserializeAsync<LoginRetornoViewModel>(contentResponse, _options);
                    return loginResponse;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
