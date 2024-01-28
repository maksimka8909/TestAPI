using TestAPI.Data.Repositories;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.Validators;
using TestAPI.ViewModels;

namespace TestAPI.Services;

public class FounderService
{
    private readonly FounderUseCase _founderUseCase;
    private readonly CommonRepository _commonRepository;
    private readonly FounderCreateValidator _createValidator;
    private readonly FounderUpdateValidator _updateValidator;

    public FounderService(FounderUseCase founderUseCase, CommonRepository commonRepository,
        FounderCreateValidator createValidator, FounderUpdateValidator updateValidator)
    {
        _founderUseCase = founderUseCase;
        _commonRepository = commonRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IReadOnlyList<Message>> Add(FounderCreateInfo founderCreateInfo)
    {
        var result = _createValidator.Validate(founderCreateInfo);
        if (result.IsValid)
        {
            var founder = await _founderUseCase.GetUserByTaxpayerNumber(founderCreateInfo.Inn);
            if (founder != null)
                return new List<Message>() { new Message("Клиент с таким ИНН уже есть") };

            await _founderUseCase.Add(new Founder(founderCreateInfo.Inn, founderCreateInfo.Fio));
            await _commonRepository.Save();
            return new List<Message>() { new Message("Клиент успешно создан") };
        }
        else
        {
            List<Message> errors = new List<Message>();
            foreach (var error in result.Errors)
            {
                errors.Add(new Message(error.ErrorMessage));
            }

            return errors;
        }
    }

    public async Task<IReadOnlyList<Message>> Update(FounderUpdate founder)
    {
        var result = _updateValidator.Validate(founder);
        if (result.IsValid)
        {
            var f = await _founderUseCase.Get(founder.Id);
            f.Fio = founder.Fio;
            await _founderUseCase.Update(f);
            await _commonRepository.Save();
            return new List<Message>() { new Message("Клиент успешно создан") };
        }
        else
        {
            List<Message> errors = new List<Message>();
            foreach (var error in result.Errors)
            {
                errors.Add(new Message(error.ErrorMessage));
            }

            return errors;
        }
    }

    public async Task Delete(int id)
    {
        await _founderUseCase.Delete(id);
        await _commonRepository.Save();
    }

    public async Task<IReadOnlyList<FounderMainInfo>> GetAll()
    {
        var founders = await _founderUseCase.GetAll();
        var viewFounders = founders.Select(x => new FounderMainInfo(x)).ToArray();
        return viewFounders;
    }

    public async Task<FounderMainInfo> Get(int id)
    {
        var founder = await _founderUseCase.Get(id);
        return new FounderMainInfo(founder);
    }
}