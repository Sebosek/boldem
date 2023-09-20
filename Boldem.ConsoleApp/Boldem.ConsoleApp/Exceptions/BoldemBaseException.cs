using System.Runtime.Serialization;

namespace Boldem.ConsoleApp.Exceptions;

[Serializable]
public class BoldemBaseException : Exception
{
    public BoldemBaseException()
    {
    }

    public BoldemBaseException(string? message) : base(message)
    {
    }

    public BoldemBaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BoldemBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}