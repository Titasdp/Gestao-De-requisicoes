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


        string conversa = @"conversa.txt";
        string salas = @"salas.txt";
        string objectos = @"objectos.txt";
        string requisicoes = ("R_" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt");
        string apoio = @"apoio.txt";
        string nome = @"utilizadores.txt";

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
                            else if (textBox4.Text != "")//filtro de utilizadores on
                            {


                                if (fill[0] == textBox4.Text)
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

                            }






                        }

                        else if (comboBox2.SelectedItem != null)//filtro de objetos on
                        {


                            if (comboBox2.Text == fill[4])
                            {


                                if (textBox4.Text == "")//filtro de utilizadores off
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
                                else if (textBox4.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox4.Text)
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
                                else if (textBox4.Text != "")//filtro de utilizadores on
                                {


                                    if (fill[0] == textBox4.Text)
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

                                }






                            }

                            else if (comboBox2.SelectedItem != null)//filtro de objetos on
                            {


                                if (comboBox2.Text == fill[4])
                                {


                                    if (textBox4.Text == "")//filtro de utilizadores off
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
                                    else if (textBox4.Text !="")//filtro de utilizadores on
                                    {


                                        if (fill[0] == textBox4.Text)
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

            sr = File.OpenText(apoio);
            while ((linha = sr.ReadLine()) != null)
            {
                sw = File.AppendText(objectos);
                sw.WriteLine(linha);
                sw.Close();
            }
            sr.Close();

            File.Delete(apoio);

            textBox12.Text = "";
            textBox11.Text = "";
            listBox1.SelectedItem = null;


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
                    sw.Write(adicionar);
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
    }
}
