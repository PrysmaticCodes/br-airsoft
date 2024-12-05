using Microsoft.AspNetCore.Mvc;
using TCC_BR.Libraries.Filtro;
using TCC_BR.Libraries.Login;
using TCC_BR.Repository.Contract;

namespace TCC_BR.Areas.Admin.Controllers
{
    [Area("Administrador")]
    public class HomeController : Controller
    {
        private IAdminRepository1 _repositoryAdmin;
        private LoginAdmin _loginAdmin;

        public HomeController(IAdminRepository1 repositoryAdmin, LoginAdmin loginAdmin)
        {
            _repositoryAdmin = repositoryAdmin;
            _loginAdmin = loginAdmin;
        }
        [AdminAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateHttpReferer]
        public IActionResult Login([FromForm] Models.Admins Admin)
        {
            Models.Admins AdminDB = _repositoryAdmin.Login(Admin.Email, Admin.Senha);


            if (AdminDB.Email != null && AdminDB.Senha != null)
            {
                _loginAdmin.Login(AdminDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }

        }

        [AdminAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }
        [AdminAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginAdmin.Logout();
            return RedirectToAction(nameof(Login));
        }

    }
}

