using AutoMapper;
using AutoMapper.QueryableExtensions;
using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Bookings;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Bookings;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Mappings;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class BookingService : IBookingService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public BookingService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookingsVm> GetAllBookings(CancellationToken cancellationToken)
    {
        return new BookingsVm
        {
            BookingsList = await _context.Bookings
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<BookingsVm> GetBookingsByClient(int clientId, CancellationToken cancellationToken)
    {
        return new BookingsVm
        {
            BookingsList = await _context.Bookings
                .Where(b => b.IdClient == clientId)
               .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken)
        };

    }

    public async  Task<PaginatedList<BookingDto>> GetBookingsWithPagination(int pageNumber, int pageSize, CancellationToken cancellation = default)
    {
        var paginatedList = await _context.Bookings
            .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
        return paginatedList;
    }

    public async Task<BookingDto> GetBookingById(int bookingId, CancellationToken cancellation = default)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null) throw new NotFoundException("Booking", bookingId);

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CreateBooking(BookingInput bookingInput, CancellationToken cancellation = default)
    {
        var booking = _mapper.Map<Booking>(bookingInput);

        try
        {
            await _context.Bookings.AddAsync(booking, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<BookingDto>(booking);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<BookingDto> UpdateBooking(int bookingId, BookingInput bookingInput,
        CancellationToken cancellation = default)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);

        if (booking == null) throw new NotFoundException("Booking", bookingId);

        try
        {
            booking.IdBooking = bookingInput.IdBooking;
            booking.IdFlight = bookingInput.IdFlight;
            booking.IdClient = bookingInput.IdClient;
            booking.NbSeats = bookingInput.NbSeats;


            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<BookingDto>(booking);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteBooking(int BookingId, CancellationToken cancellation = default)
    {
        var Booking = await _context.Bookings.FindAsync(BookingId);

        if (Booking == null) throw new NotFoundException("Booking", BookingId);
        _context.Bookings.Remove(Booking);
        await _context.SaveChangesAsync(cancellation);
    }

    public bool FlightExists(int id)
    {
        return _context.Flights.Any(f => f.IdFlight == id);
    }

    public bool ClientExists(int id)
    {
        return _context.Clients.Any(c => c.IdClient == id);
    }
}