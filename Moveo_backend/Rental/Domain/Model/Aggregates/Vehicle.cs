using Moveo_backend.Rental.Domain.Model.ValueObjects;
using System.Text.Json;

namespace Moveo_backend.Rental.Domain.Model.Aggregates;

public class Vehicle
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }

    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string Color { get; private set; }

    public string Transmission { get; private set; }
    public string FuelType { get; private set; }
    public int Seats { get; private set; }

    // Usar tipos complejos como Owned Types
    public Money DailyPrice { get; private set; }
    public Money DepositAmount { get; private set; }

    public Location Location { get; private set; }

    // Para EF Core mapeamos listas como JSON
    public string FeaturesJson { get; private set; } = "[]";
    public string RestrictionsJson { get; private set; } = "[]";
    public string PhotosJson { get; private set; } = "[]";

    public string Status { get; private set; } = "available";
    public bool IsAvailable { get; set; } = true;

    // Propiedades para acceder a las listas como objetos
    public List<string> Features
    {
        get => JsonSerializer.Deserialize<List<string>>(FeaturesJson) ?? new List<string>();
        private set => FeaturesJson = JsonSerializer.Serialize(value);
    }

    public List<string> Restrictions
    {
        get => JsonSerializer.Deserialize<List<string>>(RestrictionsJson) ?? new List<string>();
        private set => RestrictionsJson = JsonSerializer.Serialize(value);
    }

    public List<string> Photos
    {
        get => JsonSerializer.Deserialize<List<string>>(PhotosJson) ?? new List<string>();
        private set => PhotosJson = JsonSerializer.Serialize(value);
    }

    // Constructor principal
    public Vehicle(
        Guid ownerId,
        string brand,
        string model,
        int year,
        string color,
        string transmission,
        string fuelType,
        int seats,
        Money dailyPrice,
        Money depositAmount,
        Location location,
        IEnumerable<string>? features = null,
        IEnumerable<string>? restrictions = null,
        IEnumerable<string>? photos = null)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Transmission = transmission;
        FuelType = fuelType;
        Seats = seats;
        DailyPrice = dailyPrice;
        DepositAmount = depositAmount;
        Location = location;
        Features = features?.ToList() ?? new List<string>();
        Restrictions = restrictions?.ToList() ?? new List<string>();
        Photos = photos?.ToList() ?? new List<string>();
    }

    // ⚠ Constructor vacío necesario para EF Core
    protected Vehicle() { }

    public void UpdateDetails(
        string brand,
        string model,
        int year,
        string color,
        string transmission,
        string fuelType,
        int seats,
        Money dailyPrice,
        Money depositAmount,
        Location location,
        IEnumerable<string>? features,
        IEnumerable<string>? restrictions,
        IEnumerable<string>? photos)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Transmission = transmission;
        FuelType = fuelType;
        Seats = seats;
        DailyPrice = dailyPrice;
        DepositAmount = depositAmount;
        Location = location;
        Features = features?.ToList() ?? new List<string>();
        Restrictions = restrictions?.ToList() ?? new List<string>();
        Photos = photos?.ToList() ?? new List<string>();
    }

    public void ChangeStatus(string status)
    {
        Status = status;
    }
}
