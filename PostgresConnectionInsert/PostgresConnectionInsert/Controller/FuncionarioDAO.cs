using Npgsql;
using PostgresConnectionInsert.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PostgresConnectionInsert.Controller
{
    internal class FuncionarioDAO
    {
        Dataset dt = new Dataset();
        NpgsqlCommand cmd;
        NpgsqlConnection conn;
        DataTable dataT;
        

        public FuncionarioDAO() { }
        
        

        public async void InsertFunc(Funcionarios c)
        {
            Form1 form = new Form1();
            var stcx = dt.getConnectionString();
             

            try
            {
                using (conn = new NpgsqlConnection(stcx))
                {
                    conn.Open();
                    string query = ("Insert Into funcionarios(cpf,primeiro_nome,segundo_nome) values(@cpf,@primeiro_nome,@segundo_nome)");
                    using (cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cpf", c.getCpf());
                        cmd.Parameters.AddWithValue("@primeiro_nome", c.getFirstName());
                        cmd.Parameters.AddWithValue("@segundo_nome", c.getLastName());
                        cmd.ExecuteNonQuery();
                        form.Select();
                        conn.Close();
                    }
                }
                MessageBox.Show("Funcionario Cadastrado Com Sucesso!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha na conexão Insert");
            }
        }
    }
}
