namespace TestAPI.Domain.Models;

public class Founder : BaseModel
{
    public string TaxpayerNumber { get; set; }

    public string Fullname { get; set; }

    public List<Client> Clients { get; set; } = new List<Client>();

    public Founder(string taxpayerNumber, string fullname)
    {
        TaxpayerNumber = taxpayerNumber;
        Fullname = fullname;
        CreatedAt = DateTime.Now;
    }

    protected Founder()
    {
    }
}