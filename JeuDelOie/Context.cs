
/// <summary>
/// La class context contient les informations général relative à un tour de jeu
/// </summary>
public class Context
{
    Joueur joueurEnCour;

    Joueur joueurEnAttente;

    int[] lanceDeDes;

    bool partieGagner;

    public Context(Joueur joueurEnCour, Joueur joueurEnAttente)
    {
        this.joueurEnCour = joueurEnCour;
        this.joueurEnAttente = joueurEnAttente;
        this.partieGagner = false;
        this.lanceDeDes = new int[] { 0, 0 };
    }
    
    /// <summary>
    ///  Cette fonction fait le liens entre J1 & J2 
    ///  Et le joueur en cours
    /// </summary>
    public void changeDeTour()
    {
        var J2 = this.joueurEnCour;
        this.joueurEnCour = joueurEnAttente;
        this.joueurEnAttente = J2;
    }

    public Joueur getJoueurEnCour() { return this.joueurEnCour; }

    public Joueur getJoueurEnAttente() { return this.joueurEnAttente; }

    public int[] getLanceDeDes() { return this.lanceDeDes; }

    public void setPartieGagner()
    {
        this.partieGagner = true;   
    }

    public bool getPartieGagner() { return this.partieGagner; }

    public void setLanceDeDes(int[] lanceDeDes) { this.lanceDeDes = lanceDeDes; }
}

