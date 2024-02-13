namespace TestAPI.Domain.Interfaces;

public interface IGenericRepository<T>
    where T : class, IBaseModel
{
    public Task Add(T item);

    public Task<IReadOnlyList<T>> GetAll(int pageNumber, int pageSize);

    public Task<T?> Get(int id);
}