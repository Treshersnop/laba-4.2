using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _4._2_oop.Properties;

namespace _4._2_oop
{
    public partial class FormMain : Form
    {
        Model model;
        int i = 0;
        public FormMain()
        {
            InitializeComponent();
            model = new Model(Properties.Settings.Default.value1, Properties.Settings.Default.value2, Properties.Settings.Default.value3);
            model.observers += new System.EventHandler(this.UpdateFromModel);
            model.load();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb == textBox1)
                i = 1;
            else if (tb == textBox2)
                i = 2;
            else i = 3;
            if (e.KeyCode == Keys.Enter)
                model.SetValue(i, Int32.Parse(tb.Text));
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numd = (NumericUpDown)sender;
            if (numd == numericUpDown1)
                i = 1;
            else if (numd == numericUpDown2)
                i = 2;
            else i = 3;
            model.SetValue(i, Decimal.ToInt32(numd.Value));            
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar tr = (TrackBar)sender;
            if (tr == trackBar1)
                i = 1;
            else if (tr == trackBar2)
                i = 2;
            else i = 3;
            model.SetValue(i, tr.Value);
        }
        private void UpdateFromModel(object sender, EventArgs e)
        {
            textBox1.Text = model.GetValue(1).ToString();
            numericUpDown1.Value = model.GetValue(1);
            numericUpDown1.Maximum = model.GetValue(3);
            trackBar1.Value = model.GetValue(1);

            textBox2.Text = model.GetValue(2).ToString();
            numericUpDown2.Value = model.GetValue(2);
            numericUpDown2.Minimum = model.GetValue(1);
            numericUpDown2.Maximum = model.GetValue(3);
            trackBar2.Value = model.GetValue(2);

            textBox3.Text = model.GetValue(3).ToString();
            numericUpDown3.Value = model.GetValue(3);
            numericUpDown3.Minimum = model.GetValue(1);
            trackBar3.Value = model.GetValue(3);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.value1 = model.GetValue(1);
            Properties.Settings.Default.value2 = model.GetValue(2);
            Properties.Settings.Default.value3 = model.GetValue(3);
            Properties.Settings.Default.Save();
        }

        
    }
    public class Model
    {
        int val1;
        int val2;
        int val3;
        public System.EventHandler observers;
        public Model(int v1, int v2, int v3)
        {
            val1 = v1;
            val2 = v2;
            val3 = v3;
        }
        public void SetValue(int i, int value)
        {
            if (i == 1)
                SetValue1(value);
            else if (i == 2)
                SetValue2(value);
            else SetValue3(value);
        }
        public void load()
        {
            observers.Invoke(this, null);
        }
        private void SetValue1(int value)
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            if (val3 >= value)
            {
                val1 = value;
                if (val1 >= val2)
                    val2 = val1;
            }
            else val1 = GetValue(1);
            load();
        }

        private void SetValue2(int value)
        {
            if (val3 >= value)
            {
                if (val1 <= value)
                    val2 = value;
                else val2 = val1;
            }
            else val2 = val3;
            load();
        }

        private void SetValue3(int value)
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            if (val1 <= value)
            {
                val3 = value;
                if (val3 < val2)
                    val2 = value;
            }
            else val3 = GetValue(3);
            load();
        }
        public int GetValue(int i)
        {
            if (i == 1)
                return val1;
            else if (i == 2)
                return val2;
            else return val3;
        }
    }
}
