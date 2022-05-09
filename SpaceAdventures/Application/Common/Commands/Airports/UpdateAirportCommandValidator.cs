﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports
{
    public class UpdateAirportCommandValidator : AbstractValidator<UpdateAirportCommand>
    {
        public UpdateAirportCommandValidator(IAirportService airportService)       
        {
            RuleFor(c => c.airportInput.Name)
                .NotEmpty().WithMessage("Airport's name is mandatory")
                .MaximumLength(50).WithMessage("Airport's name should not exceed 50 characters");

            RuleFor(n => n.airportInput.IdAirport)
                .Must((data, id) =>
                {
                    bool exists = airportService.AirportExists(id, data.airportInput);
                    return !exists;
                }).WithMessage("This Airport already exists !");

            RuleFor(c => c.airportInput.IdPlanet)
                 .Must((idplanet) =>
                 {
                     bool exists = airportService.PlanetExists(idplanet);
                     return !exists;
                 }).WithMessage("This planet doesn't exists !");
        }
    }
}
