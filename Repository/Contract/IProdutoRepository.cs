using TCC_BR.Models;

namespace TCC_BR.Repository.Contract
{
    public interface IProdutoRepository
    {
        //CRUD
        void Cadastrar(Produto produto);
        IEnumerable<Produto> ObtertodosProdutos();

        void Atualizar(Produto produto);
        Produto ObtertoProduto(int id);
        void Excluir(int id);      
        Produto BuscaIdProduto(int Id);
        Produto BuscaNome(string nome);
    }
}
