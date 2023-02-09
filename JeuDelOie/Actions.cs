/// <summary>
/// Cette class référence des actions général sur le plateaux de jeux 
/// </summary>
public static partial class Actions
{
    /// <summary>
    /// Renvoie un set de deux dés
    /// </summary>
    /// <returns></returns>
    public static int[] lancerDes()
    {
        int[] resultat= new int[2];
        Random r = new Random();

        for (int i = 0; i < 2; i++)
        {
           resultat[i] = r.Next(1, 7);
        }

        return resultat;
    }

    /// <summary>
    /// Echange la position des deux joueurs sur le plateau de jeu
    /// </summary>
    /// <param name="context"></param>
    public static void echangejoueur(Context context)
    {
        var des = context.getLanceDeDes();
         context.getJoueurEnAttente().saut(context.getJoueurEnAttente().getCaseEnCour() - (des[0] + des[1]));
    }

    /// <summary>
    /// Modifie le tour d'un joueur pour qu'il le passe en prison
    /// </summary>
    /// <param name="joueur"></param>
    public static void tourPrison(Joueur joueur)
    {
        
        int[] resultat = lancerDes();
        int res = resultat[0] + resultat[1];
        pause(joueur);
        Console.WriteLine("\t\t...");
        pause(joueur);
        Console.WriteLine("\t\t...");
        pause(joueur);
        Console.WriteLine("\t\t...");
        Console.WriteLine();
        pause(joueur);

        if (res == 10 | res == 11 | res == 12)
        {
            Console.WriteLine($"\t\tBravo ! Tu as fait {resultat[0] + resultat[1]} tu sors de prison :D");
            joueur.setPrison(false);

        }
        else
        {
            Console.WriteLine($"\t\tDommage ! Tu as fait {resultat[0] + resultat[1]} tu restes en prison :'(");
        }
        pause(joueur, true);
    }


    /// <summary>
    /// Bloque le processus de l'application jusqu'à la pression d'un touche du clavier
    /// Par default ne block pas l'applicaiton quand le joueur est un ordinateur 
    /// Cependant stopOrdi permet de modifier ce comportement
    /// </summary>
    /// <param name="joueur"></param>
    /// <param name="stopOrdi"></param>
    public static void pause(Joueur joueur, bool stopOrdi = true)
    {
        if (!(joueur.estOrdinateur()) || joueur.estOrdinateur() && !stopOrdi)
        {
            Console.ReadKey();
        }
        
    }


}

