using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

/// <summary>
/// La Class JeuDeLoie contient la logique general de l'application : 
/// > affichage des score boards 
/// > création de partie : 
/// > > choix du mode de jeu 
/// > > choix des pseudo
/// > > choix du plateau
/// > Enregistre le score du gagnant 
/// </summary>
public static class JeuDeLoie

{

    public static Parcourt plateau1 = new Parcourt(Parcourt.Plateaux.TerreBrulee);
    public static Parcourt plateau2 = new Parcourt(Parcourt.Plateaux.SommeilDesDieux);
    public static Parcourt plateau3 = new Parcourt(Parcourt.Plateaux.AuroreEternel);
    public static Parcourt plateau4 = new Parcourt(Parcourt.Plateaux.ProfondSoupir);
    public static int parcourtSelectionne = 0;
    public static List<List<Score>> scoreBoard = new List<List<Score>>();   

    public static Parcourt [] parcourts = { plateau1, plateau2, plateau3, plateau4 } ;
    
    /// <summary>
    /// Renvoie la page principale de l'application
    /// </summary>
    public static void demarer()
    {
        scoreBoard.Clear();
        scoreBoard = getscoreBoard();

        IHM.OieDeDebut();
        Console.ReadKey();

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

    /// <summary>
    /// Charge les 5 meilleurs scores enregistrée sur chaque plateau
    /// Revnoie une List par plateau 
    /// Chaque List contient les 5 meilleurs joueurs 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Renvoie la page de choix du mode de jeu 
    /// </summary>
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

    /// <summary>
    /// Contient la logique pour la création d'un partie à deux joueurs
    /// > choix du pseudo pour le joueur 1
    /// > choix du pseudo pour le joueur 2
    /// > choix du plateau 
    /// > lance la partie
    /// </summary>
    public static void modeMultiJoueur()
    {
        Joueur j1 = new Joueur(choisirPseudo("Joueur 1"), false);
        Joueur j2 = new Joueur(choisirPseudo("Joueur 2"), false);

        Parcourt plateau = choisirPlateau();

        Partie partie = new Partie(j1, j2);

        Joueur vainceur = partie.start();
        IHM.ecrantDeFin(vainceur);

        Score score = new Score(vainceur.getPseudo(), vainceur.getScore(), plateau.getNom(), DateTime.Now);
        BDD BDD = new BDD();
        BDD.insertScore(score);

        Console.ReadKey();
        demarer();

    }

    /// <summary>
    /// Contient la logique pour la création d'un partie contre un ordinateur
    /// > choix du pseudo pour le joueur 1
    /// > choix du plateau 
    /// > lance la partie
    /// </summary>
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
        Score score = new Score(vainceur.getPseudo(), vainceur.getScore(), plateau.getNom(), DateTime.Now);
        BDD BDD = new BDD();
        BDD.insertScore(score);
        }

        Console.ReadKey();
        demarer();

    }

    /// <summary>
    /// Renvoie la page pour choisir le pseudo d'un Joueur
    /// </summary>
    /// <param name="JoueurUnOuDeux"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Renvoie la page pour choisir le plateau de jeu
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Activer par la touche -> du calvier 
    /// Passe au plateau suivant 
    /// </summary>
    public static void incrementeParcourtSelectionne() {

        if( !(parcourtSelectionne == parcourts.Length -1))
            parcourtSelectionne += 1; 
    }

    /// <summary>
    /// Activer par la touche <- du calvier 
    /// Passe au plateau précédent 
    /// </summary>
    public static void decrementeParcourtSelectionne()
    {

        if (!(parcourtSelectionne == 0))
            parcourtSelectionne += -1;
    }

}


