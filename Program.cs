using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using TCC_BR.Libraries.Login;
using TCC_BR.Repository;
using TCC_BR.Repository.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Adiciona serviços ao contêiner.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<LoginUsuario>();

// Adiciona o manipulador de contexto HTTP.
builder.Services.AddHttpContextAccessor();

// Adiciona a interface como serviço.
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<TCC_BR.Libraries.Sesao.Sessao>();
builder.Services.AddScoped<LoginUsuario>();

// Corrigir problema com TempData.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Configura o tempo de duração da sessão.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc().AddSessionStateTempDataProvider();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita o middleware de sessão ANTES da autorização.
app.UseSession();

app.UseAuthentication(); // Ativa o uso de autenticação.
app.UseAuthorization();  // Ativa o uso de autorização.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
