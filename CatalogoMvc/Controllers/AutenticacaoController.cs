using CatalogoMvc.Models.Autenticacao;
using CatalogoMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoMvc.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var login = await _autenticacaoService.Login(loginViewModel);
                if (login is null)
                {
                    ModelState.AddModelError(string.Empty, "Login inválido!");
                    return View(loginViewModel);
                }

                Response.Cookies.Append("X-Access-Token", login.Token, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = login.Expiration.ToLocalTime(),
                    SameSite = SameSiteMode.Strict
                });

                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Dados inválidos, verifique os campos.");
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var registro = await _autenticacaoService.Register(registerViewModel);
                if (registro is null)
                {
                    ModelState.AddModelError(string.Empty, "Registro inválido!");
                    return View(registerViewModel);
                }

                return Redirect("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Dados inválidos, verifique os campos.");
                return View(registerViewModel);
            }
        }
    }
}
