﻿namespace TCC_BR.Libraries.Sesao
{
    public class Sessao
    {
        //Interface com um biblioteca para manipular a sessão
        IHttpContextAccessor _context;
        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }
        /*
         * CRUD - Cadastrar/Atualizar/Consultar/Remover - RemoverTodos/Exist
         */
        //Cadastrar sessão
        public void Cadastrar(string Key, string Valor)
        {
            _context.HttpContext.Session.SetString(Key, Valor);
        }
        //Consultar sessão
        public string Consultar(string Key)
        {
            return _context.HttpContext.Session.GetString(Key);
        }
        //Verificar se existe a sessão criada
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }

            return true;
        }
        //Remover sessão
        public void Remover(string Key)
        {
            _context.HttpContext.Session.Remove(Key);
        }
        //Remover todas sessões
        public void RemoverTodos()
        {
            _context.HttpContext.Session.Clear();
        }
        //Atualizar uma sessão
        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                _context.HttpContext.Session.Remove(Key);
            }
            _context.HttpContext.Session.SetString(Key, Valor);
        }
    }
}
