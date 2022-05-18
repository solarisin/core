using System.Runtime.CompilerServices;
using Serilog;

namespace Solarisin.Core.Extensions.Logging;

/// <summary>
///     Extends <see cref="Serilog.ILogger"/> to add context enrichment capabilities to logs.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    ///     Enrich log events with context information about the code location including the file path, member name and line
    ///     number.
    /// </summary>
    /// <param name="logger">The ILogger instance</param>
    /// <param name="memberName">The calling member name (from caller attribute)</param>
    /// <param name="sourceFilePath">The source file path (from caller attribute)</param>
    /// <param name="sourceLineNumber">The source line number (from caller attribute)</param>
    /// <returns>Logger instance allowing chaining</returns>
    public static ILogger Here(this ILogger logger,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return logger
            .ForContext("MemberName", memberName)
            .ForContext("FilePath", sourceFilePath)
            .ForContext("LineNumber", sourceLineNumber);
    }

    /// <summary>
    ///     Enrich log events with the file path and line number.
    /// </summary>
    /// <param name="logger">The ILogger instance</param>
    /// <param name="sourceFilePath">The source file path (from caller attribute)</param>
    /// <param name="sourceLineNumber">The source line number (from caller attribute)</param>
    /// <returns>Logger instance allowing chaining</returns>
    public static ILogger AddFileLocation(this ILogger logger,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return logger
            .ForContext("FilePath", sourceFilePath)
            .ForContext("LineNumber", sourceLineNumber);
    }

    /// <summary>
    ///     Enrich log events with the member name.
    /// </summary>
    /// <param name="logger">The ILogger instance</param>
    /// <param name="memberName">The calling member name (from caller attribute)</param>
    /// <returns>Logger instance allowing chaining</returns>
    public static ILogger AddMemberName(this ILogger logger,
        [CallerMemberName] string memberName = "")
    {
        return logger.ForContext("MemberName", memberName);
    }
}