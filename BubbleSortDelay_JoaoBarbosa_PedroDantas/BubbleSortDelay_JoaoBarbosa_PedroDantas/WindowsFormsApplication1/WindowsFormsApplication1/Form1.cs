using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        List<string> listBubble = new List<string>();

        #region
        Random random = new Random();
        int randomNumber;
        #endregion

        public Form1()
        {
            InitializeComponent();
            list = new List(2, 3, 4, 1);

            listBox1.Items.AddRange(list.ToArray());

            #region
            for (int i = 0; i < 800; i++)
            {
                randomNumber = random.Next(0, 4000);
                Console.WriteLine(randomNumber);
                listBubble.Add(Convert.ToString(randomNumber));
            }
            listBox2.Items.AddRange(listBubble.ToArray());
            #endregion
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
            DateTime now = DateTime.Now;
            Console.WriteLine(String.Format("{0:00}hr:{1:00}min:{2:00}seg:{3:000}ms", now.Hour, now.Minute, now.Second, now.Millisecond )+ "\n");
            
            int[] bubble = new int[listBubble.Count];
            for (int i = 0; i < listBubble.Count; i++)
            {
                bubble[i] = Convert.ToInt32(listBubble.ToArray()[i]);
            }

            for (int i = 0; i < bubble.Length; i++)
            {
                for (int j = i + 1; j < bubble.Length; j++)
                {
                    if (bubble[i] > bubble[j])
                    {
                        int temp = bubble[i];
                        bubble[i] = bubble[j];
                        bubble[j] = temp;
                        label1.Text = "Result in Console";
                    }
                }
                Console.Write(bubble[i] + " ");
            }

            DateTime down = DateTime.Now;
            Console.WriteLine(String.Format("{0:00}hr:{1:00}min:{2:00}seg:{3:000}ms", down.Hour, down.Minute, down.Second, down.Millisecond) + "\n");
            
            int result;
            int minutesRest;
            int secondRest;

            minutesRest = down.Minute - now.Minute; // * 60
            secondRest = down.Second - now.Second; //* 1000
            result = (down.Millisecond - now.Millisecond) + (minutesRest * 60) + (secondRest * 1000);

            label2.Text = String.Format("{0:000} ms", result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }
    }
}