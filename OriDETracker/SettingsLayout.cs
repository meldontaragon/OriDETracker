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

            numericUpDownScale.Value = (int)(100 * par.Scaling);
            trackBarScale.Value = (int)(100 * par.Scaling);

            numericUpDownOpacity.Value = (int)(100 * par.Opacity);
            trackBarOpacity.Value = (int)(100 * par.Opacity);

            rbRandoTrees.Checked = true;

            rbRandoEvents.Enabled = false;
            rbRandoTrees.Enabled = false;
            rbOriAllSkills.Enabled = false;
            rbOriAllCells.Enabled = false;
            rbReverseEventOrder.Enabled = false;

            Refresh();
        }

        public float Scaling { get { return (float)(numericUpDownScale.Value / 100); } set { numericUpDownScale.Value = (int)(100 * value); } }


        public void Reset()
        {
            numericUpDownScale.Value = 100;
            numericUpDownOpacity.Value = 100;
            trackBarScale.Value = 100;
            trackBarOpacity.Value = 100;
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

        private void numericUpDownScaling_ValueChanged(object sender, EventArgs e)
        {
            parent.Scaling = (float)(numericUpDownScale.Value / (decimal)100.0);
            parent.Refresh();

            int tmp = (int)numericUpDownScale.Value;
            trackBarScale.Value = tmp;
            numericUpDownScale.Value = tmp;
        }

        private void percentNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.Opacity = (double)(numericUpDownOpacity.Value / (decimal)100.0);
            parent.Refresh();

            int tmp = (int)numericUpDownOpacity.Value;
            trackBarOpacity.Value = tmp;
            numericUpDownOpacity.Value = tmp;
        }

        private void buttonBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialogBackground.ShowDialog() == DialogResult.OK)
            {
                parent.BackColor = colorDialogBackground.Color;
            }
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            int tmp = trackBarScale.Value;
            if (tmp < 50)
                tmp = 50;

            parent.Scaling = (float)(tmp / (decimal)100.0);
            parent.Refresh();

            trackBarScale.Value = tmp;
            numericUpDownScale.Value = tmp;
        }

        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            parent.Opacity = (double)(trackBarOpacity.Value / (decimal)100.0);
            parent.Refresh();

            int tmp = trackBarOpacity.Value;
            trackBarOpacity.Value = tmp;
            numericUpDownOpacity.Value = tmp;
        }
    }

}
