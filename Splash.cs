using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _DairyFarmSystem
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 5;
            Progress.Value = startpoint;
            if (Progress.Value == 100)
            {
                Progress.Value = 0;
                timer1.Stop();
                Login Log = new Login();
                this.Hide();
                Log.Show();
            }
        }
    }
}
