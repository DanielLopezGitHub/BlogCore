using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogCore.Models;
using BlogCore.AccesoDatos.Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// Este es el DbContext que se Inyecta al proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

// Inyectando el Contenedor de Trabajo al proyecto
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

// Inyectamos el InicializadorDB para la Siembra de Datos.
builder.Services.AddScoped<IInicializadorDB, InicializadorDB>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// Metodo que Ejecuta la Siembra de Datos
// SiembraDeDatos();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


// Definiendo Metodo SiembraDeDatos
void SiembraDeDatos()
{
    using (var scope = app.Services.CreateScope())
    {
        var inicializadorDB = scope.ServiceProvider.GetRequiredService<IInicializadorDB>();
        inicializadorDB.Inicializar();
    }
}
