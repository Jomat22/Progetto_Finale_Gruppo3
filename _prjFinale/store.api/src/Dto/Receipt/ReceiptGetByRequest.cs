using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Receipt;

public class ReceiptGetByRequest
{
    [Required(ErrorMessage = "ID scontrino obbligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "L'ID deve essere maggiore di 0.")]
    public int Id { get; set; }
}