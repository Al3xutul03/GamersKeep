namespace Application.Models.Admin;

public record AdminOrderRow(string Number, string Customer, DateTime Date, decimal Total, string Status);

public record AdminDashboardViewModel(
    int OrdersToday,
    decimal Revenue,
    int AwaitingShipment,
    int LowStock,
    IReadOnlyList<AdminOrderRow> Recent);

public record AdminOrdersViewModel(IReadOnlyList<AdminOrderRow> Orders, string? ActiveStatus);