﻿using System.Linq;
using Auction.Application.Models;
using Auction.Core.Entities;
using AutoMapper;

namespace Auction.Application.Mapping {

	public class EntityToModelProfile : Profile {
		public EntityToModelProfile() {
			CreateMap<AuctionItemCategoryInputModel, AuctionItemCategory>();
			CreateMap<AuctionItemCategory, AuctionItemCategoryDetailedModel>();

			CreateMap<UserInputModel, User>();
			CreateMap<User, UserModel>();
			CreateMap<User, UserDetailedModel>()
				.ForMember(x => x.Roles, c => c.MapFrom(x => x.Roles.Select(x => x.Name)));

			CreateMap<BidInputModel, Bid>();
			CreateMap<Bid, BidModel>()
				.PreserveReferences();

			CreateMap<AuctionItemInputModel, AuctionItem>();
			CreateMap<AuctionItem, AuctionItemModel>()
				.PreserveReferences();
		}
	}

}