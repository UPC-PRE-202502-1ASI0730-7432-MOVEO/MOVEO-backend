using Moveo_backend.Rental.Domain.Model.ValueObjects;

namespace Moveo_backend.Rental.Domain.Model.Aggregates;

public class Rental
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public int RenterId { get; private set; }
    public int OwnerId { get; private set; }

    public DateRange RentalPeriod { get; private set; }

    public Money TotalPrice { get; private set; }

    public string Status { get; private set; } = "pending";

    public Location PickupLocation { get; private set; }
    public Location ReturnLocation { get; private set; }

    public string? Notes { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? AcceptedAt { get; private set; }

    private Rental() { }

    public Rental(
        Guid vehicleId,
        int renterId,
        int ownerId,
        DateRange rentalPeriod,
        Money totalPrice,        
        Location pickupLocation,
        Location returnLocation,
        string? notes = null)
    {
        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        RenterId = renterId;
        OwnerId = ownerId;
        RentalPeriod = rentalPeriod;
        TotalPrice = totalPrice;      
        PickupLocation = pickupLocation;
        ReturnLocation = returnLocation;
        Notes = notes;
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
    }

    public void AcceptRental()
    {
        if (Status != "pending")
            throw new InvalidOperationException("Can only accept pending rentals");

        Status = "active";
        AcceptedAt = DateTime.UtcNow;
    }

    public void FinishRental()
    {
        if (Status != "active")
            throw new InvalidOperationException("Can only finish accepted rentals");

        Status = "finished";
    }

    public void CancelRental(string? reason = null)
    {
        if (Status == "finished")
            throw new InvalidOperationException("Cannot cancel finished rentals");

        Status = "canceled";
        Notes = reason ?? Notes;
    }

    public void UpdateTotalPrice(Money newTotal)
    {
        TotalPrice = newTotal;
    }
}
