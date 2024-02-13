using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Domain.Models;

public class Client : BaseModel
{
    public string TaxpayerNumber { get; private set; }

    public string Name { get; private set; }

    public string TypeCode { get; private set; }

    [NotMapped]
    public ClientType Type
    {
        get => Enum.Parse<ClientType>(TypeCode);
        set => TypeCode = value.ToString();
    }

    public List<Founder> Founders { get; set; } = new List<Founder>();

    public void SetName(string name)
    {
        Name = name;
    }

    public Client(string taxpayerNumber, string name, ClientType type)
    {
        TaxpayerNumber = taxpayerNumber;
        Name = name;
        Type = type;
    }

    protected Client()
    {
    }
}