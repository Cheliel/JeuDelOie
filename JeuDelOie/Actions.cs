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

    public static int swtich(Joueur joueur, Joueur joueurRejoint)
    {
        return 0;
    }

    public static void tourPrison(Joueur joueur)
    {

        Actions.pause();
        int[] resultat = lancerDes();

        if (resultat[0] + resultat[1] == (11 | 12 | 13))
        {
            joueur.setPrison(false);
            Console.WriteLine($"Dommage ! Tu as fait {resultat[0] + resultat[1]} :'(");
            Actions.pause();
        }

    }

    public static void pause()
    {
        Console.Read();
    }


}

