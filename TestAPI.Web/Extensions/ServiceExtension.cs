using Microsoft.EntityFrameworkCore;
using TestAPI.Data.Repositories;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.Services;
using TestAPI.Validators;

namespace TestAPI.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddMyService(this IServiceCollection serviceCollection)
    {
        //репозитории
        serviceCollection.AddTransient<ISaveRepository, SaveRepository>();
        serviceCollection.AddTransient<IClientRepository, ClientRepository>();
        serviceCollection.AddTransient<IFounderRepository, FounderRepository>();
        

        
        
        //валидаторы
        serviceCollection.AddTransient<ClientCreateValidator>();
        serviceCollection.AddTransient<ClientUpdateValidator>();
        serviceCollection.AddTransient<FounderCreateValidator>();
        serviceCollection.AddTransient<FounderUpdateValidator>();

        //юзкейсы
        serviceCollection.AddTransient<ClientUseCase>();
        serviceCollection.AddTransient<FounderUseCase>();

        //сервисы
        serviceCollection.AddTransient<ClientService>();
        serviceCollection.AddTransient<FounderService>();
        
       
        
        return serviceCollection;
    }
}