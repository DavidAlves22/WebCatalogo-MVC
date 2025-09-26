namespace CatalogoMvc.Models.Autenticacao
{
public class LoginRetornoViewModel : RetornoViewModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
