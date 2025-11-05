//using E_Commerce.Web.Factories;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//namespace E_Commerce.Web.Extensions
//{
//	public static class ServicesRegistration
//	{
//		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
//		{
//			services.AddEndpointsApiExplorer();
//			services.AddSwaggerGen(Options =>
//			{
//				Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//				{
//					In = ParameterLocation.Header,
//					Description = "Enter 'Bearer' followed by Space and your token.",
//					Name = "Authorization",
//					Type = SecuritySchemeType.ApiKey,
//					Scheme = "Bearer"
//				});

//				Options.AddSecurityRequirement(new OpenApiSecurityRequirement
//				{
//					{
//						new OpenApiSecurityScheme
//						{
//							Reference = new OpenApiReference
//							{ 
//								Type =ReferenceType.SecurityScheme,
//								Id = "Bearer"
//							}
//						},
//						new string[] {} // Scopes are used for OAuth2
//					}
//				});

//			});

//			return services;
//		}

//		public static IServiceCollection AddWebApplicationServices(this IServiceCollection services , IConfiguration Configuration)
//		{
//			services.Configure<ApiBehaviorOptions>(Options =>
//			{
//				Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateAPiValidationResponse;
//			});
//			return ConfigureJWT(services, Configuration);
//		}

//		private static IServiceCollection ConfigureJWT(IServiceCollection services, IConfiguration Configuration)
//		{
//			services.AddAuthentication(Config =>
//			{
//				Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//				Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//			})
//							.AddJwtBearer(options =>
//							{
//								options.SaveToken = true; // Saves the JWT in the HttpContext for later use (e.g., GetTokenAsync)
//														  // await HttpContext.GetTokenAsync("access_token");
//								options.TokenValidationParameters = new TokenValidationParameters
//								{
//									ValidateIssuer = true,
//									ValidateAudience = true,
//									ValidateLifetime = true,
//									ValidIssuer = Configuration["Jwt:Issuer"],
//									ValidAudience = Configuration["Jwt:Audience"],
//									IssuerSigningKey = new SymmetricSecurityKey(
//										Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
//								};
//							}); return services;
//		}
//	}
//}
