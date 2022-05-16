using System.Runtime.CompilerServices;
using Serilog;

namespace Solarisin.Core.Extensions.Logging;

public static class LoggerExtensions
{
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
    
    public static ILogger AddFileLocation(this ILogger logger,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) {
        return logger
            .ForContext("FilePath", sourceFilePath)
            .ForContext("LineNumber", sourceLineNumber);
    }
    
    public static ILogger AddMemberName(this ILogger logger,
        [CallerMemberName] string memberName = "") {
        return logger.ForContext("MemberName", memberName);
    }
}