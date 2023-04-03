using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresConnectionInsert.Model
{
    internal class Dataset
    {
        static string ip ="127.0.0.1";
        static string database = "Dados";
        static string port ="5432";
        static string user = "postgres";
        static string password = "123456";

        string connectionString = String.Format("Host={0};Port={1};Username={2};Password={3};Database={4};",
            ip,
            port,
            user,
            password,
            database);
            
      //  NpgsqlConnection conn;
        public string getConnectionString() { 
            return this.connectionString;
        }
        public async void OpenConnection() {
            try
            {
              await using var conn = new NpgsqlConnection(connectionString);
              conn.Open();
              
            }
            catch (Exception ex)
            {
                //Show("Error na Conexão com banco de dados!");

                MessageBox.Show(ex.Message + "ERROR NA CONEXÃO");
                //conn.Close();
            }
        }
            
    }
}
