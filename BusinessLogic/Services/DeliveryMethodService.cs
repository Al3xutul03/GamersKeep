using AutoMapper;
using BusinessLogic.Models.DeliveryMethod;
using BusinessLogic.Services.Abstractions;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Service managing delivery methods available at checkout.
/// </summary>
public class DeliveryMethodService(
    IMapper mapper,
    IDeliveryMethodRepository deliveryMethodRepository) : IDeliveryMethodService
{
    private readonly IMapper _mapper = mapper;
    private readonly IDeliveryMethodRepository _deliveryMethodRepository = deliveryMethodRepository;

    /// <inheritdoc />
    public async Task<IReadOnlyList<DeliveryMethodDto>> GetActiveDeliveryMethodsAsync()
    {
        var entities = await _deliveryMethodRepository.GetActiveAsync();
        return entities.Select(_mapper.Map<DeliveryMethodDto>).ToList();
    }

    /// <inheritdoc />
    public async Task<DeliveryMethodDto?> GetByIdAsync(ulong id)
    {
        var entity = await _deliveryMethodRepository.GetByIdAsync(id, Repository.Enums.Behaviours.IncludeBehaviour.NoInclude);
        return entity != null ? _mapper.Map<DeliveryMethodDto>(entity) : null;
    }
}
