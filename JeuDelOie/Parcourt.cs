// See https://aka.ms/new-console-template for more information
using System.CodeDom;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;

public class Parcourt
{
    /// <summary>
    /// Variable static représentant le nom des différents plateau
    /// </summary>

    private static readonly string TERRE_BRULEE = "Terre Brulée";

    private static readonly string SOMEILLE_DES_DIEUX = "Sommeil des Dieux";

    private static readonly string AURORE_ETERNEL = "Aurore Eternel";

    private static readonly string PROFOND_SOUPIR = "Pronfond Soupir";

    /// <summary>
    /// Enumération des différents plateau de jeu
    /// </summary>
    public enum Plateaux
    {
        TerreBrulee = 0,
        SommeilDesDieux = 1,
        AuroreEternel = 2,
        ProfondSoupir = 3,
  
    }

    public string nom { get; set; }

    /// <summary>
    /// Ce Dictionnaire Commande l'application des règles à des cases spéciales spécifique
    /// </summary>
    public Dictionary<int, Action<Context>> plateau { get; set; }

    /// <summary>
    /// Ce Dictionnaire Référence textuellement des règles à des cases spéciales spécifique
    /// </summary>
    public Dictionary<int, string> regles { get; set; }

    public Parcourt(Plateaux nom)
    {
        this.regles = new Dictionary<int, string>();
        this.plateau = new Dictionary<int, Action<Context>>();
        this.initialisationParcourt(nom);
        this.nom= initalisePlateauNom(nom);
    }

    /// <summary>
    /// Associe à un plateau à une liste d'action et de cases spéciale en fonction de son nom
    /// </summary>
    /// <param name="nom"></param>
    private void initialisationParcourt(Plateaux nom)
    {

        switch (nom)
        {
            case Plateaux.TerreBrulee:
                this.initialiseRegle_TerreBrulee();
                this.initalisePlateau_TerreBrulee();
                break;

            case Plateaux.SommeilDesDieux:
                this.initialiseRegle_SommeilDesDieux();
                this.initalisePlateau_SommeilDesDieux();
                break;

            case Plateaux.AuroreEternel:
                this.initialiseRegle_AuroreEternel();
                this.initalisePlateau_AuroreEternel();
                break;

            case Plateaux.ProfondSoupir:
                this.initialiseRegle_ProfondSoupir();
                this.initalisePlateau_ProfondSoupir();
                break;
        }

    }

    /// <summary>
    /// Associe à un plateau une liste de règle et de cases spéciales en fonction de son nom
    /// </summary>
    /// <param name="nom"></param>
    /// <returns></returns>
    private string initalisePlateauNom(Plateaux nom)
    {
        switch (nom)
        {
            case Plateaux.TerreBrulee:
                return TERRE_BRULEE;

            case Plateaux.SommeilDesDieux:
                return SOMEILLE_DES_DIEUX;

            case Plateaux.AuroreEternel:
                return AURORE_ETERNEL;

            case Plateaux.ProfondSoupir:
                return PROFOND_SOUPIR;
        }

        return "";
    }


    /// <summary>
    /// List les règles et les actions liées aux cases spéciales d'un plateau définie
    /// </summary>

    private void initialiseRegle_TerreBrulee()
    {
        this.regles.Add(6, "Pont : Aller à la case 12");
        this.regles.Add(9,  "Oie : Avancer du même nombre tiré. ");
        this.regles.Add(18, "Oie : Avancer du même nombre tiré");
        this.regles.Add(20, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(27, "Oie : Avancer du même nombre tiré");
        this.regles.Add(31, "Puit : Tirer 10, 11, 12, pour sortir");
        this.regles.Add(36, "Oie : Avancer du même nombre tiré");
        this.regles.Add(42, "Porte close : Retourne à la casse 30");
        this.regles.Add(45, "Oie : Avancer du même nombre tiré");
        this.regles.Add(52, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(54, "Oie : Avancer du même nombre tiré");
        this.regles.Add(58, "Tête de mort : Retourner à la case 1");
        this.regles.Add(63, "Fin de partie");

    }

    private void initalisePlateau_TerreBrulee()
    {
        this.plateau.Add(6, sautCase12);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, penalite);
        this.plateau.Add(27, regle_de_loie);
        this.plateau.Add(31, prison);
        this.plateau.Add(36, regle_de_loie);
        this.plateau.Add(42, sautCase30);
        this.plateau.Add(45, regle_de_loie);
        this.plateau.Add(52, prison);
        this.plateau.Add(54, regle_de_loie);
        this.plateau.Add(58, teteDeMort);
        this.plateau.Add(63, Fin);
    }



    private void initialiseRegle_SommeilDesDieux()
    {
        this.regles.Add(3, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(6, "Pont : Aller à la case 12");
        this.regles.Add(9, "Oie : Avancer du même nombre tiré. ");
        this.regles.Add(18, "Oie : Avancer du même nombre tiré");
        this.regles.Add(20, "Teleportation : Avance à la casse 30");
        this.regles.Add(22, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(25, "Tête de mort : Retourner à la case 1");
        this.regles.Add(27, "Oie : Avancer du même nombre tiré");
        this.regles.Add(31, "Puit : Tirer 10, 11, 12, pour sortir");
        this.regles.Add(36, "Oie : Avancer du même nombre tiré");
        this.regles.Add(42, "Porte close : Retourne la casse 30");
        this.regles.Add(45, "Oie : Avancer du même nombre tiré");
        this.regles.Add(52, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(54, "Oie : Avancer du même nombre tiré");
        this.regles.Add(58, "Tête de mort : Retourner à la case 1");
        this.regles.Add(63, "Fin de partie");

    }

    private void initalisePlateau_SommeilDesDieux()
    {
        this.plateau.Add(3, penalite);
        this.plateau.Add(6, sautCase12);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, sautCase30);
        this.plateau.Add(22, penalite);
        this.plateau.Add(25, teteDeMort);
        this.plateau.Add(27, regle_de_loie);
        this.plateau.Add(29, sautCase12);
        this.plateau.Add(31, prison);
        this.plateau.Add(36, regle_de_loie);
        this.plateau.Add(42, sautCase30);
        this.plateau.Add(45, regle_de_loie);
        this.plateau.Add(52, prison);
        this.plateau.Add(54, regle_de_loie);
        this.plateau.Add(58, teteDeMort);
        this.plateau.Add(63, Fin);
    }



    private void initialiseRegle_AuroreEternel()
    {
        this.regles.Add(6, "Pont : Aller à la case 12");
        this.regles.Add(7, "Teleportation : Avance à la casse 30");
        this.regles.Add(9, "Oie : Avancer du même nombre tiré. ");
        this.regles.Add(18, "Oie : Avancer du même nombre tiré");
        this.regles.Add(20, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(22, "Oie : Avancer du même nombre tiré");
        this.regles.Add(27, "Oie : Avancer du même nombre tiré");
        this.regles.Add(31, "Puit : Tirer 10, 11, 12, pour sortir");
        this.regles.Add(36, "Oie : Avancer du même nombre tiré");
        this.regles.Add(42, "Porte close : Retourne à la casse 30");
        this.regles.Add(45, "Oie : Avancer du même nombre tiré");
        this.regles.Add(52, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(54, "Oie : Avancer du même nombre tiré");
        this.regles.Add(58, "Tête de mort : Retourner à la case 1");
        this.regles.Add(62, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(63, "Fin de partie");

    }

    private void initalisePlateau_AuroreEternel()
    {
        this.plateau.Add(6, sautCase12);
        this.plateau.Add(7, sautCase30);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, penalite);
        this.plateau.Add(22, regle_de_loie);
        this.plateau.Add(27, regle_de_loie);
        this.plateau.Add(31, prison);
        this.plateau.Add(36, regle_de_loie);
        this.plateau.Add(42, sautCase30);
        this.plateau.Add(45, regle_de_loie);
        this.plateau.Add(52, prison);
        this.plateau.Add(54, regle_de_loie);
        this.plateau.Add(58, teteDeMort);
        this.plateau.Add(62, prison);
        this.plateau.Add(63, Fin);
    }



    private void initialiseRegle_ProfondSoupir()
    {
        this.regles.Add(1, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(4, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(6, "Pont : Aller à la case 12");
        this.regles.Add(9, "Oie : Avancer du même nombre tiré. ");
        this.regles.Add(18, "Oie : Avancer du même nombre tiré");
        this.regles.Add(20, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(27, "Oie : Avancer du même nombre tiré");
        this.regles.Add(31, "Puit : Tirer 10, 11, 12, pour sortir");
        this.regles.Add(36, "Oie : Avancer du même nombre tiré");
        this.regles.Add(42, "Porte close : Retourne à la casse 30");
        this.regles.Add(43, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(45, "Oie : Avancer du même nombre tiré");
        this.regles.Add(48, "Bonnêt d'âne : Passer 2 tours");
        this.regles.Add(52, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(54, "Oie : Avancer du même nombre tiré");
        this.regles.Add(58, "Tête de mort : Retourner à la case 1");
        this.regles.Add(60, "Prison: Tirer 10, 11, 12, pour sortir");
        this.regles.Add(63, "Fin de partie");

    }

    private void initalisePlateau_ProfondSoupir()
    {
        this.plateau.Add(1, prison);
        this.plateau.Add(4, penalite);
        this.plateau.Add(6, sautCase12);
        this.plateau.Add(9, regle_case_9);
        this.plateau.Add(18, regle_de_loie);
        this.plateau.Add(20, penalite);
        this.plateau.Add(27, regle_de_loie);
        this.plateau.Add(31, prison);
        this.plateau.Add(36, regle_de_loie);
        this.plateau.Add(42, sautCase30);
        this.plateau.Add(43, prison);
        this.plateau.Add(45, regle_de_loie);
        this.plateau.Add(48, penalite);
        this.plateau.Add(52, prison);
        this.plateau.Add(54, regle_de_loie);
        this.plateau.Add(58, teteDeMort);
        this.plateau.Add(60, prison);
        this.plateau.Add(63, Fin);
    }


    /// <summary>
    /// Suite des Actions général qui sont associer à des cases spéciales
    /// </summary>

    Action<Context> sautCase12 = (Context context) =>
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
                context.getJoueurEnCour().saut(53);
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

    Action<Context> sautCase30 = (Context context) =>
    {
        context.getJoueurEnCour().saut(30);
    };

    Action<Context> teteDeMort = (Context context) =>
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

