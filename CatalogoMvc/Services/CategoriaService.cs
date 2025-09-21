using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
using System.Text.Json;

namespace CatalogoMvc.Services
{
    public class CategoriaService : ICategoriaService
    {
        private const string apiEndPoint = "api/categoria/";
        private readonly JsonSerializerOptions _options;
        public readonly IHttpClientFactory _clientFactory;

        public CategoriaService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetCategorias()
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var categorias = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(content, _options);
                    return categorias;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<CategoriaViewModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");

            using (var response = await client.GetAsync(apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var categoria = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(content, _options);
                    return categoria;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<CategoriaViewModel> Create(CategoriaViewModel categoriaViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            var categoria = JsonSerializer.Serialize(categoriaViewModel);
            StringContent content = new StringContent(categoria, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var categoriaResponse = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(contentResponse, _options);
                    return categoriaResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<CategoriaViewModel> Update(CategoriaViewModel categoriaViewModel)
        {
            var client = _clientFactory.CreateClient("CatalogoApi");
            var categoria = JsonSerializer.Serialize(categoriaViewModel);
            StringContent content = new StringContent(categoria, System.Text.Encoding.UTF8, "application/json");

            using (var response = await client.PutAsJsonAsync(apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStreamAsync();
                    var categoriaResponse = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(contentResponse, _options);
                    return categoriaResponse;
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
