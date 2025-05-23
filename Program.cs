using Microsoft.EntityFrameworkCore;
using RazorApp.Cursos.Data;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("PostgreSQL") ?? "";

// Add services to the container.

builder.Services.AddDbContext<CursosDbContext>(op =>
{
    try
    {
        op.UseNpgsql(connectionString);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("No hay string de conexión para la app.");
        Console.WriteLine(ex.Message);
        Console.WriteLine("===========================================");
        throw;
    }
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
