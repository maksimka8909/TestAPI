using System.ComponentModel.DataAnnotations;

namespace TestAPI.ViewModels;

public class ClientUpdate
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [MinLength(1)]
    public string Name { get; set; }
}