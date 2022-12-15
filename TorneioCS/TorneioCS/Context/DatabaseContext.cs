using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneioCS.Models;

namespace TorneioCS.Context
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configura o nome da tabela para a qual esse tipo de entidade é mapeado.
            modelBuilder.Entity<Competidor>().ToTable("Competidores");

            //Configura a propriedade da chave primária
            modelBuilder.Entity<Competidor>().HasKey(competidor => competidor.idCompetidor);
        }

        public DbSet<Competidor> Competidores { get; set; }

    }
}
