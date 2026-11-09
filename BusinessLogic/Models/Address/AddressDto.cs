namespace BusinessLogic.Models.Address;

/// <summary>
/// Data transfer object for reading a user's delivery address.
/// </summary>
public record AddressDto(
    ulong Id,
    ulong UserId,
    string? Label,
    string FullName,
    string Phone,
    string Street,
    string City,
    string County,
    string PostalCode,
    bool IsDefault);

/// <summary>
/// Data transfer object for creating or updating a delivery address.
/// </summary>
public record AddressCreateModel(
    string? Label,
    string FullName,
    string Phone,
    string Street,
    string City,
    string County,
    string PostalCode,
    bool IsDefault = false);
