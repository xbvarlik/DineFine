using System.Runtime.InteropServices.JavaScript;

namespace DineFine.Exception;

public static class DynamicExceptions
{
    private static RootException GenerateException(string exceptionName, string code, string level, string message)
    {
        var exceptionType = ExceptionGenerator.GenerateExceptionClass(ExceptionConstants.ProjectName, exceptionName, code, level, message);
        
        var exceptionInstance = (RootException)Activator.CreateInstance(exceptionType, message);
        return exceptionInstance ?? new RootException("Failed to generate exception type");
    }

    public static RootException NullException(string errorCode = ErrorCodes.NullError, string message = ExceptionMessages.NullExceptionMessage) => 
        GenerateException(ExceptionTypes.NullException, errorCode, ErrorLevels.Business, message);

    public static RootException BusinessException(string errorCode = ErrorCodes.BusinessInternalServerError, string message = ExceptionMessages.BusinessExceptionMessage) => 
        GenerateException(ExceptionTypes.BusinessException, errorCode, ErrorLevels.Business, message);
    
    public static RootException NotFoundException(string errorCode = ErrorCodes.NotFoundError, string message = ExceptionMessages.NotFoundExceptionMessage) => 
        GenerateException(ExceptionTypes.NotFoundException, errorCode, ErrorLevels.Business, message);
    
    public static RootException ForbiddenException(string errorCode = ErrorCodes.ForbiddenError, string message = ExceptionMessages.ForbiddenExceptionMessage) => 
        GenerateException(ExceptionTypes.ForbiddenException, errorCode, ErrorLevels.Business, message);
    
    public static RootException UnauthorizedException(string errorCode = ErrorCodes.UnauthorizedError, string message = ExceptionMessages.UnauthorizedExceptionMessage) => 
        GenerateException(ExceptionTypes.UnauthorizedException, errorCode,ErrorLevels.System, message);
    
    public static RootException OperationalException(string errorCode = ErrorCodes.OperationalError, string message = ExceptionMessages.OperationalExceptionMessage) => 
        GenerateException(ExceptionTypes.OperationalException, errorCode, ErrorLevels.System, message);
    
    public static RootException DatabaseException(string errorCode = ErrorCodes.DatabaseError, string message = ExceptionMessages.DatabaseExceptionMessage) => 
        GenerateException(ExceptionTypes.DatabaseException, errorCode, ErrorLevels.System, message);
    
    public static RootException AlreadyExistsException(string errorCode = ErrorCodes.AlreadyExistsError, string message = ExceptionMessages.AlreadyExistsExceptionMessage) => 
        GenerateException(ExceptionTypes.AlreadyExistsException, errorCode, ErrorLevels.Business, message);
}


