﻿using System;
using Auction.WebApi.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Auction.WebApi.Authorization.Attributes {

	/// <inheritdoc />
	/// <remarks>
	///     Authorization is successful if any of requirements is met
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class AuthorizeExceptAttribute : AuthorizeAttribute {
		
		public AuthorizeExceptAttribute(string policy) : base(Requirement.GetExceptPolicy(policy)) { }
		
	}

}