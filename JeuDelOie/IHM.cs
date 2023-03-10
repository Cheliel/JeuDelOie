
using System.Net.WebSockets;
using static System.Formats.Asn1.AsnWriter;

public static partial class IHM
{
    public static string construitRegle(string regle)
    {

        return
            "\t\t  |~~╔═════════════════════════════════════════════════════════════════════╗~~|\n" +
           $"\t\t  |~~║   {regle.PadRight(63)}   ║~~|\n" +
            "\t\t  |~~╚╗___________________________________________________________________╔╝~~|\n";
            
    }

    public static void signalPenalite(Context context, Joueur j1)
    {
 

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        if(context.getJoueurEnCour() == j1)
        {
            Console.ForegroundColor= ConsoleColor.Blue;    
            Console.Write($"\t\t{context.getJoueurEnCour().getPseudo()}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\t\t{context.getJoueurEnCour().getPseudo()}");

           
        }

        Console.ResetColor();
        Console.Write($" tu ne peux pas jouer il te reste {context.getJoueurEnCour().getTourDePenalite() + 1} tour de penalité ! :(");
        Console.WriteLine();
        Console.WriteLine();


        affichePrison();

    }

    public static void signalPrison(Context context, Joueur j1) {

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        if (context.getJoueurEnCour().Equals(j1))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\t\t{context.getJoueurEnCour().getPseudo()}");   
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"\t\t{context.getJoueurEnCour().getPseudo()}");
        }
        Console.ResetColor();   
        Console.Write(" tu est en prison ! ");
        Console.WriteLine();
        Console.WriteLine();

        affichePrison();

        Console.WriteLine();
        Console.WriteLine("\t\tTu dois faire 10 - 11 - 12 au choix pour sortir ! ");
        Console.WriteLine();
    }

    public static String construitSingleScoreBoard(Score score)
    {

        return  $"\t\t ╔|══════════════════════════════════════════════════╦╦══════════╦════════════|╗\r\n " +
                $"\t\t ║| {score.getPseudo().PadRight(35)}              ║║   {score.getScore().ToString().PadRight(3)}    ║  {score.formatDatePourAffichage().PadRight(8)}  |║\r\n " +
                $"\t\t ╚|══════════════════════════════════════════════════╩╩══════════╩════════════|╝\n";
    }

    public static string contruitNomPlateau()
    {

        return
            $"\t\t   ___________________________________________________________________________\n" +
            $"\t\t  |\\                                                                         /|\n" +
            $"\t\t  |/\t\t\t\t{JeuDeLoie.parcourts[JeuDeLoie.parcourtSelectionne].nom.PadRight(45)}\\|\n"+ 
            $"\t\t  |___________________________________________________________________________|\n";

    }

    public static string construitScoreBoad(List<Score> scores)
    {
        Score score = new Score();
        
        if (scores.Count == 0)
        {   
            
            return $"{contruitNomPlateau()}" +
           $"{construitSingleScoreBoard(score)}" +
           $"{construitSingleScoreBoard(score)}" +
           $"{construitSingleScoreBoard(score)}" +
           $"{construitSingleScoreBoard(score)}" +
           $"{construitSingleScoreBoard(score)}";
        }
        else if(scores.Count < 5 )
        {
            string affichage = $"{contruitNomPlateau()}";

            for (int i = 0; i < 5; i++)
            {
                if (i <= scores.Count - 1)
                {
                    affichage += construitSingleScoreBoard(scores[i]);
                    continue;
                }

                affichage += construitSingleScoreBoard(score);
            }
            return affichage;

        }else
        {
            string affichage = $"{contruitNomPlateau()}";

            for (int i = 0; i < scores.Count - 1; i++)
            {
                if (scores[i] != null)
                {
                    affichage += construitSingleScoreBoard(scores[i]);
                }

            }
            return affichage;
        }
    }

    public static void construitPlateau(Parcourt plateau, Joueur j1, Joueur j2)
    {

        // gerer le switch entre jouer 1 et joueur 2 dans le context 


        Console.Write("\t\t  |~~\\~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~/~~|\n");

        for (int i = 0; i <= 6; i++)
        {
            Console.Write("\t\t  |~~|");

            for (int y = 0; y <= 9; y++)
            {
                var stringNumberCase = i.ToString() + y.ToString();
                int numberCase = 0;

                if (!int.TryParse(stringNumberCase, out numberCase))
                {
                    //return "Erreur d'affichage : Convertion impossible !";
                    Console.Write("ko");
                    
                }

                if(i == 6 && y == 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("~{");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♥♥");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("}~");
                    Console.ResetColor();
                    Console.Write("|");
                    
                    //Console.Write("~{♥♥}~|");
                    continue;
                }

                if(i == 6 && y > 3)
                {
                    Console.Write("      |");
                    continue;
                }

                if (numberCase == j1.getCaseEnCour() && numberCase == j2.getCaseEnCour())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  J");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("J  ");
                    Console.ResetColor();
                    Console.Write("|");
                    continue;
                }
                else if (numberCase == j1.getCaseEnCour())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  J1  ");
                    Console.ResetColor();
                    Console.Write("|");
                    continue;

                }
                else if (numberCase == j2.getCaseEnCour())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("  J2  ");
                    Console.ResetColor();
                    Console.Write("|");
                    continue;
                }

                if (plateau.getRegle().ContainsKey(numberCase))
                {   
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("~{");
                    Console.ResetColor();
                    Console.Write(stringNumberCase);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("}~");
                    Console.ResetColor();
                    Console.Write("|"); 
                    //Console.Write($"~{{{stringNumberCase}}}~|");
                    continue;
                }

                if (!plateau.getRegle().ContainsKey(numberCase))
                {
                    Console.Write($"  {stringNumberCase}  |");
                    continue;
                }
            }

            Console.Write("~~|\n");

        
        }
        Console.Write("\t\t  |~~/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\~~|\n" +
                                "\t\t  |~~\\~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~/~~|\n");
    }

 
    public static void afficheEchapEtSuppr()
    {
        Console.WriteLine("\t\t╔═════╗  \t\t\t\t\t\t\t     ╔════════╗ \r\n" +
                          "\t\t║Echap║  \t\t\t\t\t\t\t     ║ Suppr. ║ \r\n" +
                          "\t\t╚═════╝  \t\t\t\t\t\t\t     ╚════════╝ \r\n" +
                          "\t\t ");
    }

    public static void affichePrison()
    {
        Console.Write("\t\t╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗\n");
        Console.Write("\t\t║       ║   ║   ║   ║   ║   ║   ║   ║   ║\n");
        Console.Write("\t\t║   ║   ║   ║   ║   ║   ║   ║       ║   ║\n");
        Console.Write("\t\t╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣\n");
        Console.Write("\t\t║   ║   ║   ║               ║   ║   ║   ║\n");
        Console.Write("\t\t║   ║   ║                   ║   ║   ║   ║\n");
        Console.Write("\t\t╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣\n");
        Console.Write("\t\t║   ║       ║   ║   ║   ║   ║   ║   ║   ║\n");
        Console.Write("\t\t║   ║   ║   ║   ║   ║   ║   ║   ║       ║\n");
        Console.Write("\t\t╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝\n");
    }

    public static void afficheChoixScoreBoard()
    {
        Console.WriteLine($"\t\t\t\t\t\t ╔═════╦═╦═════╗\n" +
                          $"\t\t\t\t\t\t ║  ◄  ║~║  ►  ║\n" +
                          $"\t\t\t\t\t\t ╚═════╩═╩═════╝");
    }

    public static void afficheNouvellePartie()
    {
        Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t        ╔══════╗ \n" +
                         $"\t\t\t\t\t\t╔═══════════════╗                       ║Entrée║ \n" +
                         $"\t\t\t\t\t\t║Nouvelle Partie║                       ╚══╗   ║ \n" +
                         $"\t\t\t\t\t\t╚═══════════════╝                          ║   ║ \t\n" +
                         $"\t\t\t\t\t\t\t\t\t\t           ╚═══╝");
        
        
    }

    public static void afficheDemarerPartie()
    {
        Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t        ╔══════╗ \n" +
                         $"\t\t\t\t\t\t╔════════════════╗                      ║Entrée║ \n" +
                         $"\t\t\t\t\t\t║ Demarer Partie ║                      ╚══╗   ║ \n" +
                         $"\t\t\t\t\t\t╚════════════════╝                         ║   ║ \t\n" +
                         $"\t\t\t\t\t\t\t\t\t\t           ╚═══╝");
    }

    public static void afficheJoueurs(Context context, Joueur j1, Joueur j2)
    {

        var des = context.getLanceDeDes();
        var d1 = des[0].ToString();
        var d2 = des[1].ToString();

        Console.Write("\t\t  |~          ╔══════════╗             |~|           ╔══════════╗            ~|\n");
        Console.Write("\t\t ╔|═══════════╝ ");
        
        if(j1 == context.getJoueurEnCour())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Joueur 1");
            Console.ResetColor();
            Console.Write(" ╚═════════════|~|═══════════╝ Joueur 2 ╚═════════════|╗\n");
        }
        else if (j2 == context.getJoueurEnCour())
        {
            Console.Write("Joueur 1 ╚═════════════|~|═══════════╝ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Joueur 2");
            Console.ResetColor();
            Console.Write(" ╚═════════════|╗\n");
        }

        if (j1 == context.getJoueurEnCour())
        {
            Console.Write("\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| {j1.getPseudo().PadRight(35)}|~| {j2.getPseudo().PadRight(35)}|║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Score : {j1.getScore().ToString().PadRight(5)}                      |~| Score : {j2.getScore().ToString().PadRight(5)}                      |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Case :  {j1.getCaseEnCour().ToString().PadRight(5)}                      |~| Case : {j2.getCaseEnCour().ToString().PadRight(5)}                       |║\n" +
              "\t\t ║|                                ╔═══|~|═══╗                                |║\n" +
              "\t\t ╚|════════════════════════════════╦═══|~|═══╬════════════════════════════════|╝\n");

            Console.Write($"\t\t                                   ║ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{d1} ");
            Console.ResetColor();
            Console.Write("|~|");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" {d2}");
            Console.ResetColor();
            Console.Write(" ║\n" +
              " \t\t                                   ╚═══|~|═══╝ \n");
        }
        else
        {
            Console.Write("\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| {j1.getPseudo().PadRight(35)}|~| {j2.getPseudo().PadRight(35)}|║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Score : {j1.getScore().ToString().PadRight(5)}                      |~| Score : {j2.getScore().ToString().PadRight(5)}                      |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Case :  {j1.getCaseEnCour().ToString().PadRight(5)}                      |~| Case : {j2.getCaseEnCour().ToString().PadRight(5)}                       |║\n" +
              "\t\t ║|                                ╔═══|~|═══╗                                |║\n" +
              "\t\t ╚|════════════════════════════════╬═══|~|═══╦════════════════════════════════|╝\n");
            Console.Write($"\t\t                                   ║ ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{d1} ");
            Console.ResetColor();
            Console.Write("|~|");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" {d2}");
            Console.ResetColor();
            Console.Write(" ║\n" +
              " \t\t                                   ╚═══|~|═══╝ \n");
      
/*
             $"\t\t                                   ║ {d1} |~| {d2} ║\n" +
              "\t\t                                   ╚═══|~|═══╝ \n"*/

        
        }


    }

    public static void affichePlateau(Context context, Parcourt plateau, Joueur j1, Joueur j2, string regle="Règle => ")
    {
        Console.Clear();
        Console.Write(
            contruitNomPlateau()
            ); construitPlateau(plateau, j1, j2);

        Console.Write(construitRegle(regle));
        afficheJoueurs(context, j1, j2);
    }

    public static void afficheChoisirPseudo(string positionJoueur, string pseudo = "...")
    {
        Console.WriteLine($"\t\t░░░░░░░▀▄░░░▄▀░░░░░░░░\n" +
                          $"\t\t░░░░░░▄█▀███▀█▄░░░░░░░ {positionJoueur.PadLeft(10)} \t\t\t\t       ╔══════╗ \n" +
                          $"\t\t░░░░░█▀███████▀█░░░░░░  ╔═══════════════════════════════════╗          ║Entrée║\n" +
                          $"\t\t░░░░░█░█▀▀▀▀▀█░█░░░░░░  ║{pseudo.PadRight(35)}║          ╚══╗   ║\n" +
                          $"\t\t░░░░░░░░▀▀░▀▀░░░░░░░░░  ╚═══════════════════════════════════╝             ║   ║\n" +
                          $"\t\t\t\t\t\t\t\t\t\t\t  ╚═══╝");
    }

    public static void afficheModeDeJeu()
    {
        Console.Clear();   
        Console.WriteLine("\n\n\n\n\n\n\n\n");

        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════╗            ╔═════╗\n" +
                          "\t\t\t\t║ Solo                              ╟────────────╢  ◄  ║\n " +
                          "\t\t\t\t╚═══════════════════════════════════╝            ╚═════╝\n");

        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════╗            ╔═════╗\n" +
                          "\t\t\t\t║ Multijoueur                       ╟────────────╢  ►  ║\n " +
                          "\t\t\t\t╚═══════════════════════════════════╝            ╚═════╝\n");

        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════╗            ╔═════╗\n" +
                          "\t\t\t\t║ En ligne                          ╟────────────╢  ▲  ║\n " +
                          "\t\t\t\t╚═══════════════════════════════════╝            ╚═════╝\n");

    }

    public static void afficheEchap()
    {
        Console.WriteLine("\t\t ╔═════╗\r\n" +
                          "\t\t ║Echap║\r\n  " +
                          "\t\t ╚═════╝\r\n   " +
                          "\t\t ");
    }

    public static void afficheEcrantDAccueil()
    {
        Console.Clear();
        Console.WriteLine();
        Console.Write(construitScoreBoad(JeuDeLoie.scoreBoard[JeuDeLoie.parcourtSelectionne]));
        afficheChoixScoreBoard();
        afficheNouvellePartie();

    }

    public static void afficheEcranChoisirPseudo(string positionJoueur, string nomjeur="...")
    {
        Console.Clear();
        Console.WriteLine();
        //Console.Write(construitScoreBoad(JeuDeLoie.scoreBoard[JeuDeLoie.parcourtSelectionne]));
        Console.Write(construitScoreBoad(JeuDeLoie.scoreBoard[JeuDeLoie.parcourtSelectionne]));
        afficheChoixScoreBoard();
        afficheChoisirPseudo(positionJoueur, nomjeur);
        afficheEchapEtSuppr();
    }

    public static void afficheEcrantChoixPlateau()
    {
        Console.Clear(); 
        Console.WriteLine("\n");
        Console.Write(construitScoreBoad(JeuDeLoie.scoreBoard[JeuDeLoie.parcourtSelectionne]));
        afficheChoixScoreBoard();
        afficheDemarerPartie();
        afficheEchap();

    }

    public static void ecrantDeFin(Joueur vainqueur)
    {
        Console.Clear();
        Console.WriteLine("\n\n\n\n");
        Console.Write($"Félicitaion ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"{vainqueur.getPseudo()}");
        Console.ResetColor();
        Console.Write("! ");
        Console.WriteLine($"Vous avez gagner avec un score de {vainqueur.getScore()}");
        Console.WriteLine("\n\n");
        Console.WriteLine("\n\n░░░░░░░▀▄░░░▄▀░░░░░░░░" +
            "              \r\n░░░░░░▄█▀███▀█▄░░░░░░░" +
            "              \r\n░░░░░█▀███████▀█░░░░░░" +
            "              \r\n░░░░░█░█▀▀▀▀▀█░█░░░░░░" +
            "              \r\n░░░░░░░░▀▀░▀▀░░░░░░░░░");

    }

    public static void OieDeDebut()
    {
        Console.Clear();
        Console.WriteLine("\n\n");
        Console.WriteLine("\t\t\t\t\t                          ...                 \r\n" +
            "\t\t                                               .7??YJJ?^..            \r\n" +
            "\t\t                                              !5!.:J^ 7&???7!^.       \r\n" +
            "\t\t   ╔═══════════════════════════╗               ^B.     .?#^^~7JPY       \r\n" +
            "\t\t   ║       Jeu De l'Oie        ║             JP    :JJ?77!~~^:.       \r\n" +
            "\t\t   ║                           ║             YP   ^P!                 \r\n" +
            "\t\t   ║ Correcteur - Tarik Ouhali ║             YP   G7                  \r\n" +
            "\t\t   ╚═══════════════════════════╝             PJ   B!                  \r\n" +
            "\t\t                                             G7   B!                  \r\n" +
            "\t\t                                             G7   B!                  \r\n" +
            "\t\t                                             G~   B7                  \r\n" +
            "\t\t                                            :B.   55                  \r\n" +
            "\t\t                                           ^P!    ^#:                 \r\n" +
            "\t\t                                  ..:::^~7JJ^      PJ                 \r\n" +
            "\t\t                        .:^~77?JJ????77!~^.        ~#.                \r\n" +
            "\t\t                   .^!?JYJ?7~^:..                   B?                \r\n" +
            "\t\t               :~?JYJ!^.                            JP                \r\n" +
            "\t\t           :~?YY7^.                                 7B                \r\n" +
            "\t\t:..   :^!?YY7^.                                     7B                \r\n" +
            "\t\t?JYBPYJ7~^.     .^~            ..                   YP                \r\n" +
            "\t\t   ?YJ??77?J55YY55?~!?Y:  .:~7JY!                  :#~                \r\n" +
            "\t\tJJ?777?JJ?!7JJJ??J##5YJ?JJJ?7~:                   ~G7                 \r\n" +
            "\t\t .:^^^^^^~?Y?^    :^^:::.                      .!Y5^    ╔═══════════════════╗            \r\n" +
            "\t\t            ^?JJ!:                        .:~?YY7^      ║ Etienne Pernon    ║       \r\n" +
            "\t\t               :!?JJ7!^:..          :7J?JJJ?7~:         ║ CCI Campus : M2II ║          \r\n" +
            "\t\t                   .^!7?JJJ5.     ~G#&?...              ╚═══════════════════╝       \r\n" +
            "\t\t                          .JY!^^!JG@J#.                               \r\n" +
            "\t\t                            .J#?&^ 5YG?                               \r\n" +
            "\t\t                             ^#~#.  #YB!^^~^~~^.                      \r\n" +
            "\t\t                             ~B:#^.JG~^??7!PB5J:                      \r\n" +
            "\t\t                            .!5G?^?JY#GGP?J?J#J                       \r\n" +
            "\t\t                            .5G5?7?YJ^.J##^ .::^.  ");
    }
}



                        
