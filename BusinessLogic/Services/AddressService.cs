using AutoMapper;
using BusinessLogic.Models.Address;
using BusinessLogic.Services.Abstractions;
using Repository.Entity;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Service managing users' saved delivery addresses.
/// </summary>
public class AddressService(
    IMapper mapper,
    IAddressRepository addressRepository) : IAddressService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressRepository _addressRepository = addressRepository;

    /// <inheritdoc />
    public async Task<IReadOnlyList<AddressDto>> GetUserAddressesAsync(ulong userId)
    {
        var entities = await _addressRepository.GetByUserIdAsync(userId);
        return entities.Select(_mapper.Map<AddressDto>).ToList();
    }

    /// <inheritdoc />
    public async Task<AddressDto?> GetUserAddressByIdAsync(ulong userId, ulong addressId)
    {
        var entity = await _addressRepository.GetUserAddressAsync(userId, addressId);
        return entity != null ? _mapper.Map<AddressDto>(entity) : null;
    }

    /// <inheritdoc />
    public async Task<AddressDto> CreateAddressAsync(ulong userId, AddressCreateModel model)
    {
        var existing = (await _addressRepository.GetByUserIdAsync(userId)).ToList();
        var isFirst = existing.Count == 0;
        var shouldBeDefault = model.IsDefault || isFirst;

        var now = DateTime.UtcNow;

        if (shouldBeDefault && existing.Count > 0)
        {
            foreach (var addr in existing.Where(a => a.IsDefault))
            {
                addr.IsDefault = false;
                addr.UpdatedAt = now;
                _addressRepository.Update(addr);
            }
        }

        var entity = new Address
        {
            UserId = userId,
            Label = model.Label?.Trim(),
            FullName = model.FullName.Trim(),
            Phone = model.Phone.Trim(),
            Street = model.Street.Trim(),
            City = model.City.Trim(),
            County = model.County.Trim(),
            PostalCode = model.PostalCode.Trim(),
            IsDefault = shouldBeDefault,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _addressRepository.AddAsync(entity);
        await _addressRepository.SaveAsync();

        return _mapper.Map<AddressDto>(entity);
    }

    /// <inheritdoc />
    public async Task<AddressDto?> UpdateAddressAsync(ulong userId, ulong addressId, AddressCreateModel model)
    {
        var entity = await _addressRepository.GetUserAddressAsync(userId, addressId);
        if (entity == null)
        {
            return null;
        }

        var now = DateTime.UtcNow;

        if (model.IsDefault && !entity.IsDefault)
        {
            var otherAddresses = await _addressRepository.GetByUserIdAsync(userId);
            foreach (var other in otherAddresses.Where(a => a.Id != addressId && a.IsDefault))
            {
                other.IsDefault = false;
                other.UpdatedAt = now;
                _addressRepository.Update(other);
            }
        }

        entity.Label = model.Label?.Trim();
        entity.FullName = model.FullName.Trim();
        entity.Phone = model.Phone.Trim();
        entity.Street = model.Street.Trim();
        entity.City = model.City.Trim();
        entity.County = model.County.Trim();
        entity.PostalCode = model.PostalCode.Trim();
        entity.IsDefault = model.IsDefault;
        entity.UpdatedAt = now;

        _addressRepository.Update(entity);
        await _addressRepository.SaveAsync();

        return _mapper.Map<AddressDto>(entity);
    }

    /// <inheritdoc />
    public async Task<bool> SetDefaultAddressAsync(ulong userId, ulong addressId)
    {
        var addresses = (await _addressRepository.GetByUserIdAsync(userId)).ToList();
        var target = addresses.FirstOrDefault(a => a.Id == addressId);
        if (target == null)
        {
            return false;
        }

        var now = DateTime.UtcNow;
        foreach (var addr in addresses)
        {
            addr.IsDefault = addr.Id == addressId;
            addr.UpdatedAt = now;
            _addressRepository.Update(addr);
        }

        await _addressRepository.SaveAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAddressAsync(ulong userId, ulong addressId)
    {
        var entity = await _addressRepository.GetUserAddressAsync(userId, addressId);
        if (entity == null)
        {
            return false;
        }

        entity.DeletedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _addressRepository.Update(entity);
        await _addressRepository.SaveAsync();
        return true;
    }
}
