namespace Moveo_backend.Rental.Interfaces.REST.Resources;

// DTO para la ubicación del vehículo
public record LocationResource(
    string District,
    string Address,
    double? Lat,
    double? Lng
);

public record CreateVehicleResource(
    int OwnerId,
    string Brand,
    string Model,
    int Year,
    string Color,
    string Transmission,
    string FuelType,
    int Seats,
    decimal DailyPrice,
    decimal DepositAmount,
    LocationResource Location,
    string? LicensePlate,
    string? Status,
    string? Description,
    string[]? Images,
    string[]? Features,
    string[]? Restrictions
)
{
    // Propiedades con valores por defecto para compatibilidad
    public string[] Photos => Images ?? Array.Empty<string>();
};