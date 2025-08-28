/*
Entreprise : ETML
Auteur : Christopher Ristic 
Date : 28.08.2025
Description : Place du marché
*/

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace placeDuMarche
{
    internal class Program
    {        
        static void Main()
        {
            CsvReader csvReader = new CsvReader(@"pdm.csv");

            int countSeller = 0;
            foreach (var item in csvReader.DataFromFile())
            {
                if (Regex.IsMatch(item.Produit, "^P.ch"))
                {
                    ++countSeller;
                }

            }

            Console.WriteLine($"il y a {countSeller} vendeur de peches");

            int maxQuantity = 0;
            CsvReader.CsvDataRow lineBestProducer = null;

            foreach (CsvReader.CsvDataRow item in csvReader.DataFromFile())
            {
                if (Regex.IsMatch(item.Produit, "^Past.q"))
                {
                    if (int.TryParse(item.Quantite, out int quantite))
                    {
                        if (quantite > maxQuantity)
                        {
                            maxQuantity = quantite;
                            lineBestProducer = item;
                        }
                    }
                }
            }

            Console.WriteLine($"Producteur : {lineBestProducer.Producteur}, Emplacement : {lineBestProducer.Emplacement}, Quantité : {maxQuantity}");




            /*
            var result = csvReader.DataFromFile()
                .Where(item => Regex.IsMatch(item.Produit, "^Past.q", RegexOptions.IgnoreCase))
                .GroupBy(item => new { item.Producteur, item.Emplacement })
                .Select(g => new
                {
                    Producteur = g.Key.Producteur,
                    Emplacement = g.Key.Emplacement,
                    QuantiteTotale = g.Sum(x => int.Parse(x.Quantite))
                })
                .OrderBy(x => x.QuantiteTotale).Last();
            
            
            Console.WriteLine($"Producteur : {result.Producteur}, Emplacement : {result.Emplacement}, Quantité : {result.QuantiteTotale}");
            */



        }
    }
}
