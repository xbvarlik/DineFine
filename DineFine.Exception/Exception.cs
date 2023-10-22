using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DineFine.Exception;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class DineFineException : System.Exception
{
    protected DineFineException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected DineFineException(string code, string level, System.Exception? innerException = null) : base(null, innerException)
    {
        Code = code;
        Level = level;
    }

    public string Code { get; } = null!;

    public string Level { get; } = null!;
}

[Serializable]
public class DineFineSystemException : DineFineException
{
    protected DineFineSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineSystemException(string code, System.Exception? innerException = null) : base(code, ErrorLevels.System, innerException)
    {
    }
}

[Serializable]
public class DineFineDatabaseException : DineFineSystemException
{
    protected DineFineDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineDatabaseException(System.Exception? innerException = null) : base(ErrorCodes.DatabaseError, innerException)
    {
    }
}

[Serializable]
public class DineFineOperationalException : DineFineSystemException
{
    protected DineFineOperationalException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineOperationalException(System.Exception? innerException = null) : base(ErrorCodes.OperationalError, innerException)
    {
    }

    public DineFineOperationalException(string errorCode, System.Exception? innerException = null) : base(errorCode, innerException)
    {
    }
}

[Serializable]
public class DineFineUnauthorizedException : DineFineSystemException
{
    protected DineFineUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineUnauthorizedException(System.Exception? innerException = null) : base(ErrorCodes.UnauthorizedError, innerException)
    {
    }
}

[Serializable]
public class DineFineBusinessException : DineFineException
{
    protected DineFineBusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineBusinessException(string code, System.Exception? innerException = null) : base(code, ErrorLevels.Business, innerException)
    {
    }
}

[Serializable]
public class DineFineForbiddenException : DineFineBusinessException
{
    protected DineFineForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineForbiddenException(System.Exception? innerException = null) : base(ErrorCodes.ForbiddenError, innerException)
    {
    }
}

[Serializable]
public class DineFineNotFoundException : DineFineBusinessException
{
    protected DineFineNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineNotFoundException(System.Exception? innerException = null) : base(ErrorCodes.NotFoundError, innerException)
    {
    }
}

[Serializable]
public class DineFineNullException : DineFineBusinessException
{
    protected DineFineNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineNullException(System.Exception? innerException = null) : base(ErrorCodes.NullError, innerException)
    {
    }
}

[Serializable]
public class DineFineAlreadyExistsException : DineFineBusinessException
{
    protected DineFineAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DineFineAlreadyExistsException(System.Exception? innerException = null) : base(ErrorCodes.AlreadyExistsError, innerException)
    {
    }
}