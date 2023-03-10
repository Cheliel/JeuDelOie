
/// <summary>
/// Cette class contient la logique permettant d'appliquer les règles d'un jeu de l'oie
/// </summary>

public static class Arbitre
{

    /// <summary>
    /// Au début d'un tout de jeu,
    /// définie si un joueur peut jouer un tour normal
    /// </summary>
    /// <param name="context"></param>
    /// <param name="joueur"></param>
    /// <returns></returns>
    public static bool peutJouer(Context context, Joueur joueur)
    {
        if (context.getJoueurEnCour().getPrison())
        {   
            IHM.signalPrison(context, joueur);   
            Actions.tourPrison(context.getJoueurEnCour());
            return false;
        }

        if(context.getJoueurEnCour().getTourDePenalite() > 0)
        {
            context.getJoueurEnCour().passeTourDePenalite();
            IHM.signalPenalite(context, joueur);
            Actions.pause(context.getJoueurEnCour());
            return false;
        }

        return true;   
    }

    /// <summary>
    /// Commande l'affichage d'une règle dans l'IHM,
    /// quand un joueur atteint une case spécial d'un plateau
    /// </summary>
    /// <param name="context"></param>
    /// <param name="parcourt"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    public static void lisRegle(Context context, Parcourt parcourt, Joueur j1, Joueur j2) 
    {
        string? regle = null;
        if (parcourt.getPlateau().ContainsKey(context.getJoueurEnCour().getCaseEnCour()))
        {
            regle = $"Règle => {parcourt.getRegle().GetValueOrDefault(context.getJoueurEnCour().getCaseEnCour())}";
        }
        
        if(regle != null) {
            IHM.affichePlateau(context, parcourt, j1, j2, regle);
        }
        else
        {
            IHM.affichePlateau(context, parcourt, j1, j2);
        }
    }

    /// <summary>
    /// Commande l'Action qui applique la règle sur laquel le joueur est tombé
    /// </summary>
    /// <param name="context"></param>
    /// <param name="parcourt"></param>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    public static void appliqueRegle(Context context, Parcourt parcourt, Joueur j1, Joueur j2)
    {   
        if (parcourt.getPlateau().ContainsKey(context.getJoueurEnCour().getCaseEnCour()))
        {
            var regle = parcourt.getPlateau().GetValueOrDefault(context.getJoueurEnCour().getCaseEnCour());

            if (regle != null)
            {
                regle(context);

                //En cas de contestation - relire les règles du jeux (What is done cannot be undone !)
                Console.Clear();
                lisRegle(context, parcourt, j1, j2);
            }
        }
    }    

    /// <summary>
    /// Vérifie si un joueur a atteint la case d'un autre
    /// Commande l'échange des positions des joueurs
    /// </summary>
    /// <param name="context"></param>
    public static void verifieSwitchJoueur(Context context)
    {
        if (context.getJoueurEnCour().getCaseEnCour().Equals(context.getJoueurEnAttente().getCaseEnCour()))
        {
            Actions.echangejoueur(context);
            IHM.construitRegle($"Vous changez votre place avec le joueur {context.getJoueurEnAttente().getPseudo()}");
        }
    }

}

    


