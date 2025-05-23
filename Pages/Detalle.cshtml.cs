using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Cursos.Data;
using RazorApp.Cursos.Data.Entities;

namespace RazorApp.Cursos.Pages
{
    public class DetalleModel(CursosDbContext dbContext, ILogger<DetalleModel> logger) : PageModel
    {
        private readonly CursosDbContext _dbContext = dbContext;
        private readonly ILogger<DetalleModel> _logger = logger;
        public required Curso Curso { get; set; }

        public async Task OnGet(Guid id)
        {
            try
            {
                var cursoDb = await _dbContext.Cursos.FindAsync(id);
                if (cursoDb != null)
                    Curso = cursoDb;
                _logger.LogWarning("No hay registros");
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo consultar el registro", ex.Message);
                throw;
            }
        }
    }
}
