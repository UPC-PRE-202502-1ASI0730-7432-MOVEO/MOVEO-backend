using Moveo_backend.Rental.Domain.Model.Aggregates;
using Moveo_backend.Rental.Domain.Model.Commands;
using Moveo_backend.Rental.Domain.Services;

namespace Moveo_backend.Rental.Application.CommandServices;

public class RentalCommandService
{
    private readonly IRentalService _rentalService;

    public RentalCommandService(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public Task<Domain.Model.Aggregates.Rental> Handle(CreateRentalCommand command)
        => _rentalService.CreateRentalAsync(command);

    public Task<Domain.Model.Aggregates.Rental?> Handle(UpdateRentalCommand command)
        => _rentalService.UpdateRentalAsync(command);

    public Task<bool> Handle(CancelRentalCommand command)
        => _rentalService.CancelRentalAsync(command);

    public Task<bool> Handle(FinishRentalCommand command)
        => _rentalService.FinishRentalAsync(command);
}