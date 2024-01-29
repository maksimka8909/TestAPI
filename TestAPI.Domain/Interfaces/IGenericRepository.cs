using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IGenericRepository<T>
    where T :  class, IBaseModel
{
    public Task Add(T item);
    public void Update(T item);
    public Task Delete(int id);
    public Task<IReadOnlyList<T>> GetAll();
    public Task<T> Get(int id);
}