using TestAPI.Domain.Models;

namespace TestAPI.ViewModels;

public class FounderMainInfo
{
    public int Id { get; }

    public string TaxpayerNumber { get; private set; }

    public string Fullname { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public FounderMainInfo(Founder founder)
    {
        Id = founder.Id;
        TaxpayerNumber = founder.TaxpayerNumber;
        Fullname = founder.Fullname;
        CreatedAt = founder.CreatedAt;
        UpdatedAt = founder.UpdatedAt;
    }
}