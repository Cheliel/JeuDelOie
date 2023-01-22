// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

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
       //this.joueurs = joueurs;
        this.estEnCourt = false;
        this.tour = 0;
        this.context = new Context(J1, J2); 
        this.j1 = J1;
        this.j2 = J2;
    }

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

    public void tourDeJeu()
    {
        
        Console.Clear();
        IHM.affichePlateau(this.context, this.parcourt, this.j1, this.j2);
        Actions.pause();

        if (Arbitre.peutJouer(this.context, this.j1))
        {

            if (this.context.getJoueurEnCour().estOrdinateur() || true)
            {
                Console.Clear();
                this.context.setLanceDeDes(Actions.lancerDes());
                var lancerDeDes = this.context.getLanceDeDes();
                this.context.getJoueurEnCour().avance(lancerDeDes[0] + lancerDeDes[1]);
                this.context.getJoueurEnCour().setScore(lancerDeDes[0] + lancerDeDes[1]);

            }

            //this.context.getJoueurEnCour().saut(52);
            //this.context.setLanceDeDes(new int[] { 6, 3 });

            Arbitre.verifieSwitchJoueur(this.context);
            Arbitre.lisRegle(this.context, this.parcourt, this.j1, this.j2);
            Arbitre.appliqueRegle(this.context, this.parcourt, this.j1,  this.j2);
            

        }
    }

    public void stop()
    {
        this.estEnCourt = false;
    }
}

