﻿using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class FounderRepository : GenericRepository<Founder>, IFounderRepository
{
    public FounderRepository(DatabaseContext database) : base(database)
    {
    }

    public async Task<Founder> GetUserByTaxpayerNumber(string number) =>
        await _dbSet.Where(f => f.TaxpayerNumber == number).FirstOrDefaultAsync();
}