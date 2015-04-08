using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List list;
        List<int> listBubble = new List<int>(); 
        Stopwatch stopWatch = new Stopwatch();

        DateTime down;
        DateTime now;

        #region
        Random random = new Random();
        int randomNumber;
        int loop;
        #endregion

        public Form1()
        {
            InitializeComponent();
            list = new List(2, 3, 4, 1);
            
            loop = 50;
            listBox1.Items.AddRange(list.ToArray());
        }
        #region
        private void button1_Click(object sender, EventArgs e)
        {
            int index = 0;
            int value = 0;
            if (int.TryParse(textBox1.Text, out value))
            {
                if (checkBox1.Checked)
                {
                    index = listBox1.SelectedIndex;
                    if (radioButton3.Checked && int.TryParse(textBox2.Text, out index))
                    {
                        index--;
                        if (index >= list.Count)
                        {
                            list.Add(value);
                        }
                        else
                        {
                            Element indexSelected = list.Get(index);
                            Element previousIndexSelected = list.Get(index - 1);
                            Element current = new Element(value, previousIndexSelected, indexSelected);
                            indexSelected.Previous = current;
                            if (previousIndexSelected != null)
                            {
                                previousIndexSelected.Next = current;
                            }
                            else
                            {
                                list.SetFirst(current);
                            }
                        }
                    }
                    if (radioButton1.Checked && index >= 0)
                    {
                        Element indexSelected = list.Get(index);
                        Element previousIndexSelected = list.Get(index - 1);
                        Element current = new Element(value, previousIndexSelected, indexSelected);
                        indexSelected.Previous = current;
                        if (previousIndexSelected != null)
                        {
                            previousIndexSelected.Next = current;
                        }
                        else
                        {
                            list.SetFirst(current);
                        }
                    }

                    if (radioButton2.Checked && index >= 0)
                    {
                        Element indexSelected = list.Get(index);
                        Element current = new Element(value, indexSelected, indexSelected.Next);
                        indexSelected.Next = current;
                    }

                    if (index < 0)
                    {
                        MessageBox.Show("Selected any item");
                    }
                }
                else
                {
                    list.Add(value);
                }
            }

            listBox1.Items.Clear();
            listBox1.Items.AddRange(list.ToArray());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                radioButton1.Visible = radioButton2.Visible = textBox2.Visible = radioButton3.Visible = true;
            }
            else
            {
                radioButton1.Visible = radioButton2.Visible = textBox2.Visible = radioButton3.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            listBubble.Clear();
            Inicializate();
        }

        private void Inicializate()
        {
            #region
            for (int i = 0; i < loop; i++)
            {
                randomNumber = random.Next(0, 40000);
                //Console.WriteLine(randomNumber);
                //listBox2.Items.Add(randomNumber.ToString());
                listBubble.Add(randomNumber);
            }
            
            Sort();
            #endregion
        }

        private void Reset()
        {
            listBubble.Clear();
            loop += 50;
            Inicializate();
        }

        private void Sort()
        {
            stopWatch.Start();

            double time = stopWatch.Elapsed.TotalMilliseconds;

            int[] listBubbleArray = listBubble.ToArray();

            for (int i = 0; i < listBubbleArray.Length - 1; i++)
            {
                for (int j = i + 1; j < listBubbleArray.Length; j++)
                {
                    int a = listBubbleArray[i];
                    int p = listBubbleArray[j];
                    if (a > p)
                    {
                        listBubbleArray[i] = p;
                        listBubbleArray[j] = a;
                    }
                }

            }
            listBubble.Clear();
            listBubble.AddRange(listBubbleArray);

            foreach (int item in listBubble)
            {
                //Console.WriteLine(item.ToString());
            }
            Console.WriteLine(listBubbleArray.Length);
           
            if (loop >= 20000)
            {
                Console.WriteLine(time + " RunTime");
                List list = new List(listBubble.ToArray());
                listBox2.Items.Clear();
                listBox2.Items.AddRange(list.ToArray());
            }
            else
            {
                stopWatch.Stop();
                Console.WriteLine(time + " RunTime");
                Reset();
                
            } 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }
    }
}