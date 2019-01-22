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
    public partial class CriarcontaAdminandSec : Form
    {

        string hu = @"histórico de utilizadores.txt";//nome de um ficheiro que guarda info sobre todos os utilizadores que já criaram contas nesta aplicacao
        string nome = @"utilizadores.txt";

        public CriarcontaAdminandSec()
        {
            InitializeComponent();
        }


        //criar utilizador
        private void button1_Click(object sender, EventArgs e)
        {

            string tipo = "";



            //Caso as textbox estejam vazias 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Existem Campos do Formulário por completar", "Formulário Incompleto", MessageBoxButtons.OK);
            }
           
            else
            {

                var contains = "@esmad.ipp.pt";
                bool exist = textBox2.Text.Contains(contains);

                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Confirme Correctamente o seu email", "Confirmação do Email", MessageBoxButtons.OK);
                }
                else if (textBox4.Text != textBox5.Text)
                {
                    MessageBox.Show("Confirme Correctamente o seu passord", "Confirmação do Password ", MessageBoxButtons.OK);

                }

                else if (exist == false)
                {
                    MessageBox.Show("Confirme Correctamente o seu email", "Email inválido", MessageBoxButtons.OK);
                }

                else if(checkBox1.Checked==false && checkBox2.Checked==false)
                {
                    MessageBox.Show("Confirme  o tipo de utilizador que esta conta vai ser", "Confirmar o tipo de utilizador", MessageBoxButtons.OK);
                }
             

                else
                {

                    int cont = 0;//Variavel contadora para aumentar o numero que vai ser atribuido ao Id da pessoa 
                    string li;
                    //string li2;// serve para impedir que seja atribuido um mesmo Id a utilizadores diferentes
                    StreamReader sr = File.OpenText(hu);
                    //Conta a quantidade de elementos existentes no array para atribuir um numero de identificacao ou seja o id;
                    while ((li = sr.ReadLine()) != null)
                    {
                        cont++;
                    }
                    sr.Close();




                    contas utilizador = new contas();// chama a classe criado nesse caso chama a instancia empty 
                    utilizador.id = cont;
                    utilizador.utilizador = textBox1.Text;
                    utilizador.email = textBox2.Text;
                    utilizador.password = textBox4.Text;
                    if (checkBox1.Checked==true)
                    {
                        tipo = "admin";
                    }
                    else if (checkBox2.Checked == true)
                    {
                        tipo = "seguranca";
                    }
                    utilizador.tipo = tipo;


                    string mail = utilizador.email;
                    string nomeutilizador = utilizador.utilizador;

                    /*if ((usernameval(nomeutilizador) != true))
                    {

                    }*/

                    if (emailval(mail) != true)
                    {
                        MessageBox.Show("O  email  já contem conta na aplicação ", "Conta Existente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (usernameval(nomeutilizador) == false)
                    {
                        MessageBox.Show("Esse nome já foi escolhido por um outro utilizador", "Nome de Perfil Indispónivel", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        //prepara a estrutura para ser armazenada
                        string linha = ((utilizador.id.ToString()) + ";" + utilizador.utilizador + ";" + utilizador.email + ";" + utilizador.password + ";" + utilizador.tipo);

                        //Adicona a nova conta ao ficheiro 
                        StreamWriter sw = File.AppendText(nome);
                        sw.WriteLine(linha);
                        sw.Close();


                        //Adiciona o novo utilizador ao ficheiro historico de utilizador para que esse possa ficar registrado 
                        StreamWriter sw2 = File.AppendText(hu);
                        sw2.WriteLine(linha);
                        sw2.Close();

                        // depois de ser criada a conta ele envia directamente para o form inicial 
                        Form f1 = new Adminp2();
                        f1.Show();
                        this.Hide();


                    }





























                }
            }
        }





        //Sendo que os email nao podem ser o mesmo caso o email ja for existente essa funcao impede que essa seja criada 
        public bool emailval(string mail)
        {

            StreamReader sr = File.OpenText(nome);
            string linha;


            while ((linha = sr.ReadLine()) != null)
            {

                string[] fill = linha.Split(';');


                if (fill.Count() < 2)//confirma se e possivel criar contas  sendo que esse pode conter um elemento invisivel 
                {
                    sr.Close();
                    return true;

                }


                else if (mail == fill[2])
                {
                    sr.Close();
                    return false;

                }


            }
            sr.Close();
            return true;
        }



        public bool usernameval(string nomeutilizador)
        {

            StreamReader sr = File.OpenText(nome);
            string linha;


            while ((linha = sr.ReadLine()) != null)
            {

                string[] fill = linha.Split(';');


                if (fill.Count() < 2)//confirma se e possivel criar contas  sendo que esse pode conter um elemento invisivel 
                {
                    sr.Close();
                    return true;

                }


                else if (nomeutilizador == fill[1])
                {
                    sr.Close();
                    return false;

                }


            }
            sr.Close();
         
            return true;
        }
        
        //elemina a posibilidade de ser  atribuida caracteristicas duplas
        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f1 = new Adminp2();
            f1.Show();
            this.Hide();
        }
    }
}
