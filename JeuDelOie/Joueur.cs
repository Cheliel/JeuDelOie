// See https://aka.ms/new-console-template for more information
public class Joueur
{
    private string pseudo;

    private int score;

    private int caseEnCour;

    private bool prison;

    private bool ordinateur;

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

    public void saut(int caseEnCour)
    {
        this.caseEnCour = caseEnCour;
    }

    public void avance(int scoreDesDés)
    {
        this.caseEnCour += scoreDesDés;
    }

    public int getScore() { return this.score; }

    public void ajouteToursDePenalite(int tourDePenalite)
    {
        this.toursDePenalite = tourDePenalite;  
    }

    public void passeTourDePenalite() { this.toursDePenalite -= 1; }

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

