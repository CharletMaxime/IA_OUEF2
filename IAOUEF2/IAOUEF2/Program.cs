﻿using System.Runtime.CompilerServices;
using IAOUEF2.Logic;
using IAOUEF2.Logic.Strategies;
using IAOUEF2.Network;

public class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        ServerConnector.OpenConnection();
        StrategyJuju strat = new StrategyJuju(game);
        string message = ServerConnector.GetMessage();
        ServerConnector.SendMessage("OUEF2");
        
        string welcomeMessage = ServerConnector.GetMessage();
        game.PlayerNumber = Convert.ToInt32(welcomeMessage.Split('|')[1]);
        InfoUpdater infoUpdater = new InfoUpdater();

        message = ServerConnector.GetMessage();

        game.Start();
        
        while (message != "FIN")
        {
            if (message.StartsWith("DEBUT_TOUR"))
            {
                game.Phases =Convert.ToInt32( message.Split('|')[2]);
                
                infoUpdater.UpdatePlayer(game);
                infoUpdater.UpdateMonstre(game);
                infoUpdater.UpdateExpeditions(game);
                strat.PlayTurn();
                
                
                 

            }                
            message = ServerConnector.GetMessage();

        }
        
        ServerConnector.CloseConnection();
    }
}