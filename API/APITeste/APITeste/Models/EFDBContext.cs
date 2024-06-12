using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APITeste.Models
{
    public class EFDBContext : DbContext
    {

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source=Banco.sqlite");
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}