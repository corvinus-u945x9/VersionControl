using IRF_3.Gyak3._0.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF_3.Gyak3._0
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User();
            {
                u.FullName = textBox1.Text;

            }
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog mentes = new SaveFileDialog();
            mentes.InitialDirectory = Application.StartupPath;
            mentes.Filter = "Comma Seperated Values (*.csv) | *.csv";
            mentes.DefaultExt = "csv";
            mentes.AddExtension = true;

            if (mentes.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(mentes.FileName, false, Encoding.UTF8))
            {
                foreach (var i in users)
                {
                    sw.WriteLine(i.FullName);
                    sw.Write(";");
                    sw.WriteLine(i.ID);
                    sw.Write(";");

                }
            }
        }
    }
}
