using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using TCC_BR.Libraries.Login;
using TCC_BR.Repository;
using TCC_BR.Repository.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<LoginUsuario>();

// Adiciona o manipulador de contexto HTTP.
builder.Services.AddHttpContextAccessor();

// Adiciona a interface como servi�o.
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<TCC_BR.Libraries.Sesao.Sessao>();
builder.Services.AddScoped<LoginUsuario>();

// Corrigir problema com TempData.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Configura o tempo de dura��o da sess�o.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc().AddSessionStateTempDataProvider();

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita o middleware de sess�o ANTES da autoriza��o.
app.UseSession();

app.UseAuthentication(); // Ativa o uso de autentica��o.
app.UseAuthorization();  // Ativa o uso de autoriza��o.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
