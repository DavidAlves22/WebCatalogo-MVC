using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoMvc.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
        {
            var categorias = await _categoriaService.GetCategorias();
            if (categorias is null) return View("Error");
            return View(categorias);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CategoriaViewModel>> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria is null) return View("Error");
            return View(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> Create(CategoriaViewModel categoriaViewModel)
        {
            var categoria = await _categoriaService.Create(categoriaViewModel);
            if (categoria is null) return View("Error");
            return View(categoria);
        }

        [HttpPut]
        public async Task<ActionResult<CategoriaViewModel>> Update(CategoriaViewModel categoriaViewModel)
        {
            var categoria = await _categoriaService.Update(categoriaViewModel);
            if (categoria is null) return View("Error");
            return View(categoria);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> Remove(int id)
        {
            var remove = await _categoriaService.Remove(id);
            if (!remove) return View("Error");
            return View(remove);
        }
    }
}
