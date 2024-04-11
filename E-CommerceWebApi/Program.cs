
using AspNetCoreRateLimit;
using E_CommerceWebApi.Data;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using E_CommerceWebApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Principal;
using System.Text;
using System.Threading.RateLimiting;

namespace E_CommerceWebApi
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
			});
			builder.Services.AddSwaggerGen(swagger =>
			{
				//This is to generate the Default UI of Swagger Documentation    
				swagger.SwaggerDoc("v2", new OpenApiInfo
				{
					Version = "v1",
					Title = "ASP.NET 7 Web API",
					Description = " ITI Projrcy"
				});

				// To Enable authorization using Swagger (JWT)    
				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
				});
				swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
					new OpenApiSecurityScheme
					{
					Reference = new OpenApiReference
					{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
					}
					},
					new string[] {}
					}
				});
			});
			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<ECEntity>(options => {

				options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
			});
			builder.Services.AddScoped<IProductRepository, ProductService>();
	
			builder.Services.AddScoped<IRepository<Review>, Repository<Review>>();
			builder.Services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
			})
		.AddEntityFrameworkStores<ECEntity>()
		.AddDefaultTokenProviders();

			// Service For Email
			builder.Services.AddScoped<IEmailService, EmailService>();
			builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


			//[Authoriz] used JWT Token in Chck Authantiaction
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValiedAudiance"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
				};
			});

			// Configure rate limiting
			builder.Services.AddRateLimiter(options => {
				options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(content =>
				RateLimitPartition.GetFixedWindowLimiter(
					partitionKey: content.Request.Headers.Host.ToString(),
					factory: Partition => new FixedWindowRateLimiterOptions
					{
						AutoReplenishment = true,
						PermitLimit = 5,
						QueueLimit = 0,
						Window = TimeSpan.FromSeconds(10)
					}
			 ));
				options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			// Configure rate limiting middleware
			app.UseRateLimiter();

			app.UseStaticFiles();
			app.UseCors();

			app.UseAuthentication(); //  Check JWT Token
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}