﻿using System;
using Microsoft.AspNetCore.Authorization;

namespace SpaceAdventures.API.Handlers
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user current user does not have a scope claim => so no permission
            if (!context.User
                    .HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Otherwise we split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');


            // If the scope array contains the required scope => access is granted
            if (scopes.Any(str => str == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}