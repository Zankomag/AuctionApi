﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Application.Models;
using Auction.Application.Services.Abstractions.Generic;

namespace Auction.Application.Services.Abstractions {

	public interface IAuctionItemService : ICrudService<AuctionItemModel, AuctionItemInputModel, int> {
		Task<IEnumerable<AuctionItemModel>> GetByNameAsync(string name);
		
		/// <summary>
		/// Get Id of User who is owner of AuctionItem
		///
		/// If AuctionItem doesn't exist - returns 0
		/// </summary>
		/// <param name="id">AuctionItem Id</param>
		Task<int> GetOwnerId(int id);
		
		/// <returns>True if user DOES own auctionItem, otherwise false</returns>
		Task<bool> IsUserOwner(int auctionItemId, int userId);
	}

}