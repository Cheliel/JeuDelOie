// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

public class Parcourt
{

    public string nom { get; set; }

    public Dictionary<int, Func<Context, Context>> plateau { get; set; }

    public Dictionary<int, string> regles { get; set; }

    public Parcourt()
    {
        this.initialisation();
    }

    private void initialisation()
    {
        this.initialiseRegle();
        this.initalisePlateau();
    }

    private void initialiseRegle()
    {
        this.regles = new Dictionary<int, string>();

        this.regles.Add(6, "Pont : Aller à la case 12");
        this.regles.Add(9, "Oie: Avancer du même nombre tiré. Lors du premier tour, si la case est atteinte par 6 + 3 aller en case 26, si la case est atteinte par 5 + 4 aller à la case 55.");
        this.regles.Add(18, "Oie : Avancer du même nombre tiré");
        this.regles.Add(20, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(27, "Oie : Avancer du même nombre tiré");
        this.regles.Add(31, "Puit : Tirer 10, 11, 12, pour sortir");
        this.regles.Add(36, "Oie : Avancer du même nombre tiré");
        this.regles.Add(42, "Porte close : Retourne à 30");
        this.regles.Add(45, "Oie : Avancer du même nombre tiré");
        this.regles.Add(52, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(54, "Oie : Avancer du même nombre tiré");
        this.regles.Add(58, "Tête de mort : Retourner à la case 1");
        this.regles.Add(63, "Fin de partie");

    }

    private void initalisePlateau()
    {
        this.plateau = new Dictionary<int, Func<Context, Context>>();

        this.plateau.Add(6, regle_case_6);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, regle_case_20);
        this.plateau.Add(27, regle_de_loie);
        this.plateau.Add(31, prison);
        this.plateau.Add(36, regle_de_loie);
        this.plateau.Add(42, regle_case_42);
        this.plateau.Add(45, regle_de_loie);
        this.plateau.Add(52, prison);
        this.plateau.Add(54, regle_de_loie);
        this.plateau.Add(58, regle_case_58);
        this.plateau.Add(63, Fin);
    }

    Func<Context, Context> regle_case_6 = (Context context) =>
    {
        context.getJoueurEnCour().saut(12);
        return context;
    };


    Func<Context, Context> regle_case_9 = (Context context) =>
    {
        if (context.getJoueurEnCour().getScore() < 12)
        {
            if (context.getLanceDeDes().Contains(6) && context.getLanceDeDes().Contains(3))
            {
                context.getJoueurEnCour().saut(26);
                return context;
            }

            if (context.getLanceDeDes().Contains(5) && context.getLanceDeDes().Contains(4))
            {
                context.getJoueurEnCour().saut(55);
                return context;
            }
        }

        context.getJoueurEnCour().avance(context.getLanceDeDes()[0] + context.getLanceDeDes()[1]);
        return context;
    };


    Func<Context, Context> regle_de_loie = (Context context) =>
    {
        context.getJoueurEnCour().avance(context.getLanceDeDes()[0] + context.getLanceDeDes()[1]);
        return context;
    };

    Func<Context, Context> regle_case_20 = (Context context) =>
    {
        context.getJoueurEnCour().ajouteToursDePenalite(2);
        return context;
    };

    Func<Context, Context> prison = (Context context) =>
    {
        context.getJoueurEnCour().setPrison(true);
        return context;
    };

    Func<Context, Context> regle_case_42 = (Context context) =>
    {
        context.getJoueurEnCour().saut(30);
        return context;
    };

    Func<Context, Context> regle_case_58 = (Context context) =>
    {
        context.getJoueurEnCour().saut(1);
        return context;
    };

    Func<Context, Context> Fin = (Context context) =>
    {
      
        context.setPartieGagner();
        return context;
    };

    public Dictionary<int, Func<Context, Context>> getPlateau() { return this.plateau; }

    public Dictionary<int, string> getRegle() { return this.regles; }





}

