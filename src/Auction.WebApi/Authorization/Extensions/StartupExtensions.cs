﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Auction.WebApi.Authorization.Abstractions;
using Auction.WebApi.Authorization.Constants;
using Auction.WebApi.Authorization.Requirements;
using Auction.WebApi.Authorization.Requirements.Handlers;
using Auction.WebApi.Authorization.Services;
using Auction.WebApi.Authorization.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auction.WebApi.Authorization.Extensions {

	public static class StartupExtensions {

		public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,
			IConfiguration configuration) {

			services.AddAuthentication(configuration);
			services.AddAuthorization();
			return services;
		}

		private static void AddAuthentication(this IServiceCollection services,
			IConfiguration configuration) {

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			var jwtSettingsConfigSection = configuration.GetSection(nameof(JwtSettings));
			services.Configure<JwtSettings>(jwtSettingsConfigSection);


			//This disables mapping normal claim names to urls microsoft uses
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services.AddAuthentication(options => {
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Secret)}"])),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(5),
					NameClaimType = JwtOpenIdProperty.Username,
					RoleClaimType = JwtOpenIdProperty.Role
				});
		}

		private static void AddAuthorization(this IServiceCollection services) {
			services.AddAuthorization(options => {
				//Override default 'DenyAnonymousAuthorizationRequirement' with similar policy
				//that fails context if user is not authenticated
				options.DefaultPolicy = new AuthorizationPolicyBuilder()
					.AddRequirements(new AuthenticationRequirement())
					.Build();
				options.AddPolicy(Requirement.Admin,
					policy => policy.AddRequirements(new AdminRequirement()));
				options.AddPolicy(Requirement.OwnerOfAuctionItemId,
					policy => policy.AddRequirements(new OwnerOfAuctionItemIdRequirement()));
				options.AddPolicy(Requirement.GetOrCombinedPolicy(Requirement.Admin, Requirement.OwnerOfUserId),
					policy => policy.AddRequirements(new AdminOrOwnerOfUserIdRequirement()));
				options.AddPolicy(Requirement.GetOrCombinedPolicy(Requirement.Admin, Requirement.OwnerOfAuctionItemId),
					policy => policy.AddRequirements(new AdminOrOwnerOfAuctionItemIdRequirement()));
				options.AddPolicy(Requirement.GetExceptPolicy(Requirement.OwnerOfAuctionItemId),
					policy => policy.AddRequirements(new ExceptOwnerOfAuctionItemIdRequirement()));
				options.AddPolicy(Requirement.GetOrCombinedPolicy(Requirement.Admin, Requirement.OwnerOfAuctionItemImageId),
					policy => policy.AddRequirements(new AdminOrOwnerOfAuctionItemImageIdRequirement()));
			});

			services.AddScoped<IRequestData, RequestData>();

			//Order of handlers is important - it determines their execution order in request pipeline
			//These services are scoped because they use scoped IRequestData, otherwise they'd be singletons
			services.AddScoped<IAuthorizationHandler, AuthenticationRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, AdminRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, OwnerOfUserIdRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, OwnerOfAuctionItemIdRequirementHandler>();

			//NotRequirement handler must be registered last
			services.AddScoped<IAuthorizationHandler, ExceptRequirementHandler>();

			services.AddHttpContextAccessor();
		}

		public static IApplicationBuilder UseAuthenticationAndAuthorization(this IApplicationBuilder app) {

			app.UseAuthentication();
			app.UseMiddleware<AuthenticationMiddleware>();
			app.UseAuthorization();
			return app;
		}
	}

}