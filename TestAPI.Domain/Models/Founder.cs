namespace TestAPI.Domain.Models;

public class Founder : BaseModel
{
    public string TaxpayerNumber { get; private set; }

    public string Fullname { get; private set; }

    public List<Client> Clients { get; set; } = new List<Client>();

    public void SetFullName(string name)
    {
        Fullname = name;
    }

    public Founder(string taxpayerNumber, string fullname)
    {
        TaxpayerNumber = taxpayerNumber;
        Fullname = fullname;
    }
}