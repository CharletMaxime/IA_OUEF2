using IAOUEF2.Network;

public class Program
{
    static void Main(string[] args)
    {
        
        ServerConnector.OpenConnection();

        string message = ServerConnector.GetMessage();
        ServerConnector.SendMessage("OUEF_2");
        
        ServerConnector.CloseConnection();
    }
}