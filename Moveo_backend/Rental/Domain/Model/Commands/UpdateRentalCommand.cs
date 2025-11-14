using Moveo_backend.Rental.Domain.Model.ValueObjects;

namespace Moveo_backend.Rental.Domain.Model.Commands;

public record UpdateRentalCommand(
    Guid Id,
    Money TotalPrice);