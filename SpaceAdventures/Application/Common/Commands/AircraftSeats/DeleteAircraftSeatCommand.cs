﻿using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats
{
    public record DeleteAircraftSeatCommand(int Id) : IRequest;

    public class DeleteAircraftSeatCommandHandler : IRequestHandler<DeleteAircraftSeatCommand>
    {
        private readonly IAircraftSeatService _aircraftSeatService;

        public DeleteAircraftSeatCommandHandler(IAircraftSeatService aircraftSeatService)
        {
            _aircraftSeatService = aircraftSeatService;
        }

        public async Task<Unit> Handle(DeleteAircraftSeatCommand request, CancellationToken cancellationToken)
        {
            await _aircraftSeatService.DeleteAircraftSeat(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}