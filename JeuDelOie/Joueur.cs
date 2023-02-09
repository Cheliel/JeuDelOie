/// <summary>
/// Contient les informations et les actions relative à un joueur
/// </summary>
public class Joueur
{
    private string pseudo;

    private int score;

    private int caseEnCour;

    private bool prison;

    private bool ordinateur;

    /// <summary>
    /// Un tour de pénalité représente un tour où le joueur ne peux pas jouer
    /// </summary>
    private int toursDePenalite; 

    public Joueur(string pseudo, bool estOrdinateur)
    {
        this.pseudo = pseudo;
        this.score = 0;
        this.caseEnCour = 0;
        this.prison = false;
        this.ordinateur = estOrdinateur;
        this.toursDePenalite = 0;
    }   

    /// <summary>
    /// Envoie le joueur sur une case spécifique du tableau
    /// </summary>
    /// <param name="nouvelleCaseEnCour"></param>
    public void saut(int nouvelleCaseEnCour)
    {
        this.caseEnCour = nouvelleCaseEnCour;
    }

    /// <summary>
    /// Incrémente la position du joueur sur le plateau 
    /// </summary>
    /// <param name="scoreDesDés"></param>
    public void avance(int scoreDesDés)
    {
        this.caseEnCour += scoreDesDés;
    }

    public void ajouteToursDePenalite(int tourDePenalite)
    {
        this.toursDePenalite = tourDePenalite;  
    }

    public void passeTourDePenalite() { this.toursDePenalite -= 1; }

    public int getScore() { return this.score; }

    public int getTourDePenalite() { return this.toursDePenalite;}

    public void setPrison(bool estEnPrison)
    {
        this.prison = estEnPrison;  
    }

    public bool getPrison() { return this.prison; } 

    public string getPseudo() { return this.pseudo; }   

    public int getCaseEnCour() { return this.caseEnCour; }  

    public bool estOrdinateur() { return this.ordinateur; }

    public void setScore(int avencement) { this.score += avencement; }  

   

}

