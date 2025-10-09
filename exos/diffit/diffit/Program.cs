using System;
using System.Globalization;
using System.Text.RegularExpressions;

///MENU
Console.WriteLine("+--------------------------------+");
Console.WriteLine("|DIFFIT : A very limited DIFFTOOL|");
Console.WriteLine("+--------------------------------+");

Console.Write("Fichier A: ");
string? pathA = Console.ReadLine();

Console.Write("Fichier B: ");
string? pathB = Console.ReadLine();

// Vérification des entrées utilisateur
var paths = new string?[] { pathA, pathB };
bool filesAreValid = paths.Aggregate(true, (a, b) => a && b != null && File.Exists(b));
if (!filesAreValid)
{
    Console.WriteLine("Erreur: les fichiers doivent être existants et accessibles !");
    Environment.Exit(-2);
}

/// CHARGEMENT DES DONNÉES
// TODO: 01 Charger le contenu texte du fichier A (indice: File.ReadAllLines...)
string[] linesA = File.ReadAllLines(pathA);

// TODO: 02 Charger le contenu texte du fichier B (indice: File.ReadAllLines...)
string[] linesB = File.ReadAllLines(pathB);

// TODO: 03 Vérifier que les fichier ont le même nombre de lignes
if (linesA.Length != linesB.Length)
{
    Console.WriteLine("Erreur: les fichiers n'ont pas le même nombre de ligne");
    Environment.Exit(-2);
}

Console.WriteLine(">Fichiers chargés avec succés");

// TODO: 04 Définir les fonctions de nettoyage
// Une fonction de nettoyage reçoit un texte (une ligne de fichier) et renvoie cette même ligne adaptée
// Il existe la fonction Replace sur les string...
// Le caractère tabulation s’écrit \t
Func<string, string> cleanSpaces = text => text.Replace(" ", "");
Func<string, string> cleanTabs = text => text.Replace("\t", " ");
Func<string, string> enforceCase = text => text.ToLower();

/// OPTIONS DE NETTOYAGE
Console.WriteLine("Choisir les options:");

Console.Write("-Ignorer les espaces [o/n]: ");
bool ignoreSpaces = Console.ReadLine() == "o";

Console.Write("-Ignorer les tabulations [o/n]: ");
bool ignoreTabs = Console.ReadLine() == "o";

Console.Write("-Ignorer la casse [o/n]: ");
bool ignoreCase = Console.ReadLine() == "o";

// TODO:  05 Appliquer le nettoyage selon la demande utilisateur
Func<string, string> applyCleaning = text =>
{
    if (!ignoreSpaces)
    {
        text = cleanSpaces(text);
    }
    if (!ignoreTabs)
    {
        text = cleanTabs(text);
    }
    if (!ignoreCase)
    {
        text = enforceCase(text);
    }
    return text;
};


// TODO: 06 Créer et remplir une liste de LinesComparison à partir de linesA et linesB
List<LinesContent> contents = new();

for(int i = 0; i< linesA.Length; ++i)
{
    contents.Add(new LinesContent
    {
        Number = i,
        ContentA = applyCleaning(linesA[i]),
        ContentB = applyCleaning(linesB[i]),
    });
}

// TODO: 07 Sélectionner les lignes qui ont des différences
var diffLines = new List<LinesContent>();
diffLines = contents.Where(c => c.ContentA != c.ContentB).ToList();


// TODO: 08 Afficher le nombre de lignes identiques et différentes entre les 2 fichiers
int sameCount = contents.Count(c => c.ContentA == c.ContentB);
int diffCount = diffLines.Count;

Console.WriteLine($"\n> {sameCount} lignes identiques");
Console.WriteLine($"> {diffCount} lignes différentes");


// TODO: 09 Définir une fonction qui compte les différences (caractères différents) entre deux textes (sera utilisé pour les 2 lignes de A et B...)
// Pour info/rappel, la fonction Zip (comme une fermeture éclair) permet d’associer deux listes.
// Et pour info/rappel, un string est une liste de char...
// Ainsi "12345".Zip("ABCDE", (a, b) => $"{a}{b}").ToList().ForEach(Console.Write);//1A2B3C4D5E
// ATTENTION: zip ne prend que le nombre d’éléments minimum commun entre 2 listes...
// Ceci implique une correction: en plus du nombre de différences, il faut ajouter la différence du nombre de caractères entre les deux...

Func<LinesContent, int> countVariations = comp =>
{
    int commonLength = Math.Min(comp.ContentA.Length, comp.ContentB.Length);

    int differences = comp.ContentA
        .Take(commonLength)
        .Zip(comp.ContentB, (a, b) => a == b ? 0 : 1)
        .Sum();

    return differences + comp.LengthVariation;
};


// TODO: 10 Afficher pour chaque ligne différente, le nombre de variations
foreach (var diff in diffLines)
{
    Console.WriteLine($"Ligne {diff.NumberHuman} : {countVariations(diff)} différences");
}

/// Diff coloré
// TODO: 11 Colorier les différences
// Pour chaque ligne où il y a des différences:
// On affiche ainsi:
// Les lettres similaires sont en vert
// Les lettres différentes sont en rouge (options entre[a/b])
// On n’indique rien sur les caractères en plus ou en moins
foreach (var diff in diffLines)
{
    Console.Write($"Ligne {diff.NumberHuman}: ");
    int commonLength = Math.Min(diff.ContentA.Length, diff.ContentB.Length);

    for (int i = 0; i < commonLength; i++)
    {
        if (diff.ContentA[i] == diff.ContentB[i])
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(diff.ContentA[i]);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{diff.ContentA[i]}/{diff.ContentB[i]}]");
        }
    }

    // Caractères restants
    if (diff.ContentA.Length > commonLength)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(diff.ContentA.Substring(commonLength));
    }
    else if (diff.ContentB.Length > commonLength)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(diff.ContentB.Substring(commonLength));
    }

    Console.ResetColor();
    Console.WriteLine();
}

/// Chiffrement
// TODO: 11 Créer une fonction qui chiffre le 1er fichier en décalant les caratères d’un nombre
//saisi par l’utilisateur (clé)
// Le contenu chiffré est enregistré sur le disque dans le fichier "cipheredA.txt"
// Le pendant de ReadAllLines est WriteAllLines
Console.Write("\n\nSPECIAL FEATURE: Clé de chiffrement [1-25]: ");
byte key = Convert.ToByte(Console.ReadLine());

string CaesarShift(string input, int shift)
{
    return new string(input.Select(c =>
    {
        if (!char.IsLetter(c)) return c;

        char offset = char.IsUpper(c) ? 'A' : 'a';
        return (char)((((c - offset) + shift) % 26) + offset);
    }).ToArray());
}

string[] ciphered = linesA.Select(line => CaesarShift(line, key)).ToArray();
File.WriteAllLines("cipheredA.txt", ciphered);
Console.WriteLine(">Fichier A chiffré enregistré dans 'cipheredA.txt'");

/// <summary>
/// Classe pour porter une information de comparaison
/// </summary>
public class LinesContent
{
    public int Number { get; set; }
    public string ContentA { get; set; } = "";
    public string ContentB { get; set; } = "";

    /// <summary>
    /// Ajuste le numéro de ligne...
    /// </summary>
    public int NumberHuman
    {
        get => Number + 1;
    }

    public int LengthVariation { get => Math.Abs(ContentA.Length - ContentB.Length); }
}
