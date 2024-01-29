using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace TestAPI.ViewModels;

public class ClientCreateInfo
{
    public string TaxpayerNumber { get; set; }

    public string Name { get; set; }
}
