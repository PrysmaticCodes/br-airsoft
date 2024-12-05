using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using TCC_BR.Models;
using TCC_BR.Repository.Contract;

namespace TCC_BR.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger; // Injetando ILogger
        }

        [HttpGet]
        public IActionResult CadUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadUsuario(Usuario usuario)
        {
            _logger.LogInformation("Tentando cadastrar usuário...");

            try
                {
                    if (ModelState.IsValid)
                    { 

                        _usuarioRepository.Cadastrar(usuario);
                    return RedirectToAction("Login", "UsuarioLogin");
                    }

                    return View();
                    //chamando a tela de login - 1 tela action 2 controller
                    
                }

            catch (MySqlException ex)
            {

                throw new Exception("Erro no Banco ao Cadastrar o Usuario" + ex.Message);
            }
            //catch (Exception ex)
            //{

            //    throw new Exception("Erro ao cadastrar Usuario" + ex.Message);
            //}




        }
    }
}

