using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestão_De_requisições
{
    //Utilizado para facilitar a conversa 
    class chat
    {

       
        public string utilizador;
        public string conversa;
        public string email;
        public string para;



        public chat()
        {
        }


        public chat(string utilizador, string email, string para , string conversa)

        {
            
            this.utilizador = utilizador;
            this.conversa = conversa;
            this.email = email;
            this.para = para;
        }








    }
}
