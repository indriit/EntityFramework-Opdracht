using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Strip
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public int Nummer { get; set; }
        public Reeks Reeks { get; set; }
        public Uitgeverij Uitgeverij { get; set; }
        public List<Auteur> Auteurs { get; set; } = new List<Auteur>();
    }
}
