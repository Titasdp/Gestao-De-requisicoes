using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestão_De_requisições
{
   // criacao de uma classe necessaria para guardar os dados no log in e no sign up e consequentemente no resto da aplicacao 
    class contas
    {
        // Atributos do utilizador 
        public int id; //podera vir a desaparecer 
        public string utilizador;
        public string email;
        public string password;
        public string tipo;

        //Activador/ instancia  nao activada
        public contas()
        {
        }

        //instancia inicializada
        public contas(int id, string utilizador , string email, string password, string tipo)

        {
            this.id = id;
            this.utilizador = utilizador;
            this.email = email;
            this.password = password;
            this.tipo = tipo;
        }


    }
}
