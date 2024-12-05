using TCC_BR.Models;

namespace TCC_BR.Repository.Contract
{
    public interface ICompraRepository
    {
        //CRUD
        IEnumerable<Compra> ObterTodasCompras();

        void Cadastrar(Compra compra);

        void Atualizar(Compra compra);

        Compra ObterCompras(int Id);

        void buscaIdCompra(Compra compra);

        void Excluir(int Id);

    }
}
