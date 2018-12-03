using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUILayer.Forms
{
    public partial class frmColorSelect : Form
    {
        public int selectedColorNum;
        public string selectedColor;

        public frmColorSelect()
        {
            InitializeComponent();
            selectedColor = comboBox1.Text.Trim();
            selectedColorNum = comboBox1.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedColor = comboBox1.Text.Trim();
            selectedColorNum = comboBox1.SelectedIndex;
        }
    }
}
