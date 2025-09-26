using CatalogoMvc.Models;
using CatalogoMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogoMvc.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string _token = string.Empty;

        public ProdutoController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var produtos = await _produtoService.GetProdutos(GetToken());
            if (produtos is null) return View("Error");
            return View(produtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProdutoViewModel>> GetById(int id)
        {
            var produto = await _produtoService.GetById(GetToken(), id);
            if (produto is null) return View("Error");
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> CriarNovoProduto()
        {
            ViewBag.Categorias = new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoService.Create(GetToken(), produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));

                ViewBag.Categorias = new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduto(int id)
        {
            var produto = await _produtoService.GetById(GetToken(), id);

            if (produto is null)
                return View("Error");

            ViewBag.Categorias = new SelectList(await _categoriaService.GetCategorias(), "Id", "Nome");
            return View(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> UpdateProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoService.Update(GetToken(), produtoViewModel);
                if (produto is not null)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = "Erro ao criar produto";
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduto(int id)
        {
            var produto = await _produtoService.GetById(GetToken(), id);

            if (produto is null)
                return View("Error");

            return View(produto);
        }

        [HttpPost(), ActionName("RemoveProduto")]
        public async Task<ActionResult> Remove(int id)
        {
            var remove = await _produtoService.Remove(GetToken(), id);

            if (remove)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }

        private string GetToken()
        {
            if(HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                _token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
            
            return _token;
        }
    }
}
