namespace DineFine.Exception;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ExceptionCodeAttribute : Attribute
{
    public string Code { get; }

    public ExceptionCodeAttribute(string code)
    {
        Code = code;
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ExceptionLevelAttribute : Attribute
{
    public string Level { get; }

    public ExceptionLevelAttribute(string level)
    {
        Level = level;
    }
}