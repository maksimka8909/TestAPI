using TestAPI.Domain.Models;

namespace TestAPI.ViewModels;

public class FounderMainInfo
{
    public int Id { get; }

    public string TaxpayerNumber { get; set; }

    public string Fullname { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public FounderMainInfo(Founder? founder)
    {
        Id = founder.Id;
        TaxpayerNumber = founder.TaxpayerNumber;
        Fullname = founder.Fullname;
        CreatedAt = founder.CreatedAt;
        UpdatedAt = founder.UpdatedAt;
    }

    public FounderMainInfo()
    {
    }
}