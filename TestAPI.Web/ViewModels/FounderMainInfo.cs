using TestAPI.Domain.Models;

namespace TestAPI.ViewModels;

public class FounderMainInfo
{
    public int Id { get; }
    
    public string Inn { get; set; }

    public string Fio { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public FounderMainInfo(Founder founder)
    {
        Id = founder.Id;
        Inn = founder.Inn;
        Fio = founder.Fio;
        CreatedAt = founder.CreatedAt;
        UpdatedAt = founder.UpdatedAt;
    }
}