﻿using Microsoft.AspNetCore.Authorization;

// ReSharper disable InheritdocConsiderUsage

namespace Auction.WebApi.Authorization.Requirements {

	public static partial class Requirement {
		public abstract partial class IsAdminOrOwnerOf {
			/// <summary>
			///     Policy requirement that authorizes only Admins or Users that do own User 'id' parameter of request (self Id owners)
			/// </summary>
			public partial class User : IAuthorizationRequirement {
				public const string Policy = IsAdminOrOwnerOf.policy + nameof(AuctionItem);
			}
		}
	}

}