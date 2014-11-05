using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.openFileDialog1.CheckFileExists = true;
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "*.txt|*.txt";
            this.openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.richTextBox1.Text = System.IO.File.ReadAllText(this.openFileDialog1.FileName); 
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Message.Show("closing");
            this.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
