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

			numericUpDownScale.Value = (int) (100 * par.Scaling);
			trackBarScale.Value = (int) (100 * par.Scaling);

			numericUpDownOpacity.Value = (int) (100 * par.Opacity);
			trackBarOpacity.Value = (int) (100 * par.Opacity);

			if (parent.ImagePixelSize == 400)
			{
				this.rb_400.Checked = true;
				this.rb_600.Checked = false;
			}
			else if (parent.ImagePixelSize == 600)
			{
				this.rb_400.Checked = false;
				this.rb_600.Checked = true;
			}
			else
			{

			}

			this.cb_shards.Checked = parent.DisplayShards;

			this.Text = "Tracker Layer v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			rbRandoTrees.Checked = true;

			rbRandoEvents.Enabled = false;
			rbRandoTrees.Enabled = false;
			rbOriAllSkills.Enabled = false;
			rbOriAllCells.Enabled = false;
			rbReverseEventOrder.Enabled = false;

			Refresh();
		}

		public float Scaling {
			get { return (float) (numericUpDownScale.Value / 100);
			} set { numericUpDownScale.Value = (int) (100 * value);
			}
		}


		public void Reset()
		{
			numericUpDownScale.Value = 100;
			numericUpDownOpacity.Value = 100;
			trackBarScale.Value = 100;
			trackBarOpacity.Value = 100;
			rb_400.Checked = false;
			rb_600.Checked = true;
			cb_shards.Checked = false;
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
			parent.Scaling = (float) (numericUpDownScale.Value / (decimal) 100.0);
			parent.Refresh();

			int tmp = (int) numericUpDownScale.Value;
			trackBarScale.Value = tmp;
			numericUpDownScale.Value = tmp;
		}

		private void percentNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			parent.Opacity = (double) (numericUpDownOpacity.Value / (decimal) 100.0);
			parent.Refresh();

			int tmp = (int) numericUpDownOpacity.Value;
			trackBarOpacity.Value = tmp;
			numericUpDownOpacity.Value = tmp;
		}

		private void buttonBackgroundColor_Click(object sender, EventArgs e)
		{
			if (colorDialogBackground.ShowDialog() == DialogResult.OK)
			{
				parent.BackColor = colorDialogBackground.Color;
			}
			parent.Refresh();
		}

		private void trackBarScale_Scroll(object sender, EventArgs e)
		{
			int tmp = trackBarScale.Value;

			if (tmp < 50)
			{
				tmp = 50;
			}

			parent.Scaling = (float) (tmp / (decimal) 100.0);
			parent.Refresh();

			trackBarScale.Value = tmp;
			numericUpDownScale.Value = tmp;
		}

		private void trackBarOpacity_Scroll(object sender, EventArgs e)
		{
			parent.Opacity = (double) (trackBarOpacity.Value / (decimal) 100.0);
			parent.Refresh();

			int tmp = trackBarOpacity.Value;
			trackBarOpacity.Value = tmp;
			numericUpDownOpacity.Value = tmp;
		}

		private void cb_shards_CheckedChanged(object sender, EventArgs e)
		{
			parent.DisplayShards = cb_shards.Checked;
			parent.ChangeShards();
		}

		private void rb_400_CheckedChanged(object sender, EventArgs e)
		{
			parent.ImagePixelSize = 400;
			parent.UpdateImages();
			parent.Refresh();
		}

		private void rb_600_CheckedChanged(object sender, EventArgs e)
		{
			parent.ImagePixelSize = 600;
			parent.UpdateImages();
			parent.Refresh();
		}

		private void button_mapstone_font_Click(object sender, EventArgs e)
		{
			if (colorDialogFont.ShowDialog() == DialogResult.OK)
			{
                parent.FontColor = colorDialogFont.Color;
			}
			parent.Refresh();
		}

		internal void ChangeShards(bool display_shards)
		{
			cb_shards.Checked = display_shards;
		}
	}

}
