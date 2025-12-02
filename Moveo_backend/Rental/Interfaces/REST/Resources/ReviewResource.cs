namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record ReviewResource(
    int Id,
    Guid RentalId,
    Guid ReviewerId,
    Guid RevieweeId,
    int Rating,
    string Comment,
    string Type,
    DateTime CreatedAt);
