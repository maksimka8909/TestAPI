namespace TestAPI.Domain.Interfaces;

public interface IPersonRepository<T>
where T: class
{
    public Task<T> GetUserByTaxpayerNumber(string number);
}