namespace TestAPI.Domain.Models;

public class Client : BaseModel
{
    public string Inn { get; set; }

    public string Name { get; set; }

    public ClientType Type { get; set; }

    public List<Founder> Founders { get; set; } = new List<Founder>();

    public Client(string inn, string name, ClientType type)
    {
        Inn = inn;
        Name = name;
        Type = type;
    }
}