﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _DairyFarmSystem
{
    public partial class Breeding : Form
    {
        public Breeding()
        {
            InitializeComponent();
            populate();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Cows Ob = new Cows();
            Ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MilkProduction Ob = new MilkProduction();
            Ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            CowHealth Ob = new CowHealth();
            Ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            MilkSales Ob = new MilkSales();
            Ob.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Finances Ob = new Finances();
            Ob.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            DashBoard Ob = new DashBoard();
            Ob.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl");
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;
            Con.Close();
        }

        private void populate()
        {
            Con.Open();
            string Query = "select * from BreedTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BreedDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCowName()
        {
            Con.Open();
            String query = "select * from CowTbl where CowId=" + CowIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
                CowAgeTb.Text = dr["Age"].ToString();
            }

            Con.Close();
        }
        private void Breeding_Load(object sender, EventArgs e)
        {

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }
        private void Clear()
        {
            CowIdCb.SelectedIndex = -1;
            CowNameTb.Text = "";
            CowAgeTb.Text = "";
            RemarksTb.Text = "";
            key = 0;
        }
        int key = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || CowAgeTb.Text == "" || RemarksTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into BreedTbl values('" + HeatDate.Value.Date + "','" + BreedDate.Value.Date + "','" + CowIdCb.SelectedValue.ToString() + "','" + CowNameTb.Text + "','" + PregDate.Value.Date + "','" + ExpDate.Value.Date + "', '" + DateCalved.Value.Date + "', '" + CowAgeTb.Text + "', '" + RemarksTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breed saved");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BreedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void BreedDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CowIdCb.SelectedValue = BreedDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = BreedDGV.SelectedRows[0].Cells[2].Value.ToString();
            HeatDate.Text = BreedDGV.SelectedRows[0].Cells[3].Value.ToString();
            BreedDate.Text = BreedDGV.SelectedRows[0].Cells[4].Value.ToString();
            PregDate.Text = BreedDGV.SelectedRows[0].Cells[5].Value.ToString();
            ExpDate.Text = BreedDGV.SelectedRows[0].Cells[6].Value.ToString();
            DateCalved.Text = BreedDGV.SelectedRows[0].Cells[7].Value.ToString();
            RemarksTb.Text = BreedDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(BreedDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select Breed");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from BreedTbl where BrId= " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breed Deleted");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || CowAgeTb.Text == "" || RemarksTb.Text == "")
            {
                MessageBox.Show("Select Breed");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update BreedTbl set HealthDate='" + HeatDate.Value.Date + "',BreedDate= " + BreedDate.Value.Date + ",CowId=" + CowIdCb.SelectedValue.ToString() + ",CowName=" + CowNameTb.Text + ",PregDate=" + PregDate.Value.Date + ",ExpDateCalve='" + ExpDate.Value.Date + ",DateCalved='" + DateCalved.Value.Date + ",CowAge='" + CowAgeTb.Text + ",Remarks='" + RemarksTb.Text + "' where BrId=" + key + " ;";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
