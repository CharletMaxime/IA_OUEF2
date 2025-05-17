using System.Runtime.CompilerServices;
using IAOUEF2.Logic;
using IAOUEF2.Network;

public class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        ServerConnector.OpenConnection();

        string message = ServerConnector.GetMessage();
        ServerConnector.SendMessage("OUEF2");
        
        string welcomeMessage = ServerConnector.GetMessage();
        game.PlayerNumber = Convert.ToInt32(welcomeMessage.Split('|')[1]);
        
        ServerConnector.CloseConnection();
    }
}