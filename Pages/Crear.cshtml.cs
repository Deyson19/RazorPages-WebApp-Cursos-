using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Cursos.Data;
using RazorApp.Cursos.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RazorApp.Cursos.Pages
{
    public class CrearModel : PageModel
    {
        private readonly CursosDbContext _dbContext;

        public CrearModel(CursosDbContext context)
        {
            _dbContext = context;
        }

        [BindProperty]
        public required CursoDto CursoDto { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? Id { get; set; }

        private string currentId { get; set; }
        public async Task<ActionResult> OnGet()
        {
            var newCursoMock = new CursoDto
            {
                Name = "Introducción a .NET",
                Price = "20",
                Instructor = "Andrew Dev",
                Description = "En este curso aprenderás lo fundamentos de C# y .NET",
                Technologies = ["C#", ".NET", "VBasic"],
            };
            try
            {
                var cursoDb = await _dbContext.Cursos.FindAsync(Id);
                if (cursoDb == null)
                {
                    CursoDto = newCursoMock;
                }
                else
                {
                    currentId = cursoDb.Id.ToString();
                    CursoDto = new CursoDto
                    {
                        Name = cursoDb.Name,
                        Description = cursoDb.Description,
                        Price = cursoDb.Price.ToString(),
                        Technologies = cursoDb.Technologies,
                        Instructor = cursoDb.Instructor,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar datos", ex.Message);
                throw;
            }

            return Page();
        }
        public async Task<ActionResult> OnPost()
        {

            var model = CursoDto;
            if (ModelState.IsValid)
            {
                try
                {
                    if (Id.ToString() == null)
                    {
                        var nuevoCurso = new Curso
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Instructor = model.Instructor,
                            Price = double.Parse(model.Price),
                            Technologies = model.Technologies,
                        };
                        await _dbContext.Cursos.AddAsync(nuevoCurso);
                        await _dbContext.SaveChangesAsync();
                        return Page();
                    }
                    //Actualizar registro
                    currentId = currentId ?? Id.ToString();
                    var cursoDb = await _dbContext.Cursos.FindAsync(Guid.Parse(currentId));
                    cursoDb.Name = model.Name;
                    cursoDb.Description = model.Description;
                    cursoDb.Instructor = model.Instructor;
                    cursoDb.Price = double.Parse(model.Price);
                    cursoDb.Technologies = model.Technologies;
                    await _dbContext.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear el curso");
                    return RedirectToPage("Error", "No fue posible realizar la acción");
                    throw;
                }
            }
            return RedirectToAction("Error", "Error al crear un registros");
        }
    }
}
