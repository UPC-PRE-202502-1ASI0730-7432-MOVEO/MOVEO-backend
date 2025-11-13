namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record CreateVehicleResource(
    Guid OwnerId,
    string Brand,
    string Model,
    int Year,
    string Color,
    string Transmission,
    string FuelType,
    int Seats,
    decimal DailyPrice,
    decimal DepositAmount,
    string Location,
    string[] Features,
    string[] Restrictions,
    string[] Photos
);