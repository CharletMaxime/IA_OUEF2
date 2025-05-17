namespace IAOUEF2.Logic.Entities;

public class Monster
{
    /// <summary>
    /// Nom du monstre
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Nombre de points de vie
    /// </summary>
    public int Hp { get; set; }
    
    /// <summary>
    /// Retournera le nom de points de savoir lors de sa mort
    /// </summary>
    public int KnowledgeScore { get; set; }
}