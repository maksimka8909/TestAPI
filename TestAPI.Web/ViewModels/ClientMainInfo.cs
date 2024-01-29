using TestAPI.Domain.Models;

namespace TestAPI.ViewModels;

public class ClientMainInfo
{
    public int Id { get; }

    public string TaxpayerNumber { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<FounderMainInfo> Founders { get; set; }

    public ClientMainInfo(Client client)
    {
        Id = client.Id;
        TaxpayerNumber = client.TaxpayerNumber;
        Name = client.Name;
        Type = client.TypeCode;
        CreatedAt = client.CreatedAt;
        UpdatedAt = client.UpdatedAt;
        Founders = new List<FounderMainInfo>(client.Founders.Select(f => new FounderMainInfo(f)).ToArray());
    }
}