using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class FounderUseCase : ICommonRepository<Founder>
{
    private readonly IFounderRepository _founderRepository;

    public FounderUseCase(IFounderRepository founderRepository)
    {
        _founderRepository = founderRepository;
    }

    public async Task Add(Founder item) =>
        await _founderRepository.Add(item);

    public async Task Update(Founder item) => 
        await _founderRepository.Update(item);

    public async Task Delete(int id) =>
        await _founderRepository.Delete(id);

    public async Task<IReadOnlyList<Founder>> GetAll() =>
        await _founderRepository.GetAll();

    public async Task<Founder> Get(int id) =>
        await _founderRepository.Get(id);
    
    public async Task<Founder> GetUserByTaxpayerNumber(string number)=>
        await _founderRepository.GetUserByTaxpayerNumber(number);
}