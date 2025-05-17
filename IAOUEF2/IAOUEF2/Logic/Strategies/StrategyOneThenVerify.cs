using IAOUEF2.Logic.Entities;
using IAOUEF2.Network;

namespace IAOUEF2.Logic.Strategies;

public class StrategyOneThenVerify : Strategy
{
    private Game _game;
    private Card _card;
    private InfoUpdater _updater;

    public Game Game
    {
        get => _game;
        set => _game = value;
    }

    public StrategyOneThenVerify()
    {
        _game = new Game();
        _card = new Card();
        _updater = new InfoUpdater();
    }

    public override void PlayTurn()
    {
        if (_game.Turn == 1 && _game.Night == 1)
        {
            try
            {
                string message = ServerConnector.GetMessage();
                Console.WriteLine($"Message reçu: {message}");

                if (!message.StartsWith("PIOCHES"))
                {
                    Console.WriteLine("Message non reconnu comme informations de pioche");
                    return;
                }

                // Nettoyage du message pour ne garder que les infos utiles
                string[] lines = message.Split('\n');
                string piochesLine = lines.FirstOrDefault(l => l.StartsWith("PIOCHES"));

                if (piochesLine == null)
                {
                    Console.WriteLine("Aucune information de pioche trouvée");
                    return;
                }

                string[] tokens = piochesLine.Substring(8).Split('|');
                if (tokens.Length % 2 != 0)
                {
                    Console.WriteLine("Format de pioches invalide");
                    return;
                }

                Dictionary<int, (TypeCard type, int amount)> availableCards = new();
                int index = 0;

                for (int i = 0; i < tokens.Length; i += 2)
                {
                    if (!Enum.TryParse(tokens[i], out TypeCard type))
                    {
                        Console.WriteLine($"Type de carte inconnu : {tokens[i]}");
                        continue;
                    }

                    if (!int.TryParse(tokens[i + 1], out int amount))
                    {
                        Console.WriteLine($"Valeur de carte invalide : {tokens[i + 1]}");
                        continue;
                    }

                    if (amount > 0)
                    {
                        availableCards[index] = (type, amount);

                        if (index < _game.Expeditions.Count)
                        {
                            _game.Expeditions[index].TypeCard = type;
                            _game.Expeditions[index].Amount = amount;
                        }
                    }

                    index++;
                }

                // Pioche la carte de DEFENSE avec la plus grande valeur
                var bestDefense = availableCards
                    .Where(c => c.Value.type == TypeCard.DEFENSE)
                    .OrderByDescending(c => c.Value.amount)
                    .FirstOrDefault();

                if (!bestDefense.Equals(default(KeyValuePair<int, (TypeCard, int)>)))
                {
                    Console.WriteLine($"Pioche de DEFENSE {bestDefense.Value.amount} à l'index {bestDefense.Key}");
                    _game.Draw(bestDefense.Key);
                    return;
                }

                // Sinon, pioche la carte avec la plus grande valeur tous types confondus
                var bestCard = availableCards
                    .OrderByDescending(c => c.Value.amount)
                    .FirstOrDefault();

                if (!bestCard.Equals(default(KeyValuePair<int, (TypeCard, int)>)))
                {
                    Console.WriteLine(
                        $"Pioche de {bestCard.Value.type} {bestCard.Value.amount} à l'index {bestCard.Key}");
                    _game.Draw(bestCard.Key);
                }
                else
                {
                    Console.WriteLine("Aucune carte positive disponible à piocher.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur PlayTurn : {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}