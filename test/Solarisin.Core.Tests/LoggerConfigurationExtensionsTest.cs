using Serilog;
using Solarisin.Core.Extensions.Logging;

namespace Solarisin.Core.Tests;

public class LoggerConfigurationExtensionsTest
{
    [Fact]
    public void TestConfigurationExtensions()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithAssemblyName()
            .Enrich.WithAssemblyVersion()
            .Enrich.WithAssemblyInformationalVersion()
            .CreateLogger();

        Log.Logger.Information("Hello, world!");
        Log.Logger.Warning("Warning, world!");
        Log.Logger.Error("Error, world!");
    }

    [Fact]
    public void TestTypedConfigurationExtensions()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithAssemblyName<string>()
            .Enrich.WithAssemblyVersion<string>()
            .Enrich.WithAssemblyInformationalVersion<string>()
            .CreateLogger();

        Log.Logger.Information("Hello, world!");
        Log.Logger.Warning("Warning, world!");
        Log.Logger.Error("Error, world!");
    }
}