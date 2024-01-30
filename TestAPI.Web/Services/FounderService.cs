﻿using TestAPI.Data.Repositories;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.Validators;
using TestAPI.ViewModels;

namespace TestAPI.Services;

public class FounderService
{
    private readonly FounderUseCase _founderUseCase;
    private readonly FounderCreateValidator _createValidator;
    private readonly FounderUpdateValidator _updateValidator;
    private readonly ISaveRepository _saveRepository;

    public FounderService(FounderUseCase founderUseCase,
        FounderCreateValidator createValidator, FounderUpdateValidator updateValidator, ISaveRepository saveRepository)
    {
        _founderUseCase = founderUseCase;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _saveRepository = saveRepository;
    }

    public async Task<IReadOnlyList<Message>> Add(FounderCreateInfo founderCreateInfo)
    {
        var result = _createValidator.Validate(founderCreateInfo);
        if (result.IsValid)
        {
            var founder = await _founderUseCase.GetUserByTaxpayerNumber(founderCreateInfo.TaxpayerNumber);
            if (founder != null)
                return new List<Message>() { new Message("Учредитель с таким ИНН уже есть") };

            await _founderUseCase.Add(new Founder(founderCreateInfo.TaxpayerNumber, founderCreateInfo.Fullname));
            await _saveRepository.Save();
            return new List<Message>() { new Message("Учредитель успешно создан") };
        }
        else
        {
            return result.Errors.Select(error => new Message(error.ErrorMessage)).ToList();
        }
    }

    public async Task<IReadOnlyList<Message>> Update(FounderUpdate founder)
    {
        var result = _updateValidator.Validate(founder);
        if (result.IsValid)
        {
            var currentFounder = await _founderUseCase.Get(founder.Id);
            if (currentFounder == null)
            {
                return new List<Message>() { new Message("Учредитель не найден") };
            }
            currentFounder.Fullname = founder.Fullname;
            currentFounder.UpdatedAt = DateTime.Now;
            await _saveRepository.Save();
            return new List<Message>() { new Message("Учредитель успешно обновлен") };
        }
        else
        {
            return result.Errors.Select(error => new Message(error.ErrorMessage)).ToList();
        }
    }

    public async Task<IReadOnlyList<Message>> Delete(int id)
    {
        var founder = await _founderUseCase.Get(id);
        if (founder == null)
        {
            return new List<Message>() { new Message("Учредитель не найден") };
        }
        founder.DeletedAt = DateTime.Now;
        await _saveRepository.Save();
        return new List<Message>() { new Message("Учредитель успешно удален") };
    }

    public async Task<IReadOnlyList<FounderMainInfo>> GetAll()
    {
        IReadOnlyList<Founder?> founders = await _founderUseCase.GetAll();
        var viewFounders = founders.Select(x => new FounderMainInfo(x)).ToArray();
        return viewFounders;
    }

    public async Task<FounderMainInfo> Get(int id)
    {
        var founder = await _founderUseCase.Get(id);
        if (founder == null)
        {
            return new FounderMainInfo();
        }
        return new FounderMainInfo(founder);
    }
}