using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Data;
using MovieApi.Data.Repositories;
using MovieApi.Services;
using MovieApi.Services.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace MovieApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("MovieApiContext") ?? throw new InvalidOperationException("Connection string 'MovieApiContext' not found.");

        builder.Services.AddDbContext<MovieApiContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<IActorService, ActorService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();

        // Add services to the container.

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();
        });

        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
