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
