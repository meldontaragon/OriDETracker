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

			numericUpDownOpacity.Value = (int) (100 * par.Opacity);
			trackBarOpacity.Value = (int) (100 * par.Opacity);

			if (parent.TrackerSize == TrackerPixelSizes.size420px)
			{
				this.rb_420.Checked = true;
				this.rb_640.Checked = false;
                this.rb_300.Checked = false;
                this.rb_720.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size640px)
			{
				this.rb_420.Checked = false;
				this.rb_640.Checked = true;
                this.rb_300.Checked = false;
                this.rb_720.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size300px)
            {
                this.rb_420.Checked = false;
                this.rb_640.Checked = false;
                this.rb_300.Checked = true;
                this.rb_720.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size720px)
            {
                this.rb_420.Checked = false;
                this.rb_640.Checked = false;
                this.rb_300.Checked = false;
                this.rb_720.Checked = true;
            }
            else
			{
                //parent.Log.WriteToLog("**ERROR** : Invalid Size (" + parent.TrackerSize + ")");
                parent.TrackerSize = TrackerPixelSizes.size640px;

                this.rb_420.Checked = false;
                this.rb_640.Checked = true;
                this.rb_300.Checked = false;
                this.rb_720.Checked = false;
            }

            if (parent.RefreshRate == (AutoUpdateRefreshRates)500)
            {
                this.rb_500_mHz.Checked = true;
                this.rb_10_hz.Checked = false;
                this.rb_1_hz.Checked = false;
                this.rb_60_hz.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)1000)
            {
                this.rb_500_mHz.Checked = false;
                this.rb_10_hz.Checked = false;
                this.rb_1_hz.Checked = true;
                this.rb_60_hz.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)10000)
            {
                this.rb_500_mHz.Checked = false;
                this.rb_10_hz.Checked = true;
                this.rb_1_hz.Checked = false;
                this.rb_60_hz.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)60000)
            {
                this.rb_500_mHz.Checked = false;
                this.rb_10_hz.Checked = false;
                this.rb_1_hz.Checked = false;
                this.rb_60_hz.Checked = true;
            }
            else
            {
               //parent.Log.WriteToLog("**ERROR** : Invalid Refresh Rate (" + parent.RefreshRate + ")");
                parent.RefreshRate = (AutoUpdateRefreshRates)10000;

                this.rb_500_mHz.Checked = false;
                this.rb_10_hz.Checked = true;
                this.rb_1_hz.Checked = false;
                this.rb_60_hz.Checked = false;
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

        public void RefreshOpacityBar()
        {
            numericUpDownOpacity.Value = (int)(100 * parent.Opacity);
            trackBarOpacity.Value = (int)(100 * parent.Opacity);
        }

		public void Reset()
		{
            numericUpDownOpacity.Value = 100;
            trackBarOpacity.Value = 100;
            rb_300.Checked = false;
            rb_420.Checked = false;
            rb_640.Checked = true;
            rb_720.Checked = false;
            cb_shards.Checked = false;
            cb_teleporters.Checked = false;
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
			parent.Refresh();
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

        private void cb_teleporters_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackTeleporters = cb_teleporters.Checked;
        }

        private void rb_720_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size720px;
            parent.UpdateImages();
            parent.Refresh();
        }
        private void rb_600_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size640px;
            parent.UpdateImages();
            parent.Refresh();
        }
        private void rb_400_CheckedChanged(object sender, EventArgs e)
		{
			parent.TrackerSize = TrackerPixelSizes.size420px;
			parent.UpdateImages();
			parent.Refresh();
		}
        private void rb_300_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size300px;
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


        private void rb_1_hz_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)1000;
        }

        private void rb_10_hz_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)10000;
        }

        private void rb_60_hz_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)60000;
        }

        private void rb_500_mHz_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)500;
        }
    }

}
