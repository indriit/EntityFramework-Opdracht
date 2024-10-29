using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp4
{
    public class StripsContext : DbContext
    {
        public DbSet<Strip> Strips { get; set; }
        public DbSet<Uitgeverij> Uitgeverijen { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Reeks> Reeksen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RG2TQP2\SQLEXPRESS;Initial Catalog=Strips;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
