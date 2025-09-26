using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
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

        public async Task<IEnumerable<ProdutoViewModel>> GetProdutos()
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

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

        public async Task<ProdutoViewModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

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

        public async Task<ProdutoViewModel> Create(ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
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

        public async Task<ProdutoViewModel> Update(ProdutoViewModel produtoViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

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

        public async Task<bool> Remove(int id)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

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
    }
}
