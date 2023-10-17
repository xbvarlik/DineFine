namespace DineFine.Exception;

public class RootException : System.Exception
{
    public string Code { get; }
    public string Level { get; }
    public RootException(string message) : base(message)
    {
    }

    public RootException(string code, string message, string level) : base(message)
    {
        Code = code;
        Level = level;
    }

}
