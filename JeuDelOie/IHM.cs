
public static class IHM
{
    public static void afficheRegle(string regle)
    {
        Console.WriteLine(regle);
    }

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

    }

    public static void afficheScoreDes(int[] lancerDes)
    {
        Console.WriteLine($"Dés : {lancerDes[0]} - {lancerDes[1]}");
    }

    public static void signalPenalite(Joueur joueur)
    {
        Console.Clear();
        Console.WriteLine($"{joueur.getPseudo()} tu ne peux pas jouer il te reste {joueur.getTourDePenalite()+1} de penalité ! :(");
        Console.ReadLine();
    }

    public static void signalPrison(Joueur joueur) {

        Console.Clear();
        Console.WriteLine($"{joueur.getPseudo()} tu est en prison ! ");
        Console.WriteLine("Tu dois faire 10 - 11 - 12 au choix pour sortir ! ");
    }
}




