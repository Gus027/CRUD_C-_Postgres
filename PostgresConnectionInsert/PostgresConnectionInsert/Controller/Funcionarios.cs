using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresConnectionInsert.Controller
{
    internal class Funcionarios
    {
        private string cpf;
        private string firstName;
        private string lastName;

        public void setCpf(string cpf) { 
            this.cpf = cpf;
        }
        public void setFirstName(string firstname)
        {
            this.firstName = firstname;
        }
        public void setLastName(string lastname)
        {
            this.lastName = lastname;
        }

        public string getCpf()
        {
            return this.cpf;
        }

        public string getFirstName() { 
            return this.firstName;
        }

        public string getLastName()
        {
            return this.lastName;
        }
    }
}
