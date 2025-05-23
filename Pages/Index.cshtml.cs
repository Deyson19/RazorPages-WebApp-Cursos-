using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorApp.Cursos.Data;
using RazorApp.Cursos.Data.Entities;
using System.Threading.Tasks;

namespace RazorApp.Cursos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly CursosDbContext _dbContext;

        public readonly string Message = "Hola, esta una propiedad del modelo de Index";
        public List<Curso> cursos { get; set; }

        public IndexModel(ILogger<IndexModel> logger, CursosDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            //Tomar datos desde alguna fuente o recibir parametros
            try
            {
                var cursosDb = await _dbContext.Cursos.ToListAsync();
                cursos = cursosDb;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al consultar los registros de curos",ex.Message);
                cursos = [];
                throw;
            }
        }
    }
}
