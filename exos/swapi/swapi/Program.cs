
using swapi;
using System.Text.Json;

//Un client global pour éviter de surcharger l'OS et profiter d'un socket avec tout son contexte
//pour toutes les requêtes (DNS,TCP,...)
HttpClient client = new HttpClient();

//Récupération du json
var moviesJson = Helper.HttpGet("films");

//Conversion Json vers une classe définie
var moviesResult = JsonSerializer.Deserialize<FilmResult>(moviesJson);

//Récupération d'une sous-partie
var movies = moviesResult.results;
//Console.WriteLine(movies[0].title);
//movies.Write();

//1.
var mo = movies.Aggregate((a, b) => a.title.Length > b.title.Length ? a : b).title;//.Write();
Console.WriteLine(mo);

//2.
string character = JsonSerializer.Deserialize<Character>(Helper.HttpGet(movies[0].characters[0])).name;
Console.WriteLine(character);





public static class Extensions
{
    public static void Write(this IEnumerable<object> target, char separator = ',')
    {
        Console.WriteLine(String.Join('\n', target));
    }

    public static void Write(this Film target)
    {
        Console.WriteLine(target.title);
    }
}

//Définition des classes
class FilmResult
{
    public int count { get; set; }
    public List<Film> results { get; set; }

    public override string? ToString()
    {
        return $" {count}, {results}";
    }
}

public class Film
{
    public string title { get; set; }
    public List<string> characters { get; set; }

    public override string? ToString()
    {
        var charactersInfo = characters
            .Select(url => JsonSerializer.Deserialize<Character>(
                Helper.HttpGet(url)
                ).name);
        return $" {title}: {string.Join(',', charactersInfo)}";
    }
}

class Character
{
    public string name { get; set; }
}