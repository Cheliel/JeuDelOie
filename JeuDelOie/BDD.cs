
using System.Data.SQLite;

public partial class BDD
{


    public BDD()
    {

    }


    private SQLiteConnection getConnexion()
    {
        return new SQLiteConnection("Data Source= " +
      " database.db; Version = 3; New = True; Compress = True; ");
    }

    public void initalisation()
    {
        Console.WriteLine("tring to initializer BDD");

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
                                                                           " date TEXT)";

                        // Création de la commande SQLite à partir de la commande SQL
                        using (SQLiteCommand createCommand = new SQLiteCommand(createTableSQL, connection))
                        {
                            // Exécution de la commande pour créer la table
                            createCommand.ExecuteNonQuery();

                            Score score = new Score("--", 999, "Terre Brulée", "--/--/--");
                            Score score1 = new Score("--", 999, "Aurore Eternel", "--/--/--");
                            Score score2 = new Score("--", 999, "Someil des Dieux", "--/--/--");
                            Score score3 = new Score("--", 999, "Profond Soupire", "--/--/--");
                            
                            this.insertScore(score);
                            this.insertScore(score1);
                            this.insertScore(score2);
                            this.insertScore(score3);

                            Console.WriteLine("Initialisation de la BDD réussi appyer sur une touche pour continuer");
                            Console.ReadKey();
                        }
                    }
                }
            }

            // Fermeture de la connexion
            connection.Close();
            
        }
    }

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



    public Score[] getScore(string plateau, int limit=5)
    {
        using (SQLiteConnection connection = getConnexion())
        {
            connection.Open();
            Score[] result;

            string selectSQL = "SELECT pseudo, score, plateau, date FROM scoreboard WHERE plateau = @plateau ORDER BY score DESC LIMIT @limit";

            using (SQLiteCommand selectCommand = new SQLiteCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@plateau", plateau);
                selectCommand.Parameters.AddWithValue("@limit", limit);
                // Exécution de la commande pour récupérer les résultats
                using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                {
                    result = new Score[reader.StepCount];
                    int i = 0;
                    while (reader.Read())
                    {
                        result[i] = new Score();
                        result[i].pseudo = reader.GetString(0);
                        result[i].score = reader.GetInt32(1);
                        result[i].plateau= reader.GetString(2);
                        result[i].date = reader.GetString(3);

                        i++;
                    }
                }
                connection.Close();
            }
            return result;
        }
    }

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




                        
