using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = @"C:\Users\indri\Downloads\StripsListEF.txt";

            List<Strip> strips = new List<Strip>();
            List<Auteur> auteurs = new List<Auteur>();
            List<Reeks> reeksen = new List<Reeks>();
            List<Uitgeverij> uitgeverijen = new List<Uitgeverij>();


            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(';');
                if (data.Length != 5) continue;

                string titel = data[0].Trim();
                int nummer = int.Parse(data[1].Trim());
                string reeksNaam = data[2].Trim();
                string[] auteurNamen = data[3].Split(',').Select(a => a.Trim()).ToArray();
                string uitgeverijNaam = data[4].Trim();

                if (strips.Any(s => s.Titel == titel && s.Nummer == nummer)) continue;

                Reeks reeks = reeksen.FirstOrDefault(r => r.Naam == reeksNaam) ??
                              new Reeks { Naam = reeksNaam };

                if (reeksen.All(r => r.Naam != reeks.Naam))
                    reeksen.Add(reeks);

                Uitgeverij uitgeverij = uitgeverijen.FirstOrDefault(u => u.Naam == uitgeverijNaam) ??
                                         new Uitgeverij { Naam = uitgeverijNaam };

                if (uitgeverijen.All(u => u.Naam != uitgeverij.Naam))
                    uitgeverijen.Add(uitgeverij);

                var strip = new Strip
                {
                    Titel = titel,
                    Nummer = nummer,
                    Reeks = reeks,
                    Uitgeverij = uitgeverij,
                    Auteurs = new List<Auteur>()
                };
                strips.Add(strip);

                foreach (var naam in auteurNamen)
                {
                    Auteur auteur = auteurs.FirstOrDefault(a => a.Naam == naam) ??
                                    new Auteur { Naam = naam };

                    if (auteurs.All(a => a.Naam != auteur.Naam))
                        auteurs.Add(auteur);

                    if (!strip.Auteurs.Contains(auteur))
                    {
                        strip.Auteurs.Add(auteur);
                        auteur.Strips.Add(strip);
                    }
                }
            }
            using (var context = new StripsContext())
            {
                await context.Reeksen.AddRangeAsync(reeksen);
                await context.Uitgeverijen.AddRangeAsync(uitgeverijen);
                await context.Auteurs.AddRangeAsync(auteurs);
                await context.Strips.AddRangeAsync(strips);
                await context.SaveChangesAsync();
            }

            Console.WriteLine("Strips:");
            foreach (var strip in strips)
            {
                Console.WriteLine($"Titel: {strip.Titel}, Reeks: {strip.Reeks.Naam}, Uitgeverij: {strip.Uitgeverij.Naam}");
            }
        }
    }
}

