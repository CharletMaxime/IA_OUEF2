using System.Net.Sockets;

namespace IAOUEF2.Network;

public class ServerConnector
{
    private static ServerConnector instance;

    public static ServerConnector Instance => instance;

    private TcpClient address;
    
    //Flux provenant du serveur
    private StreamReader reader;
    
    //Flux sortant du serveur
    private StreamWriter writer;

    private ServerConnector()
    {
        this.address = new TcpClient("127.0.0.1", 1234);
        this.reader = new StreamReader(this.address.GetStream());
        this.writer = new StreamWriter(this.address.GetStream());
        this.writer.AutoFlush = true;
    }

    /// <summary>
    /// Envoie un message au serveur
    /// </summary>
    /// <example>Une action à faire pour l'IA</example>
    /// <param name="message">Le message à envoyer</param>
    public static void SendMessage(string message)
    {
        Instance.writer.WriteLine(message);
        Console.WriteLine(">> " + message);
    }

    /// <summary>
    /// Récupère le message provenant du serveur
    /// </summary>
    /// <returns>Le message</returns>
    public static string GetMessage()
    {
        string message = Instance.reader.ReadLine();
        Console.WriteLine(">> " + message);
        return message;
    }

    /// <summary>
    /// Ouvre une connexion au serveur
    /// </summary>
    public static void OpenConnection()
    {
        if(instance != null) CloseConnection();
        instance = new ServerConnector();
    }

    /// <summary>
    /// Ferme la liaison au serveur
    /// </summary>
    public static void CloseConnection()
    {
        instance.address.Close();
        instance = null;
    }
}