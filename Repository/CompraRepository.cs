using MySql.Data.MySqlClient;
using TCC_BR.Models;
using TCC_BR.Repository.Contract;

namespace TCC_BR.Repository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly string _ConexaoMySQL;
        // construtor com paramentro injeção da conexao do banco
        public CompraRepository(IConfiguration conf)
        {
            _ConexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Compra compra)
        {
            throw new NotImplementedException();
        }

        public void buscaIdCompra(Compra compra)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlDataReader dr;
                MySqlCommand cmd = new MySqlCommand("SELECT codEmp FROM //Falta o fernando// ORDER BY codEmp DESC limit 1", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    compra.CodCompra = dr[0].ToString();
                }
                conexao.Close();
            }
        }

        public void Cadastrar(Compra compra)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into //FALTA O FERNANDO// (ID, DataCompra, ) values(@Id, @DataCompra )", conexao);

                cmd.Parameters.Add("@dtEmpre", MySqlDbType.VarChar).Value = compra.DataCompra;
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = compra.RefUsuario.Id;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Compra ObterCompras(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Compra> ObterTodasCompras()
        {
            throw new NotImplementedException();
        }
    }
}
