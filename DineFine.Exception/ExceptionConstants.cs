namespace DineFine.Exception;

public abstract class ExceptionConstants
{
    public const string ProjectName = "DineFine";
}

public abstract class ExceptionTypes
{
    public const string NullException = "NullException";
    public const string BusinessException = "BusinessException";
    public const string NotFoundException = "NotFoundException";
    public const string ForbiddenException = "ForbiddenException";
    public const string UnauthorizedException = "UnauthorizedException";
    public const string OperationalException = "OperationalException";
    public const string DatabaseException = "DatabaseException";
    public const string AlreadyExistsException = "AlreadyExistsException";
}

public abstract class ExceptionMessages
{
    public const string NullExceptionMessage = "Null exception";
    public const string BusinessExceptionMessage = "Bad Request";
    public const string NotFoundExceptionMessage = "Not Found";
    public const string ForbiddenExceptionMessage = "Forbidden";
    public const string UnauthorizedExceptionMessage = "Unauthorized";
    public const string OperationalExceptionMessage = "Operational Error";
    public const string DatabaseExceptionMessage = "Database Error";
    public const string AlreadyExistsExceptionMessage = "Already Exists";
}

public abstract class ErrorCodes
{
    public const string SystemInternalServerError = "System.Internal.Server.Error";
    public const string DatabaseError = "System.Database.Error";
    public const string OperationalError = "System.Operational.Error";
    public const string UnauthorizedError = "System.Unauthorized.Error";
    public const string EmailCannotBeEmptyError = "System.Email.Cannot.Be.Empty.Error";
    public const string LoginProviderCannotBeEmptyError = "System.Login.Provider.Cannot.Be.Empty.Error";
    public const string InvalidTokenError = "System.Invalid.Token.Error";
    public const string InvalidAgentError = "System.Invalid.Agent.Error";
    public const string BusinessInternalServerError = "Business.Internal.Server.Error";
    public const string NotFoundError = "Business.Not.Found.Error";
    public const string NullError = "Business.Null.Error";
    public const string ForbiddenError = "Business.Forbidden.Error";
    public const string AlreadyExistsError = "Business.Already.Exists.Error";
}

public abstract class ErrorLevels
{
    public const string System = "System.Exception";
    public const string Business = "Business.Exception";
}