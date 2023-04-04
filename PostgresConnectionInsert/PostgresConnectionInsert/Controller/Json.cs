using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nancy.Json;
using System.Data;

namespace PostgresConnectionInsert.Controller
{
    internal class Json
    {
        private string cpfFuncionario;
        private string firsName;
        private string lastName;
        
       
       public void fucJson()
        {
            //-----Testing method in console------

            FuncionarioDAO fdao = new FuncionarioDAO();
            Funcionarios f = new Funcionarios();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string json = String.Format(@"{ ""CPF"" : {0}, ""FirstName"" : {1}, ""LastName"": {2} }",
                f.getCpf(),f.getFirstName(),f.getLastName());

            dynamic resultado = serializer.DeserializeObject(json);

            Console.WriteLine("  ==  Resultado da leitura do arquivo JSON  == ");
            Console.WriteLine("");

            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value as string;
                Console.WriteLine(String.Format("{0} : {1}", key, value));
            }

            Console.WriteLine("");
            Console.WriteLine(serializer.Serialize(resultado));

          
        }
        public string DataTable_JSON_StringBuilder(DataTable tabela)
        {
            var JSONString = new StringBuilder();
            if (tabela.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < tabela.Columns.Count; j++)
                    {
                        if (j < tabela.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + tabela.Columns[j].ColumnName.ToString() + "\":" + "\"" + tabela.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == tabela.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + tabela.Columns[j].ColumnName.ToString() + "\":" + "\"" + tabela.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == tabela.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            MessageBox.Show(JSONString.ToString());
            return JSONString.ToString();
            
        }
    }
}
