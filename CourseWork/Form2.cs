using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Form2 : Form
    {
        Form1 form1;
        public Form2(Form1 form1, bool isSearch)
        {
            InitializeComponent();
            this.form1 = form1;
            dateTimePicker1.MinDate = new DateTime(1980, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(2030, 12, 31);
            if(isSearch)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.AddToTableList(textBox1.Text, textBox2.Text, dateTimePicker1.Text);
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.DeleteInTableList(textBox1.Text, textBox2.Text, dateTimePicker1.Text);
            this.Dispose();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            form1.SearchInTableList(textBox1.Text, textBox2.Text, dateTimePicker1.Text);
            this.Dispose();
        }
    }
}
