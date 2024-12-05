using MySql.Data.MySqlClient;
using System.Data;

namespace TCC_BR.Data
{
    public class DatabaseContext
    {
        private readonly string _ConexaoMySQL;

        public DatabaseContext(string conexao)
        {
            _ConexaoMySQL = conexao;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_ConexaoMySQL);
        }
    }
}
