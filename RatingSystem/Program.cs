using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingSystem.Application;
using RatingSystem.Application.Queries;
using RatingSystem.Data;
using RatingSystem.ExternalService;
using RatingSystem.PublishedLanguage.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RatingSystem
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // setup
            var services = new ServiceCollection();

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            services.RegisterBusinessServices(Configuration);
            services.AddRatingDataAccess(Configuration);

            services.Scan(scan => scan
                .FromAssemblyOf<GetAverageRating>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
