using AdventOfCode2022.CSharp.Console;
using AdventOfCode2022.CSharp.Console.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode2022.CSharp;

internal class Program
{
    private static string? _linePrefix;
    private static string? _version;

    private static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();

        while (true)
        {
            System.Console.Write(_linePrefix);
            string commandLine = System.Console.ReadLine()!;
            
            var factory = host.Services.GetService<CommandFactory>();
            if (factory == null)
                throw new Exception("factory not found");

            ICommand command = factory.Build(commandLine);

            if (!command.HadErrorInCreation())
            {
                command.Execute();
            }
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.json");
                builder.AddEnvironmentVariables();

                var configuration = builder.Build();
                var appSettings = configuration.GetSection("AppSettings");
                _linePrefix = appSettings["prefix"] ?? throw new ArgumentNullException("prefix");
                _version = appSettings["version"] ?? throw new ArgumentNullException("version");
            })
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<VersionCommand>(sp => new VersionCommand(_version!));
                services.AddTransient<CommandFactory>();
            });
}