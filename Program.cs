using Microsoft.EntityFrameworkCore;
using BookBase.Models;
using BookBase.Data;
using BookBase.Services;
using BookBase.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace BookBase
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Access configuration
            var configuration = builder.Configuration;


            //CORS config
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            //Registering dbContext in Dependency Injection (DI) container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));



            //Register services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddSingleton<IPasswordHasherService, BcryptPasswordHasherService>();



            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {

                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

            });


            // Registering controllers
            builder.Services.AddControllers(); 


            var app = builder.Build();


            //Enable CORS middleware
            app.UseCors();

            //Enable Authentication middleware
            app.UseAuthentication();

            //Enable Authorization middleware
            app.UseAuthorization();


            //Map Controllers
            app.MapControllers();

            

            app.Run();
        }
    }
}
