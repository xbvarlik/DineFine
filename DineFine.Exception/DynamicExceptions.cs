namespace DineFine.Exception;

public static class DynamicExceptions
{
    private static RootException GenerateException(string exceptionName, string code, string level, string message)
    {
        var exceptionType = ExceptionGenerator.GenerateExceptionClass(ExceptionConstants.ProjectName, exceptionName, code, level, message);
        
        var exceptionInstance = (RootException)Activator.CreateInstance(exceptionType, message);
        return exceptionInstance ?? new RootException("Failed to generate exception type");
    }

    public static RootException NullException(string message = ExceptionMessages.NullExceptionMessage, string errorCode = ErrorCodes.NullError) => 
        GenerateException(ExceptionTypes.NullException, errorCode, ErrorLevels.Business, message);

    public static RootException BusinessException(string message = ExceptionMessages.BusinessExceptionMessage, string errorCode = ErrorCodes.BusinessInternalServerError) => 
        GenerateException(ExceptionTypes.BusinessException, errorCode, ErrorLevels.Business, message);
    
    public static RootException NotFoundException(string message = ExceptionMessages.NotFoundExceptionMessage, string errorCode = ErrorCodes.NotFoundError) => 
        GenerateException(ExceptionTypes.NotFoundException, errorCode, ErrorLevels.Business, message);
    
    public static RootException ForbiddenException(string message = ExceptionMessages.ForbiddenExceptionMessage, string errorCode = ErrorCodes.ForbiddenError) => 
        GenerateException(ExceptionTypes.ForbiddenException, errorCode, ErrorLevels.Business, message);
    
    public static RootException UnauthorizedException(string message = ExceptionMessages.UnauthorizedExceptionMessage, string errorCode = ErrorCodes.UnauthorizedError) => 
        GenerateException(ExceptionTypes.UnauthorizedException, errorCode,ErrorLevels.System, message);
    
    public static RootException OperationalException(string message = ExceptionMessages.OperationalExceptionMessage, string errorCode = ErrorCodes.OperationalError) => 
        GenerateException(ExceptionTypes.OperationalException, errorCode, ErrorLevels.System, message);
    
    public static RootException DatabaseException(string message = ExceptionMessages.DatabaseExceptionMessage, string errorCode = ErrorCodes.DatabaseError) => 
        GenerateException(ExceptionTypes.DatabaseException, errorCode, ErrorLevels.System, message);
    
    public static RootException AlreadyExistsException(string message = ExceptionMessages.AlreadyExistsExceptionMessage, string errorCode = ErrorCodes.AlreadyExistsError) => 
        GenerateException(ExceptionTypes.AlreadyExistsException, errorCode, ErrorLevels.Business, message);
}


