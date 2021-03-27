﻿using AuctionAPI.Application.Models;
using AuctionAPI.Core.Entities;
using AutoMapper;

namespace AuctionAPI.Application.Mapping {

	public class EntityToModelProfile : Profile {
		public EntityToModelProfile() => CreateMap<AuctionItemCategory, AuctionItemCategoryModel>()
			.ReverseMap();
	}

}