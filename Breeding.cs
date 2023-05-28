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
    public partial class Breeding : Form
    {
        public Breeding()
        {
            InitializeComponent();
            populate();
            FillCowId();
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
            string Query = "select * from HealthTbl";
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
    }
}
