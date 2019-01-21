using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestão_De_requisições
{
    static class Program
    {
        public static string utilname;
        public static string utilemail;
        public static string tipoad; //Ajuda saber no form  samuka se o utilizador é admin ou outro
 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
