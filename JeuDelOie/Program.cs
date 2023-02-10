

Console.Title = "Jeu de L'Oie";
//Console.SetWindowSize(115, 35);
BDD BDD = new BDD();
//BDD.supprTable("scoreboard");
BDD.initalisation();

JeuDeLoie.demarer();
Console.ReadKey();
