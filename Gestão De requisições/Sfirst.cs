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
    public partial class Sfirst : Form
    {

        string alterar; //alteracoes no file
        string comparacao; // faz a comparacao 


        string conversa = @"conversa.txt";
        string salas = @"salas.txt";
        string objectos = @"objectos.txt";
        string requisicoes = ("R_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");
        string apoio = @"apoio.txt";

        chat message = new chat();


        public Sfirst()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //Ao fazer o log in  transfire todas as mensagens enviadas para os seguranças 
        private void Sfirst_Load(object sender, EventArgs e)
        {

            actualizarchat();//Chama a funcao que acrescenta as mensagens direcionadas aos seguranças na listbox 

        }


        //funcao actualizadora de chat
        public void actualizarchat()
        {





            string linha;
            StreamReader sw = File.OpenText(conversa);

            while ((linha = sw.ReadLine()) != null)
            {
                string[] fill = linha.Split(';');

                if (fill[3] == "seguranca" && fill.Length<5)
                {

                    message.utilizador = fill[0];
                    message.conversa = fill[2];


                    string linha2 = message.utilizador + ":" + message.conversa;

                    listBox1.Items.Add(linha2);
                }

            }
            sw.Close();
        }

        //ao escolheres um dos comentarios/ mensagem essa textbox mostra a quem vais responder  
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string linha = listBox1.SelectedItem.ToString();
            string[] fill = linha.Split(':');
            textBox3.Text = fill[0];
        }


        //Envia a mensagem ao utilizador que se pretende responder 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text=="")
            {
                MessageBox.Show("Escolha quem deseja responder", "Escolher Receptor",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else if (textBox3.Text=="EU")
            {
                MessageBox.Show("Não pode responder às suas próprias mensagens", "Escolher Receptor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string[] telegraf = listBox1.SelectedItem.ToString().Split(':');
                string email="";//Email para que caso exista multiplas pessoas com o mesmo nome sendo que nesse programa o Id e o email é  que diferenciam as pessoas 
                string linha;//Adiciona ao ficheiro  
                string linha2;//Adiciona a listbox
                chat mes = new chat();
                contas utilizador = new contas();

                //Abre para poder retitrar o email do receptor para que a mensagem nao seja enviada para  um pessoa errada
                string emailcap;
                StreamReader sr = File.OpenText(conversa);
                while ((emailcap=sr.ReadLine())!=null)
                {
                    string[] fill = emailcap.Split(';');



                    if (fill[0]== textBox3.Text && telegraf[1]==fill[2])
                    {
                        email = fill[1];
                    }

    


                }
                sr.Close();









                mes.utilizador = Program.utilname;
                mes.conversa = textBox2.Text;
                mes.email = Program.utilemail;
                mes.para = textBox3.Text;


                linha = (mes.utilizador + ";" + mes.email + ";" + mes.conversa + ";" + mes.para+ ";" + email);


                //Adiciopna a mensagem de uma forma estruturada no ficheiro 
                StreamWriter sw = File.AppendText(conversa);
                sw.WriteLine(linha);
                sw.Close();


                //adiciona a listbox
                linha2 = ("EU" + ":" + mes.conversa);
                listBox1.Items.Add(linha2);
                textBox2.Text = "";
                
            }
           

        }

        //Actualizar a listbox
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//eliminar os elementos 
            actualizarchat();//Chama a funcao que acrescenta as mensagens direcionadas aos seguranças na listbox 
        }


        //acrescenta um valor  "resolvido" ao existente na lisbox para afirmar ao utilizador que o seu problema foi resolvido 
        private void button4_Click(object sender, EventArgs e)
        {

              DialogResult  resposta= MessageBox.Show("O problema já foi completamente resolvido?", "Resolvido", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta== DialogResult.Yes)
            {
            string[] resolver = listBox1.SelectedItem.ToString().Split(':');
            
            string linha;//linha como esta presente na actualidade  
            string linha2;

            //Abre ficheiro para retirar a mensagem e o nome do itilizador para colocar numa nova string que vai ser alterado no ficheiro 
            StreamReader sr = File.OpenText(conversa);
            while((linha= sr.ReadLine())!=null)
            {
                string[] fill = linha.Split(';');

                if (fill[0]==resolver[0]&& fill[2]==resolver[1] && fill.Length<5)
                {
                    alterar = linha +";"+"resolvido"+";"+ Program.utilname; // Alteracao para submeter para o ficheiro actualizado  
                    comparacao = linha;

                }

            }
            sr.Close();




            //Cria o ficheiro de apoio impedindo a perda de informacao ao eliminar  

                StreamWriter sw = File.CreateText(apoio);
                sw.Close();


             
             
                sr = File.OpenText(conversa);
                while ((linha2 = sr.ReadLine()) != null)
                {
                    //ESCREVE NO OURTO FICHEIRO
                    StreamWriter sw2 = File.AppendText(apoio);

                    if (comparacao ==  linha2)//caso a linha encontrada for igual a linha escrita no ficheiro
                    {
                        sw2.WriteLine(alterar);//passa para o outro ficheiro a alteracao  sendo que impede com que a linha seja escrita 
                        sw2.Close();
                    }
                    else
                    {
                        sw2.WriteLine(linha2);//caso nao for encontrada algo igual ele escreve a linha como esta escrita
                        sw2.Close();
                    }

                }
                sr.Close();


                File.Delete(conversa);//Elimina o ficheiro anterior para permitir refazelo do zero 
                StreamWriter sw3 = File.CreateText(conversa);//Recria o ficheiro para ser tranferido de novo os dados atualizados 
                sw3.Close();

                string linharecr;//escreve de novo no ficheiro actualizado

                sr = File.OpenText(apoio);
                while ((linharecr = sr.ReadLine()) != null)
                {
                    //Devolve os dados para o ficheiro reconstruido 
                    StreamWriter sw2 = File.AppendText(conversa);
                    sw2.WriteLine(linharecr);
                    sw2.Close();


                }
                sr.Close();

                File.Delete(apoio);//elimina o ficheiro de apoio 


            }








        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
