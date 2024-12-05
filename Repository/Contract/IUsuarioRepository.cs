using TCC_BR.Models;
namespace TCC_BR.Repository.Contract
{
    public interface IUsuarioRepository
    {
        Usuario Login(string Email, string Senha);
        void Cadastrar(Usuario usuario);
        IEnumerable<Usuario> ObtertodosUsuarios();
        
        void Atualizar(Usuario usuario);
        Usuario ObtertoUsuario(int id);
        void Excluir(int id);
        void Ativar(int id);
        void Desativar(int id);
        Usuario BuscaCpfCliente(string CPF);
        Usuario BuscaEmailCliente(string email);





    }
}
