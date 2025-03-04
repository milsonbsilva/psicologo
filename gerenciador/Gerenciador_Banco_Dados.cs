using Microsoft.Data.Sqlite;

internal class Gerenciador_Banco_Dados
{
    internal void NovaTabela(string conexao_string)
    {
        using (var conexao = new SqliteConnection(conexao_string))
        {
            using (var tabela = conexao.CreateCommand())
            {
                conexao.Open();

            tabela.CommandText =
                @"CREATE TABLE IF NOT EXISTS usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    PrimeiroNome TEXT, 
                    SegundoNome TEXT, 
                    Email TEXT,
                    Celular TEXT,
                    Senha TEXT,
                    Genero VARCHAR(1)
                )";

                tabela.ExecuteNonQuery();

            }
        }
    }
}