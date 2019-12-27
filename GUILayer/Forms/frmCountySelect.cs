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
    public partial class frmCountySelect : Form
    {
        public List<string> countiesChecked = new List<string>();
        public List<int> indChecked = new List<int>();

        public frmCountySelect(List<string> counties)
        {
            InitializeComponent();
            for (int i = 0; i < counties.Count; i++)
            {
                checkedListBox1.Items.Add(counties[i], false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                indChecked.Add(indexChecked);
                countiesChecked.Add(checkedListBox1.Items[indexChecked].ToString().Trim());

                //string s =  checkedListBox1.GetItemCheckState(indexChecked).ToString();
            }


            //foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                //countiesChecked.Add(checkedListBox1.GetItemCheckState(checkedListBox1.Items[itemChecked]);
                //countiesChecked.Add(checkedListBox1.GetItemCheckState(checkedListBox1.Items.IndexOf(itemChecked)).ToString());
            }

        }
    }
}
