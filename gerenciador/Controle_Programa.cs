using System.Configuration;
using Microsoft.Data.Sqlite;

internal class Controle_Programa
{
    string conexao_string = ConfigurationManager.AppSettings.Get("ConnectionString");

    internal void Criar(Gerenciador gerenciador)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela = conexao.CreateCommand())
            {
                conexao.Open();
                tabela.CommandText = $"INSERT INTO usuarios (PrimeiroNome, SegundoNome, Email, Celular, Senha, Genero) VALUES ('{gerenciador.PrimeiroNome}', '{gerenciador.SegundoNome}', '{gerenciador.Email}', '{gerenciador.Celular}', '{gerenciador.Senha}', '{gerenciador.Genero}')";
                tabela.ExecuteNonQuery();
            }
        }
    }

    internal void Excluir(int id)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_usuarios = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_usuarios.CommandText = $"DELETE from usuarios WHERE Id = '{id}'";
                tabela_usuarios.ExecuteNonQuery();

                Console.WriteLine($"O registro id: {id} foi excluído");
            }
        }
    }

    internal void Atualizar(Gerenciador gerenciador)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_usuarios = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_usuarios.CommandText = 
                    $@"UPDATE usuarios SET 
                        PrimeiroNome = '{gerenciador.PrimeiroNome}', 
                        SegundoNome = '{gerenciador.SegundoNome}',
                        Email = '{gerenciador.Email}',
                        Celular = '{gerenciador.Celular}', 
                        Senha = '{gerenciador.Senha}',
                        Genero = '{gerenciador.Genero}' 
                       WHERE 
                        Id = {gerenciador.Id}
                     ";

                tabela_usuarios.ExecuteNonQuery();
            }
        }
        Console.WriteLine($"Registo id: {gerenciador.Id} foi atualizado");

    }
    internal void Get()
    {
        List<Gerenciador> tabela = new List<Gerenciador>();
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_usuarios = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_usuarios.CommandText = "SELECT * FROM usuarios";

                using (var reader = tabela_usuarios.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tabela.Add(
                            new Gerenciador
                            {
                                Id = reader.GetInt32(0),
                                PrimeiroNome = reader.GetString(1),
                                SegundoNome = reader.GetString(2),
                                Email = reader.GetString(3),
                                Celular = reader.GetString(4),
                                Senha = reader.GetString(5),
                                Genero = reader.GetChar(6)
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma coluna foi encontrada");
                    }
                }

            }
        }
        Ver_Tabela.ShowTable(tabela);

    }

    internal Gerenciador GetId(int id)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela_usuarios = conexao.CreateCommand())
            {
                conexao.Open();
                tabela_usuarios.CommandText = $"SELECT * FROM usuarios Where Id = '{id}'";

                using (var reader = tabela_usuarios.ExecuteReader())
                {
                    Gerenciador gerenciador = new();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        gerenciador.Id = reader.GetInt32(0);
                        gerenciador.PrimeiroNome = reader.GetString(1);
                        gerenciador.SegundoNome = reader.GetString(2);
                        gerenciador.Email = reader.GetString(3);
                        gerenciador.Celular = reader.GetString(4);
                        gerenciador.Senha = reader.GetString(5);
                        gerenciador.Genero = reader.GetChar(6);
                    }

                        Console.WriteLine("\n\n");

                        return gerenciador;
                    };

            }
        }
    }

}

