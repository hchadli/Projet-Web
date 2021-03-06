using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Application.Common.Queries.NASA;

public record NasaQuery(string search) : IRequest<NasaCollection>;

public class NasaQueryHandler : IRequestHandler<NasaQuery, NasaCollection>
{
    private readonly INasaApiService _nasaApiService;

    public NasaQueryHandler(INasaApiService nasaApiService)
    {
        _nasaApiService = nasaApiService;
    }

    public async Task<NasaCollection> Handle(NasaQuery request, CancellationToken cancellationToken)
    {
        return await _nasaApiService.GetAlbum(request.search, cancellationToken);
    }
}