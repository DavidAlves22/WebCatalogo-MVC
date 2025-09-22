namespace CatalogoMvc.Validation;

public static class ValidationMessages
{
    public const string Required = "O campo {0} é obrigatório.";
    public const string MinLength = "O campo {0} precisa ter pelo menos {1} caracteres.";
    public const string MaxLength = "O campo {0} pode ter no máximo {1} caracteres.";
    public const string StringLength = "O campo {0} deve ter entre {2} e {1} caracteres.";
    public const string Url = "O campo {0} deve ser uma URL válida.";
}
