using System.Runtime.InteropServices.JavaScript;

namespace DineFine.Exception;

public static class DynamicExceptions
{
    private const string ProjectName = "DineFineException";

    private static RootException GenerateException(string exceptionName, string code, string level, string message)
    {
        var exceptionType = ExceptionGenerator.GenerateExceptionClass(ProjectName, exceptionName, code, level, message);
        
        var exceptionInstance = (RootException)Activator.CreateInstance(exceptionType, message);
        return exceptionInstance ?? new RootException("Failed to generate exception type");
    }

    public static RootException NullException(string message) => GenerateException( "NullException", ErrorCodes.NullError, ErrorLevels.Business, message);

    public static RootException NotFoundException(string message) => GenerateException("NotFoundException", ErrorCodes.NotFoundError, ErrorLevels.Business, message);
    
    public static RootException ForbiddenException(string message) => GenerateException("ForbiddenException", ErrorCodes.ForbiddenError, ErrorLevels.Business, message);
    
    public static RootException UnauthorizedException(string message) => GenerateException("UnauthorizedException", ErrorCodes.UnauthorizedError, ErrorLevels.System, message);
    
    public static RootException OperationalException(string message) => GenerateException("OperationalException", ErrorCodes.OperationalError, ErrorLevels.System, message);
    
    public static RootException DatabaseException(string message) => GenerateException("DatabaseException", ErrorCodes.DatabaseError, ErrorLevels.System, message);
    
    public static RootException AlreadyExistsException(string message) => GenerateException("AlreadyExistsException", ErrorCodes.AlreadyExistsError, ErrorLevels.Business, message);
}
