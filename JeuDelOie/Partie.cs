// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

public class Partie
{
    public Parcourt parcourt { get; set; }   

    public int tour { get; set; }

    public Joueur j1 { get; set; }

    public bool estEnCourt = false;

    public Context context { get; set; }

    

    public Partie(Joueur J1, Joueur J2) 
    { 
        this.parcourt = new Parcourt();
       //this.joueurs = joueurs;
        this.estEnCourt = false;
        this.tour = 0;
        this.context = new Context(J1, J2); 
        this.j1 = J1;
    }

    public Joueur start()
    {
        this.estEnCourt=true;
        Console.WriteLine("Apouyez sur un Tuch pour louchoumencer");
        Console.ReadLine();

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
            Console.ReadLine();
            
        }

        return this.context.getJoueurEnCour();
    }

    public void stop()
    {
        this.estEnCourt=false;
    }

    public void tourDeJeu()
    {
        
        Console.Clear();
        IHM.debutDeTourDescription(this.context);
        Actions.pause();

        if (Arbitre.peutJouer(this.context.getJoueurEnCour()))
        {

            if (this.context.getJoueurEnCour().estOrdinateur() || true)
            {
                Console.Clear();
                Console.WriteLine("Apui sur une touche pour lancer les dès");
                this.context.setLanceDeDes(Actions.lancerDes());
                var lancerDeDes = this.context.getLanceDeDes();
                this.context.getJoueurEnCour().avance(lancerDeDes[0] + lancerDeDes[1]);
                this.context.getJoueurEnCour().setScore(lancerDeDes[0] + lancerDeDes[1]);

            }

            IHM.finDeTourDescription(this.context);
            Arbitre.lisRegle(this.context, this.parcourt);
            Actions.pause();
            Arbitre.appliqueRegle(this.context, this.parcourt);
            Actions.pause();

        }
    }
}

