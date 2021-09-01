using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lorena_Library_Salons;
using System.Data.SQLite;


namespace LorenaKitchen_TestTask
{
    public partial class Form1 : Form
    {
        private SQLiteConnection conn;
        private SQLiteCommand comm;
        public Form1()
        {
            InitializeComponent();
            conn = new SQLiteConnection("Data Source=SalonsBaseDate.db");
            comm = new SQLiteCommand();
        }

        public void UpdateDataGrid()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "CREATE TABLE IF NOT EXISTS Salone(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name_salon nvarchar(100) not null, discount double,addiction BOOLEAN, description nvarchar(124),parent_id INTEGER )";
                comm.ExecuteNonQuery();
                MessageBox.Show("Connected");
                conn.Close();

                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "CREATE TABLE IF NOT EXISTS Reports(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name_salon nvarchar(100) not null, price DOUBLE NOT NULL, result DOUBLE NOT NULL)";
                comm.ExecuteNonQuery();
                conn.Close();



                conn.Open();
                comm.CommandText = "SELECT COUNT(*) FROM Salone";
                int cnt = 1;
                using (SQLiteDataReader r = comm.ExecuteReader())
                {
                    if (r.Read())
                    cnt = r.GetInt32(0);
                }
                conn.Close();

                if(cnt==0)
                {
                    conn.Open();
                    comm.CommandText = "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        "VALUES('Миасс',4,false,'opisanie',0);" +
                        "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        "VALUES('Амелия',5,true,'opisanie1',1); " +
                        "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        "VALUES('Тест1',2,true,'opisanie2',2);" +
                        "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        "VALUES('Тест2',0,true,'opisanie3',1);" +
                        "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        "VALUES('Курган',11,false,'opisanie4',0);";
                    comm.ExecuteNonQuery();
                    conn.Close();
                }

                conn.Open();
                DataSet setter = new DataSet();
                string comtext = "Select * From Salone";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comtext, conn);
                adapter.Fill(setter);
                dataGridView1.DataSource = setter.Tables[0].DefaultView;
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox3.Text= dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            bool addit;
            if (textBox3.Text != "")
            {
                addit = true;
            }
            else addit = false;
            int par_id = 0;
            if (textBox3.Text != "") par_id = Convert.ToInt32(textBox3.Text);
            comm.CommandText= "INSERT INTO Salone(name_salon,discount,addiction,description,parent_id) " +
                        $"VALUES('{textBox1.Text}',{Convert.ToDouble(textBox2.Text)},{addit},'{textBox4.Text}',{par_id})";
            comm.ExecuteNonQuery();
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            DataSet setter = new DataSet();
            string comtext = "Select * From Salone";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(comtext, conn);
            adapter.Fill(setter);
            dataGridView1.DataSource = setter.Tables[0].DefaultView;
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            int column_index = dataGridView1.CurrentCell.ColumnIndex;
            string name_column = dataGridView1.Columns[column_index].HeaderText;
            if(name_column=="description"||name_column=="name_salon")
            {
                comm.CommandText = $"UPDATE Salone  SET {name_column}='{Convert.ToString(dataGridView1.CurrentCell.Value)}'";
            }
            else
            {
                comm.CommandText = $"UPDATE Salone  SET {name_column}={dataGridView1.CurrentCell.Value}";
            }
            
            conn.Close();
        }

        public void Add_information_at_list()
        {
            List<Salons> temp_list = new List<Salons>();
            
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                Salons temp = new Salons(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value), Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), Convert.ToString(dataGridView1.Rows[i].Cells[4].Value), Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value));
                temp_list.Add(temp);
                
            }
            List_Salons salone_list = new List_Salons(temp_list);
            string temp_name_salon = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            int index_column = dataGridView1.CurrentRow.Index;

            double temp_price = temp_list[index_column].SetPrice(Convert.ToDouble(textBox5.Text), salone_list.GetParentDiscount(index_column + 1));
            MessageBox.Show(Convert.ToString(temp_price));
            conn.Open();
            comm.CommandText = "INSERT INTO Reports(name_salon,price.result) VALUES ()";
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_information_at_list();
        }
    }
}
