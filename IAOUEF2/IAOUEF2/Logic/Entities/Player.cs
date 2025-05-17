namespace IAOUEF2.Logic.Entities;

public class Player
{
    /// <summary>
    /// Nom du joueur
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Ses points de vies
    /// </summary>
    public int Hp { get; set; }
    
    /// <summary>
    /// Points de défense
    /// </summary>
    public int DefenseScore { get; set; }
    
    /// <summary>
    /// Points d'attaque
    /// </summary>
    public int AttackScore { get; set; }
    
    /// <summary>
    /// Points de savoir
    /// </summary>
    public int KnowledgeScore { get; set; }
}