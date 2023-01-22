
using System.Net.WebSockets;

public static class IHM
{
    public static string construitRegle(string regle)
    {
        //gérer le cas des règle plus longue que 60 

        return
            "\t\t  |~~╔═════════════════════════════════════════════════════════════════════╗~~|\n" +
           $"\t\t  |~~║   {regle.PadRight(63)}   ║~~|\n" +
            "\t\t  |~~╚╗___________________________________________________________________╔╝~~|\n";
            
    }

/*
    public static void debutDeTourDescription(Context context)
    {
        Console.Clear();
        Console.WriteLine($"Tour de : {context.getJoueurEnCour().getPseudo()}");
        Console.WriteLine($"Score : {context.getJoueurEnCour().getScore()}");
        Console.WriteLine($"Case : {context.getJoueurEnCour().getCaseEnCour()}");
        

    }

    public static void finDeTourDescription(Context context)
    {
        Console.Clear();
        Console.WriteLine($"Tour de : {context.getJoueurEnCour().getPseudo()}");
        Console.WriteLine($"Score : {context.getJoueurEnCour().getScore()}");
        Console.WriteLine($"Case : {context.getJoueurEnCour().getCaseEnCour()}");
        IHM.afficheScoreDes(context.getLanceDeDes());

    }*/

    public static void afficheScoreDes(int[] lancerDes)
    {
        Console.WriteLine($"Dés : {lancerDes[0]} - {lancerDes[1]}");
    }

    public static void signalPenalite(Joueur joueur)
    {
        Console.Clear();
        Console.WriteLine($"{joueur.getPseudo()} tu ne peux pas jouer il te reste {joueur.getTourDePenalite()+1} de penalité ! :(");
        
    }

    public static void signalPrison(Joueur joueur) {

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"\t\t{joueur.getPseudo()} tu est en prison ! ");
        Console.WriteLine();

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

        Console.WriteLine();
        Console.WriteLine("\t\tTu dois faire 10 - 11 - 12 au choix pour sortir ! ");
        Console.WriteLine();
    }

    public static string contruitNomPlateau(Parcourt plateau)
    {

        return
            $"\t\t   ___________________________________________________________________________\n" +
            $"\t\t  |\\                                                                         /|\n" +
            $"\t\t  |/\t\t\t\t{plateau.nom.PadRight(43)}  \\|\n"+ 
            $"\t\t  |___________________________________________________________________________|\n";

    }

    public static void construitPlateau(Context context, Parcourt plateau, Joueur j1, Joueur j2)
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
                    Console.Write("~{♥♥}~|");
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
                    Console.Write($"~{{{stringNumberCase}}}~|");
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

    public static void afficheJoueurs(Context context, Parcourt plateau, Joueur j1, Joueur j2)
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

         if(j1 == context.getJoueurEnCour())
        {
            Console.Write("\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| {j1.getPseudo().PadRight(20)}               |~| {j2.getPseudo().PadRight(20)}               |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Score : {j1.getScore().ToString().PadRight(5)}                      |~| Score : {j2.getScore().ToString().PadRight(5)}                      |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Case :  {j1.getCaseEnCour().ToString().PadRight(5)}                      |~| Case : {j2.getCaseEnCour().ToString().PadRight(5)}                       |║\n" +
              "\t\t ║|                                ╔═══|~|═══╗                                |║\n" +
              "\t\t ╚|════════════════════════════════╦═══|~|═══╬════════════════════════════════|╝\n" +
             $"\t\t                                   ║ {d1} |~| {d2} ║\n" +
              "\t\t                                   ╚═══|~|═══╝ \n"

              );
        }
        else
        {
            Console.Write("\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| {j1.getPseudo().PadRight(20)}               |~| {j2.getPseudo().PadRight(20)}               |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Score : {j1.getScore().ToString().PadRight(5)}                      |~| Score : {j2.getScore().ToString().PadRight(5)}                      |║\n" +
              "\t\t ║|                                    |~|                                    |║\n" +
             $"\t\t ║| Case :  {j1.getCaseEnCour().ToString().PadRight(5)}                      |~| Case : {j2.getCaseEnCour().ToString().PadRight(5)}                       |║\n" +
              "\t\t ║|                                ╔═══|~|═══╗                                |║\n" +
              "\t\t ╚|════════════════════════════════╬═══|~|═══╦════════════════════════════════|╝\n" +
             $"\t\t                                   ║ {d1} |~| {d2} ║\n" +
              "\t\t                                   ╚═══|~|═══╝ \n"

              );
        }


    }


    public static void affichePlateau(Context context, Parcourt plateau, Joueur j1, Joueur j2, string regle="Règle => ")
    {
        Console.Write(
            contruitNomPlateau(plateau)
            ); construitPlateau(context, plateau, j1, j2);

        Console.Write(construitRegle(regle));
        afficheJoueurs(context, plateau, j1, j2);
    }
}




