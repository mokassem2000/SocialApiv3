using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialClint._services.Classes;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.Dto.AutoMapper;
using SocialClint.entity;
using SocialClint.Repository;
using SocialClint.Repository.Repo;
using System.Text;

namespace SocialClint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddControllers();
            
            
            builder.AddAuthModels();    
            builder.Services.AddScoped<IRepository<MemberDto>, UserRepo>();

            builder.Services.Configure<ClouiddinarySetting>(builder.Configuration.GetSection("Cloudinary"));
            builder.Services.AddScoped<PhotoService>();


            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();
            builder.Services.AddDbContext<DataContext>(opt => {

                opt.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
   
            
            
            
            builder.Services.AddAuthorization(opt => opt.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim("testClaim")));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}