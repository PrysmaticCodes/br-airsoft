﻿namespace TCC_BR.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = httpContextAccessor;
            _configuration = configuration;
        }
        /*
         * CRUD - Cadastrar/Atualizar/Consultar/Remover - RemoverTodos/Exist
         */

        // Verificar se existe
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }

            return true;
        }
        // Deleta Cookie
        public void Remover(string Key)
        {
            _context.HttpContext.Response.Cookies.Delete(Key);
        }
        // Cadastrar Cookie
        public void Cadastrar(string Key, string Valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            Options.IsEssential = true;

            _context.HttpContext.Response.Cookies.Append(Key, Valor, Options);
        }

        // Consulta Cookie
        public string Consultar(string Key)
        {
            var valor = _context.HttpContext.Request.Cookies[Key];
            return valor;
        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Cadastrar(Key, Valor);
        }
    }
}
