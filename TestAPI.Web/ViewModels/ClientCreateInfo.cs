﻿using System.ComponentModel.DataAnnotations;

namespace TestAPI.ViewModels;

public class ClientCreateInfo
{
    [Required]
    [RegularExpression(@"\b\d{10}\b|\b\d{12}\b", ErrorMessage = "ИНН введен некорректно")]
    public string TaxpayerNumber { get; set; }

    [Required]
    [MaxLength(255)]
    [MinLength(1)]
    public string Name { get; set; }
}