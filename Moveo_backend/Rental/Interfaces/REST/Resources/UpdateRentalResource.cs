namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record UpdateRentalResource(
    Guid Id,
    decimal TotalPrice
);