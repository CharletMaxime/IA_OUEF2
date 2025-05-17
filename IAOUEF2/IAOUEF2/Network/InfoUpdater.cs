using IAOUEF2.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAOUEF2.Network
{
    public class InfoUpdater
    {

        public InfoUpdater() { }


        public void UpdateMonstre(Game game)
        {
            ServerConnector.SendMessage("MONSTRES");
            string reponse = ServerConnector.GetMessage();
            string[] monsters = reponse.Split('|');
            for (int i = 0; i <3; i++)
            {
                game.Monsters[i].Hp = Convert.ToInt32(monsters[i+0]);
                game.Monsters[i].KnowledgeScore = Convert.ToInt32(monsters[i+1]);
            }
        }


    }
}
