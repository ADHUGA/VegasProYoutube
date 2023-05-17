using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Highlights
{
    public partial class SettingsForm : Form
    {

       

        private Form1 form1;
        public SettingsForm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure that you want to reset this program's settings? \n NOTE: This means you will have to set your folder directories again!", "Reset Settings?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                form1.ResetVariables();
                MessageBox.Show("Successfully Reset All Variables!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.afterIntroFadeOne = (double)numericUpDown2.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            form1.beforeOutroFadeOne = (double)numericUpDown1.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form1.sonyImportOne = (int)numericUpDown3.Value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form1.timelineHighlightOne = (int)numericUpDown4.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
