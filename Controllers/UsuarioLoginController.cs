using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TCC_BR.Models;
using TCC_BR.Repository.Contract;
using MySqlX.XDevAPI;
using TCC_BR.Libraries.Login;

public class UsuarioLoginController : Controller
{
    private IUsuarioRepository _usuarioRepository;
    private LoginUsuario _loginUsuario;

    public UsuarioLoginController(IUsuarioRepository usuarioRepository, LoginUsuario loginUsuario)
    {
        _usuarioRepository = usuarioRepository;
        _loginUsuario = loginUsuario ?? throw new ArgumentNullException(nameof(loginUsuario));
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login([FromForm] Usuario usuario)
    {
        Usuario usuarioDB = _usuarioRepository.Login(usuario.Email, usuario.Senha);

        if (usuarioDB.Email != null && usuarioDB.Senha != null)
        {
            _loginUsuario.Login(usuarioDB);
            return new RedirectResult(Url.Action(nameof(PainelCliente)));
        }
        else
        {
            //Erro na sessão
            ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitados";
            return View();
        }
    }
    public IActionResult PainelCliente()
    {
        return View();
    }
}