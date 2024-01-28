namespace TestAPI.Domain.Interfaces;

public interface ICommonRepository<T>
    where T : class
{
    public Task Add(T item);
    public Task Update(T item);
    public Task Delete(int id);
    public Task<IReadOnlyList<T>> GetAll();
    public Task<T> Get(int id);
}