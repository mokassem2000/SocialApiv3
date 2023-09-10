using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialClint._services.Classes;
using SocialClint._services.Interfaces;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.Dto.AutoMapper;
using SocialClint.entity;
using SocialClint.Hubmo;
using SocialClint.Repository.Interfaces;
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
            builder.Services.AddScoped<ImailService, MailService>();

            builder.AddAuthModels();
            builder.Services.AddScoped<IRepository<MemberDto>, UserRepo>();

            builder.Services.AddScoped<ILikeRepo, LikeRepo>();
        


            builder.Services.Configure<ClouiddinarySetting>(builder.Configuration.GetSection("Cloudinary"));
            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));
           
            builder.Services.AddScoped<PhotoService>();
            builder.Services.AddScoped<IMessageRepo, MessageRepo>();
            builder.Services.AddSignalR().AddHubOptions<MessageHub>(options =>
            {
                options.EnableDetailedErrors = true;
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddIdentity<AppUser, IdentityRole>(
                opt=> {
                    opt.Password.RequiredLength = 7;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.User.RequireUniqueEmail = true;
                    opt.SignIn.RequireConfirmedEmail = true;
                }
                
                
                ).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            builder.Services.AddDbContext<DataContext>(opt =>
            {

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
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();


            app.UseCors(policy =>
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200"));
                       
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHub<MessageHub>("/MessageHub");
            app.MapControllers();

            app.Run();
        }
    }
}