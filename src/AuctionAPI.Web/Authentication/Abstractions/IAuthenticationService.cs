﻿using System.Threading.Tasks;

namespace AuctionAPI.Web.Authentication.Abstractions {

	public interface IAuthenticationService {
		/// <returns>JWT Token on success or null if email or password is wrong</returns>
		Task<TokenModel> GetToken(string email, string password);
	}

}