using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TCC_BR.Libraries.Login;

namespace TCC_BR.Libraries.Filtro
{
    public class AdminAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginAdmin _loginAdmin;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginAdmin = (LoginAdmin)context.HttpContext.RequestServices.GetService(typeof(LoginAdmin));
            Models.Admins adm = _loginAdmin.Getadms();
            if (adm != null)
            {
                context.Result = new ForbidResult();
            }
           
        }
    }
}
