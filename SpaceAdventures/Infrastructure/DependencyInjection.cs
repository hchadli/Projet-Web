﻿using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Infrastructure.Persistence.Services;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SpaceAdventuresDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(SpaceAdventuresDbContext).Assembly.FullName)));

        services.AddScoped<ISpaceAdventureDbContext,SpaceAdventuresDbContext>();

        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IPlanetService, PlanetService>();
        services.AddScoped<IAirportService, AirportService>();
        services.AddScoped<IItineraryService, ItineraryService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IAircraftService, AircraftService>();
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAircraftSeatService, AircraftSeatService>();
        services.AddHttpClient<IUsersManagementApiService, UsersManagementApiService>();

        // Policy Service
        /* services.AddSingleton(new ClientPolicy());
         services.AddHttpClient("RetryPolicy").AddPolicyHandler(request => new ClientPolicy().ExponentialHttpRetry);*/

        services.AddHttpClient<IISSCLService, ISSCLService>();
        services.AddHttpClient<INasaApiService, NasaApiService>();


        return services;
    }
}