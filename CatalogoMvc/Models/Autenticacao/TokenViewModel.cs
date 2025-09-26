using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models.Autenticacao
{
    public class TokenViewModel
    {
        [Required(ErrorMessage = "Necessário informar o Token")]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = "Necessário informar o Refresh Token")]
        public string RefreshToken { get; set; }
    }
}
