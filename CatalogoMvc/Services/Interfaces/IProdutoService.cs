using CatalogoMvc.Models;

namespace CatalogoMvc.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetProdutos();
        Task<ProdutoViewModel> GetById(int id);
        Task<ProdutoViewModel> Create(ProdutoViewModel produtoViewModel);
        Task<ProdutoViewModel> Update(ProdutoViewModel produtoViewModel);
        Task<bool> Remove(int id);
    }
}
