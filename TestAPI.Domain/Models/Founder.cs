namespace TestAPI.Domain.Models;

public class Founder : BaseModel
{
    public string Inn { get; set; }

    public string Fio { get; set; }

    public List<Client> Clients { get; set; } = new List<Client>();

    public Founder(string inn, string fio)
    {
        Inn = inn;
        Fio = fio;
    }
}