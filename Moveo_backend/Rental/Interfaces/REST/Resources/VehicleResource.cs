namespace Moveo_backend.Rental.Interfaces.REST.Resources;

public record VehicleLocationResource(
    string District,
    string Address,
    double? Lat,
    double? Lng
);

public record VehicleResource(
    string Id,
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
    VehicleLocationResource Location,
    string[] Features,
    string[] Restrictions,
    string[] Photos,
    string Status,
    bool IsAvailable
);

public record UpdateVehicleStatusResource(string Status);