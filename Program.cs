<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using TCC_BR.Data;
using TCC_BR.Libraries.Login;
using TCC_BR.Repository;
using TCC_BR.Repository.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Adicionado para manipular a Sessão
builder.Services.AddHttpContextAccessor();

//Adicionar a Interface como um serviço 

builder.Services.AddScoped<ICompraRepository, CompraRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<TCC_BR.Libraries.Sesao.Sessao>();
builder.Services.AddScoped<TCC_BR.Libraries.CarrinhoCompra.CookieCarrinhoCompra>();
builder.Services.AddScoped<LoginUsuario>();


//Add Gerenciador Arquivo como serviços
builder.Services.AddScoped<GerenciaArquivo>();
builder.Services.AddScoped<TCC_BR.Libraries.Cookie.Cookie>();
builder.Services.AddScoped<TCC_BR.Libraries.CarrinhoCompra.CookieCarrinhoCompra>();


// Corrigir problema com TEMPDATA
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Definir um tempo para duração. 
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    // Mostrar para o navegador que o cookie e essencial   
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc().AddSessionStateTempDataProvider();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCookiePolicy();
app.UseSession();
// app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
=======
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
>>>>>>> f71c5fd1607ea9b877a7fdbfe4a888ab72f5b4a4
