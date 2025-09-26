using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoMvc.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var produtos = await _produtoService.GetProdutos();
            if (produtos is null) return View("Error");
            return View(produtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProdutoViewModel>> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);
            if (produto is null) return View("Error");
            return View(produto);
        }

        [HttpGet]
        public IActionResult CriarNovoProduto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoService.Create(produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduto(int id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> UpdateProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoService.Update(produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduto(int id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpPost(), ActionName("RemoveProduto")]
        public async Task<ActionResult> Remove(int id)
        {
            var remove = await _produtoService.Remove(id);

            if (remove)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }
    }
}
