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
    public partial class Form1 : Form
    {
        Controller control;
        public Form1()
        {
            InitializeComponent();
            control = new Controller();
            RefreshTableList();
            RefreshTreeList();
        }

        void RefreshTableList()
        {
            dataGridView2.Rows.Clear();
            TableRecords[] table = control.GetTable();
            if (table != null)
            {
                for (int i = 0; i < table.Length; i++)
                {
                    dataGridView2.Rows.Add(table[i].h1, table[i].h2, table[i].news.topic, table[i].news.title,
                        table[i].news.date.PrintDate());
                }
            }
        }

        void RefreshTreeList()
        {
            dataGridView1.Rows.Clear();
            Comment[] comments = control.GetTree();
            if(comments != null)
            {
                for (int i = 0; i < comments.Length; i++)
                {
                    dataGridView1.Rows.Add(comments[i].author, comments[i].title, comments[i].date.PrintDate());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this, false);
            form2.Show();
        }

        public void AddToTableList(string _topic, string _title, string _date)
        {
            Date dateRec = Parser.ParseDate(_date);
            if ((_topic == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось добавить, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }
            if(!control.AddNews(new News { topic = _topic, title = _title, date = dateRec }))
            {
                MessageBox.Show("Не удалось добавить, так как такая новость уже присутствует", "Ошибка");
            }
            RefreshTableList();
        }

        public void DeleteInTableList(string _topic, string _title, string _date)
        {
            Date dateRec = Parser.ParseDate(_date);
            if ((_topic == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось удалить, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }

            if (control.DeleteNews(new News { topic = _topic, title = _title, date = dateRec }))
            {
                RefreshTableList();
            }
            else
            {
                MessageBox.Show("Не удалось удалить, так как либо присутствуют комментарии к этой новости, либо такой новости нет", "Ошибка");
            }
        }

        public void SearchInTableList(string _topic, string _title, string _date)
        {
            dataGridView2.ClearSelection();
            Date dateRec = Parser.ParseDate(_date);
            if ((_topic == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось выполнить поиск, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Selected = false;
                if (dataGridView2.Rows[i].Cells[2].Value.ToString().Contains(_topic) && 
                    dataGridView2.Rows[i].Cells[3].Value.ToString().Contains(_title) && 
                    dataGridView2.Rows[i].Cells[4].Value.ToString().Contains(dateRec.PrintDate()))
                {
                    dataGridView2.Rows[i].Selected = true;
                    return;
                }
            }
            MessageBox.Show("Не удалось найти новость", "Ошибка");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this, false);
            form3.Show();
        }

        public void AddToTreeList(string _author, string _title, string _date)
        {
            Date dateRec = Parser.ParseDate(_date);

            if ((_author == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось добавить, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }

            if (control.AddComment(new Comment { author = _author, title = _title, date = dateRec }))
            {
                RefreshTreeList();
            }
            else
            {
                MessageBox.Show("Не удалось добавить, так как либо такой новости нет, либо такой комментарий уже присутствует", "Ошибка");
            }
        }

        public void DeleteInTreeList(string _author, string _title, string _date)
        {
            Date dateRec = Parser.ParseDate(_date);
            if ((_author == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось удалить, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }

            if (!control.DeleteComment(new Comment { author = _author, title = _title, date = dateRec }))
            {
                MessageBox.Show("Не удалось удалить, так как такого комментария нет", "Ошибка");
            }
            RefreshTreeList();
        }

        public void SearchInTreeList(string _author, string _title, string _date)
        {
            dataGridView1.ClearSelection();
            Date dateRec = Parser.ParseDate(_date);
            if ((_author == "") || (_title == ""))
            {
                MessageBox.Show("Не удалось выполнить поиск, так как незаполнено одно или несколько обязательных полей", "Ошибка");
                return;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(_author) &&
                    dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(_title) &&
                    dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(dateRec.PrintDate()))
                {
                    dataGridView1.Rows[i].Selected = true;
                    return;
                }
            }
            MessageBox.Show("Не удалось найти комментарий", "Ошибка");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(control.PrintTree(), "Дерево");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string str = control.SearchForTopic(textBox1.Text);
            if (str != "")
            {
                MessageBox.Show(control.SearchForTopic(textBox1.Text), "Тематики");
            }
            else
            {
                MessageBox.Show("Такого автора нет", "Ошибка");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Parser.WriteFirst(control.GetTable());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Parser.WriteSecond(control.GetTree());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this, true);
            form2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this, true);
            form3.Show();
        }
    }
}
