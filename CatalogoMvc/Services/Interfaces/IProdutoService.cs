using CatalogoMvc.Models;

namespace CatalogoMvc.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
        Task<ProdutoViewModel> GetById(string token, int id);
        Task<ProdutoViewModel> Create(string token, ProdutoViewModel produtoViewModel);
        Task<ProdutoViewModel> Update(string token, ProdutoViewModel produtoViewModel);
        Task<bool> Remove(string token, int id);
    }
}
