using TCC_BR.Models;

namespace TCC_BR.Repository.Contract
{
    public interface IAdminRepository1
    {
        void Atualizar(Admins adm);
        void AtualizarSenha(Admins adm);
        void Cadastrar(Admins adm);
        void Excluir(int Id);
        Admins Login(string Email, string Senha);
        Admins ObterAdmin(int Id);
        List<Admins> ObterAdminPorEmail(string email);
        IEnumerable<Admins> ObterTodosAdmins();
    }
}