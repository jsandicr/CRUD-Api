using Microsoft.EntityFrameworkCore;
using System;
using WebApiPrueba.Entities;

namespace WebApiPrueba.Context
{
    public partial class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
