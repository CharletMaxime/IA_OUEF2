using System.Runtime.CompilerServices;
using IAOUEF2.Logic;
using IAOUEF2.Logic.Strategies;
using IAOUEF2.Network;

public class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        ServerConnector.OpenConnection();
        StrategySurvival strat = new StrategySurvival();
        string message = ServerConnector.GetMessage();
        ServerConnector.SendMessage("OUEF2");
        
        string welcomeMessage = ServerConnector.GetMessage();
        game.PlayerNumber = Convert.ToInt32(welcomeMessage.Split('|')[1]);
        
        message = ServerConnector.GetMessage();
        InfoUpdater infoUpdater = new InfoUpdater();
        infoUpdater.UpdateMonstre(game);
        while (message != "FIN")
        {
            if (message.StartsWith("DEBUT_TOUR"))
            {
                strat.PlayTurn();
            }
        }
        
        ServerConnector.CloseConnection();
    }
}