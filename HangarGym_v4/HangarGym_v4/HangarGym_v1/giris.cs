using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangarGym_v1
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void giris_Load(object sender, EventArgs e)
        {
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            timer1.Start();

        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac += 1;
            if (sayac ==6)
            {
                Main frm1 = new Main();
                frm1.Show();
                this.Hide();
            }
        }
    }
}
