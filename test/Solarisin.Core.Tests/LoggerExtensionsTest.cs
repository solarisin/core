using Serilog;
using Solarisin.Core.Extensions.Logging;

namespace Solarisin.Core.Tests;

public class LoggerExtensionsTest
{
    [Fact]
    public void TestContextExtensions()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .CreateLogger();

        Log.Logger.Here().Information("Hello, world!");
        Log.Logger.AddFileLocation().Warning("Warning, world!");
        Log.Logger.AddMemberName().Error("Error, world!");
    }
}