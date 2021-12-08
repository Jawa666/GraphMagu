using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Kursovaya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // некоторые глобальные переменные 
        int graphValue;

        private void button1_Click(object sender, EventArgs e) //Кнопка Ввести
        {
            try
            {
                graphValue = int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Некорректная запись","Сообщение",MessageBoxButtons.OK);
            }

            int[,] graphMatrix = new int[graphValue, graphValue];  //Сгенерировал матрицу нулями
            for (int i = 0; i < graphValue; i++)
                for (int j = 0; j < graphValue; j++)
                    graphMatrix[i, j] = 0;

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            for(int i = 0; i < graphValue+1; i++)
            {
                if (i != 0 )
                {
                    dataGridView1.Columns.Add(Convert.ToString(Convert.ToChar(64 + i)), Convert.ToString(Convert.ToChar(64 + i)));
                    dataGridView1.Columns[i].Width = 30;
                    if(i != graphValue)
                        dataGridView1.Rows.Add();
                }
                else
                {
                    dataGridView1.Columns.Add(Convert.ToString(Convert.ToChar(64 + i)), "");
                    dataGridView1.Columns[i].Width = 30;
                    if (i != graphValue)
                        dataGridView1.Rows.Add();
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)              //по числу ячейк вроде норм, но при обращении к ячейке заменил i,j местами
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (j == 0) dataGridView1[j, i].Value = Convert.ToString(Convert.ToChar(65 + i));
                    else dataGridView1[j, i].Value = "0";

        }

        private void button2_Click(object sender, EventArgs e)   //Кнопка (Метод магу)
        {
            int[,] graphMatrix = new int[graphValue, graphValue];  //Сгенерировал матрицу нулями
            for (int i = 0; i < graphValue; i++)
                for (int j = 0; j < graphValue; j++) 
                    graphMatrix[i, j] = int.Parse(Convert.ToString( dataGridView1[j+1,i].Value ));
            //Проверки класа днф
            DNF dnf = new DNF();
            List<string> list = dnf.DNFMagu(graphMatrix);
            // берем значения к хрому
            HromMagu hr = new HromMagu();
            List<List<string>> HromChislo = hr.Hrom(list, graphMatrix.GetLength(0));
            label7.Text = Convert.ToString(HromChislo[0].Count());  //вывод хроматического числа.

            label5.Text = Convert.ToString(list.Count());

            for (int i = 0; i < list.Count(); i++)
                if(i == 0)
                    textBox2.Text = ("S" + (i + 1) + " = {" + list[i] + "};");
                else if(i != list.Count()-1)
                    textBox2.Text += Environment.NewLine + ("S"+(i+1)+" = {"+list[i]+"};");
                else
                    textBox2.Text += Environment.NewLine + ("S" + (i+1) + " = {" + list[i] + "}.");


            //Ввывод хроматичексих чисел
            for(int i = 0; i < HromChislo.Count(); i++)
            {
                string str = "";
                for(int j = 0; j < HromChislo[i].Count(); j++)
                {
                    if (j == 0)
                        str = str + "S" + (i + 1) + " = {" + HromChislo[i][j] + ", ";
                    else if (j == HromChislo[i].Count() - 1)
                        str = str + HromChislo[i][j] + "}.";
                    else
                        str = str + HromChislo[i][j] + ", ";
                }
                if (i == 0)
                    textBox3.Text = str;
                else
                    textBox3.Text += Environment.NewLine + str;
            }
        }

        private void button3_Click(object sender, EventArgs e)   // Кнопка (Случайный)
        {
            try
            {
                graphValue = int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Некорректная запись", "Сообщение", MessageBoxButtons.OK);
            }

            int[,] graphMatrix = new int[graphValue, graphValue];  //Сгенерировал матрицу нулями
            for (int i = 0; i < graphValue; i++)
                for (int j = 0; j < graphValue; j++)
                    graphMatrix[i, j] = 0;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < graphValue + 1; i++)
            {
                if (i != 0)
                {
                    dataGridView1.Columns.Add(Convert.ToString(Convert.ToChar(64 + i)), Convert.ToString(Convert.ToChar(64 + i)));
                    dataGridView1.Columns[i].Width = 30;
                    if (i != graphValue)
                        dataGridView1.Rows.Add();
                }
                else
                {
                    dataGridView1.Columns.Add(Convert.ToString(Convert.ToChar(64 + i)), "");
                    dataGridView1.Columns[i].Width = 30;
                    if (i != graphValue)
                        dataGridView1.Rows.Add();
                }
            }

            var rnd = new Random();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)              //по числу ячейк вроде норм, но при обращении к ячейке заменил i,j местами
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (j == 0) dataGridView1[j, i].Value = Convert.ToString(Convert.ToChar(65 + i));
                    else
                    {
                        int r1 = rnd.Next(101);
                        if (r1 < 19 && i != (j-1))
                            dataGridView1[j, i].Value = "1";
                        else
                            dataGridView1[j, i].Value = "0";
                    }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[,] graphMatrix = new int[graphValue, graphValue];  
            for (int i = 0; i < graphValue; i++)
                for (int j = 0; j < graphValue; j++)
                    graphMatrix[i, j] = int.Parse(Convert.ToString(dataGridView1[j + 1, i].Value));
            
            Graph graph = new Graph(graphMatrix);
            graph.Show();


            
        }
    }
}
