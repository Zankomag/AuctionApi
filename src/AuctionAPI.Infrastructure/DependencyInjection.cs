﻿using AuctionAPI.Application.Mapping;
using AuctionAPI.Application.Services;
using AuctionAPI.Application.Services.Abstractions;
using AuctionAPI.Core.Repositories;
using AuctionAPI.Infrastructure.Data;
using AuctionAPI.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionAPI.Infrastructure {

	public static class DependencyInjection {
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config,
			Profile webApiMappingProfile) {
			string connection = config.GetConnectionString("AuctionDb");
			services.AddDbContext<AuctionDbContext>(options => options.UseSqlServer(connection));

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<IAuctionItemCategoryService, AuctionItemCategoryService>();
			services.AddScoped<IUserService, UserService>();

			MapperConfiguration mapperConfig = new MapperConfiguration(x => {
				x.AddProfile<EntityToModelProfile>();
				x.AddProfile(webApiMappingProfile);
			});

			services.AddSingleton(mapperConfig.CreateMapper());

			return services;
		}
	}

}