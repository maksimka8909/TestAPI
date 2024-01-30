using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IFounderRepository 
{
    public Task<Founder?> GetUserByTaxpayerNumber(string number);
}