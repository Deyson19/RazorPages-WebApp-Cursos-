using Microsoft.EntityFrameworkCore;
using RazorApp.Cursos.Data.Entities;

namespace RazorApp.Cursos.Data
{
    public class CursosDbContext(DbContextOptions<CursosDbContext> op) : DbContext(op)
    {
        public DbSet<Curso> Cursos { get; set; }
    }
}
