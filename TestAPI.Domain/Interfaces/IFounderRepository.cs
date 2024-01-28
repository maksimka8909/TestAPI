using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IFounderRepository : ICommonRepository<Founder>, IPersonRepository<Founder>
{
}