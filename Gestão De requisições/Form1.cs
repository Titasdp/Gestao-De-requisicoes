using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gestão_De_requisições
{
    public partial class Form1 : Form
    {
        string req_global = @"requniversal.txt";// para o sistema de alerta;
        contas utilizador = new contas();
        string hu = @"histórico de utilizadores.txt";//nome de um ficheiro que guarda info sobre todos os utilizadores que já criaram contas nesta aplicacao  
        string nome = @"utilizadores.txt";// nome do ficheiro dos utilizadores 
        string conversa = @"conversa.txt";
        string salas = @"salas.txt";//ficheiro de salas
        string objectos = @"objectos.txt";//Ficheiro dos objetos
        string requisicoes = ("R_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");

        public Form1()
        {
            InitializeComponent();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //This button will open the main pages (obs: right now it only openes the first page of the "Docente")
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")// Caso os campos do formulários necessitam de ser completados 
            {
                MessageBox.Show("Existem Campos do Formulário por completar", "Formulário Incompleto", MessageBoxButtons.OK);
            }
            else // caso o formulario esteja completado 
            {
                string pass = textBox2.Text;// variaveis necessario para fazer a confirmacao do  password
                string email = textBox1.Text;// Variavel para confirmar password 

                if (confperfil(email) != true)
                {
                    MessageBox.Show("O  email  não contem conta na aplicação ", "Conta Existente", MessageBoxButtons.OK);
                }
                else if (Confpass(pass) != true)
                {
                    MessageBox.Show("O  password está incorrecto ", "Password incorrecto", MessageBoxButtons.OK);
                }
                else
                {
                    //contas utilizador = new contas();
                    string linha;
                    
                    StreamReader sr = File.OpenText(nome);


                    //Permite guardar os valores dos atributos dos utilizadores e consequentemente permite com que esse se abra o form dependendo do tipo do utilizador 
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] fill = linha.Split(';');


                        if (fill[2] == textBox1.Text && fill[3] == textBox2.Text)
                        {


                            utilizador.id = Convert.ToInt16(fill[0]);
                            utilizador.utilizador = fill[1];
                            Program.utilname = utilizador.utilizador;//Tranfera o nome do utilizador para uma variavel "universal"
                            utilizador.email = fill[2];
                            Program.utilemail = utilizador.email;//Tranfera email do utilizador para uma variavel "universal"
                            utilizador.password = fill[3];
                            utilizador.tipo = fill[4];

                        }
                    }
                    sr.Close();


                    //Abre o  form do docente caso esse o tipo de utilizador for docente 
                    if (utilizador.tipo == "docente")
                    {
                        Form f3 = new samuka();
                        f3.Show();
                        this.Hide();

                    }
                    else if ((utilizador.tipo)=="seguranca")
                    {
                        Form f4 = new Sfirst();
                        f4.Show();
                        this.Hide();

                    }
                    else if ((utilizador.tipo).TrimEnd()=="admin")
                    {
                        Form f4 = new Adminp2();
                        f4.Show();
                        this.Hide();
                    }






                }


            }






        }

        //This button opens the Sign Up form 
        private void Button2_Click(object sender, EventArgs e)
        {
            Form f2 = new Signup();
            f2.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //na nabertura caso o ficheiro de utilizadores nao existir esse cria o ficheiro (nesse projecto ainda so serve para dar um id a pessoa)
        private void Form1_Load(object sender, EventArgs e)
        {
            if ((File.Exists(req_global))==false)
            {
                StreamWriter sr = File.CreateText(req_global);
                sr.Close();
            }
            else
            {
                StreamReader sw = File.OpenText(req_global);//
                sw.Close();
            }

            // cria o ficheiro historico de utilizador caso esse nao exisrte
            if ((File.Exists(hu)) == false)//caso nao existir 
            {
                StreamWriter sr = File.CreateText(hu);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(hu);//
                sw.Close();
            }


            // cria o ficheiro de requisicoes sendo que cada dia faz um novo ficheiro de requizicao 
            if ((File.Exists(requisicoes)) == false)//caso nao existir 
            {
                StreamWriter sr = File.CreateText(requisicoes);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(requisicoes);//evitar erros 
                sw.Close();
            }


            //criar ficheiro chat
            if ((File.Exists(conversa)) == false)
            {
                StreamWriter sr = File.CreateText(conversa);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(conversa);//evitar erro
                sw.Close();

            }


            //Criar  ficheiro salas
            if ((File.Exists(salas)) == false)
            {
                StreamWriter sr = File.CreateText(salas);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(salas);//evitar erro
                sw.Close();
            }

            //Criar ficheiro objetos 
            if ((File.Exists(objectos)) == false)
            {
                StreamWriter sr = File.CreateText(objectos);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(objectos);// evitar erros
                sw.Close();

            }





            // ficheiro de utilizadores nesse programa
            if ((File.Exists(nome)) == false)//caso nao existir 
            {
                StreamWriter sr = File.CreateText(nome);
                sr.Close();

            }
            else
            {
                StreamReader sw = File.OpenText(nome);//utilizo esse metedo para impedir erros de occorer 
                sw.Close();

            }

        }





        //Confirma se o password existe
        public bool Confpass(string pass)
        {

            string linha;
            StreamReader sr = File.OpenText(nome);


            while ((linha = sr.ReadLine()) != null)
            {

                string[] fill = linha.Split(';');


                if (fill[3] == pass)
                {
                    sr.Close();
                    return true;
                  
                }

            }
            sr.Close();

            return false;
        }





        //Confirma se o email existe 
        public bool confperfil(string email)
        {

            string linha;
            StreamReader sr = File.OpenText(nome);


            while ((linha = sr.ReadLine()) != null)
            {
                string[] fill = linha.Split(';');
                if (fill.Count() < 2)//confirma se e possivel abrir  contas  sendo que esse pode conter um elemento invisivel 
                {
                    sr.Close();
                    return true;

                }

                else if (fill[2] == email)
                {
                    sr.Close();
                    return true;
                }

            }
            sr.Close();
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
