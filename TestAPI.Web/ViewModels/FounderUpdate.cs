using System.ComponentModel.DataAnnotations;

namespace TestAPI.ViewModels;

public class FounderUpdate
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [MinLength(1)]
    public string Fullname { get; set; }
}