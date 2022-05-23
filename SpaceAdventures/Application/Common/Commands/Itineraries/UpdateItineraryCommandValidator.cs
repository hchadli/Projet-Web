﻿using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public class UpdateItineraryCommandValidator : AbstractValidator<UpdateItineraryCommand>
{
    public UpdateItineraryCommandValidator(IItineraryService itineraryService)
    {
        RuleFor(c => c.itineraryInput.Rate)
            .NotNull().WithMessage("Rate is mandatory")
            .GreaterThan(0).WithMessage("Rate should be greater than 0")
            .LessThan(100000000000).WithMessage("Rate should be less then 100 000 000 000");

        RuleFor(c => c.itineraryInput.IdAirport1)
            .NotNull().WithMessage("The starting Airport is mandatory");

        RuleFor(c => c.itineraryInput.IdAirport2)
            .NotNull().WithMessage("The destination Airport is mandatory");

        RuleFor(c => c.itineraryInput)
            .Must(data =>
            {
                var exists = itineraryService.ItineraryExists(data);
                return !exists;
            }).WithMessage("This itinerary already exist !");
    }
}