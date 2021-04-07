﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Core.Entities;
using Auction.Core.Repositories;
using Auction.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repositories {

	public class AuctionItemRepository : CrudRepository<AuctionItem, int>, IAuctionItemRepository {
		/// <inheritdoc />
		public AuctionItemRepository(DbContext context) : base(context) { }

		/// <inheritdoc />
		public async Task<IEnumerable<AuctionItem>> GetAllWithDetailsAsync()
			=> await GetAllWithDetails().ToListAsync();

		/// <inheritdoc />
		public async Task<AuctionItem> GetByIdWithDetailsAsync(int id) 
			=> await GetAllWithDetails().FirstOrDefaultAsync(x => x.Id == id);

		/// <inheritdoc />
		public void UpdateActualClosingDate(AuctionItem auctionItem) 
			=> Context.Entry(auctionItem).Property(x => x.ActualCloseDate).IsModified = true;

		/// <inheritdoc />
		public void UpdateWinningBidId(AuctionItem auctionItem)
			=> Context.Entry(auctionItem).Property(x => x.WinningBidId).IsModified = true;

		/// <inheritdoc />
		public async Task AddImageAsync(int auctionItemId, byte[] image) {
			AuctionItemImage auctionItemImage = new AuctionItemImage() {
				AuctionItemId = auctionItemId,
				Image = image
			};
			await Context.Set<AuctionItemImage>().AddAsync(auctionItemImage);
		}

		public IQueryable<AuctionItem> GetAllWithDetails()
			=> GetAll()
				.Include(x => x.Images)
				.Include(x => x.WinningBid).ThenInclude(x => x.Bidder)
				.Include(x => x.Bids).ThenInclude(x => x.Bidder)
				.Include(x => x.Seller)
				.Include(x => x.AuctionItemCategory);

		/// <inheritdoc />
		public override Task<bool> DeleteByIdAsync(int id) {
			DbSet.Remove(new AuctionItem() {Id = id});
			return Task.FromResult(true);
		}
	}

}