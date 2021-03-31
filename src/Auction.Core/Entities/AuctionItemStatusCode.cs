﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Core.Entities {

	public enum AuctionItemStatusCodeId {
		Scheduled = 0,
		Started = 1,
		Finished = 2
	}


	public class AuctionItemStatusCode : Entity<AuctionItemStatusCodeId> {
		[Required]
		[StringLength(30)]
		public string Name { get; set; }


		public ICollection<AuctionItem> AuctionItems { get; set; }
	}

}