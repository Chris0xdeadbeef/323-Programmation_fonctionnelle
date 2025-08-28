/*
Entreprise : ETML
Auteur : Christopher Ristic 
Date : 28.08.2025
Description : Place du marché
*/


using System;
using System.Collections.Generic;
using System.IO;

namespace placeDuMarche
{
    internal class CsvReader
    {
        private string filePath;
        public class CsvDataRow
        {          
            public int Emplacement { get; set; }
            public string Producteur { get; set; }
            public string Produit { get; set; }
            public string Quantite { get; set; }
            public string Unite { get; set; }
            public decimal PrixUnite { get; set; }
            public override string ToString()
            {
                return $"{Emplacement}, {Producteur}, {Produit}, {Quantite}, {Unite}, {PrixUnite}";
            }
        }
        
        public CsvReader(string filePath)
        {
            this.filePath = filePath;

            if(!IsFileFound(filePath))
                Console.WriteLine($"Fichier introuvable {filePath}");
        }

        public List<CsvDataRow> DataFromFile()
        {
            List<CsvDataRow> data = new List<CsvDataRow>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++) // i = 1 pour ignorer l'en-tête
            {
                string[] columns = lines[i].Split(';');

                if (columns.Length != 6)
                    continue; // ou gérer différemment

                CsvDataRow row = new CsvDataRow();
                row.Emplacement = int.Parse(columns[0]);
                row.Producteur = columns[1];
                row.Produit = columns[2];
                row.Quantite = columns[3];
                row.Unite = columns[4];
                row.PrixUnite = decimal.Parse(columns[5]);

                data.Add(row);
            }

            return data;
        }

        /// <summary>
        /// Vérifie que le fichier est bien existant
        /// </summary>
        /// <param name="filePath">chemin du fichier</param>
        /// <returns>retourne false si fichier introuvable</returns>
        private static bool IsFileFound(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Essayer de lire la première ligne pour valider le flux
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }
                    return true;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichier introuvable.");
                return false;
            }
        }
    }
}
