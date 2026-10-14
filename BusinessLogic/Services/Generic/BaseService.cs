using AutoMapper;
using Repository.Enums.Behaviours;
using Repository.Repositories.Generic;

namespace BusinessLogic.Services.Generic;

/// <summary>
/// The implementation of the <see cref="IBaseService{T}"/> interface
/// </summary>
/// <typeparam name="T">The class model for the repository</typeparam>
/// <typeparam name="TReadModel">The read model of the entity</typeparam>
/// <typeparam name="TCreateModel">The create model of the entity</typeparam>
/// <typeparam name="TUpdateModel">The update model of the entity</typeparam>
/// <param name="mapper">The mapper for the models</param>
/// <param name="repository">The main repository the service communicates with</param>
public abstract class BaseService<T, TReadModel, TCreateModel, TUpdateModel>(
    IMapper mapper,
    IBaseRepository<T> repository) : IBaseService<T, TReadModel, TCreateModel, TUpdateModel>
    where T : class
    where TReadModel : class
    where TCreateModel : class
    where TUpdateModel : class
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IBaseRepository<T> _repository = repository;

    /// <inheritdoc />
    public virtual async Task<TReadModel?> GetByIdAsync(ulong id)
    {
        var entity = await _repository.GetByIdAsync(id, IncludeBehaviour.NoInclude);
        return entity != null ? _mapper.Map<TReadModel>(entity) : null;
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TReadModel>> GetAllAsync()
    {
        IEnumerable<T> entities = await _repository.GetAllAsync(IncludeBehaviour.NoInclude);
        return _mapper.Map<IEnumerable<TReadModel>>(entities);
    }

    /// <inheritdoc />
    public virtual async Task CreateAsync(TCreateModel entityCreateModel)
    {
        var entity = _mapper.Map<T>(entityCreateModel);
        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync(TUpdateModel updateModel)
    {

        var entity = _mapper.Map<T>(updateModel);
        _repository.Update(entity);
        await _repository.SaveAsync();
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(ulong id)
    {
        var entity = await _repository.GetByIdAsync(id, IncludeBehaviour.NoInclude)
            ?? throw new Exception("User not found");
        _repository.Delete(entity);
        await _repository.SaveAsync();
    }
}