
using System.Data.SQLite;

public partial class BDD
{


    public BDD()
    {

    }

    /// <summary>
    /// Ouvre la connexion à la BDD
    /// </summary>
    /// <returns></returns>
    private SQLiteConnection getConnexion()
    {
        return new SQLiteConnection("Data Source= " +
      " database.db; Version = 3; New = True; Compress = True; ");
    }

    /// <summary>
    /// Crée le fichier de BDD si il n'existe pas déjà
    /// </summary>
    public void initalisation()
    {

        using (SQLiteConnection connection = getConnexion())
        {
            // Ouverture de la connexion
            connection.Open();

            // Vérification si la table existe déjà
            string tableExistsSQL = "SELECT name FROM sqlite_master WHERE type='table' AND name='scoreboard';";
            using (SQLiteCommand command = new SQLiteCommand(tableExistsSQL, connection))
            {
                // exécution de la commande
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Si la table n'existe pas 
                    if (!reader.HasRows)
                    {
                        // Création de la commande SQL pour créer une table nommée "users"
                        string createTableSQL = "CREATE TABLE scoreboard (id INTEGER PRIMARY KEY, " +
                                                                           " pseudo TEXT, " +
                                                                           " score INTEGER, " +
                                                                           " plateau TEXT, " +
                                                                           " date DATETIME)";

                        // Création de la commande SQLite à partir de la commande SQL
                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableSQL, connection))
                        {
                            // Exécution de la commande pour créer la table
                            createCommand.ExecuteNonQuery();


                            Console.WriteLine("Initialisation de la BDD réussi appuyer sur une touche pour continuer");
                            Console.ReadKey();
                        }
                    }
                }
            }

            // Fermeture de la connexion
            connection.Close();
            
        }
    }

    /// <summary>
    /// Enregistre le score d'une partie dans la BDD
    /// </summary>
    /// <param name="score"></param>
    public void insertScore(Score score) 
    {
        using (SQLiteConnection connection = getConnexion())
        {
            connection.Open();
            
            string insertSQL = "INSERT INTO scoreboard (pseudo, score, plateau, date) VALUES (@pseudo, @score, @plateau, @date)";

            using (SQLiteCommand insertCommand = new SQLiteCommand(insertSQL, connection))
            {
                // Ajout des paramètres à la commande
                insertCommand.Parameters.AddWithValue("@pseudo", score.getPseudo());
                insertCommand.Parameters.AddWithValue("@score", score.getScore());
                insertCommand.Parameters.AddWithValue("@plateau", score.getPlateau() );
                insertCommand.Parameters.AddWithValue("@date", score.getDate());

                // Exécution de la commande pour insérer les données
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Renvoie pour un plateau donnée
    /// La liste des 5 meilleurs score enregistrée
    /// </summary>
    /// <param name="plateau"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public List<Score> getScore(string plateau, int limit=5)
    {
        using (SQLiteConnection connection = getConnexion())
        {
            connection.Open();
            List<Score> scores = new List<Score>();


            string selectSQL = "SELECT pseudo, score, plateau, date FROM scoreboard WHERE plateau = @plateau ORDER BY score LIMIT @limit";

            using (SQLiteCommand selectCommand = new SQLiteCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@plateau", plateau);
                selectCommand.Parameters.AddWithValue("@limit", limit);

                // Exécution de la commande pour récupérer les résultats
                using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                {
                   
                    int i = 0;
                    while (reader.Read())
                    {
                            scores.Add(new Score(
                           reader.GetString(0),
                           reader.GetInt32(1),
                           reader.GetString(2),
                           reader.GetDateTime(3)
                            ));
                        i++;
                    }

                }
                connection.Close();
            }
            return scores;
        }
    }

    /// <summary>
    /// Supprime une table de la BDD à partir de son nom
    /// </summary>
    /// <param name="tableName"></param>
    public void supprTable(string tableName)
    {
        using (SQLiteConnection connection = getConnexion())
        {
            // Ouverture de la connexion
            connection.Open();

            // Création de la commande SQL pour supprimer la table
            string deleteSQL = "DROP TABLE " + tableName;

            // Création de la commande SQLite à partir de la commande SQL
            using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteSQL, connection))
            {
                // Exécution de la commande pour supprimer la table
                deleteCommand.ExecuteNonQuery();
            }

            // Fermeture de la connexion
            connection.Close();
        }
    }
}




                        
