using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Models;

namespace HipicaFacilSQL.Data
{
    public class HipicaContext : DbContext
    {
        public HipicaContext (DbContextOptions<HipicaContext> options)
            : base(options)
        {
        }

        public DbSet<HipicaFacilSQL.Models.Cliente> Clientes { get; set; } = default!;
        public DbSet<HipicaFacilSQL.Models.Cavalo> Cavalos { get; set; } = default!;
        public DbSet<HipicaFacilSQL.Models.Produto> Produtos { get; set; } = default!;
        public DbSet<HipicaFacilSQL.Models.Financa> Financas { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cavalo>().ToTable("Cavalo");
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Financa>().ToTable("Financa");

        }
    }
}
