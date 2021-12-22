using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
        }
        SqlSimulation ss = new SqlSimulation();
        private bool Working;


        string[] logins = { "efremov95", "grig21", "roma34", "alinka03", "vasileva02" };
        string[] passwords = { "234", "ujh78", "sedr45", "13579", "2460" };
        string[] names = { "Иван", "Григорий", "Роман", "Алина", "Ольга" };
        string[] surnames = { "Ефремов", "Смирнов", "Иванов", "Умнова", "Васильева" };
        Random random = new Random();
        int count = 0; // счётчик для регистрации
        public void SimulateRegistration()
        {
            while (Working)
            {
                string Login = logins[random.Next(0, logins.Length)] + random.Next(10000, 99999);
                string Password = passwords[random.Next(0, passwords.Length)];
                string Name = names[random.Next(0, names.Length)];
                string Surname = surnames[random.Next(0, names.Length)];
                ss.RegUsers(Login, Password, Name, Surname);
                count++;
                label2.Invoke(new Action(() => label2.Text = count.ToString()));
                Thread.Sleep(1000);
            }
        }
        class SqlSimulation
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=project1");
            public bool RegUsers(string Login, string Password, string Name, string Surname)
            {
                bool flag = false;
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO users (Login, Password, Name, Surname) VALUES (@uLogin, @password, @name, @surname)", connection);
                cmd.Parameters.AddWithValue("@uLogin", Login);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@surname", Surname);
                connection.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    flag = true;
                }
                connection.Close();
                return flag;
            }
            // Авторизация пользователя
            public bool LogUsers(string Login, string Password)
            {
                bool flag = false;
                MySqlCommand cmd = new MySqlCommand($"SELECT login FROM users WHERE Login = @login AND Password = @password", connection);
                cmd.Parameters.AddWithValue("@login", Login);
                cmd.Parameters.AddWithValue("@password", Password);
                connection.Open();
                MySqlDataReader srd = cmd.ExecuteReader();
                if (srd.HasRows)
                {
                    flag = true;
                }
                connection.Close();
                return flag;
            }
        }

        private void StartSimbutton_Click(object sender, EventArgs e)
        {
            Working = true;
            Task.Run(() => SimulateRegistration());
        }

        private void EndSimbutton_Click(object sender, EventArgs e)
        {
            {
                Working = false;
               //this.Hide();
                //Admin admin = new Admin();
                //admin.Show();
            }
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        Point lastpoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;

            }

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            {
                lastpoint = new Point(e.X, e.Y);
            }

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;

            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            {
                lastpoint = new Point(e.X, e.Y);
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;

            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            {
                lastpoint = new Point(e.X, e.Y);
            }
        }


    }
}
