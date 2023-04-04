using Npgsql;
using PostgresConnectionInsert.Controller;
using PostgresConnectionInsert.Model;
using System;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostgresConnectionInsert
{
    public partial class Form1 : Form
    {
        Dataset dt = new Dataset();
        NpgsqlCommand cmd;
        NpgsqlConnection conn;
        DataTable dataT;
        private int rowIndex = -1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Show(string v)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //conn = new NpgsqlConnection(connectionString);
            Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Função de cadastro
            Button btCadastrar = sender as Button;

            Funcionarios f = new Funcionarios();
            FuncionarioDAO fdao = new FuncionarioDAO();
            
            try {
                f.setCpf(txtCpf.Text);
                f.setFirstName(txtFirstName.Text);
                f.setLastName(txtLastName.Text);
                try {
                    fdao.InsertFunc(f);
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+"Error ao inserir os dados do Funcionario.");
                }
                dt.OpenConnection();
                Select();


                // MessageBox.Show("Saved sucessful");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+" ERROR NA CONEXÃO");
            }
            
        }

        public void Select()
        {

            var stcx = dt.getConnectionString();
            try
            {
                using (conn = new NpgsqlConnection(stcx))
                {
                    conn.Open();
                    string querySelect = @"SELECT * FROM funcionarios ORDER BY primeiro_nome;";
                    cmd = new NpgsqlCommand(querySelect, conn);
                    dataT = new DataTable();
                    dataT.Load(cmd.ExecuteReader());
                    conn.Close();
                    dgvData.DataSource = null; //Reset dataGrid
                    dgvData.DataSource = dataT;
                    
                }

            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error na Select!");
            }
        }
        public void deleteCpf() 
        {
            try
            {

            }
            catch(Exception ex )
            {
            
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
         //   FuncionarioDAO fdao = new FuncionarioDAO();
            Select();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0) 
            {
                MessageBox.Show("Favor especificar o funcionario.");
                return;
            }
            try 
            {
                conn.Open();
                string sql = @"SELECT * FROM row_delete_cpf(:_cpf)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_cpf", int.Parse(dgvData.Rows[rowIndex].Cells["cpf"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Delete Funcionario successfully");
                    rowIndex = -1;
                    Select();
                }
                conn.Close();
            }catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show("Delete fail: ERROR -> "+ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Please Choose funcionario to update");
                return;
            }
            txtFirstName.Enabled = txtLastName.Enabled = true;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                txtCpf.Text = dgvData.Rows[e.RowIndex].Cells["Cpf"].Value.ToString();
                txtFirstName.Text = dgvData.Rows[e.RowIndex].Cells["primeiro_nome"].Value.ToString();
                txtLastName.Text = dgvData.Rows[e.RowIndex].Cells["segundo_nome"].Value.ToString();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            int result=0;
            if (rowIndex < 0)
            {
                try
                {
                    conn.Open();
                    string sql = @"select * from row_insert_cpf(:_primeiro_nome,:_segundo_nome)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_primeiro_nome", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("_segundo_nome", txtLastName.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Inserted new Funcionario Successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Inserted fail");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Inserted fail ERROR: ->" + ex.Message);
                }
            }
            else //update
            {
                try 
                {
                    conn.Open();
                    string sql = @"select * from row_update_cpf(:_cpf,_primeiro_nome,_segundo_nome)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_cpf", int.Parse(dgvData.Rows[rowIndex].Cells["cpf"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_primeiro_nome",txtFirstName.Text);
                    cmd.Parameters.AddWithValue("_segundo_nome", txtLastName.Text);
                    result=(int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Update Successfully");
                        Select();
                    }
                    else 
                    {
                        MessageBox.Show("Update Fail");
                    }
                    
                    
                }
                catch (Exception ex)             
                {
                    MessageBox.Show("Update fail ERROR: ->" + ex.Message);
                }
            }
            result = 0;
            txtFirstName.Text = txtLastName.Text = null;
            txtFirstName.Enabled=txtLastName.Enabled=true;
        }



        private void jsonButton_Click(object sender, EventArgs e)
        {
            Json j = new Json();
            j.DataTable_JSON_StringBuilder(dataT);
        }
    }
}