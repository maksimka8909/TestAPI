using TestAPI.Domain.Interfaces;

namespace TestAPI.Domain.UseCases;

public class GenericUseCase<T>
    where T : class, IBaseModel
{
    private readonly IGenericRepository<T> _genericRepository;

    public GenericUseCase(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task Add(T item) =>
        await _genericRepository.Add(item);

    public async Task<IReadOnlyList<T>> GetAll() =>
        await _genericRepository.GetAll();

    public async Task<T?> Get(int id) =>
        await _genericRepository.Get(id);

}