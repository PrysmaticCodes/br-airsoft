using TCC_BR.Repository.Contract;
using TCC_BR.Models.Constante;
using TCC_BR.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace TCC_BR.Repository
{
    public class AdminRepository : IAdminRepository1
    {
        private readonly string _ConexaoMySQL;

        //Metodo construtor da classe AdminRepository    
        public AdminRepository(IConfiguration conf)
        {
            // Injeção de dependencia do banco de dados
            _ConexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Admins adm)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update administrador set Nome=@Nome, " +
                    " Email=@Email, Senha=@Senha, Tipo=@Tipo Where Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = adm.Id;
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = adm.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = adm.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = adm.Senha;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void AtualizarSenha(Admins adm)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Admins adm)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into administrador(Nome, email, Senha) " +
                                                     " values (@Nome, @Email, @Senha)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = adm.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = adm.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = adm.Senha;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from administrador WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Admins Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from administrador where email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Admins adm = new Admins();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    adm.Id = (Int32)(dr["Id"]);
                    adm.Nome = (string)(dr["Nome"]);
                    adm.Email = (string)(dr["Email"]);
                    adm.Senha = (string)(dr["Senha"]);
                }
                return adm;
            }
        }

        public Admins ObterAdmin(int Id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from administrador WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Admins adm = new Admins();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    adm.Id = (Int32)(dr["Id"]);
                    adm.Nome = (string)(dr["Nome"]);
                    adm.Email = (string)(dr["Email"]);
                    adm.Senha = (string)(dr["Senha"]);
                }
                return adm;
            }
        }

        public List<Admins> ObterAdminPorEmail(string email)
        {
            List<Admins> colabList = new List<Admins>();
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from administrador WHERE email=@email ", conexao);
                cmd.Parameters.AddWithValue("@Id", email);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    colabList.Add(
                        new Admins
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Senha = (string)(dr["Senha"]),
                            Email = (string)(dr["Email"]),
                        });
                }
                return colabList;
            }
        }

        public IEnumerable<Admins> ObterTodosAdmins()
        {
            List<Admins> colabList = new List<Admins>();
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM administrador", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    colabList.Add(
                        new Admins
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Email = (string)(dr["Email"]),
                            Senha = (string)(dr["Senha"]),
                        });
                }
                return colabList;
            }
        }
    }
}
