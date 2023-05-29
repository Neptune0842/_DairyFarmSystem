using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _DairyFarmSystem
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void populate()
        {
            Con.Open();
            string Query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Employees_Load(object sender, EventArgs e)
        {

        }

        private void clear()
        {
            PhoneTb.Text = "";
            EmpNameTb.Text = "";
            AddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || GenderCb.SelectedIndex == -1 || AddressTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into EmployeeTbl values('" + EmpNameTb.Text + "','" + DoB.Value.Date + "','" + GenderCb.SelectedValue.ToString() + "','" + PhoneTb.Text + "','" + AddressTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved");
                    Con.Close();
                    populate();
                    clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
