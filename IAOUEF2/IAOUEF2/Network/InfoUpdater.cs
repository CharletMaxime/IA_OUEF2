using IAOUEF2.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = IAOUEF2.Logic.Action;

namespace IAOUEF2.Network
{
    public class InfoUpdater
    {

        public InfoUpdater() { }



        /// <summary>
        /// Mise à jour des infos des monstres
        /// </summary>
        /// <param name="game">le jeu</param>
        public void UpdateMonstre(Game game)
        {
            ServerConnector.SendMessage("MONSTRES");
            string reponse = ServerConnector.GetMessage();
            string[] monsters = reponse.Split('|');

            int monsternb = 0;

            for (int i = 0; i <6; i+=2)
            {
                game.Monsters[monsternb].Hp = Convert.ToInt32(monsters[i]);
                game.Monsters[monsternb].KnowledgeScore = Convert.ToInt32(monsters[i+1]);

                monsternb++;
            }
        }


        /// <summary>
        /// mise à jour des infos des joueurs
        /// </summary>
        /// <param name="game">le jeu</param>
        public void UpdatePlayer(Game game)
        {
        ServerConnector.SendMessage(Demande.JOUEURS.ToString());
            string reponse = ServerConnector.GetMessage();
            string[] player = reponse.Split('|');

            int playernb = 0;

            for (int i = 0; i<16; i+=4)
            {
                game.Players[playernb].Hp = Convert.ToInt32(player[i]);
                game.Players[playernb].DefenseScore = Convert.ToInt32(player[i+1]);
                game.Players[playernb].AttackScore = Convert.ToInt32(player[i+2]);
                game.Players[playernb].KnowledgeScore = Convert.ToInt32(player[i+3]);

                playernb++;
            }

            
        }


        /// <summary>
        /// mise à jour des infos des expéditions
        /// </summary>
        /// <param name="game">le jeu</param>
        public void UpdateExpeditions (Game game)
        {
            ServerConnector.SendMessage(Demande.PIOCHES.ToString());
            string reponse = ServerConnector.GetMessage();
            string[] expeditions = reponse.Split('|');

            int expednb = 0;
            for (int i = 0; i < 12; i+=2)
            {
                switch(expeditions[i])

                {
                    case "SAVOIR":
                        game.Expeditions[expednb].TypeCard = TypeCard.SAVOIR;
                        break;
                    case "DEFENSE":
                        game.Expeditions[expednb].TypeCard = TypeCard.DEFENSE;
                        break;
                    case "ATTAQUE":
                        game.Expeditions[expednb].TypeCard = TypeCard.ATTAQUE;
                        break;
                    default: break;
                        
                }
                game.Expeditions[expednb].Amount = Convert.ToInt32(expeditions[i + 1]);
                expednb++;
            }
        }

        public void UpdateRedLadyDamage(Game game)
        {
            ServerConnector.SendMessage(Demande.DEGATS.ToString());
            game.RedLadyDamage = Convert.ToInt32(ServerConnector.GetMessage());
        }


    }
}
