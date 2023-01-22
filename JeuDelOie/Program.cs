// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata;

Console.Title = "Jeu de L'Oie";
//Console.SetWindowSize(115,35);

/*Console.ForegroundColor= ConsoleColor.Red;

Console.WriteLine("Une ligne en rouge !");

Console.ResetColor();

Console.WriteLine("Une ligne en rouge !");*/

Joueur J1 = new Joueur("razer", false);

Joueur J2 = new Joueur("blazer", true);

Partie game = new Partie(J1, J2);

Joueur vainceur = game.start();

Console.WriteLine($"Félicitaion {vainceur.getPseudo()} ! ");
Console.WriteLine($"Vous avez gagner avec un score de {vainceur.getScore()}");

if(vainceur.getScore() > 100)
{
    Console.Write("...Ce qui est mauvais.");
} 

Console.ReadLine(); 