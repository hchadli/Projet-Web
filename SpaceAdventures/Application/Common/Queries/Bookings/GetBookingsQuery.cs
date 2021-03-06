using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Bookings;

// Query
public record GetBookingsQuery : IRequest<BookingsVm>
{
}

// Handler
public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, BookingsVm>
{
    private readonly IBookingService _bookingService;

    public GetBookingsQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingsVm> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetAllBookings(cancellationToken);
    }
}