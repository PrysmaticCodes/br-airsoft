using MySql.Data.MySqlClient;
using TCC_BR.Models.Constante;
using TCC_BR.Models;
using TCC_BR.Repository.Contract;
using System.Data;

namespace TCC_BR.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _ConexaoMySQL;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger)
        {
            _ConexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
            _logger = logger;
        }

        //public UsuarioRepository(IConfiguration configuration)
        //{
        //    _ConexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        //}

        public void Cadastrar(Usuario usuario)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_ConexaoMySQL))
                {
                    connection.Open();

                    // Teste rápido de conexão
                    _logger.LogInformation("Conexão com o banco de dados aberta com sucesso.");

                    string sql = "INSERT INTO cliente (nome_cliente, email, telefone, cpf, senha) VALUES (@NomeCliente, @Email, @Telefone, @CPF, @Senha)";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        // Configurando parâmetros da query
                        //command.Parameters.AddWithValue("@NomeCliente", usuario.Nome);
                        command.Parameters.Add("@NomeCliente", MySqlDbType.VarChar).Value = usuario.Nome;
                        //command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = usuario.Email;
                        //command.Parameters.AddWithValue("@Telefone", usuario.Celular);
                        command.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = usuario.Celular;
                        //command.Parameters.AddWithValue("@CPF", usuario.CPF);
                        command.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = usuario.CPF;
                        //command.Parameters.AddWithValue("@Senha", usuario.Senha); // da pra criptografar a senha se sobrar tempo fazer
                        command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;

                        // Executa a inserção e verifica sucesso
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            _logger.LogInformation("Usuário {Nome} cadastrado com sucesso no banco de dados.", usuario.Nome);
                        }
                        else //logs de erro pra facilitar depuração
                        {
                            _logger.LogWarning("A inserção do usuário {Nome} não foi concluída.", usuario.Nome);
                        }
                    }
                }
            }
            catch (Exception ex)  //logs de erro pra facilitar depuração
            {
                _logger.LogError(ex, "Erro ao cadastrar o usuário {Nome}.", usuario.Nome);
                Console.WriteLine($"Erro ao abrir a conexão: {ex.Message}");
                throw;
            }
        }

        // Implementações vazias para os outros métodos CRUD
        public IEnumerable<Usuario> ObtertodosUsuarios()
        {
            List<Usuario> cliList = new List<Usuario>();
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM endereco", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Celular = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"]),
                            CEP = Convert.ToString(dr["CEP"]),
                            Estado = Convert.ToString(dr["Estado"]),
                            Cidade = Convert.ToString(dr["Cidade"]),
                            Bairro = Convert.ToString(dr["Bairro"]),
                            Endereco = Convert.ToString(dr["Endereco"]),
                            Complemento = Convert.ToString(dr["Complemento"]),
                            Numero = Convert.ToString(dr["Numero"])

                        });
                }
                return cliList;
            }
        }
        public void Atualizar(Usuario usuario)
        {
            throw new NotImplementedException();
        } 
        public Usuario ObtertoUsuario(int id)
        {
            throw new NotImplementedException();
        }    
        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from cliente WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        
        public void Ativar(int id)
        {
            string Situacao = SituacaoConstante.Ativo;
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Desativar(int id)
        {
            string Situacao = SituacaoConstante.Desativado;
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Usuario BuscaCpfCliente(string CPF)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select CPF from cliente WHERE CPF=@CPF ", conexao);
                cmd.Parameters.AddWithValue("@CPF", CPF);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario cliente = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.CPF = (string)(dr["CPF"]);

                }
                return cliente;
            }
        }

        public Usuario BuscaEmailCliente(string email)
        {
               using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select Email from cliente WHERE Email=@Email ", conexao);
                cmd.Parameters.AddWithValue("@Email", email);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario cliente = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Email = (string)(dr["Email"]);

                }
                return cliente;
            }
        }

       public Usuario Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from cliente where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    usuario.CPF = Convert.ToString(dr["CPF"]);
                    usuario.Celular = Convert.ToString(dr["Telefone"]);
                    usuario.Nome = Convert.ToString(dr["nome_cliente"]); //tem que ser o mesmo nome que esta no banco
                    usuario.Email = Convert.ToString(dr["Email"]);
                    usuario.Senha = Convert.ToString(dr["Senha"]);
                    usuario.Id = (Int32)(dr["id_cliente"]);
                    usuario.CPF = (string)(dr["cpf"]);
                    //usuario.Situacao = (string)(dr["Situacao"]);
                    //usuario.CEP = (string)(dr["CEP"]);
                    //usuario.Estado = (string)(dr["Estado"]);
                    //usuario.Cidade = (string)(dr["Cidade"]);
                    //usuario.Bairro = (string)(dr["Bairro"]);
                    //usuario.Endereco = Convert.ToString(dr["Endereco"]);
                    //usuario.Complemento = Convert.ToString(dr["Complemento"]);
                    //usuario.Numero = Convert.ToString(dr["Numero"]);
                }
                return usuario;
            }
        }
    }
}

