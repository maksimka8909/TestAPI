using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class FounderUseCase
{
    private readonly IFounderRepository _founderRepository;
    private readonly IGenericRepository<Founder> _genericRepository;
    public FounderUseCase(IFounderRepository founderRepository, IGenericRepository<Founder> genericRepository)
    {
        _founderRepository = founderRepository;
        _genericRepository = genericRepository;
    }

    public async Task Add(Founder item) =>
        await _genericRepository.Add(item);

    public async Task<IReadOnlyList<Founder>> GetAll() =>
        await _genericRepository.GetAll();

    public async Task<Founder> Get(int id) =>
        await _genericRepository.Get(id);

    public async Task<Founder?> GetUserByTaxpayerNumber(string number) =>
        await _founderRepository.GetUserByTaxpayerNumber(number);
}