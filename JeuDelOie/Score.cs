/// <summary>
/// Représente les informations à :
/// > Afficher dans le score Board
/// > Enregister dans la BDD
/// </summary>
public class Score
{
    public string pseudo;

    public int score;

    public string plateau;

    public DateTime date;

    public Score()
    { 

        this.pseudo = "--";
        this.score = 999;
        this.plateau = JeuDeLoie.parcourts[JeuDeLoie.parcourtSelectionne].getNom();

    }

    public Score(string pseudo, int score, string plateau, DateTime date)
    {
        this.pseudo = pseudo;
        this.score = score;
        this.plateau = plateau;
        this.date = date;
    } 
        
    public string getPseudo() { return pseudo; }    

    public int getScore() { return score; } 

    public string getPlateau() { return plateau; }

    public DateTime getDate() { return date; }

    /// <summary>
    /// 
    /// Renvoie une date formaté pour l'affichage dans le score board
    /// --
    /// 
    /// Renvoie une date par defaut quand le score board 
    /// n'a pas encore enregisté 5 joueur sur un plateau donnée
    /// --
    /// </summary>
    /// <returns></returns>
    public string formatDatePourAffichage()
    {
        if(this.date.Date > DateTime.MinValue)
        {
             return this.date.Date.ToString("yy/MM/dd");
        }
        return "--/--/--";
    }

 

}



                        
