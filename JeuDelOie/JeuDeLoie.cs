// See https://aka.ms/new-console-template for more information


using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public static class JeuDeLoie
{
    public static Parcourt plateau1 = new Parcourt();
    public static Parcourt plateau2 = new Parcourt("Someil des Dieux");
    public static Parcourt plateau3 = new Parcourt("Aurore Eternel");
    public static Parcourt plateau4 = new Parcourt("Profond Soupire");
    public static int parcourtSelectionne = 0;
    public static List<List<Score>> scoreBoard = new List<List<Score>>();   

    public static Parcourt [] parcourts = { plateau1, plateau2, plateau3, plateau4 } ;
    
    public static void demarer()
    {
        scoreBoard.Clear();
        scoreBoard = getscoreBoard();

        while (true)
        {
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

                    choisirModeDeJeu();
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

    public static void choisirModeDeJeu()
    {
        IHM.afficheModeDeJeu();

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.RightArrow:
                modeMultiJoueur();
                break;

            case ConsoleKey.LeftArrow:
                modeSolo();
                break;

            case ConsoleKey.UpArrow:
                modeSolo();
                break;

            default:
                choisirModeDeJeu();
                break;
        }
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

        Score score = new Score(vainceur.getPseudo(), vainceur.getScore(), plateau.getNom(), "--/--/--");
        BDD BDD = new BDD();
        BDD.insertScore(score);

        Console.ReadKey();
        demarer();

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

        if( !(parcourtSelectionne == parcourts.Length -1))
            parcourtSelectionne += 1; 
    }

    public static void decrementeParcourtSelectionne()
    {

        if (!(parcourtSelectionne == 0))
            parcourtSelectionne += -1;
    }

}


