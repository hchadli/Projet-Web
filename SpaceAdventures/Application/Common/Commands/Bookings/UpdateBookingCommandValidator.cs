﻿using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Bookings
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {

        public UpdateBookingCommandValidator(IBookingService bookingService)
        {

            RuleFor(b => b.bookingInput.BookingAmount).NotEmpty().WithMessage("Booking amount is mandatory")
                .InclusiveBetween(1, 1000000).WithMessage("Booking amount should be between 1 and 1000000");

            // Control existing IdFlight
            // Control existing IdClient

        }
    }
}
