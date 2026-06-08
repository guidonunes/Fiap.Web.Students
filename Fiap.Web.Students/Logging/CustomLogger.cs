namespace Fiap.Web.Students.Logging;


public interface ICustomLogger
{
    void Log(string message);
}


public class MockLogger: ICustomLogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}