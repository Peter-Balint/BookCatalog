
using BookCatalog.DataAccess;
using BookCatalog.DataAccess.Models;
using BookCatalog.WebAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BookCatalog.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddAutoMapperCustom();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = null;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MultiScheme";
                options.DefaultChallengeScheme = "MultiScheme";
            })
                .AddScheme<AuthenticationSchemeOptions, SimpleTokenHandler>("Token", null)
                .AddPolicyScheme("MultiScheme", "Multi Auth", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        // Use token if there's an Authorization header, else use Cookies
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                            return "Token";

                    return "Identity.Application";
                };
            });
            

            // Configure CORS policy for Blazor client
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlazorPolicy",
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration["BlazorUrl"]
                                           ?? throw new ArgumentNullException("BlazorUrl"))
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("BlazorPolicy");

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var context = scope.ServiceProvider.GetRequiredService<BookCatalogDbContext>();
                DbInitializer.Initialize(context, userManager);
            }

            app.Run();
        }
    }
}
