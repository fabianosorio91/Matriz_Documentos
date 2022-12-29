using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//pattern: "{controller=Documentos}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "temp",
pattern: "{controller=Ordenes}/{action=Index}/{id?}");

app.Run();

