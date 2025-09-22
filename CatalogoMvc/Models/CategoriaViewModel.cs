using CatalogoMvc.Validation;
using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MinLength(3, ErrorMessage = ValidationMessages.MinLength)]
        [MaxLength(80, ErrorMessage = ValidationMessages.MaxLength)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [Display(Name = "Imagem")]
        [MinLength(5, ErrorMessage = ValidationMessages.MinLength)]
        [StringLength(300, ErrorMessage = ValidationMessages.StringLength)]
        public string? ImagemUrl { get; set; }
    }
}
