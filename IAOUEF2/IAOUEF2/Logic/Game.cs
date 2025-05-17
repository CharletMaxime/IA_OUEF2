using IAOUEF2.Logic.Entities;
using IAOUEF2.Network;

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

    #region Attributes Rules

    private int turn;

    private int phases;

    private int night;

    private bool isBloodMoon;

    #endregion

    #region Properties

    public Dictionary<int, Player> Players
    {
        get => players;
        set => players = value;
    }

    public Dictionary<int, Monster> Monsters
    {
        get => monsters;
        set => monsters = value;
    }

    public Dictionary<int, Card> Expeditions
    {
        get => expeditions;
        set => expeditions = value;
    }

    public Dictionary<TypeCard, Card> Hand
    {
        get => hand;
        set => hand = value;
    }

    public int PlayerNumber
    {
        get => playerNumber;
        set => playerNumber = value;
    }

    #endregion

    #region Rules properties

    /// <summary>
    /// Tours
    /// </summary>
    public int Turn
    {
        get => turn;
        set
        {
            if (turn > 20)
                ServerConnector.CloseConnection();
            this.turn = value;
        }
    }

    /// <summary>
    /// Phases
    /// </summary>
    public int Phases
    {
        get => phases;
        set
        {
            if (value is < 0 or > 17)
                phases = 1;
            phases = value;
        }
    }

    /// <summary>
    /// Nuits
    /// </summary>
    public int Night
    {
        get => night;
        set => night = value;
    }

    /// <summary>
    /// Représente une lune de sang
    /// </summary>
    public bool IsBloodMoon
    {
        get => isBloodMoon;
        set => isBloodMoon = value;
    }

    #endregion

    #region Constructor

    public Game()
    {
        players = new Dictionary<int, Player>();
        monsters = new Dictionary<int, Monster>();
        this.monsters[0] = new Monster();
        this.monsters[1] = new Monster();
        this.monsters[2] = new Monster();
        this.players[playerNumber] = new Player();
    }

    #endregion

    #region Methods

    /// <summary>

    /// Utilise une carte de la main du joueur
    /// </summary>
    /// <example>
    /// Si une carte contient 45 d'attaque et qu'on l'utilise,
    /// elle repasse à 0 mais le contenant va dans l'utilisable
    /// </example>
    /// <param name="card">La carte qui est utilisée</param>
    public void UseCards(TypeCard card)
    {
        switch (card)
        {
            case TypeCard.SAVOIR:
                hand[card].Amount = 0;
                break;
            case TypeCard.ATTAQUE:
                hand[card].Amount = 0;
                break;
            case TypeCard.DEFENSE:
                hand[card].Amount = 0;
                break;
        }
    }
    
    /// <summary>
    /// Attribue les valeurs des cartes secteur gauche sur le secteur droit au type de carte sélectionné
    /// </summary>
    /// <param name="card">La clé déterminant la carte à changer</param>
    public void Draw(int card)
    {
        this.hand[expeditions[card].TypeCard].Amount += expeditions[card].Amount;
    }

    #endregion

    #region Rules

    /// <summary>
    /// Initialise les valeurs au début de la partie
    /// </summary>
    public void Start()
    {
        this.turn = 1;
        this.phases = 1;
        this.night = 0;
        this.isBloodMoon = false;
    }

    /// <summary>
    /// Etabli les règles dans les tours
    /// </summary>
    public void RulesTurn()
    {
        //Gère le cas d'ajout d'une nuit
        if (phases % 4 == 0) night++;

        //Gère la lune de sang
        if (night % 4 == 0 && phases == 17) isBloodMoon = true;

        //Gère le cas d'un nouveau tour
        if (isBloodMoon && phases == 1)
        {
            isBloodMoon = false;
            turn++;
        }
    }

    #endregion
}