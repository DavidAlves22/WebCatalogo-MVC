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
            var token = Request.Cookies["X-Access-Token"];
            var produtos = await _produtoService.GetProdutos(token);
            if (produtos is null) return View("Error");
            return View(produtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProdutoViewModel>> GetById(int id)
        {
            var token = Request.Cookies["X-Access-Token"];
            var produto = await _produtoService.GetById(token, id);
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
                var token = Request.Cookies["X-Access-Token"];
                var produto = await _produtoService.Create(token, produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduto(int id)
        {
            var token = Request.Cookies["X-Access-Token"];
            var produto = await _produtoService.GetById(token, id);

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> UpdateProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var token = Request.Cookies["X-Access-Token"];
                var produto = await _produtoService.Update(token, produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduto(int id)
        {
            var token = Request.Cookies["X-Access-Token"];
            var produto = await _produtoService.GetById(token, id);

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpPost(), ActionName("RemoveProduto")]
        public async Task<ActionResult> Remove(int id)
        {
            var token = Request.Cookies["X-Access-Token"];
            var remove = await _produtoService.Remove(token, id);

            if (remove)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }
    }
}
