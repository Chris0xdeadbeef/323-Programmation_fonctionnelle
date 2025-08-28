/*
Entreprise : ETML
Auteur : Christopher Ristic 
Date : 28.08.2025
Description : Place du marché
*/

using System;
using System.IO;
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




        }
    }
}
