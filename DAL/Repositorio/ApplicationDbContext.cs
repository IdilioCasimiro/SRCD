using DAL.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DAL.Repositorio
{
    public class SRCDDbContext : IdentityDbContext
    {

        public SRCDDbContext(DbContextOptions<SRCDDbContext> options)
            :base(options)
        {
        }

        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Drogaria> Drogarias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clinica>().ToTable("Clinicas");
            modelBuilder.Entity<Drogaria>().ToTable("Drogarias");
        }
    }
}
