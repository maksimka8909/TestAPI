using TestAPI.Domain.Models;

namespace TestAPI.ViewModels;

public class ClientMainInfo
{
    public int Id { get; }

    public string TaxpayerNumber { get; private set; }

    public string Name { get; private set; }

    public string Type { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public List<FounderMainInfo> Founders { get; private set; }

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