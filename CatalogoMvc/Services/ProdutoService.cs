using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
using NuGet.Common;
using System.Text.Json;

namespace CatalogoMvc.Services
{
    public class ProdutoService : IProdutoService
    {
        private const string apiEndPoint = "api/produto/";
        private readonly JsonSerializerOptions _options;
        public readonly IHttpClientFactory _clientFactory;

        public ProdutoService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            PutTokenInHeader(client, token);

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var produtos = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(content, _options);
                    return produtos;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ProdutoViewModel> GetById(string token, int id)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            PutTokenInHeader(client, token);

            using (var response = await client.GetAsync(apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var produto = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(content, _options);
                    return produto;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ProdutoViewModel> Create(string token, ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            PutTokenInHeader(client, token);

            var produto = JsonSerializer.Serialize(produtoViewModel);
            StringContent content = new StringContent(produto, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var produtoResponse = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(contentResponse, _options);
                    return produtoResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ProdutoViewModel> Update(string token, ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            PutTokenInHeader(client, token);

            using (var response = await client.PutAsJsonAsync(apiEndPoint, produtoViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var produtoResponse = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(contentResponse, _options);
                    return produtoResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> Remove(string token, int id)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            PutTokenInHeader(client, token);

            using (var response = await client.DeleteAsync(apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static void PutTokenInHeader(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
