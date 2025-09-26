using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models.Autenticacao;

public class LoginViewModel
{
    [Required(ErrorMessage = "O campo UserName é obrigatório.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "O campo Password é obrigatório.")]
    public string Password { get; set; }
}
