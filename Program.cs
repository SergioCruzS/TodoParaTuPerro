using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Login/Login";  //Especificamos que el formulario de acceso está en la vista Index del Controlador Acceso
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20); //La cookie 8o acceso) expirará en 20 min
        option.AccessDeniedPath = "/Home/Privacy";  //Si no se tiene acceso te redirecciona a la vista Privacy
    })
    ;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Login}/{action=Index}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
