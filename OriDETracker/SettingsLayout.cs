using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class SettingsLayout : Form
    {
        Tracker parent;
        public SettingsLayout(Tracker par)
        {
            InitializeComponent();

            parent = par;
            numericUpDownScaling.Value = (int)(100 * par.Scaling);

            Refresh();
        }

        public float Scaling { get { return (float)(numericUpDownScaling.Value / 100); } set { numericUpDownScaling.Value = (int)(100 * value); } }
        
        private void numericUpDownScaling_ValueChanged(object sender, EventArgs e)
        {
            parent.Scaling = (float)(numericUpDownScaling.Value / (decimal)100.0);
            parent.Refresh();
        }

        public void Reset()
        {
            numericUpDownScaling.Value = 100;
        }

        private void rbRandoTrees_CheckedChanged(object sender, EventArgs e)
        {
            parent.ChangeLayout(TrackerLayout.RandomizerAllTrees);
        }

        private void rbRandoEvents_CheckedChanged(object sender, EventArgs e)
        {
            parent.ChangeLayout(TrackerLayout.RandomizerAllEvents);
        }

        private void rbOriAllSkills_CheckedChanged(object sender, EventArgs e)
        {
            parent.ChangeLayout(TrackerLayout.AllSkills);
        }

        private void rbOriAllCells_CheckedChanged(object sender, EventArgs e)
        {
            parent.ChangeLayout(TrackerLayout.AllCells);
        }

        private void rbReverseEventOrder_CheckedChanged(object sender, EventArgs e)
        {
            parent.ChangeLayout(TrackerLayout.ReverseEventOrder);
        }

        private void SettingsLayout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.FormOwnerClosing))
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        private void percentNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.Opacity = (double)(percentNumericUpDown.Value / (decimal)100.0);
        }

        private void buttonBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialogBackground.ShowDialog() == DialogResult.OK)
            {
                parent.BackColor = colorDialogBackground.Color;
            }
        }
    }

}
