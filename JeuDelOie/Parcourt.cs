// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

public class Parcourt
{

    public string nom { get; set; }

    public Dictionary<int, Action<Context>> plateau { get; set; }

    public Dictionary<int, string> regles { get; set; }

    public Parcourt(string nom ="Terre Brulée")
    {
        this.regles = new Dictionary<int, string>();
        this.plateau = new Dictionary<int, Action<Context>>();
        this.initialisation();
        this.nom= nom;
    }

    private void initialisation()
    {
        this.initialiseRegle();
        this.initalisePlateau();
    }

    private void initialiseRegle()
    {
        this.regles.Add(6, "Pont : Aller à la case 12");
        //this.regles.Add(9, "Oie: Avancer du même nombre tiré. Lors du premier tour, si la case est atteinte par 6 + 3 aller en case 26, si la case est atteinte par 5 + 4 aller à la case 55.");
        this.regles.Add(9,  "Oie : Avancer du même nombre tiré. Premier tour : ???");
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
        this.plateau.Add(6, regle_case_6);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, penalite);
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

    Action<Context> regle_case_6 = (Context context) =>
    {
        context.getJoueurEnCour().saut(12);
    };

    Action<Context> regle_case_9 = (Context context) =>
    {
        if (context.getJoueurEnCour().getScore() < 12)
        {
            if (context.getLanceDeDes().Contains(6) && context.getLanceDeDes().Contains(3))
            {
                context.getJoueurEnCour().saut(26);
            }

            if (context.getLanceDeDes().Contains(5) && context.getLanceDeDes().Contains(4))
            {
                context.getJoueurEnCour().saut(55);
            }
        }

        if(context.getJoueurEnCour().getScore() > 12)
        {
            context.getJoueurEnCour().avance(context.getLanceDeDes()[0] + context.getLanceDeDes()[1]);
        }
    };

    Action<Context> regle_de_loie = (Context context) =>
    {
        context.getJoueurEnCour().avance(context.getLanceDeDes()[0] + context.getLanceDeDes()[1]);
    };

    Action<Context> penalite = (Context context) =>
    {
        context.getJoueurEnCour().ajouteToursDePenalite(2);
    };

    Action<Context> prison = (Context context) =>
    {
        context.getJoueurEnCour().setPrison(true);
    };

    Action<Context> regle_case_42 = (Context context) =>
    {
        context.getJoueurEnCour().saut(30);
    };

    Action<Context> regle_case_58 = (Context context) =>
    {
        context.getJoueurEnCour().saut(1);
    };

    Action<Context> Fin = (Context context) =>
    {  
        context.setPartieGagner();
    };

    public Dictionary<int, Action<Context>> getPlateau() { return this.plateau; }

    public Dictionary<int, string> getRegle() { return this.regles; }

    public string getNom() { return nom; }





}

