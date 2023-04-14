using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataBase DataBase = new DataBase();
        public Form1()
        {
            InitializeComponent();
        }
        private int count;

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var pass = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            String loginquery = $"select login, password, isAdmin from users where login = '{login}' and password = '{pass}'";

            SqlCommand command = new SqlCommand(loginquery, DataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                var user = new checkUser(table.Rows[0].ItemArray[0].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[2]));

                MessageBox.Show("Вход выполнен успешно");
                osnova osn = new osnova();
                this.Hide();
                osn.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");

                if (count++ == 2)
                {
                    New_pas np = new New_pas();
                    this.Hide();
                    np.ShowDialog();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
