using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A imagem url é obrigatória")]
        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }
    }
}
