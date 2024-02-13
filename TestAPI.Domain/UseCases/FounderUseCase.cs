using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class FounderUseCase : GenericUseCase<Founder>
{
    private readonly IFounderRepository _founderRepository;

    public FounderUseCase(IFounderRepository founderRepository) : base(founderRepository)
    {
        _founderRepository = founderRepository;
    }

    public async Task<Founder?> GetFounderByTaxpayerNumber(string number) =>
        await _founderRepository.GetFounderByTaxpayerNumber(number);
}