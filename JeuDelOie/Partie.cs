/// <summary>
/// La class partie contient la logique du : 
/// Premier tour de jeu jusqu'à la victoire d'un joueur
/// </summary>

public partial class Partie
{
    public Parcourt parcourt { get; set; }   

    public int tour { get; set; }

    public Joueur j1 { get; set; }

    public Joueur j2 { get; set; }

    public bool estEnCourt = false;

    public Context context { get; set; }
    


    public Partie(Joueur J1, Joueur J2) 
    {
        this.parcourt = JeuDeLoie.parcourts[JeuDeLoie.parcourtSelectionne];
        this.estEnCourt = false;
        this.tour = 0;
        this.context = new Context(J1, J2); 
        this.j1 = J1;
        this.j2 = J2;
    }

    /// <summary>
    /// Boucle while représentant une partie de Jeu de Loie 
    /// </summary>
    /// <returns></returns>
    public Joueur start()
    {
        this.estEnCourt=true;

        while (estEnCourt)
        {
            tourDeJeu();

            if(this.context.getJoueurEnCour().getCaseEnCour() >= 63)
            { 
                stop();
                Console.Clear();
            }
            else if (!this.context.getPartieGagner())
            {
                this.context.changeDeTour();
            }

            context.setLanceDeDes(new int[] {0,0});
        }

        return this.context.getJoueurEnCour();
    }


    /// <summary>
    /// Function qui représente le tour de jeu d'un 1 joueur :
    /// --
    /// > Vérification : es ce que le joueur a le droit jouer son tour 
    /// > Vérification : es ce que le joueur est un ordinateur 
    /// > Action       : lance les dés 
    /// > Vérification : es que le joueur a atteint la case d'un autre joueur 
    /// > Affichage    : de la règle 
    /// > Action       : applicaiton de la règle 
    /// </summary>
    public void tourDeJeu()
    {
        
        Console.Clear();
        IHM.affichePlateau(this.context, this.parcourt, this.j1, this.j2);


        if (Arbitre.peutJouer(this.context, this.j1))
        {


            Actions.pause(this.context.getJoueurEnCour());

            Console.Clear();
            this.context.setLanceDeDes(Actions.lancerDes());
            var lancerDeDes = this.context.getLanceDeDes();
            this.context.getJoueurEnCour().avance(lancerDeDes[0] + lancerDeDes[1]);
            this.context.getJoueurEnCour().setScore(lancerDeDes[0] + lancerDeDes[1]);


            //this.context.getJoueurEnCour().saut(52);
            Arbitre.verifieSwitchJoueur(this.context);
            Arbitre.lisRegle(this.context, this.parcourt, this.j1, this.j2);
            Arbitre.appliqueRegle(this.context, this.parcourt, this.j1, this.j2);

            Actions.pause(this.context.getJoueurEnCour(), false);
       
        }
    }

    public void stop()
    {
        this.estEnCourt = false;
    }
}

