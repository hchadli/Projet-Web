﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Membership
{

    // Query
    public class GetMembershipQuery : IRequest<MembershipVm>
    {
    }

    // Handler
    public class GetMembershipQueryHandler : IRequestHandler<GetMembershipQuery, MembershipVm>
    {
        private readonly IMembershipService _MembershipService;

        public GetMembershipQueryHandler(IMembershipService MembershipService)
        {
            _MembershipService = MembershipService;
        }

        public async Task<MembershipVm> Handle(GetMembershipQuery request, CancellationToken cancellationToken)
        {
            return await _MembershipService.GetAllMemberships(cancellationToken);
        }
    }
}
