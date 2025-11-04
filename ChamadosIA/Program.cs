var builder = WebApplication.CreateBuilder(args);

// Adicione suporte a sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tempo de expiração
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

// Adiciona o serviço de IA
builder.Services.AddHttpClient<ChamadosIA.Services.IAssistenteService>();


var app = builder.Build();

// Middleware de sessão precisa vir **antes** do UseAuthorization
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // <-- Adicione aqui

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}/{id?}");

app.Run();
