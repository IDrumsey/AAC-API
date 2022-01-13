using AnimalAdoptionCenter.Data;
using AnimalAdoptionCenter.Profiles;
using AnimalAdoptionCenter.Repositories.Animal;
using AnimalAdoptionCenter.Repositories.Store;
using AnimalAdoptionCenter.Services.Animal;
using AnimalAdoptionCenter.Services.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AnimalAdoptionCenter.Services.Authentication;
using AnimalAdoptionCenter.Services.Users;
using AnimalAdoptionCenter.Repositories.Users;
using AnimalAdoptionCenter.Services;

namespace AnimalAdoptionCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // resources I used for setting up authentication and authorization
            // https://www.youtube.com/watch?v=Lh82WlOvyQk
            // https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5

            // setup authentication
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // set the token
                            // https://stackoverflow.com/questions/52184431/in-asp-net-core-read-jwt-token-from-cookie-instead-of-headers
                            context.Token = context.Request.Cookies["jwt"];
                            return Task.CompletedTask;
                        }
                    };

                    options.RequireHttpsMetadata = false; // set to true in production

                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );


            // https://stackoverflow.com/questions/50130796/angular-withcredentials-not-working-for-post-put
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

            services.AddControllers();

            // add connection to the database
            services.AddDbContext<AppDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            // link repository interfaces with implementations
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // link service interfaces with implementations
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();

            // set up automapper
            services.AddAutoMapper(typeof(StoreToStoreResourceProfile));
            services.AddAutoMapper(typeof(NewStoreResourceToStoreProfile));
            services.AddAutoMapper(typeof(DayOperationHoursToDayOperationHoursResourceProfile));
            services.AddAutoMapper(typeof(SimpleTimeToSimpleTimeResourceProfile));
            services.AddAutoMapper(typeof(UpdatedAnimalToAnimalProfile));
            services.AddAutoMapper(typeof(NewAnimalToAnimalProfile));
            services.AddAutoMapper(typeof(UpdatedStoreResourceToStoreProfile));
            services.AddAutoMapper(typeof(UserToUserResource));

            // allow configuration access anywhere in files
            services.AddSingleton<IConfiguration>(Configuration);


            // link DatabaseOperations
            services.AddScoped<IDatabaseOperations, DatabaseOperations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
