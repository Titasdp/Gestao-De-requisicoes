using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestão_De_requisições
{
    //Tratamento das requisicoes 
    class requisitar
    {
        public string docente;
        public string data;
        public string hora;
        public string sala;
        public string objeto;
        public string dataentre;
        public string horaentre;



        public requisitar()
        {

        }




        public requisitar(string docente, string data, string hora, string sala, string objeto,string dataentre, string horaentre)
        {

            this.docente = docente;
            this.data = data;
            this.hora = hora;
            this.sala = sala;
            this.objeto = objeto;
            this.dataentre = dataentre;
            this.horaentre = horaentre;

        }






    }
}
