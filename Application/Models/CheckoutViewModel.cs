using System.ComponentModel.DataAnnotations;
using BusinessLogic.Models.Address;
using BusinessLogic.Models.DeliveryMethod;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Application.Models;

public class CheckoutViewModel : IValidatableObject
{
    public ulong SelectedAddressId { get; set; }

    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [StringLength(250)]
    public string Street { get; set; } = string.Empty;

    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [StringLength(100)]
    public string County { get; set; } = string.Empty;

    [StringLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    public ulong DeliveryMethodId { get; set; }

    [BindNever, ValidateNever]
    public IReadOnlyList<AddressDto> SavedAddresses { get; set; } = [];

    [BindNever, ValidateNever]
    public IReadOnlyList<DeliveryMethodDto> DeliveryMethods { get; set; } = [];

    [BindNever, ValidateNever]
    public CartViewModel Cart { get; set; } = new();

    public decimal ShippingFee => DeliveryMethods.FirstOrDefault(d => d.Id == DeliveryMethodId)?.Price ?? 0m;
    public decimal TotalWithShipping => Cart.GrandTotal + ShippingFee;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SelectedAddressId == 0)
        {
            if (string.IsNullOrWhiteSpace(FullName))
                yield return new ValidationResult("Introdu numele complet.", new[] { nameof(FullName) });
            if (string.IsNullOrWhiteSpace(Phone))
                yield return new ValidationResult("Introdu numărul de telefon.", new[] { nameof(Phone) });
            if (string.IsNullOrWhiteSpace(Street))
                yield return new ValidationResult("Introdu adresa.", new[] { nameof(Street) });
            if (string.IsNullOrWhiteSpace(City))
                yield return new ValidationResult("Introdu orașul.", new[] { nameof(City) });
            if (string.IsNullOrWhiteSpace(County))
                yield return new ValidationResult("Introdu județul.", new[] { nameof(County) });
            if (string.IsNullOrWhiteSpace(PostalCode))
                yield return new ValidationResult("Introdu codul poștal.", new[] { nameof(PostalCode) });
        }

        if (DeliveryMethodId == 0)
            yield return new ValidationResult("Alege o metodă de livrare.", new[] { nameof(DeliveryMethodId) });
    }
}

public class OrderConfirmationItemViewModel
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}

public class OrderConfirmationViewModel
{
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string DeliveryMethodName { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal DeliveryCost { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }
    public List<OrderConfirmationItemViewModel> Items { get; set; } = new();
}