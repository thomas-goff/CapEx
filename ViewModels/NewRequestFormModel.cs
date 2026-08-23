using System.ComponentModel.DataAnnotations;

namespace CapEx.ViewModels;

public sealed class NewRequestFormModel
{
    [Required(ErrorMessage = "Give the request a title.")]
    [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter an amount.")]
    [Range(0.01, 9_999_999_999.99, ErrorMessage = "Amount must be more than R 0.00.")]
    public decimal? Amount { get; set; }

    [Required(ErrorMessage = "Explain why this spend is needed.")]
    [StringLength(2000, ErrorMessage = "Motivation cannot be longer than 2000 characters.")]
    public string Motivation { get; set; } = string.Empty;
}
