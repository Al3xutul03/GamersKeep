namespace Application.Models;

public class CheckoutViewModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public string DeliveryMethod { get; set; } = "Standard"; 
    public decimal ShippingFee => DeliveryMethod == "Express" ? 25.00m : 15.00m;

    public CartViewModel Cart { get; set; } = new();
    public decimal TotalWithShipping => Cart.GrandTotal + ShippingFee;
}

public class OrderConfirmationViewModel
{
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}