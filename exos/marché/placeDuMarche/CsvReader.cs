/*
Entreprise : ETML
Auteur : Christopher Ristic 
Date : 28.08.2025
Description : Place du marché
*/


using System;
using System.IO;

namespace placeDuMarche
{
    internal class CsvReader
    {
        private string filePath;

        public CsvReader(string filePath)
        {
            this.filePath = filePath;

            if(!IsFileFound(filePath))
                Console.WriteLine($"Fichier introuvable {filePath}");
        }

        public string CountSeller(string sellerName)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {

            }

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
