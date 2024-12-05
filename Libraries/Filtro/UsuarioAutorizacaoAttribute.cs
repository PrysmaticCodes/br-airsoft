using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using TCC_BR.Libraries.Login;
using TCC_BR.Models;

namespace TCC_BR.Libraries.Filtro
{
   
        public class UsuarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
        {
            LoginUsuario _loginUsuario;
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                _loginUsuario = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));
                Usuario usuario = _loginUsuario.GetUsuario();
                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Login", "Home", null);
                }
            }
        }

    
}
