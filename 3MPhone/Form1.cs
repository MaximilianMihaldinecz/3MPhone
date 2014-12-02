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

namespace _3MPhone
{
    /* Thanks for checking out this code. I hope you will find it usefull. 
     * The key idea is to keep it simple, just functional. The program is for "teaching" purposes, not really for real-life use.
     * There are tons of things you can improve... e.g. data validation for user input and file management.
     * 
     * You can find the license in a seperate file, it is GPLv2. You can use it accordingly in your work.
     * Original author: Maximilian Milan Mihaldinecz
    */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataValidation())
            {
                if (IsThereSpace())
                {
                    AddToListBox();
                    IncrementCounters();
                }
                else
                {
                    MessageBox.Show("This entry cannot be added. No space for it");
                }
            }
            else
            {
                MessageBox.Show("The data validation failed. You put invalid value to the name or phone number field");
            }
            
        }

        private void IncrementCounters()
        {
            if (radioButton1.Checked)
            {
                lblFri.Text = (Decimal.Parse(lblFri.Text)+1).ToString();
            }
            else
            {
                lblColl.Text = (Decimal.Parse(lblColl.Text) + 1).ToString();                
            }
        }

        private bool IsThereSpace()
        {
            if (radioButton1.Checked)
            {
                return numericUpDown1.Value > Decimal.Parse(lblFri.Text);
            }
            else
            {
                return numericUpDown2.Value > Decimal.Parse(lblColl.Text);
            }
        }

        private void AddToListBox()
        {
            string newItem = textBox2.Text + "\t" + textBox1.Text + "\t";
            if (radioButton1.Checked)
            {
                newItem += "Friend";
            }
            else
            {
                newItem += "Colleague";
            }
            listBox1.Items.Add(newItem);
        }

        private bool DataValidation()
        {
            //Needs to be implemented. (Check the data before add it to the list)
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DecreaseCounter(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Select something from the list first!");
            }
        }

        private void DecreaseCounter(string p)
        {
            string[] temp = p.Split('\t');
            if (temp[2] == "Friend")
            {
                lblFri.Text = (Decimal.Parse(lblFri.Text) - 1).ToString();
            }
            else
            {
                lblColl.Text = (Decimal.Parse(lblColl.Text) - 1).ToString();     
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveToFile(saveFileDialog1.FileName);
            }
        }

        private void SaveToFile(string p)
        {
            //You want to implement some exception handling here for sure!
            StreamWriter sw = new StreamWriter(p);
            //Write out settings
            string settings = numericUpDown1.Value.ToString() + "\t" + lblFri.Text + "\t" + numericUpDown2.Value.ToString() + "\t" + lblColl.Text;

            if (listBox1.Items.Count > 0)
            {
                sw.WriteLine(settings);
            }
            else
            {
                sw.Write(settings);
            }
            
            //Write out listbox items
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i < listBox1.Items.Count - 1)
                {
                    sw.WriteLine(listBox1.Items[i].ToString());
                }
                else
                {
                    sw.Write(listBox1.Items[i].ToString());
                }
            }
            sw.Flush();
            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFromFile(openFileDialog1.FileName);
            }
        }

        private void LoadFromFile(string p)
        {
            //You want to implement some exception handling here for sure!
            StreamReader sr = new StreamReader(p);
            //Read settings, refresh UI
            string[] settings = sr.ReadLine().Split('\t');
            numericUpDown1.Value = Decimal.Parse(settings[0]);
            lblFri.Text = settings[1];
            numericUpDown2.Value = Decimal.Parse(settings[2]);
            lblColl.Text = settings[3];

            //Fill up the listbox
            listBox1.Items.Clear();
            while (!sr.EndOfStream)
            {
                listBox1.Items.Add(sr.ReadLine());
            }
            sr.Close();
        }
    }
}
