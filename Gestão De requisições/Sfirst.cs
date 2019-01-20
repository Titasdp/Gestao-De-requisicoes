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

            StreamReader sr;
           
            //Retira as salas do ficheiro salas e acrescenta na comboxbox das salas



            sr = File.OpenText(salas);
            string linha;
            while ((linha = sr.ReadLine()) != null)    //OBS: FALTA DE CONSTRUCAO 
            {
                comboBox1.Items.Add(linha);
            }
            sr.Close();


            //Retira os objectos do ficheiro dos objectos e acrescenta na combobox dos objectos
            sr = File.OpenText(objectos);
            string linha2;
            while ((linha2 = sr.ReadLine()) != null) //FALTA DE CONSTRUCAO 
            {

                string[] fill = linha2.Split(';');

                comboBox2.Items.Add(fill[1]);
            }
            sr.Close();

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




        //consulta
        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            int lin = 0;
            DateTime data = Convert.ToDateTime(dateTimePicker1.Text);

            string filedata = ("R_" + data.ToString("yyyy-MM-dd") + ".txt");



            if (File.Exists(filedata) != true)
            {
                MessageBox.Show("O ficheiro dessa data não foi encontrado", "Ficheiro inexistente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string linha;
                StreamReader sr = File.OpenText(filedata);


                while ((linha = sr.ReadLine()) != null)
                {
                    string[] fill = linha.Split(';');
                    if (comboBox1.SelectedItem == null)// filtros de salas  desativadas  
                    {

                        if (comboBox2.SelectedItem == null)//filtro de objetos off
                        {


                            if (textBox1.Text == "")//filtro de utilizadores off
                            {



                                if (fill.Length <= 5)
                                {
                                    dataGridView1.Rows.Add(1);
                                    dataGridView1[0, lin].Value = fill[0];
                                    dataGridView1[1, lin].Value = fill[2];
                                    dataGridView1[2, lin].Value = fill[1];
                                    dataGridView1[3, lin].Value = fill[3];
                                    dataGridView1[4, lin].Value = fill[4];
                                    dataGridView1[5, lin].Value = "-";
                                    dataGridView1[6, lin].Value = "-";
                                    lin++;


                                }
                                else if (fill.Length > 5)
                                {

                                    dataGridView1.Rows.Add(1);
                                    dataGridView1[0, lin].Value = fill[0];
                                    dataGridView1[1, lin].Value = fill[2];
                                    dataGridView1[2, lin].Value = fill[1];
                                    dataGridView1[3, lin].Value = fill[3];
                                    dataGridView1[4, lin].Value = fill[4];
                                    dataGridView1[5, lin].Value = fill[6];
                                    dataGridView1[6, lin].Value = fill[5];
                                    lin++;
                                }




                            }
                            else if (textBox1.Text != "")//filtro de utilizadores on
                            {


                                if (fill[0] == textBox1.Text)
                                {


                                    if (fill.Length <= 5)
                                    {
                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = "-";
                                        dataGridView1[6, lin].Value = "-";
                                        lin++;


                                    }
                                    else if (fill.Length > 5)
                                    {

                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = fill[6];
                                        dataGridView1[6, lin].Value = fill[5];
                                        lin++;
                                    }







                                }

                            }






                        }

                        else if (comboBox2.SelectedItem != null)//filtro de objetos on
                        {


                            if (comboBox2.Text == fill[4])
                            {


                                if (textBox1.Text == "")//filtro de utilizadores off
                                {



                                    if (fill.Length <= 5)
                                    {
                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = "-";
                                        dataGridView1[6, lin].Value = "-";
                                        lin++;


                                    }
                                    else if (fill.Length > 5)
                                    {
                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = fill[6];
                                        dataGridView1[6, lin].Value = fill[5];
                                        lin++;
                                    }




                                }
                                else if (textBox1.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox1.Text)
                                    {


                                        if (fill.Length <= 5)
                                        {
                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = "-";
                                            dataGridView1[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = fill[6];
                                            dataGridView1[6, lin].Value = fill[5];
                                            lin++;

                                        }

                                    }

                                }
                            }
                        }
                    }
                    else if (comboBox1.SelectedItem != null)// filtro de salas ativadas
                    {


                        if (fill[3] == comboBox1.SelectedItem.ToString())
                        {


                            if (comboBox2.SelectedItem == null)//filtro de objetos off
                            {


                                if (textBox1.Text == "")//filtro de utilizadores off
                                {



                                    if (fill.Length <= 5)
                                    {
                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = "-";
                                        dataGridView1[6, lin].Value = "-";
                                        lin++;


                                    }
                                    else if (fill.Length > 5)
                                    {

                                        dataGridView1.Rows.Add(1);
                                        dataGridView1[0, lin].Value = fill[0];
                                        dataGridView1[1, lin].Value = fill[2];
                                        dataGridView1[2, lin].Value = fill[1];
                                        dataGridView1[3, lin].Value = fill[3];
                                        dataGridView1[4, lin].Value = fill[4];
                                        dataGridView1[5, lin].Value = fill[6];
                                        dataGridView1[6, lin].Value = fill[5];
                                        lin++;

                                    }




                                }
                                else if (textBox1.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox1.Text)
                                    {


                                        if (fill.Length <= 5)
                                        {
                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = "-";
                                            dataGridView1[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = fill[6];
                                            dataGridView1[6, lin].Value = fill[5];
                                            lin++;
                                        }







                                    }

                                }






                            }

                            else if (comboBox2.SelectedItem != null)//filtro de objetos on
                            {


                                if (comboBox2.Text == fill[4])
                                {


                                    if (textBox1.Text == "")//filtro de utilizadores off
                                    {



                                        if (fill.Length <= 5)
                                        {
                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = "-";
                                            dataGridView1[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView1.Rows.Add(1);
                                            dataGridView1[0, lin].Value = fill[0];
                                            dataGridView1[1, lin].Value = fill[2];
                                            dataGridView1[2, lin].Value = fill[1];
                                            dataGridView1[3, lin].Value = fill[3];
                                            dataGridView1[4, lin].Value = fill[4];
                                            dataGridView1[5, lin].Value = fill[6];
                                            dataGridView1[6, lin].Value = fill[5];
                                            lin++;
                                        }




                                    }
                                    else if (textBox1.Text != "")//filtro de utilizadores on
                                    {


                                        if (fill[0] == textBox1.Text)
                                        {


                                            if (fill.Length <= 5)
                                            {
                                                dataGridView1.Rows.Add(1);
                                                dataGridView1[0, lin].Value = fill[0];
                                                dataGridView1[1, lin].Value = fill[2];
                                                dataGridView1[2, lin].Value = fill[1];
                                                dataGridView1[3, lin].Value = fill[3];
                                                dataGridView1[4, lin].Value = fill[4];
                                                dataGridView1[5, lin].Value = "-";
                                                dataGridView1[6, lin].Value = "-";
                                                lin++;


                                            }
                                            else if (fill.Length > 5)
                                            {

                                                dataGridView1.Rows.Add(1);
                                                dataGridView1[0, lin].Value = fill[0];
                                                dataGridView1[1, lin].Value = fill[2];
                                                dataGridView1[2, lin].Value = fill[1];
                                                dataGridView1[3, lin].Value = fill[3];
                                                dataGridView1[4, lin].Value = fill[4];
                                                dataGridView1[5, lin].Value = fill[6];
                                                dataGridView1[6, lin].Value = fill[5];
                                                lin++;

                                            }

                                        }

                                    }
                                }
                            }
                        }

                    }

                }
                sr.Close();

            }



        }

        //Filtro por salas e objetos 
        /*
        public void por_salas_e_objectos() {





            int lin = 0;

            requisitar req = new requisitar();

            string linha = "";

            //abre o ficheiro para adicionar à datagridview os ficheiros existents
            StreamReader sr;


            if (comboBox2.SelectedItem!=null && comboBox1.SelectedItem!=null)
            {


                sr = File.OpenText(requisicoes);

                while ((linha = sr.ReadLine()) != null)
                {
                    

                    string[] fill = linha.Split(';');

                    if (comboBox1.SelectedItem.ToString() == fill[4] && fill[3] == comboBox2.SelectedItem.ToString())
                    {
                        dataGridView1.Rows.Add(1);
                        if (fill.Length <= 5)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = "";
                            req.dataentre = "";
                        }
                        else if (fill.Length >= 7)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = fill[5];
                            req.dataentre = fill[6];

                        }
                        //Adiciona à datagridview
                        dataGridView1[0, lin].Value = req.docente;
                        dataGridView1[1, lin].Value = req.hora;
                        dataGridView1[2, lin].Value = req.data;
                        dataGridView1[3, lin].Value = req.objeto;
                        dataGridView1[4, lin].Value = req.sala;
                        dataGridView1[5, lin].Value = req.horaentre;
                        dataGridView1[6, lin].Value = req.dataentre;

                        lin++;
                    }

                }
                sr.Close();






            }
            else if (comboBox1.SelectedItem!=null &&comboBox2.SelectedItem==null)
            {



                sr = File.OpenText(requisicoes);

                while ((linha = sr.ReadLine()) != null)
                {
                    dataGridView1.Rows.Add(1);

                    string[] fill = linha.Split(';');

                    if (comboBox1.SelectedItem.ToString() == fill[4])
                    {

                        if (fill.Length <= 5)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = "";
                            req.dataentre = "";
                        }
                        else if (fill.Length >= 7)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = fill[5];
                            req.dataentre = fill[6];


                        }
                        //Adiciona à datagridview
                        dataGridView1[0, lin].Value = req.docente;
                        dataGridView1[1, lin].Value = req.hora;
                        dataGridView1[2, lin].Value = req.data;
                        dataGridView1[3, lin].Value = req.objeto;
                        dataGridView1[4, lin].Value = req.sala;
                        dataGridView1[5, lin].Value = req.horaentre;
                        dataGridView1[6, lin].Value = req.dataentre;





                        lin++;
                    }

                    
                }
                sr.Close();

            }

            else if (comboBox1.SelectedItem == null && comboBox2.SelectedItem != null)
            {



                sr = File.OpenText(requisicoes);

                while ((linha = sr.ReadLine()) != null)
                {
                    dataGridView1.Rows.Add(1);

                    string[] fill = linha.Split(';');

                    if (comboBox2.SelectedItem.ToString() == fill[3])
                    {

                        if (fill.Length <= 5)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = "";
                            req.dataentre = "";
                        }
                        else if (fill.Length >= 7)
                        {
                            req.docente = fill[0];
                            req.hora = fill[1];
                            req.data = fill[2];
                            req.objeto = fill[3];
                            req.sala = fill[4];
                            req.horaentre = fill[5];
                            req.dataentre = fill[6];

                        }
                        //Adiciona à datagridview
                        dataGridView1[0, lin].Value = req.docente;
                        dataGridView1[1, lin].Value = req.hora;
                        dataGridView1[2, lin].Value = req.data;
                        dataGridView1[3, lin].Value = req.objeto;
                        dataGridView1[4, lin].Value = req.sala;
                        dataGridView1[5, lin].Value = req.horaentre;
                        dataGridView1[6, lin].Value = req.dataentre;





                        lin++;
                    }

                    
                }
                sr.Close();







            }

        }*/























        /*
        public void tudo() {

            int lin = 0;

            requisitar req = new requisitar();

            string linha = "";

            //abre o ficheiro para adicionar à datagridview os ficheiros existents
            StreamReader sr = File.OpenText(requisicoes);

            while ((linha = sr.ReadLine()) != null)
            {
                dataGridView1.Rows.Add(1);

                string[] fill = linha.Split(';');


                if (fill.Length <= 5)
                {
                    req.docente = fill[0];
                    req.hora = fill[1];
                    req.data = fill[2];
                    req.objeto = fill[3];
                    req.sala = fill[4];
                    req.horaentre = "";
                    req.dataentre = "";
                }
                else if (fill.Length >= 7)
                {
                    req.docente = fill[0];
                    req.hora = fill[1];
                    req.data = fill[2];
                    req.objeto = fill[3];
                    req.sala = fill[4];
                    req.horaentre = fill[5];
                    req.dataentre = fill[6];

                }




                //Adiciona à datagridview
                dataGridView1[0, lin].Value = req.docente;
                dataGridView1[1, lin].Value = req.hora;
                dataGridView1[2, lin].Value = req.data;
                dataGridView1[3, lin].Value = req.objeto;
                dataGridView1[4, lin].Value = req.sala;
                dataGridView1[5, lin].Value = req.horaentre;
                dataGridView1[6, lin].Value = req.dataentre;





                lin++;
            }
            sr.Close();










        }*/












        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
