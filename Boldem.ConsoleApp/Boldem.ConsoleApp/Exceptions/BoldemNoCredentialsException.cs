namespace Boldem.ConsoleApp.Exceptions;

[Serializable]
public class BoldemNoCredentialsException : BoldemBaseException
{
    public BoldemNoCredentialsException() : 
        base("Missing credentials, have you defined environment variables?")
    {
    }
}