using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models.Autenticacao
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string Password { get; set; }
    }
}
