﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable InheritdocConsiderUsage

namespace Auction.WebApi.Authorization.Requirements {

	public static partial class Requirement {

		/// <summary>
		///     Fails if user is not authenticated
		/// </summary>
		public class IsAuthenticated : IAuthorizationRequirement {

			//If IAuthorizationRequirement is combined with AuthorizationHandler
			//then somehow HandleRequirementAsync is called twice, so we need to separate them
			public class Handler : AuthorizationHandler<IsAuthenticated> {
				/// <inheritdoc />
				protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
					IsAuthenticated requirement) {
					if(context.User.Identity?.IsAuthenticated == true) {
						context.Succeed(requirement);
					} else {
						context.Fail();
					}
					return Task.CompletedTask;
				}
			}
		}
	}

}