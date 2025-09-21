using CatalogoMvc.Models;

namespace CatalogoMvc.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> GetCategorias();
        Task<CategoriaViewModel> GetById(int id);
        Task<CategoriaViewModel> Create(CategoriaViewModel categoriaViewModel);
        Task<CategoriaViewModel> Update(CategoriaViewModel categoriaViewModel);
        Task<bool> Remove(int id);
    }
}
