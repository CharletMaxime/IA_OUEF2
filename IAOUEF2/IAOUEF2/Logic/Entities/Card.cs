namespace IAOUEF2.Logic.Entities;

public class Card
{
    /// <summary>
    /// Nom de la carte
    /// </summary>
    public string Name
    {
        get
        {
            TypeCard typecard = TypeCard;
            switch (typecard)
            {
                case TypeCard.DEFENSE:
                    return TypeCard.DEFENSE.ToString();
                case TypeCard.ATTAQUE:
                    return TypeCard.ATTAQUE.ToString();
                case TypeCard.SAVOIR:
                    return TypeCard.SAVOIR.ToString();
            }
            return "";
        }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            Name = value.ToString();
        }
    }

    /// <summary>
    /// Nombre de points de la carte
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// Quel type de carte
    /// </summary>
    public TypeCard TypeCard { get; set; }
}