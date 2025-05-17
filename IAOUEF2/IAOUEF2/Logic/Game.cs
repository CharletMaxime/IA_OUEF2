using IAOUEF2.Logic.Entities;

namespace IAOUEF2.Logic;

public class Game
{
    #region Attributes

    private Dictionary<int, Player> players;
    private Dictionary<int, Monster> monsters;
    private Dictionary<int, Card> expeditions;
    private Dictionary<TypeCard, Card> hand;
    private int playerNumber;

    #endregion

    #region Properties
    public Dictionary<int, Player> Players { get => players; set => players = value; }
    public Dictionary<int, Monster> Monsters { get => monsters; set => monsters = value; }
    public Dictionary<int, Card> Expeditions { get => expeditions; set => expeditions = value; }
    public Dictionary<TypeCard, Card> Hand { get => hand; set => hand = value; }
    public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
    #endregion

    #region Constructor

    public Game()
    {
        players = new Dictionary<int, Player>();
        this.players[playerNumber] = new Player();
    }
    #endregion

    #region Methods

    public void UseCards(TypeCard card)
    {
        switch (card)
        {
            case TypeCard.SAVOIR:
                players[playerNumber].KnowledgeScore += hand[card].Amount;
                hand[card].Amount = 0;
                break;
            case TypeCard.ATTAQUE:
                players[playerNumber].AttackScore += hand[card].Amount;
                hand[card].Amount = 0;
                break;
            case TypeCard.DEFENSE:
                players[playerNumber].DefenseScore += hand[card].Amount;
                hand[card].Amount = 0;
                break;
        }
    }

    public void Draw(int card)
    {
        this.hand[expeditions[card].TypeCard].Amount += expeditions[card].Amount;
    }
    #endregion
}