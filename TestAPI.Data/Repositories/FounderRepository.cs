using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class FounderRepository : IFounderRepository
{
    private readonly ApiContext _db;

    public FounderRepository(ApiContext db)
    {
        _db = db;
    }

    public async Task Add(Founder item)
    {
        await _db.Founders.AddAsync(item);
    }

    public async Task Update(Founder item)
    {
        item.UpdatedAt = DateTime.Now;
    }

    public async Task Delete(int id)
    {
        var founder = await _db.Founders.FindAsync(id);
        founder.DeletedAt = DateTime.Now;
    }

    public async Task<IReadOnlyList<Founder>> GetAll()=>
        await _db.Founders.Where(c => c.DeletedAt == null).ToListAsync();

    public async Task<Founder> Get(int id)=>
        await _db.Founders.Where(f => f.DeletedAt == null && f.Id == id).FirstOrDefaultAsync();

    public async Task<Founder> GetUserByTaxpayerNumber(string number) =>
        await _db.Founders.Where(f => f.Inn == number).FirstOrDefaultAsync();

}