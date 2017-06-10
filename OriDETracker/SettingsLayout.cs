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
            parent = par;
            InitializeComponent();
        }

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
    }

}
