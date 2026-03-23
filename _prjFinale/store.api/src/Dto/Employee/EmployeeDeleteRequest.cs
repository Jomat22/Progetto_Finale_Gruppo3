using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Employee;

public class EmployeeDeleteRequest
{
    [Required(ErrorMessage = "Il Codice Meccanografico è obbligatorio.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Il Codice Meccanografico deve essere di 10 caratteri.")]
    public string CodiceMeccanografico { get; set; } = default!;
}