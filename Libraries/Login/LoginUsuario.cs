using MySqlX.XDevAPI;
using Newtonsoft.Json;

using TCC_BR.Models;

namespace TCC_BR.Libraries.Login
{
    public class LoginUsuario
    {
        private string Key = "Login.Usuario";
        private Sesao.Sessao _sessao;
        public LoginUsuario(Sesao.Sessao sessao)
        {
            _sessao = sessao;
        }
        //Converte o objeto Usuario para Json ** Serializar **
        public void Login(Usuario usuario)
        {
            // Serializar
            string UsuarioJSONString = JsonConvert.SerializeObject(usuario);

            _sessao.Cadastrar(Key, UsuarioJSONString);
        }
        //Reverter Json para objeto Usuario ** Deserializar **
        public Usuario? GetUsuario()
        {
            // Deserializar
            if (_sessao.Existe(Key))
            {
                string UsuarioJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Usuario>(UsuarioJSONString);
            }
            else
            {
                return null;
            }
        }
        //Remove a sessão e desloga o Usuario
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
