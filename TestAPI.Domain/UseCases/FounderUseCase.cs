using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class FounderUseCase : GenericUseCase<Founder>
{
    private readonly IFounderRepository _founderRepository;

    public FounderUseCase(IFounderRepository founderRepository, IGenericRepository<Founder> genericRepository)
        : base(genericRepository)
    {
        _founderRepository = founderRepository;
    }

    public async Task<Founder?> GetUserByTaxpayerNumber(string number) =>
        await _founderRepository.GetUserByTaxpayerNumber(number);
}