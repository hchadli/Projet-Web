using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;
using SpaceAdventures.Application.Common.Queries.Airports;

namespace SpaceAdventures.Application.Common.Queries.Planets;

public class PlanetDto : IMapFrom<Planet>
{
    public int IdPlanet { get; set; }
    public string Name { get; set; }
    public virtual ICollection<AirportDto> Airports { get; set; } = new List<AirportDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Planet, PlanetDto>().ReverseMap();
        
    }
}