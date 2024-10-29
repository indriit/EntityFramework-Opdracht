using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Uitgeverij
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Strip> Strips { get; set; } = new List<Strip>();
    }
}
