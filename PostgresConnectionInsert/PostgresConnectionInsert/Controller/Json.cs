using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nancy.Json;

namespace PostgresConnectionInsert.Controller
{
    internal class Json
    {
        private string cpfFuncionario;
        private string firsName;
        private string lastName;
        
       
       public void fucJson()
        {
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
            MessageBox.Show(serializer.Serialize(resultado));

            /*
            Funcionarios f = new Funcionarios();

            var Json = new Json
            {
                cpfFuncionario = f.getCpf(),
                firsName = f.getFirstName(),
                lastName = f.getLastName()
            };

            string JsonTxt = JsonSerializer.Serialize(Json);

            Console.WriteLine(JsonTxt);
            MessageBox.Show("Json executado com sucesso!");
            */
        }    
    }
}
