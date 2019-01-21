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
    public partial class Adminp2 : Form
    {
        string req_global = @"requniversal.txt";//Ficheiro que se baseia para que o  sistema de alerta de devolucao pendentes  funcione 
        string backup = @"backup.txt";//ajuda na passagem de informacao para o ficheiro req_global
        int cont;
        string iden = "";
        string conversa = @"conversa.txt";
        string salas = @"salas.txt";
        string objectos = @"objectos.txt";
        string requisicoes = ("R_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");
        string apoio = @"apoio.txt";
        string nome = @"utilizadores.txt";
        string obh = @"obh.txt";

        public Adminp2()
        {
            InitializeComponent();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void Adminp2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
           

            int x = 0;
            string confpassado = "";
            int quant = 0;
            StreamReader srconf = File.OpenText(req_global);
            while ((confpassado = srconf.ReadLine()) != null)
            {
                string[] fillc = confpassado.Split(';');
                if (true)
                {
                    if (fillc.Length < 7)
                    {

                        dataGridView4.Rows.Add(1);
                        dataGridView4[0, x].Value = fillc[0];
                        dataGridView4[1, x].Value = fillc[2];
                        dataGridView4[2, x].Value = fillc[1];
                        dataGridView4[3, x].Value = fillc[3];
                        dataGridView4[4, x].Value = fillc[4];
                        dataGridView4[5, x].Value = "-";
                        dataGridView4[6, x].Value = "-";
                        quant++;
                        x++;
                    }
                }
            }
            srconf.Close();
            if (quant > 0)
            {
                MessageBox.Show("Existem objetos por devolver. ", "Objetos por Devolver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("Painel de devoluções em atraso ativado.", "Painel ativado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            StreamReader sr;

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

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;



            //Gestao de salas e objetos
            //Retira os objectos do ficheiro dos objectos e acrescenta na Listbox2 dos salas

            sr = File.OpenText(salas);
            string linha3;
            while ((linha3 = sr.ReadLine()) != null)    
            {
                listBox2.Items.Add(linha3);
            }
            sr.Close();


            //Retira os objectos do ficheiro dos objectos e acrescenta na Listbox3 dos objectos
            sr = File.OpenText(objectos);
            string linha4;
            while ((linha4 = sr.ReadLine()) != null) 
            {

                string[] fill = linha4.Split(';');

                listBox3.Items.Add(fill[0]+"-"+fill[1]);
            }
            sr.Close();


            comboBox3.Items.Add("A");
            comboBox3.Items.Add("B");
            comboBox3.Items.Add("C");
            comboBox3.Items.Add("D");
            comboBox3.Items.Add("E");
            comboBox3.Items.Add("F");
            comboBox3.Items.Add("G");
            comboBox3.Items.Add("H");
            comboBox3.Items.Add("I");
            comboBox3.Items.Add("J");
            comboBox3.Items.Add("K");
            comboBox3.Items.Add("L");
            comboBox3.Items.Add("M");
            comboBox3.Items.Add("N");
            comboBox3.Items.Add("O");
            comboBox3.Items.Add("P");
            comboBox3.Items.Add("Q");
            comboBox3.Items.Add("R");
            comboBox3.Items.Add("S");
            comboBox3.Items.Add("T");
            comboBox3.Items.Add("U");
            comboBox3.Items.Add("V");
            comboBox3.Items.Add("X");
            comboBox3.Items.Add("Y");
            comboBox3.Items.Add("Z");

            string pa_cont;
             cont = 0;
            if (File.Exists(obh)==true)
            {
                StreamReader si = File.OpenText(obh);
                while ((pa_cont = si.ReadLine()) != null)
                {

                    cont++;
                }
                si.Close();

            }
            else
            {
                StreamWriter sc= File.CreateText(obh);
                sc.Close();
               
            }

            iden = cont.ToString();
            if (cont <10)
            {

                textBox16.Text = "0" + iden;
            }
            else
            {
                textBox16.Text = iden;
            }
           


        }

        //Faz a pesquiza relacionado aos utilizadores aqui que acontece todo o código 
        private void button1_Click(object sender, EventArgs e)
        {


            dataGridView1.Rows.Clear();//elemina os elementos da datagridview para permetir uma nova apresentacao de dados


            string tipo = "T";

            if (radioButton1.Checked==true)
            {
                tipo = "admin";
            }
            else if(radioButton2.Checked == true)
            {
                tipo = "docente";
            }
            else if (radioButton3.Checked == true)
            {
              
                tipo = "seguranca";
            }
            else
            {
                tipo = "T";
            }

            
            string linha;// os conteudos do ficheiro sao passados para essa string
            int lin = 0;// linha da datagrid
            StreamReader sr;
            sr = File.OpenText(nome);
            while ((linha= sr.ReadLine())!=null)
            {

                if (tipo!="T")
                {

                    string[] fill = linha.Split(';');//faz a separacao da string que contem os dados dos utilizadores 

                    if (tipo==fill[4])
                    {
                        if (fill[1]== textBox1.Text )//caso o filtro do utilizador for acionado
                        {

                            if (textBox2.Text!="" && fill[2]==textBox2.Text)//caso o email for acionado 
                            {
                                dataGridView1.Rows.Add(1);
                                dataGridView1[0, lin].Value = fill[0];//id
                                dataGridView1[1, lin].Value = fill[1];//utilizador
                                dataGridView1[2, lin].Value = fill[2];//email
                                dataGridView1[3, lin].Value = fill[4];//tipo
                                lin++;
                            }
                            else if (textBox2.Text == "")
                            {
                                dataGridView1.Rows.Add(1);
                                dataGridView1[0, lin].Value = fill[0];//id
                                dataGridView1[1, lin].Value = fill[1];//utilizador
                                dataGridView1[2, lin].Value = fill[2];//email
                                dataGridView1[3, lin].Value = fill[4];//tipo
                                lin++;

                            }
                            


                        }
                        else if (textBox1.Text=="")//Caso o filtro do utilizador nao seja acionado 
                           {
                            if (textBox2.Text != "" && fill[2] == textBox2.Text)//caso o email for acionado 
                            {
                                dataGridView1.Rows.Add(1);
                                dataGridView1[0, lin].Value = fill[0];//id
                                dataGridView1[1, lin].Value = fill[1];//utilizador
                                dataGridView1[2, lin].Value = fill[2];//email
                                dataGridView1[3, lin].Value = fill[4];//tipo
                                lin++;
                            }
                            else if (textBox2.Text == "")
                            {
                                dataGridView1.Rows.Add(1);
                                dataGridView1[0, lin].Value = fill[0];//id
                                dataGridView1[1, lin].Value = fill[1];//utilizador
                                dataGridView1[2, lin].Value = fill[2];//email
                                dataGridView1[3, lin].Value = fill[4];//tipo
                                lin++;

                            }
                        }



                        
                    }
                  
                }
                else if (tipo == "T")// caso o filtro de tipo nao seja adicionado 
                {

                    string[] fill = linha.Split(';');
                    if (fill[1] == textBox1.Text)//caso o filtro do utilizador for acionado
                    {

                        if (textBox2.Text != "" && fill[2] == textBox2.Text)//caso o email for acionado 
                        {
                            dataGridView1.Rows.Add(1);
                            dataGridView1[0, lin].Value = fill[0];//id
                            dataGridView1[1, lin].Value = fill[1];//utilizador
                            dataGridView1[2, lin].Value = fill[2];//email
                            dataGridView1[3, lin].Value = fill[4];//tipo
                            lin++;
                        }
                        else if (textBox2.Text == "")
                        {
                            dataGridView1.Rows.Add(1);
                            dataGridView1[0, lin].Value = fill[0];//id
                            dataGridView1[1, lin].Value = fill[1];//utilizador
                            dataGridView1[2, lin].Value = fill[2];//email
                            dataGridView1[3, lin].Value = fill[4];//tipo
                            lin++;

                        }



                    }
                    else if (textBox1.Text == "")//Caso o filtro do utilizador nao seja acionado 
                    {
                        if (textBox2.Text != "" && fill[2] == textBox2.Text)//caso o email for acionado 
                        {
                            dataGridView1.Rows.Add(1);
                            dataGridView1[0, lin].Value = fill[0];//id
                            dataGridView1[1, lin].Value = fill[1];//utilizador
                            dataGridView1[2, lin].Value = fill[2];//email
                            dataGridView1[3, lin].Value = fill[4];//tipo
                            lin++;
                        }
                        else if (textBox2.Text == "")
                        {
                            dataGridView1.Rows.Add(1);
                            dataGridView1[0, lin].Value = fill[0];//id
                            dataGridView1[1, lin].Value = fill[1];//utilizador
                            dataGridView1[2, lin].Value = fill[2];//email
                            dataGridView1[3, lin].Value = fill[4];//tipo
                            lin++;
                        }
                    }



                }


                
            }
            sr.Close();
            StreamReader sr2 = File.OpenText(nome);
            sr2.Close();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //elimina o elelmento que encontra-se na texbox4
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Precisa escolher na tabela a conta que deseja eliminar", "Conta por a eleminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                 if (textBox3.Text == Program.utilname)
                {

                    MessageBox.Show("Não é possivel eliminar essa conta do programa", "Eleminação Impossível", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }
                else
                {

                    DialogResult resposta = MessageBox.Show("Tem certeza que deseja eliminar este utilizador do programa", "Eliminar conta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resposta == DialogResult.Yes)
                    {

                        string poreleminar = textBox3.Text;
                        string linha;

                        //Cria o ficheiro de apoio impedindo a perda de informacao ao eliminar  

                        StreamWriter sw = File.CreateText(apoio);
                        sw.Close();


                        StreamReader sr;


                        sr = File.OpenText(nome);
                        while ((linha = sr.ReadLine()) != null)
                        {

                            string[] fill = linha.Split(';');
                            //ESCREVE NO OURTO FICHEIRO
                            StreamWriter sw2 = File.AppendText(apoio);
                            if (fill[1] != poreleminar)//caso a linha encontrada for diferente do por eliminar 
                            {
                                sw2.WriteLine(linha);
                                sw2.Close();

                            }
                            else
                            {
                                sw2.Close();//caso a linha for aquele ele apenes fecha o streamwriter
                            }


                        }
                        sr.Close();

                        //textes
                        if (File.Exists(nome) == true)
                        {
                            File.Delete(nome);
                        }
                        else
                        {
                            StreamReader sI = File.OpenText(conversa);
                            sI.Close();
                        }


                        StreamWriter sw3 = File.CreateText(nome);//Recria o ficheiro para ser tranferido de novo os dados atualizados 
                        sw3.Close();

                        string linharecr;//escreve de novo no ficheiro actualizado

                        sr = File.OpenText(apoio);
                        while ((linharecr = sr.ReadLine()) != null)
                        {
                            //Devolve os dados para o ficheiro reconstruido 
                            StreamWriter sw2 = File.AppendText(nome);
                            sw2.WriteLine(linharecr);
                            sw2.Close();


                        }
                        sr.Close();




                        File.Delete(apoio);//elimina o ficheiro de apoio }

                        MessageBox.Show("O processo de eliminação da conta , foi concluida com sucesso", "Processo interrompido", MessageBoxButtons.OK, MessageBoxIcon.Information);




                    }
                    else
                    {
                        MessageBox.Show("O processo de eliminação da conta selecionada foi interrompido", "Processo interrompido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }






                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int lin= dataGridView1.CurrentCell.RowIndex;

            textBox3.Text = dataGridView1[1, lin].Value.ToString();




            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form f1 = new CriarcontaAdminandSec();
            f1.Show();
            this.Hide();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();



            string tipo = "";
            if (checkBox1.Checked== true && checkBox2.Checked == false)
            {
                tipo = "D";
            }
            else if (checkBox2.Checked==true && checkBox1.Checked == false)
            {
                tipo = "R";
            }
            else if (checkBox1.Checked==false || checkBox2.Checked==false)
            {
                tipo = "T";
            }
            else if (checkBox1.Checked == true || checkBox2.Checked == true)
            {
                tipo = "T";
            }
           
           
            







            int lin = 0;
            DateTime data = Convert.ToDateTime(dateTimePicker1.Text);

            string filedata = ("R_" + data.ToString("yyyy-MM-dd") + ".txt");



            if (File.Exists(filedata)!= true)
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


                            if (textBox4.Text == "")//filtro de utilizadores off
                            {

                                if (tipo == "T")
                                {



                                    if (fill.Length <= 5)
                                    {



                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = "-";
                                        dataGridView2[6, lin].Value = "-";
                                        lin++;


                                    }
                                    else if (fill.Length > 5)
                                    {

                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = fill[6];
                                        dataGridView2[6, lin].Value = fill[5];
                                        lin++;
                                    }
                                }
                                else if (tipo=="D"&&fill.Length>5)
                                {
                                    dataGridView2.Rows.Add(1);
                                    dataGridView2[0, lin].Value = fill[0];
                                    dataGridView2[1, lin].Value = fill[2];
                                    dataGridView2[2, lin].Value = fill[1];
                                    dataGridView2[3, lin].Value = fill[3];
                                    dataGridView2[4, lin].Value = fill[4];
                                    dataGridView2[5, lin].Value = fill[6];
                                    dataGridView2[6, lin].Value = fill[5];
                                    lin++;
                                }
                                else if(tipo =="R"&&fill.Length<=5)
                                {
                                    dataGridView2.Rows.Add(1);
                                    dataGridView2[0, lin].Value = fill[0];
                                    dataGridView2[1, lin].Value = fill[2];
                                    dataGridView2[2, lin].Value = fill[1];
                                    dataGridView2[3, lin].Value = fill[3];
                                    dataGridView2[4, lin].Value = fill[4];
                                    dataGridView2[5, lin].Value = "-";
                                    dataGridView2[6, lin].Value = "-";
                                    lin++;
                                }
                              




                            }
                            else if (textBox4.Text != "")//filtro de utilizadores on
                            {


                                if (fill[0] == textBox4.Text)
                                {


                                    if (tipo == "T")
                                    {



                                        if (fill.Length <= 5)
                                        {



                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                    }
                                    else if (tipo == "D" && fill.Length > 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = fill[6];
                                        dataGridView2[6, lin].Value = fill[5];
                                        lin++;
                                    }
                                    else if (tipo == "R" && fill.Length <= 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = "-";
                                        dataGridView2[6, lin].Value = "-";
                                        lin++;
                                    }








                                }

                            }






                        }

                        else if (comboBox2.SelectedItem != null)//filtro de objetos on
                        {


                            if (comboBox2.Text == fill[4])
                            {


                                if (textBox4.Text == "")//filtro de utilizadores off
                                {



                                    if (tipo == "T")
                                    {



                                        if (fill.Length <= 5)
                                        {



                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                    }
                                    else if (tipo == "D" && fill.Length > 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = fill[6];
                                        dataGridView2[6, lin].Value = fill[5];
                                        lin++;
                                    }
                                    else if (tipo == "R" && fill.Length <= 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = "-";
                                        dataGridView2[6, lin].Value = "-";
                                        lin++;
                                    }




                                }
                                else if (textBox4.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox4.Text)
                                    {


                                        if (tipo == "T")
                                        {



                                            if (fill.Length <= 5)
                                            {



                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = "-";
                                                dataGridView2[6, lin].Value = "-";
                                                lin++;


                                            }
                                            else if (fill.Length > 5)
                                            {

                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = fill[6];
                                                dataGridView2[6, lin].Value = fill[5];
                                                lin++;
                                            }
                                        }
                                        else if (tipo == "D" && fill.Length > 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                        else if (tipo == "R" && fill.Length <= 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
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


                                if (textBox4.Text == "")//filtro de utilizadores off
                                {



                                    if (tipo == "T")
                                    {



                                        if (fill.Length <= 5)
                                        {



                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
                                            lin++;


                                        }
                                        else if (fill.Length > 5)
                                        {

                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                    }
                                    else if (tipo == "D" && fill.Length > 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = fill[6];
                                        dataGridView2[6, lin].Value = fill[5];
                                        lin++;
                                    }
                                    else if (tipo == "R" && fill.Length <= 5)
                                    {
                                        dataGridView2.Rows.Add(1);
                                        dataGridView2[0, lin].Value = fill[0];
                                        dataGridView2[1, lin].Value = fill[2];
                                        dataGridView2[2, lin].Value = fill[1];
                                        dataGridView2[3, lin].Value = fill[3];
                                        dataGridView2[4, lin].Value = fill[4];
                                        dataGridView2[5, lin].Value = "-";
                                        dataGridView2[6, lin].Value = "-";
                                        lin++;
                                    }





                                }
                                else if (textBox4.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox4.Text)
                                    {


                                        if (tipo == "T")
                                        {



                                            if (fill.Length <= 5)
                                            {



                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = "-";
                                                dataGridView2[6, lin].Value = "-";
                                                lin++;


                                            }
                                            else if (fill.Length > 5)
                                            {

                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = fill[6];
                                                dataGridView2[6, lin].Value = fill[5];
                                                lin++;
                                            }
                                        }
                                        else if (tipo == "D" && fill.Length > 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                        else if (tipo == "R" && fill.Length <= 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
                                            lin++;
                                        }








                                    }

                                }






                            }

                            else if (comboBox2.SelectedItem != null)//filtro de objetos on
                            {


                                if (comboBox2.Text == fill[4])
                                {


                                    if (textBox4.Text == "")//filtro de utilizadores off
                                    {



                                        if (tipo == "T")
                                        {



                                            if (fill.Length <= 5)
                                            {



                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = "-";
                                                dataGridView2[6, lin].Value = "-";
                                                lin++;


                                            }
                                            else if (fill.Length > 5)
                                            {

                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = fill[6];
                                                dataGridView2[6, lin].Value = fill[5];
                                                lin++;
                                            }
                                        }
                                        else if (tipo == "D" && fill.Length > 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = fill[6];
                                            dataGridView2[6, lin].Value = fill[5];
                                            lin++;
                                        }
                                        else if (tipo == "R" && fill.Length <= 5)
                                        {
                                            dataGridView2.Rows.Add(1);
                                            dataGridView2[0, lin].Value = fill[0];
                                            dataGridView2[1, lin].Value = fill[2];
                                            dataGridView2[2, lin].Value = fill[1];
                                            dataGridView2[3, lin].Value = fill[3];
                                            dataGridView2[4, lin].Value = fill[4];
                                            dataGridView2[5, lin].Value = "-";
                                            dataGridView2[6, lin].Value = "-";
                                            lin++;
                                        }





                                    }
                                    else if (textBox4.Text !="")//filtro de utilizadores on
                                    {


                                        if (fill[0] == textBox4.Text)
                                        {


                                            if (tipo == "T")
                                            {



                                                if (fill.Length <= 5)
                                                {



                                                    dataGridView2.Rows.Add(1);
                                                    dataGridView2[0, lin].Value = fill[0];
                                                    dataGridView2[1, lin].Value = fill[2];
                                                    dataGridView2[2, lin].Value = fill[1];
                                                    dataGridView2[3, lin].Value = fill[3];
                                                    dataGridView2[4, lin].Value = fill[4];
                                                    dataGridView2[5, lin].Value = "-";
                                                    dataGridView2[6, lin].Value = "-";
                                                    lin++;


                                                }
                                                else if (fill.Length > 5)
                                                {

                                                    dataGridView2.Rows.Add(1);
                                                    dataGridView2[0, lin].Value = fill[0];
                                                    dataGridView2[1, lin].Value = fill[2];
                                                    dataGridView2[2, lin].Value = fill[1];
                                                    dataGridView2[3, lin].Value = fill[3];
                                                    dataGridView2[4, lin].Value = fill[4];
                                                    dataGridView2[5, lin].Value = fill[6];
                                                    dataGridView2[6, lin].Value = fill[5];
                                                    lin++;
                                                }
                                            }
                                            else if (tipo == "D" && fill.Length > 5)
                                            {
                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = fill[6];
                                                dataGridView2[6, lin].Value = fill[5];
                                                lin++;
                                            }
                                            else if (tipo == "R" && fill.Length <= 5)
                                            {
                                                dataGridView2.Rows.Add(1);
                                                dataGridView2[0, lin].Value = fill[0];
                                                dataGridView2[1, lin].Value = fill[2];
                                                dataGridView2[2, lin].Value = fill[1];
                                                dataGridView2[3, lin].Value = fill[3];
                                                dataGridView2[4, lin].Value = fill[4];
                                                dataGridView2[5, lin].Value = "-";
                                                dataGridView2[6, lin].Value = "-";
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

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedItem = null;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem!=null)
            {
                int nchar = listBox2.SelectedItem.ToString().Length;//Envia a quantidade de caracteres
                textBox12.Text = listBox2.SelectedItem.ToString().Substring(0, 1);//Adiciona a "familia" da sala ou seja A,B,C,D 
                textBox11.Text = listBox2.SelectedItem.ToString().Substring(1, nchar - 1);//Adiciona o numero de identificacao 
            }
            else
            {
                textBox12.Text = "";
                textBox11.Text = "";
            }
       




        }

        private void button7_Click(object sender, EventArgs e)
        {


            string por_eliminar = listBox2.SelectedItem.ToString();

            string linha="";

            StreamWriter sw = File.CreateText(apoio);
            sw.Close();

            StreamReader sr = File.OpenText(salas);

            while ((linha = sr.ReadLine())!=null)
            {
                sw = File.AppendText(apoio);
                if (linha==por_eliminar)
                {
                    sw.Close();
                }
                else
                {
                    sw.WriteLine(linha);
                    sw.Close();
                }
            }
            sr.Close();



            File.Delete(salas);

            sw = File.CreateText(salas);
            sw.Close();

            sr = File.OpenText(apoio);
            while ((linha = sr.ReadLine()) != null)
            {
                sw = File.AppendText(salas);
                sw.WriteLine(linha);
                sw.Close();
            }
            sr.Close();

            File.Delete(apoio);


            actualizacao_total();
        }


        //Faz a actualizacao dos dados nos ficheiros existentes ou seja esse vai atualiazar os dados sempre que o Admin fizer alguma alteracao nos files
        public void actualizacao_total()
        {


            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();


            StreamReader sr;

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



            //Gestao de salas e objetos
            //Retira os objectos do ficheiro dos objectos e acrescenta na Listbox2 dos salas

            sr = File.OpenText(salas);
            string linha3;
            while ((linha3 = sr.ReadLine()) != null)
            {
                listBox2.Items.Add(linha3);
            }
            sr.Close();


            //Retira os objectos do ficheiro dos objectos e acrescenta na Listbox3 dos objectos
            sr = File.OpenText(objectos);
            string linha4;
            while ((linha4 = sr.ReadLine()) != null)
            {

                string[] fill = linha4.Split(';');

                listBox3.Items.Add(fill[0] + "-" + fill[1]);
            }
            sr.Close();


            textBox12.Text ="";
            textBox11.Text ="";
            maskedTextBox1.Text = "";
            comboBox3.SelectedItem = null;
        }



        //Eleminacao das salas
        private void button10_Click(object sender, EventArgs e)
        {
           string[] fill = listBox3.SelectedItem.ToString().Split('-');

            string por_eliminar = fill[0] + ";" + fill[1];

            string linha = "";

            StreamWriter sw = File.CreateText(apoio);
            sw.Close();

            StreamReader sr = File.OpenText(objectos);

            while ((linha = sr.ReadLine()) != null)
            {
                sw = File.AppendText(apoio);
                if (linha == por_eliminar)
                {
                    sw.Close();
                }
                else
                {
                    sw.WriteLine(linha);
                    sw.Close();
                }
            }
            sr.Close();



            File.Delete(objectos);

            sw = File.CreateText(objectos);
            sw.Close();

            sr = File.OpenText(apoio);
            while ((linha = sr.ReadLine()) != null)
            {
                sw = File.AppendText(objectos);
                sw.WriteLine(linha);
                sw.Close();
            }
            sr.Close();

            File.Delete(apoio);

            textBox5.Text = "";
            textBox6.Text = "";
            listBox2.SelectedItem = null;


            actualizacao_total();
         


        }

        private void listBox3_Click(object sender, EventArgs e)
        {
            //5 6
            string[] fill = listBox3.SelectedItem.ToString().Split('-');

            textBox5.Text = fill[0];
            textBox6.Text = fill[1];


            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel12.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {

            string adicionar = comboBox3.Text.ToUpper() + maskedTextBox1.Text;
            if (maskedTextBox1.Text == ""|| comboBox3.Text.ToUpper() == "")
            {
                MessageBox.Show("Por favor,preenche o formulário correctamente","Formulário incompleto", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {


                if (confexistencia_sala(adicionar)==false)
                {
                    MessageBox.Show("A sala não pode ser adicionada , pois ela já existe", "Sala Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    StreamWriter sw = File.AppendText(salas);
                    sw.WriteLine(adicionar);
                    sw.Close();
                    actualizacao_total();
                    MessageBox.Show("A sala foi adicionada com sucesso", "Sala Adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }


        }


        public bool confexistencia_sala(string texto)
        {
            string linha = "";
            StreamReader sr = File.OpenText(salas);


            while ((linha= sr.ReadLine())!=null)
            {

                if (texto==linha)
                {
                    sr.Close();
                    return false;
                }


            }
            sr.Close();
            return true;
        }


        public bool confexistencia_obj(string texto)
        {
            string linha = "";
            StreamReader sr = File.OpenText(objectos);


            while ((linha = sr.ReadLine()) != null)
            {
                string[] split = linha.Split(';');



                if (split[0]==textBox16.Text)
                {
                    sr.Close();
                    return false;
                }
                else
                {
                    if (texto == linha)
                    {
                        sr.Close();
                        return false;
                    }
                }
            }
            sr.Close();
            return true;
        }





        private void button8_Click(object sender, EventArgs e)
        {
            panel12.Visible = true;
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = "";
            
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            comboBox3.SelectedItem = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel13.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel13.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string adicionar = textBox16.Text+";"+textBox15.Text;
            if (textBox15.Text == "" || textBox16.Text == "")
            {
                MessageBox.Show("Por favor,preenche o formulário correctamente", "Formulário incompleto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {


                if (confexistencia_obj(adicionar) == false)
                {
                    MessageBox.Show("O objeto não pode ser adicionada , pois ela já existe", "Objeto Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    StreamWriter sw = File.AppendText(objectos);
                    sw.WriteLine(adicionar);
                    sw.Close();
                    actualizacao_total();
                    MessageBox.Show("O objeto foi adicionado com sucesso", "Objeto Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sw=File.AppendText(obh);
                    sw.WriteLine(adicionar);
                    sw.Close();




                }
               
                string pa_cont;
                cont = 0;
                if (File.Exists(obh) == true)
                {
                    StreamReader sr= File.OpenText(obh);
                    while ((pa_cont = sr.ReadLine()) != null)
                    {
                        cont++;
                    }
                    sr.Close();

                }

                iden = cont.ToString();
                if (cont < 10)
                {

                    textBox16.Text = "0" + iden;
                }
                else
                {
                    textBox16.Text = iden;
                }

            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x = 0;//Datagrid linhas
            string linha = "";
            dataGridView3.Rows.Clear();
         
            StreamReader sr = File.OpenText(conversa);

            while ((linha=sr.ReadLine())!=null)
            {
                string[] fill = linha.Split(';'); 
                if (textBox9.Text =="")
                {
                    if (textBox8.Text == "")
                    {
                        if (fill.Length >= 6)
                        {
                            dataGridView3.Rows.Add(1);
                            dataGridView3[0, x].Value = fill[0];
                            dataGridView3[1, x].Value = fill[2];
                            dataGridView3[2, x].Value = fill[3];
                            dataGridView3[3, x].Value = fill[5];
                            x++;
                        }
                        else
                        {
                            dataGridView3.Rows.Add(1);
                            dataGridView3[0, x].Value = fill[0];
                            dataGridView3[1, x].Value = fill[2];
                            dataGridView3[2, x].Value = fill[3];
                            dataGridView3[3, x].Value = "-";
                            x++;
                        }
                    }
                    else if (textBox8.Text != "")
                    {
                        if (fill[3] == textBox8.Text)//Receptor
                        {
                            if (fill.Length >= 6)
                            {
                                dataGridView3.Rows.Add(1);
                                dataGridView3[0, x].Value = fill[0];
                                dataGridView3[1, x].Value = fill[2];
                                dataGridView3[2, x].Value = fill[3];
                                dataGridView3[3, x].Value = fill[5];
                                x++;
                            }
                            else if (fill.Length < 6)
                            {
                                dataGridView3.Rows.Add(1);
                                dataGridView3[0, x].Value = fill[0];
                                dataGridView3[1, x].Value = fill[2];
                                dataGridView3[2, x].Value = fill[3];
                                dataGridView3[3, x].Value = "-";
                                x++;
                            }



                        }
                    }

                }
                else if (textBox9.Text !="")
                {
                    if (textBox9.Text==fill[0])
                    {
                        if (textBox8.Text== "")
                        {
                            if (fill.Length >= 6)
                            {
                                dataGridView3.Rows.Add(1);
                                dataGridView3[0, x].Value = fill[0];
                                dataGridView3[1, x].Value = fill[2];
                                dataGridView3[2, x].Value = fill[3];
                                dataGridView3[3, x].Value = fill[5];
                                x++;
                            }
                            else
                            {
                                dataGridView3.Rows.Add(1);
                                dataGridView3[0, x].Value = fill[0];
                                dataGridView3[1, x].Value = fill[2];
                                dataGridView3[2, x].Value = fill[3];
                                dataGridView3[3, x].Value = "-";
                                x++;
                            }
                        }
                        else if (textBox8.Text != "")
                        {
                            if (fill[3] ==textBox8.Text)//Receptor
                            {
                                if (fill.Length>=6)
                                {
                                    dataGridView3.Rows.Add(1);
                                    dataGridView3[0, x].Value = fill[0];
                                    dataGridView3[1, x].Value = fill[2];
                                    dataGridView3[2, x].Value = fill[3];
                                    dataGridView3[3, x].Value = fill[5];
                                    x++;
                                }
                                else if(fill.Length <6)
                                {
                                    dataGridView3.Rows.Add(1);
                                    dataGridView3[0, x].Value = fill[0];
                                    dataGridView3[1, x].Value = fill[2];
                                    dataGridView3[2, x].Value = fill[3];
                                    dataGridView3[3, x].Value = "-";
                                    x++;
                                }
                              
                                

                            }
                        }
                       

                    }
                }
            }
            sr.Close();

            
          
            
           
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Deve escolher o objeto a ser devolvido", "Formulário Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string dia = "R_" + textBox17.Text + ".txt";
                string cmp = textBox17.Text + ";" + textBox7.Text + ";" + textBox10.Text + ";" + textBox14.Text + ";" + textBox13.Text;
                string alterado = textBox17.Text + ";" + textBox7.Text + ";" + textBox10.Text + ";" + textBox14.Text + ";" + textBox13.Text + ";" + DateTime.Now.ToString("hh:mm:ss") + ";" + DateTime.Today.ToString("yyyy-MM-dd");



                StreamWriter sw = File.CreateText(apoio);
                sw.Close();


                string linha;
                StreamReader sr;
                sr = File.OpenText(dia);
                while ((linha = sr.ReadLine()) != null)
                {
                    //ESCREVE NO OURTO FICHEIRO
                    StreamWriter sw2 = File.AppendText(apoio);

                    if (linha == cmp)//caso a linha encontrada for igual a linha escrita no formulario de requisicao 
                    {
                        sw2.WriteLine(alterado);//passa para o outro ficheiro a linha2 sendo que impede com que a linha seja escrita sm data e hora
                        sw2.Close();
                    }
                    else
                    {
                        sw2.WriteLine(linha);//caso nao for encontrada algo igual ele escreve a linha como esta escrita
                        sw2.Close();
                    }

                }
                sr.Close();


                File.Delete(dia);//Elimina o ficheiro anterior para permitir refazelo do zero 
                StreamWriter sw3 = File.CreateText(dia);//Recria o ficheiro para ser tranferido de novo os dados atualizados 
                sw3.Close();

                string linharecr;//escreve de novo no ficheiro 

                sr = File.OpenText(apoio);
                while ((linharecr = sr.ReadLine()) != null)
                {
                    //Devolve os dados para o ficheiro reconstruido 
                    StreamWriter sw2 = File.AppendText(dia);
                    sw2.WriteLine(linharecr);
                    sw2.Close();


                }
                sr.Close();

                File.Delete(apoio);//elimina o ficheiro de apoio 



                //Escreve no ficheiro global de requisicao 
                sr = File.OpenText(req_global);
                while ((linha = sr.ReadLine()) != null)
                {
                    //ESCREVE NO OURTO FICHEIRO
                    StreamWriter sw2 = File.AppendText(backup);

                    if (linha == cmp)//caso a linha encontrada for igual a linha escrita no formulario de requisicao 
                    {
                        sw2.WriteLine(alterado);//passa para o outro ficheiro a linha2 sendo que impede com que a linha seja escrita sm data e hora
                        sw2.Close();
                    }
                    else
                    {
                        sw2.WriteLine(linha);//caso nao for encontrada algo igual ele escreve a linha como esta escrita
                        sw2.Close();
                    }

                }
                sr.Close();


                File.Delete(req_global);//Elimina o ficheiro anterior para permitir refazelo do zero 
                sw3 = File.CreateText(req_global);//Recria o ficheiro para ser tranferido de novo os dados atualizados 
                sw3.Close();



                sr = File.OpenText(backup);
                while ((linharecr = sr.ReadLine()) != null)
                {
                    //Devolve os dados para o ficheiro reconstruido 
                    StreamWriter sw2 = File.AppendText(req_global);
                    sw2.WriteLine(linharecr);
                    sw2.Close();


                }
                sr.Close();

                File.Delete(backup);//elimina o ficheiro de apoio


                MessageBox.Show("O processo foi completado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                int x = 0;
                string confpassado = "";
                int quant = 0;
                StreamReader srconf = File.OpenText(req_global);
                while ((confpassado = srconf.ReadLine()) != null)
                {
                    string[] fillc = confpassado.Split(';');
                    if (true)
                    {
                        if (fillc.Length < 7)
                        {

                            dataGridView4.Rows.Add(1);
                            dataGridView4[0, x].Value = fillc[0];
                            dataGridView4[1, x].Value = fillc[2];
                            dataGridView4[2, x].Value = fillc[1];
                            dataGridView4[3, x].Value = fillc[3];
                            dataGridView4[4, x].Value = fillc[4];
                            dataGridView4[5, x].Value = "-";
                            dataGridView4[6, x].Value = "-";
                            quant++;
                            x++;
                        }
                    }
                }
                srconf.Close();
                if (quant == 0)
                {
                    MessageBox.Show("Todos os objetos já foram devolvidos. ", "Todo o Máterial foi devolvido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }



            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Status1.Text = "Data:" + DateTime.Today.ToString("yyyy-MM-dd") + "     " + "Hora:" + DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
