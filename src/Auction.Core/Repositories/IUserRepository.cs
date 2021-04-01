﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Core.Entities;

namespace Auction.Core.Repositories {

	public interface IUserRepository {
		Task<IEnumerable<User>> GetAllAsync();

		Task<User> GetByIdAsync(int id);

		Task<User> GetByEmailAsync(string email);
		
		/// <returns>User with Id, Role, PasswordHash and PasswordSalt</returns>
		Task<User> GetAuthorizationInfoByEmailAsync(string email);

		Task AddAsync(User user);

		void UpdateRoleAsync(int userId, string role);

		/// <returns>True on success</returns>
		Task<bool> DeleteAsync(int userId);
	}

}