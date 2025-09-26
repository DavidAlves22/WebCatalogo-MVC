using CatalogoMvc.Models.Autenticacao;

namespace CatalogoMvc.Services.Interfaces;

public interface IAutenticacaoService
{
    Task<LoginRetornoViewModel> Login(LoginViewModel loginViewModel);
    Task<RetornoViewModel> Register(RegisterViewModel model);
    Task<RetornoViewModel> AddUserToRole(string email, string role);
    Task<LoginRetornoViewModel> RefreshToken(TokenViewModel tokenModel);
    Task<RetornoViewModel> Revoke(string username);
    Task<RetornoViewModel> CreateRole(string role);
}
