
public class Score
{
    public string pseudo;

    public int score;

    public string plateau;

    public string date; 

    public Score()
    {
        this.pseudo = "--";
        this.score = 999;
        this.plateau = JeuDeLoie.parcourts[JeuDeLoie.parcourtSelectionne].getNom();
        this.date = "--/--/--";
    }

    public Score(string pseudo, int score, string plateau, string date)
    {
        this.pseudo = pseudo;
        this.score = score;
        this.plateau = plateau;
        this.date = date; // formatString(date);
    } 
        
    public string getPseudo() { return pseudo; }    

    public int getScore() { return score; } 

    public string getPlateau() { return plateau; }

    public string getDate() { return date; }

    //string date = DateTime.Now.ToString();

}



                        
