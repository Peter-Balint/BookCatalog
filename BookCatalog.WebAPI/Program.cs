
using BookCatalog.DataAccess;
using BookCatalog.DataAccess.Models;
using BookCatalog.WebAPI.Infrastructure;
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
