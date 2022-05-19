﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Flights;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.ISSCL
{
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
            return await _nasaApiService.GetAlbum(request.search,cancellationToken);
        }
    }
}

