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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            Finance();
            Logistic();
            GetMax();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MilkProduction Ob = new MilkProduction();
            Ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Cows Ob = new Cows();
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
           
        }

        private void guna2GradientPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Finance()
        {

        }
        private void DashBoard_Load(object sender, EventArgs e)
        {

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(IncAmt) from IncomeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int inc, exp;
            double bal;
            inc = Convert.ToInt32(dt.Rows[0][0].ToString());

            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            IncLbl.Text = "Egp" + dt.Rows[0][0].ToString();

            exp = Convert.ToInt32(dt.Rows[0][0].ToString());
            bal = inc - exp;
            ExpLbl.Text = "Egp" + dt1.Rows[0][0].ToString();
            BalanceLbl.Text = "Egp" + bal;
            Con.Close();

        }
        private void Logistic()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from CowTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select Sum(TotalMilk) from MilkTbl", Con);
            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from EmployeeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CowsNumLbl.Text = dt.Rows[0][0].ToString();
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            MilkStockLbl.Text = dt1.Rows[0][0].ToString() + " " + "Liters";
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            EmpNumLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();

        }
        private void GetMax()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Max(IncAmt) from IncomeTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select Max(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            HighAmt.Text = "Rs " + dt.Rows[0][0].ToString();
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            HighExp.Text = "Rs " + dt1.Rows[0][0].ToString();
            Con.Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
