// See https://aka.ms/new-console-template for more information
public static partial class Actions
{
    public static int[] lancerDes()
    {
        int[] resultat= new int[2];

        for (int i = 0; i < 2; i++)
        {
           resultat[i] = lancerLeDe();
        }

        return resultat;
    }

    private static int lancerLeDe()
    {
        Random r = new Random();
        var res = r.Next(0, 6);
        return res;
    }

    public static void swtich(Context context)
    {
        var des = context.getLanceDeDes();
         context.getJoueurEnAttente().saut(context.getJoueurEnAttente().getCaseEnCour() - (des[0] + des[1]));
    }

    public static void tourPrison(Joueur joueur)
    {
        
        int[] resultat = lancerDes();
        int res = resultat[0] + resultat[1];
        pause();
        Console.WriteLine("\t\t...");
        pause();
        Console.WriteLine("\t\t...");
        pause();
        Console.WriteLine("\t\t...");
        Console.WriteLine();
        pause();

        if (res == 10 | res == 11 | res == 12)
        {
            Console.WriteLine($"\t\tBravo ! Tu as fait {resultat[0] + resultat[1]} tu sors de prison :D");
            joueur.setPrison(false);

        }
        else
        {
            Console.WriteLine($"\t\tDommage ! Tu as fait {resultat[0] + resultat[1]} tu restes en prison :'(");
        }
        pause();
    }

    public static void pause()
    {
        Console.ReadKey();
        
    }


}

