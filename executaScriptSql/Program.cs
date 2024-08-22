using System;
using System.Data.SqlClient;
using System.IO;

namespace ExecuteSQLScriptsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=server;Initial Catalog=EasyNFe;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True"; // Substitua pela sua string de conexão
            string scriptsFolder = @"C:\Users\w10\Desktop\Nova pasta\ScriptsInsert"; // Substitua pelo caminho da sua pasta de scripts

            string[] scriptFiles = Directory.GetFiles(scriptsFolder, "*.sql");

            foreach (string scriptFile in scriptFiles)
            {
                string script = File.ReadAllText(scriptFile);

                try
                {
                    ExecuteScript(connectionString, script);
                    Console.WriteLine($"Script '{Path.GetFileName(scriptFile)}' executado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar o script '{Path.GetFileName(scriptFile)}': {ex.Message}");
                }
            }
        }

        static void ExecuteScript(string connectionString, string script)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(script, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
