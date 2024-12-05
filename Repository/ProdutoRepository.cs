using MySql.Data.MySqlClient;
using System.Data;
using TCC_BR.Models;
using TCC_BR.Repository.Contract;

namespace TCC_BR.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _ConexaoMySQL;

        public ProdutoRepository(IConfiguration conf)
        {
            _ConexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Produto (nome_prod, caminho_imagem, desc_prod, valor_prod, quant_produto) " +
                    "values(@NomeProduto, @ImagemProduto, @DescProd, @ValorProd, @QuantProd)", conexao);

                cmd.Parameters.Add("@NomeProduto", MySqlDbType.VarChar).Value = produto.NomeProduto;
                cmd.Parameters.Add("@ImagemProduto", MySqlDbType.VarChar).Value = produto.ImagemProd;
                cmd.Parameters.Add("@DescProd", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@ValorProd", MySqlDbType.VarChar).Value = produto.PrecoProd;
                cmd.Parameters.Add("@QuantProd", MySqlDbType.VarChar).Value = produto.QuantProd;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        
        public void Atualizar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Produto> ObtertodosProdutos()
        {
            List<Produto> ProdutoList = new List<Produto>();

            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Produto", conexao);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoList.Add(
                        new Produto
                        {
                            ID = Convert.ToInt32(dr["id_prod"]),
                            NomeProduto = Convert.ToString(dr["nome_prod"]),
                            ImagemProd = Convert.ToString(dr["caminho_imagem"]),
                            PrecoProd = Convert.ToDecimal(dr["valor_prod"]),
                        });
                }
                return ProdutoList;
            }
        }

        public Produto ObtertoProduto(int id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Produto where id_prod=@cod", conexao);
                cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = id;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.ID = Convert.ToInt32(dr["id_prod"]);
                    produto.NomeProduto = Convert.ToString(dr["nome_prod"]);
                    produto.ImagemProd = Convert.ToString(dr["caminho_imagem"]);
                }
                return produto;
            }
        }

        public Produto BuscaIdProduto(int Id)
        {
            throw new NotImplementedException();
        }

        public Produto BuscaNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
