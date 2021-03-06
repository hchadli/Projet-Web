using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Flights;

public record DeleteFlightCommand(int Id) : IRequest;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand>
{
    private readonly IFlightService _flightService;

    public DeleteFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        await _flightService.DeleteFlight(request.Id, cancellationToken);
        return Unit.Value;
    }
}