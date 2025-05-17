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
            ServerConnector.SendMessage("DEGATS");
            int degats = Convert.ToInt32(ServerConnector.GetMessage());


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

            if (game.Phases == 16)
            {
                Console.WriteLine("Je suis dans le tour 16");
                if (game.Hand[TypeCard.DEFENSE].Amount > degats + 15 && game.Players[game.PlayerNumber].DefenseScore < degats + 15)
                {
                    ServerConnector.SendMessage("UTILISER|DEFENSE");
                    game.UseCards(TypeCard.DEFENSE);
                    game.Draw(2);
                }
                else if (game.Players[game.PlayerNumber].Hp > degats)
                {
                    game.Draw(2);

                }
                else
                {
                    ServerConnector.SendMessage("UTILISER|SAVOIR");
                    game.UseCards(TypeCard.SAVOIR);
                    game.Draw(2);

                }
            }

            else if (game.Phases % 4 == 0 && game.Phases!=0)
            {
                if (game.Hand[TypeCard.ATTAQUE].Amount >= game.Monsters[minHpMonster].Hp)
                {
                    ServerConnector.SendMessage("UTILISER|ATTAQUE");
                    game.UseCards(TypeCard.ATTAQUE);
                    ServerConnector.SendMessage("ATTAQUER|"+ minHpMonster);
                }
                else
                {
                    game.Draw(2);
                    
                }
            }
            else if (game.Phases!=16)
            {
                if (game.Hand[TypeCard.DEFENSE].Amount < degats+15)
                {
                    if (game.Expeditions[0].Amount > game.Expeditions[3].Amount)
                    {
                        game.Draw(0);
                    }
                    else
                    {
                        game.Draw(3);
                    }

                }
                else if (game.Hand[TypeCard.ATTAQUE].Amount < game.Monsters[minHpMonster].Hp)
                {

                    if (game.Expeditions[1].Amount > game.Expeditions[4].Amount)
                    {
                        game.Draw(1);
                    }
                    else
                    {
                        game.Draw(4);
                    }
                }
                else game.Draw(2);

            }

        }
    }
}
