 public static class Arbitre
{

    public static bool peutJouer(Context context, Joueur j1)
    {
        if (context.getJoueurEnCour().getPrison())
        {   
            IHM.signalPrison(context, j1);   
            Actions.tourPrison(context.getJoueurEnCour());
            return false;
        }

        if(context.getJoueurEnCour().getTourDePenalite() > 0)
        {
            context.getJoueurEnCour().passeTourDePenalite();
            IHM.signalPenalite(context, j1);
            Actions.pause();
            return false;
        }

        return true;   
    }

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
        Actions.pause();
    }

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
                Actions.pause();
            }
        }
    }    

    public static void verifieSwitchJoueur(Context context)
    {
        if (context.getJoueurEnCour().getCaseEnCour().Equals(context.getJoueurEnAttente().getCaseEnCour()))
        {
            Actions.swtich(context);
            IHM.construitRegle($"Vous changez votre place avec le joueur {context.getJoueurEnAttente().getPseudo()}");
        }
    }

}

    


