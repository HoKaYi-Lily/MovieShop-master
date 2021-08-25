using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieShopAPI", Version = "v1" });
            });

            //configure services, tell asp.net code which class to inject for each interface
            services.AddScoped<IMovieService, MovieService>();
            //tell Imovieservices please inject new instance of movieservice class
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            services.AddMemoryCache();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            //genre services is using in memory caching so we need that one
            //genre list never change, just get cache
            //the API is not going to ICurrentUser service somehow..

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.Name = "MovieShopAuthCookie";
                  options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    //options.LoginPath = "/Account/Login";
                });
            services.AddDbContext<MovieShopDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection"))
                );

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieShopAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(builder =>
            {

                builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
