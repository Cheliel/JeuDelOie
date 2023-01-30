// See https://aka.ms/new-console-template for more information


using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System;
using System.Collections.Specialized;
using System.Data.Entity.ModelConfiguration.Configuration;

public static class JeuDeLoie
{
    public static Parcourt plateau1 = new Parcourt();
    public static Parcourt plateau2 = new Parcourt("Someil des Dieux");
    public static Parcourt plateau3 = new Parcourt("Aurore Eternel");
    public static Parcourt plateau4 = new Parcourt("Profond Soupire");
    public static int parcourtSelectionne = 0;
    public static List<List<Score>> scoreBoard = new List<List<Score>>();
    public static Server server;
    public static TcpClient client;

    public static Parcourt [] parcourts = { plateau1, plateau2, plateau3, plateau4 } ;
    
    public async static void demarer()
    {
        scoreBoard.Clear();
        scoreBoard = getscoreBoard();
  

        while (true)
        {
            Console.WriteLine("Je vais afficher l'ecrant d'accueil !...");
            Console.ReadKey();
            IHM.afficheEcrantDAccueil();

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    incrementeParcourtSelectionne();
                    break;

                case ConsoleKey.LeftArrow:
                    decrementeParcourtSelectionne();
                    break;

                case ConsoleKey.Enter:

                    await choisirModeDeJeu();
                    Console.WriteLine("fin choisir mode de jeu");
                    Console.ReadKey();

                    break;
            }
        }

         
    }

    public static List<List<Score>> getscoreBoard()
    {
        List<List<Score>> newScoreBoard = new List<List<Score>>();
        BDD BDD = new BDD();

        for (int i = 0; i < parcourts.Length; i++)
        {
            newScoreBoard.Add(BDD.getScore(parcourts[i].nom));
        }

        return newScoreBoard;  
    }

    public async static Task<int> choisirModeDeJeu()
    {
        IHM.afficheModeDeJeu();
        bool achoisi = false;

        while(!achoisi) 
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    modeMultiJoueur();
                    break;

                case ConsoleKey.LeftArrow:
                    modeSolo();
                    break;

                case ConsoleKey.UpArrow:
                    await Task.Run(() => modeEnLigne());
                    break;

                default:
                    await choisirModeDeJeu();
                    break;
            }
        }

        return 0;
    }

    public static void modeMultiJoueur()
    {
        Joueur j1 = new Joueur(choisirPseudo("Joueur 1"), false);
        Joueur j2 = new Joueur(choisirPseudo("Joueur 2"), false);

        Parcourt plateau = choisirPlateau();

        Partie partie = new Partie(j1, j2);

        Joueur vainceur = partie.start();
        IHM.ecrantDeFin(vainceur);

        Score score = new Score(vainceur.getPseudo(), vainceur.getScore(), plateau.getNom(), "--/--/--");
        BDD BDD = new BDD();
        BDD.insertScore(score);

        Console.ReadKey();
        demarer();

    }

    public static void modeSolo()
    {
        Joueur j1 = new Joueur(choisirPseudo("Joueur 1"), false);
        Joueur j2 = new Joueur("Ordinateur", true);

        Parcourt plateau = choisirPlateau();

        Partie partie = new Partie(j1, j2);

        Joueur vainceur = partie.start();
        IHM.ecrantDeFin(vainceur);

        if (!vainceur.estOrdinateur())
        {
            Score score = new Score(vainceur.getPseudo(), vainceur.getScore(), plateau.getNom(), "--/--/--");
            BDD BDD = new BDD();
            BDD.insertScore(score);
        }
        

        Console.ReadKey();
        demarer();

    }

    public async static void modeEnLigne() 
    {
        IHM.afficheServer();
        Console.WriteLine("before switch mode en ligne");
        bool taskEnd = false;

        while (!taskEnd)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    await startServer();
                    Console.ReadKey();
                    break;

                case ConsoleKey.LeftArrow:
                    taskEnd = await TCPClient();
                    Console.WriteLine("outta TCPCLient");
                    Console.ReadKey();
  
                    break;

                case ConsoleKey.Escape:
                    demarer();
                    Console.WriteLine("after escpae in mode en ligne");
                    break;

                default:
                    modeEnLigne();
                    Console.WriteLine("after default in mode en ligne");
                    break;
            }
            Console.WriteLine("outta swith modeEnLigne");
            Console.ReadKey();
        }

    }

    public static async Task<bool> TCPClient()
    {
        Console.WriteLine("Connecting...");
        const int REMOTE_PORT = 11000;
        var remoteIP = IPAddress.Parse("127.0.0.1");
        var ep = new IPEndPoint(remoteIP, REMOTE_PORT);


       // var entry = choisirPseudo("Joueur 2");
        var entry = Console.ReadLine();
        //IHM.afficheEcrantChoixPlateau();
        client = new TcpClient();
        await client.ConnectAsync(ep);
        await using var stream = client.GetStream();
        int count = 0; 

        client.SendTimeout = 60 * 1000; 
        client.ReceiveTimeout= 60 * 1000;
        stream.Socket.ReceiveTimeout = 60 * 1000;
        stream.Socket.SendTimeout= 60 * 1000;

        while (stream.CanRead)
        {
            
            byte[] buffer = new byte[1024];
            //var data = Encoding.UTF8.GetBytes(entry.KeyChar.ToString());
            var data = Encoding.UTF8.GetBytes(entry);

            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
            Console.WriteLine(entry);

            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Message from server: {message}");

            //choisirPseudo("Joueur 2");
            
            entry = Console.ReadLine();
            count ++;
            Console.WriteLine("wilhe count : " + count + 1);

        }
        return true;
        Console.WriteLine("Quitting");
        Console.ReadKey();
        
    }

    public static string? nouvelleAction()
    {
        return Console.ReadLine();
    }

    public static async Task<bool> startServer()
    {
        var srv = new Server();
        srv.MessageReceived += Srv_MessageReceived;

        void Srv_MessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            var msg = Encoding.UTF8.GetString(e.RawData);
            Console.WriteLine($"Received from {e.ClientEndPoint}");
            Console.WriteLine(msg);
        }

        await srv.StartAsync();

        Console.ReadLine();

        Console.WriteLine("Arret du serveur");
        srv.Stop();

        Console.ReadLine();
        return true;
    }

    public static string choisirPseudo(string JoueurUnOuDeux)

    {
        IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux);
        

        bool pseudoChoisi = false;
        string pseudo = "";


        while (!pseudoChoisi)
        {
            var entre = Console.ReadKey();
            
            switch (entre.Key)
            {
                case ConsoleKey.Escape:
                    JeuDeLoie.demarer();
                    break;

                case ConsoleKey.Backspace:
                    
                    pseudo = "";
                    IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux);
                    break;

                case ConsoleKey.Enter:
                    pseudoChoisi = true;
                    break;


                case ConsoleKey.RightArrow:
                    incrementeParcourtSelectionne();
                    IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux, pseudo);
                    break;


                case ConsoleKey.LeftArrow:
                    decrementeParcourtSelectionne();
                    IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux, pseudo);
                    break;

                default:
                    
                    IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write($"pseudo : {entre.Key}");
                    pseudo += entre.Key + Console.ReadLine();
                    IHM.afficheEcranChoisirPseudo(JoueurUnOuDeux, pseudo);
                    break;

            }
        }

        return pseudo;  

    }

    public static Parcourt choisirPlateau()
    {
        bool aChoisi = false;
        IHM.afficheEcrantChoixPlateau();

        while (!aChoisi)
        {
            IHM.afficheEcrantChoixPlateau();

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    incrementeParcourtSelectionne();
                    break;

                case ConsoleKey.LeftArrow:
                    decrementeParcourtSelectionne();
                    break;

                case ConsoleKey.Escape:
                    modeSolo();
                    break;

                case ConsoleKey.Enter:

                    aChoisi= true;  
                    break;
            }
        }

        return parcourts[parcourtSelectionne];

    }

    public static void incrementeParcourtSelectionne() {

        if(!(parcourtSelectionne == parcourts.Length -1))
            parcourtSelectionne += 1; 
    }

    public static void decrementeParcourtSelectionne()
    {

        if (!(parcourtSelectionne == 0))
            parcourtSelectionne += -1;
    }

}
