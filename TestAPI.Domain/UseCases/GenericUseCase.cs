using TestAPI.Domain.Interfaces;

namespace TestAPI.Domain.UseCases;

public class GenericUseCase<T>
    where T : class, IBaseModel
{
    private readonly IGenericRepository<T> _genericRepository;

    protected GenericUseCase(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task Add(T item) =>
        await _genericRepository.Add(item);

    public async Task<IReadOnlyList<T>> GetAll(int pageNumber, int pageSize) =>
        await _genericRepository.GetAll(pageNumber, pageSize);

    public async Task<T?> Get(int id) =>
        await _genericRepository.Get(id);
}