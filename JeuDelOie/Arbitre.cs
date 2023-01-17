 public static class Arbitre
{

    public static bool peutJouer(Joueur joueur)
    {
        if (joueur.getPrison())
        {   
            IHM.signalPrison(joueur);   
            Actions.tourPrison(joueur);
            return false;
        }

        if(joueur.getTourDePenalite() > 0)
        {   
            joueur.passeTourDePenalite();
            IHM.signalPenalite(joueur);
            return false;
        }

        return true;   
    }

    public static void lisRegle(Context context, Parcourt parcourt)
    {
        string regle = "";
        if (parcourt.getPlateau().ContainsKey(context.getJoueurEnCour().getCaseEnCour()))
        {
            regle = $"Règle : {parcourt.getRegle().GetValueOrDefault(context.getJoueurEnCour().getCaseEnCour())}";

        }
        else
        {
            regle = $"Règle : /";
        }
        
        IHM.afficheRegle(regle);

          
    }

    public static void appliqueRegle(Context context, Parcourt parcourt)
    {   
        if (parcourt.getPlateau().ContainsKey(context.getJoueurEnCour().getCaseEnCour()))
        {
            var regle = parcourt.getPlateau().GetValueOrDefault(context.getJoueurEnCour().getCaseEnCour());

            if (regle != null)
            {
                regle(context);

                //En cas de contestation - relire les règles du jeux (toute action est irrévocable)
                Console.Clear();
                IHM.finDeTourDescription(context);
                lisRegle(context, parcourt);
            }
        }
    }    



}

    


