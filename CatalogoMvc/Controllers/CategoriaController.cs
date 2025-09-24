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

        [HttpGet]
        public IActionResult CriarNovaCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = await _categoriaService.Create(categoriaViewModel);
                if (categoria is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar categoria";
            }

            return View(categoriaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategoria(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            if (categoria is null)
                return View("Error");

            return View(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> UpdateCategoria(CategoriaViewModel categoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = await _categoriaService.Update(categoriaViewModel);
                if (categoria is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar categoria";
            }

            return View(categoriaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveCategoria(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            if (categoria is null)
                return View("Error");

            return View(categoria);
        }

        [HttpPost(), ActionName("RemoveCategoria")]
        public async Task<ActionResult> Remove(int id)
        {
            var remove = await _categoriaService.Remove(id);

            if (remove)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }
    }
}
