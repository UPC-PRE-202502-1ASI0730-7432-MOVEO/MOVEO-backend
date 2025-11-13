using Moveo_backend.Rental.Domain.Model.ValueObjects;

namespace Moveo_backend.Rental.Domain.Model.Commands;

public record UpdateVehicleCommand(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    string Color,
    string Transmission,
    string FuelType,
    int Seats,
    Money DailyPrice,
    Money DepositAmount,
    Location Location,
    List<string> Features,
    List<string> Restrictions,
    List<string> Photos);