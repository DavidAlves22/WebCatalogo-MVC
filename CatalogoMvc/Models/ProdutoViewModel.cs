using System.ComponentModel.DataAnnotations;

namespace CatalogoMvc.Models;

public class ProdutoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatória")]
    [MinLength(5)]
    [Display(Name = "Descrição")]
    [MaxLength(300)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Preço é obrigatório")]
    [Display(Name = "Preço")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A imagem url é obrigatória")]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    [Required(ErrorMessage = "Estoque é obrigatório")]
    [Range(1, 99999)]
    public int Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }
}
