using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Auth;

public class AuthLoginRequest
{
    [Required(ErrorMessage = "L'Email aziendale è obbligatoria.")]
    [EmailAddress(ErrorMessage = "Formato Email aziendale non valido.")]
    [StringLength(255, ErrorMessage = "L'Email aziendale non può superare i 255 caratteri.")]
    public string EmailAziendale { get; set; } = default!;

    [Required(ErrorMessage = "La Password è obbligatoria.")]
    [StringLength(24, MinimumLength = 8, ErrorMessage = "La Password deve essere compresa tra 8 e 24 caratteri.")]
    public string Password { get; set; } = default!;
}