using IAOUEF2.Logic.Entities;
using IAOUEF2.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAOUEF2.Logic.Strategies
{
    public class StrategyJuju:Strategy
    {

        private Game game;
        private InfoUpdater infoUpdater;



        public StrategyJuju(Game game)
        {
            this.game = game;
            this.infoUpdater = new InfoUpdater();
        }


        public override void PlayTurn()
        {
            infoUpdater.UpdateMonstre(game);
            infoUpdater.UpdatePlayer(game);
            infoUpdater.UpdateExpeditions(game);

            int minHpMonster = 0;
            int[] monstersHp = [game.Monsters[0].Hp, game.Monsters[1].Hp, game.Monsters[2].Hp];
            if (monstersHp[0] >= monstersHp[1])
            {
                minHpMonster = 1;
                if (monstersHp[1] >= monstersHp[2])
                {
                    minHpMonster = 2;
                }
            }
            else
            {
                minHpMonster = 0;
                if (monstersHp[0] >= monstersHp[2])
                {
                    minHpMonster = 2;
                }
            }
            if (monstersHp[1] == monstersHp[2] && monstersHp[0] == monstersHp[0])
            {
                int minMonsterKnowledge = 0;
                int[] monsterKnowledge = [game.Monsters[0].KnowledgeScore, game.Monsters[1].KnowledgeScore, game.Monsters[2].KnowledgeScore];
                if (monsterKnowledge[0] >= monsterKnowledge[1])
                {
                    minMonsterKnowledge = 1;
                    if (monsterKnowledge[1] <= monsterKnowledge[2])
                    {
                        minMonsterKnowledge = 2;
                    }
                }
            }

            if (game.Phases % 4 == 0)
            {

            }
            else if (game.Phases!=17)
            {
                ServerConnector.SendMessage("DEGATS");
                if (game.Hand[TypeCard.DEFENSE].Amount < Convert.ToInt32(ServerConnector.GetMessage()))
                {
                    Console.WriteLine(game.Hand[TypeCard.DEFENSE].Amount);
                    if (game.Expeditions[0].Amount > game.Expeditions[3].Amount)
                    {
                        ServerConnector.SendMessage("PIOCHER|0");
                        game.Draw(0);
                    }
                    else ServerConnector.SendMessage("PIOCHER|0");
                    game.Draw(0);

                }
                
            }

        }
    }
}
