using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class ItineraryService : IItineraryService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPlanetService _planetService;

    public ItineraryService(ISpaceAdventureDbContext context, IMapper mapper, IPlanetService planetService)
    {
        _context = context;
        _mapper = mapper;
        _planetService = planetService;
    }


    public bool ItineraryExists(ItineraryInput itineraryInput)
    {
        return _context.Itineraries.Any(c =>
            c.IdAirport1 == itineraryInput.IdAirport1 && c.IdAirport2 == itineraryInput.IdAirport2);
    }

    public async Task<ItineraryVm> GetAllItineraries(CancellationToken cancellationToken)
    {
        return new ItineraryVm
        {
            ItinerariesList = await _context.Itineraries
                .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<ItineraryDto> GetItineraryById(int itineraryId, CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);
        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);

        return _mapper.Map<ItineraryDto>(itinerary);
    }

    public async Task<ItineraryVm> GetItinerariesByAirportsOfPlanet(AirportVm lstAirport, CancellationToken cancellationToken = default)
    {

        List<int> airportsId = lstAirport.AirportsList.Select(a => a.IdAirport).ToList();

        return new ItineraryVm
        {
            ItinerariesList = await _context.Itineraries.Where(i => airportsId.Contains(i.IdAirport2))
                .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

    }


    

    #region Not used
    public async Task<ItineraryDto> CreateItinerary(ItineraryInput itineraryInput,
        CancellationToken cancellation = default)
    {
        var itinerary = _mapper.Map<Itinerary>(itineraryInput);

        try
        {
            await _context.Itineraries.AddAsync(itinerary, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<ItineraryDto>(itinerary);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<ItineraryDto> UpdateItinerary(int itineraryId, ItineraryInput itineraryInput,
        CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);

        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);

        try
        {

            itinerary.IdAirport1 = itineraryInput.IdAirport1;
            itinerary.IdAirport2 = itineraryInput.IdAirport2;

            _context.Itineraries.Update(itinerary);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<ItineraryDto>(itinerary);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteItinerary(int itineraryId, CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);

        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);
        _context.Itineraries.Remove(itinerary);
        await _context.SaveChangesAsync(cancellation);
    }
    #endregion

}
