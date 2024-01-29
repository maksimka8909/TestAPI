using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Domain.Models;

public class Client : BaseModel
{
    public string TaxpayerNumber { get; set; }

    public string Name { get; set; }

    public string TypeCode { get; protected set; }

    [NotMapped]
    public ClientType Type { 
        get => Enum.Parse<ClientType>(TypeCode);  
        set => TypeCode = value.ToString(); }

    public List<Founder> Founders { get; set; } = new List<Founder>();

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