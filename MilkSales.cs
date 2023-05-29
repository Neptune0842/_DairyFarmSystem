using System;
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
    public partial class MilkSales : Form
    {
        public MilkSales()
        {
            InitializeComponent();
            FillEmpId();
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

        private void label7_Click(object sender, EventArgs e)
        {
            Breeding Ob = new Breeding();
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

        private void FillEmpId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select EmpId from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;
            Con.Close();
        }
        private void MilkSales_Load(object sender, EventArgs e)
        {

        }
        private void SaveTrans()
        {


            try
            {
                String Sales = "Sales";
                Con.Open();
                string Query = "insert into IncomeTbl values('" + Date.Value.Date + "','" + Sales + "'," + TotalTb.Text + "," + EmpIdCb.SelectedValue.ToString() + ")";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Income Saved");
                Con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void populate()
        {
            Con.Open();
            string Query = "select * from MilkSalesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void clear()
        {
            TotalTb.Text = "";
            QuantityTb.Text = "";
            PriceTb.Text = "";
            PhoneTb.Text = "";
            ClientNameTb.Text = "";
            EmpIdCb.SelectedIndex = -1;
        }

        private void QuantityTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(PriceTb.Text) * Convert.ToInt32(QuantityTb.Text);
            TotalTb.Text = "" + total;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdCb.SelectedIndex == -1 || ClientNameTb.Text == "" || PhoneTb.Text == "" || PriceTb.Text == "" || QuantityTb.Text == "" || TotalTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into MilkSalesTbl values('" + Date.Value.Date + "','" + PriceTb.Text + "','" + ClientNameTb.Text + "','" + PhoneTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + "," + QuantityTb.Text + ", " + TotalTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Milk Sales Saved");
                    Con.Close();
                    populate();
                    SaveTrans();
                    clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SalesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
