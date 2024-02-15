using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.ViewModels;

namespace TestAPI.Services;

public class FounderService
{
    private readonly FounderUseCase _founderUseCase;
    private readonly ISaveRepository _saveRepository;

    public FounderService(FounderUseCase founderUseCase, ISaveRepository saveRepository)
    {
        _founderUseCase = founderUseCase;
        _saveRepository = saveRepository;
    }

    public async Task<FounderMainInfo?> Add(FounderCreateInfo founderCreateInfo)
    {
        var founder = await _founderUseCase.GetFounderByTaxpayerNumber(founderCreateInfo.TaxpayerNumber);
        if (founder != null)
            return null;
        var newFounder = new Founder(founderCreateInfo.TaxpayerNumber, founderCreateInfo.Fullname);
        await _founderUseCase.Add(newFounder);
        await _saveRepository.Save();

        return new FounderMainInfo(newFounder);
    }

    public async Task<FounderMainInfo?> Update(FounderUpdate founder)
    {
        var currentFounder = await _founderUseCase.Get(founder.Id);
        if (currentFounder == null)
        {
            return null;
        }

        currentFounder.SetFullName(founder.Fullname);
        await _saveRepository.Save();

        return new FounderMainInfo(currentFounder);
    }

    public async Task<FounderMainInfo?> Remove(int id)
    {
        var founder = await _founderUseCase.Get(id);
        if (founder == null)
        {
            return null;
        }

        _founderUseCase.Remove(founder);
        await _saveRepository.Save();
        return new FounderMainInfo(founder);
    }

    public async Task<IReadOnlyList<FounderMainInfo>> GetAll(int pageNumber, int pageSize)
    {
        IReadOnlyList<Founder?> founders = await _founderUseCase.GetAll(pageNumber, pageSize);
        var viewFounders = founders.Select(x => new FounderMainInfo(x)).ToArray();
        return viewFounders;
    }

    public async Task<FounderMainInfo?> Get(int id)
    {
        var founder = await _founderUseCase.Get(id);
        return founder == null ? null : new FounderMainInfo(founder);
    }
}