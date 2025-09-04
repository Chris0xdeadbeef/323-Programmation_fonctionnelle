using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExercicesWords
{
    public class ExercicesWords
    {
        public static void exercice1()
        {
            string[] words = { "bonjourx", "xhello", "monde", "vert", "xrouge", "bleu", "jaune" };


            Func<string, bool> listXwords = word => !word.Contains("x");

            Func<string, bool> fourOrMore = word => word.Count() >= 4;

            List<Func<string, bool>> funcs = new() { listXwords, fourOrMore };

            Console.WriteLine($"Liste de mots : {string.Join(',', words)}");
            Console.WriteLine("1. Pas de x");
            Console.WriteLine("2. >= 4");

            Console.WriteLine(string.Join(", ", words.Where(funcs[int.Parse(Console.ReadLine()) - 1])));
        }

        public static void exercice2()
        {
            string[] words = { "whatThe!!!", "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune", "My kingdom for a horse !", "Ooops I did it again" };

            Func<string, string> parasite = word => word.Remove(word.Length - 2, 2).Remove(0, 1);

            Console.WriteLine(string.Join(", ", words.Select(parasite)));
        }

        public static void exercice3()
        {
            string[] words = { "+++++", "<<<<<", ">>>>>", "bonjour", "hello", "@@@@", "vert", "rouge", "bleu", "jaune", "#####", "%%%%%%%" };

            Func<string, bool> filtered = word => Regex.IsMatch(word, @"\w+");

            Console.WriteLine(string.Join(", ", words.Where(filtered)));
        }

        public static void exercice4() 
        {
            string[] words = { "i am the winner", "hello", "monde", "vert", "rouge", "bleu", "i am the looser" };
                    
            Console.WriteLine($"{words.First()} : {words.Last()}");
            Console.WriteLine($"{words.Last()} : {words.First()}");
        }
    }
}
