using Newtonsoft.Json;
using TCC_BR.Libraries.Sesao;
using TCC_BR.Models;

namespace TCC_BR.Libraries.Login
{
    public class LoginAdmin
    {
        // Criar uma chave para a sessão
        private string Key = "Login.Adm";
        private Sesao.Sessao _sessao;
        //Injetar sessão na classe LoginAdm
        public LoginAdmin(Sesao.Sessao sessao)
        {
            _sessao = sessao;
        }
        //Converte o objeto Admin para Json ** Serializar **
        public void Login(Admins adm)
        {
            //Serializar
            string AdmJSONString = JsonConvert.SerializeObject(adm);

            _sessao.Cadastrar(Key, AdmJSONString);
        }
        //Reverter Json para objeto Admin ** Deserializar **
        public Admins Getadms()
        {
            //Deserializar
            if (_sessao.Existe(Key))
            {
                string AdminJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Admins>(AdminJSONString);
            }
            else
            {
                return null;
            }
        }
        //Remove a sessão e desloga o Adm
        public void Logout()
        {
            _sessao.RemoverTodos();
        }

       
    }
}
