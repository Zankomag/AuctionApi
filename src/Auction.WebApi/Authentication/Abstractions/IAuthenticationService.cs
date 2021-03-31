﻿using System.Threading.Tasks;
using Auction.WebApi.Authentication.Models;

namespace Auction.WebApi.Authentication.Abstractions {

	public interface IAuthenticationService {
		/// <returns>JWT Token on success or null if email or password is wrong</returns>
		Task<TokenModel> GetToken(string email, string password);
	}

}