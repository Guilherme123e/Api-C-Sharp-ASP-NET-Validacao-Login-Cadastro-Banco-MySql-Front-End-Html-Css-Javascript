using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
  public class Usuario
  {
    public int Id { get; set; }

    [Required, StringLength(100), RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Nome inválido.")]
    public string Nome { get; set; } = string.Empty;

    [Required, RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
    public string Cpf { get; set; } = string.Empty;

    [Required]
    public string Rg { get; set; } = string.Empty;

    [Required]
    public DateTime Data_nascimento { get; set; }

    [Required]
    public string Endereco { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha_hash { get; set; } = string.Empty;

    [Required]
    public string Genero { get; set; } = string.Empty;
  }
}
