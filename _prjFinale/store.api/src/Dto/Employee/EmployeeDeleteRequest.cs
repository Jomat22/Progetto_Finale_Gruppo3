using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Employee;

public class EmployeeDeleteRequest
{
    [Required(ErrorMessage = "L'ID della persona associata è obbligatorio.")]
    public int PersonId { get; set; }
}