using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IFounderRepository: IGenericRepository<Founder>
{
    public Task<Founder?> GetUserByTaxpayerNumber(string number);
}