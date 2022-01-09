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

namespace Pendulum_KK
{
    public partial class Form1 : Form
    {
        public string ConnectionString { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        private void FF(object sender, EventArgs e)
        {
            ConnectionString = "Server = (localdb)\\MSSQLLocalDB;" + "Database = music;";

            comboBox2.Enabled = false;
            textBox1.Enabled = false;
            FillComboBox1();


        }
        private void FillDvg()
        {
            dataGridView1.Rows.Clear();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var r = new SqlCommand(
                    $"SELECT Tracks.title,Tracks.url, Tracks.length FROM Tracks, Albums WHERE Albums.id = Tracks.album AND Albums.title LIKE '{comboBox2.SelectedItem}%' AND Tracks.title LIKE '{textBox1.Text}%' ;", conn).ExecuteReader();
                while (r.Read())
                {
                    dataGridView1.Rows.Add(r[0], r[1], r[2]);
                }

            }
            textBox1.Enabled = true;
        }

        private void FillComboBox2()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var r = new SqlCommand(
                    "SELECT title FROM Albums GROUP BY title", conn).ExecuteReader();
                while (r.Read())
                {
                    comboBox2.Items.Add($"{r[0]}");
                }
                comboBox2.Enabled = true;
            }
        }

        private void FillComboBox1()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var r = new SqlCommand(
                    "SELECT artist FROM Albums GROUP BY artist", conn).ExecuteReader();
                while (r.Read())
                {
                    comboBox1.Items.Add($"{r[0]}");
                }

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDvg();
            if (comboBox2.Text == "Hold Your Colour")
            {
                pictureBox1.Image = Properties.Resources.hold_your_colour;
            }
            if (comboBox2.Text == "In Silico")
            {
                pictureBox1.Image = Properties.Resources.in_silico;
            }
            if (comboBox2.Text == "Immersion")
            {
                pictureBox1.Image = Properties.Resources.immersion;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            FillComboBox2();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillDvg();
        }



        //----------------------------------------------------------------------------------------------------------------------------------
        //Kisebb hibáknak köszönhetően pirosan a 2db véletlen generáltat aláhuzza, és nem megy olyankor a form szóval ezt így tudom csak 
        //---------------------------------------------------------------------------------------------------------------------------------

    //    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    //    {
    //        if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != "")
    //        {
    //            linkLabel3.Text = $"https://youtu.be/{dataGridView1.CurrentRow.Cells[1].Value.ToString()}";
    //        }
    //        else
    //        {
    //            linkLabel3.Text = "There is no link for this track!";
    //        }
    //    }
    //}

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        Console.ReadKey();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}

