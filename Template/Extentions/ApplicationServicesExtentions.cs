using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository;
using Core.Service.Contract;
using Services;
using Template.Errors;
using Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Template.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            #region connection Strings
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
           
            #endregion

            #region LifeTime
            //   services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Security
            services.AddScoped<IAuthService, AuthService>();
            #endregion

            #region MappingProfile
            ////services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(M => M.AddProfile(new MappingProfile(Configuration["ApiBaseUrl"])));
            #endregion

            #region ErrorHandle
            //Error handle
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                       .Where(e => e.Value.Errors.Count > 0)
                       .SelectMany(e => e.Value.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList();

                    var errorResponse = new VaildationErrorResponse(errors);


                    return new BadRequestObjectResult(errorResponse);
                };
            });
            #endregion

            #region Identity
            services.AddIdentity<AppUser, IdentityRole>().
                AddEntityFrameworkStores<AppDbContext>();
            #endregion

            #region jwt
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(Configuration["JWT:DurationInDayes"])),

                };
            }
               );
            #endregion

            return services;


        }
    }
}
