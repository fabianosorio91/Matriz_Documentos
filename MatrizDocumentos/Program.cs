using Microsoft.AspNetCore.Authentication.Cookies; //Autenticacion ..................
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*Autenticacion .....................
builder.Services.AddControllersWithViews(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => 
    {
        option.LoginPath = "/Login/Usuarios";

    }); //autenticacion*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//autenticacion..................
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",


//pattern: "{controller=Usuarios}/{action=Login}/{id?}"); //Inicio login
pattern: "{controller=Documentos}/{action=Index}/{id?}"); //Inicio documentos

app.Run();

