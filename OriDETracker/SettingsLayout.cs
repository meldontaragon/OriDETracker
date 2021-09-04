using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class SettingsLayout : Form
    {
        private readonly Tracker parent;
        public SettingsLayout(Tracker par)
        {
            InitializeComponent();
            this.Text = "Tracker Layer v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            parent = par;
            Reset();
        }

        internal void Reset()
        {
            SetTrackingOptions();
            SetDisplayOptions();
            SetRefreshRate();
            SetTrackerSize();
            SetOpacity();
        }

        private void SetTrackingOptions()
        {
            this.TrackShardsCheckbox.Checked = parent.TrackShards;
            this.TrackTeleportersCheckbox.Checked = parent.TrackTeleporters;
            this.TrackTreesCheckbox.Checked = parent.TrackTrees;
            this.TrackRelicsCheckbox.Checked = parent.TrackRelics;
            this.TrackMapstonesCheckbox.Checked = parent.TrackMapstones;
        }
        private void SetDisplayOptions()
        {
            this.DisplayGreyTreesCheckbox.Checked = parent.DisplayEmptyTrees;
            this.DisplayGreyTeleportersCheckbox.Checked = parent.DisplayEmptyTeleporters;
            this.DisplayExistingRelicsCheckbox.Checked = parent.DisplayEmptyRelics;
            this.DisplayGreyShardsCheckbox.Checked = parent.DisplayEmptyShards;
        }
        private void SetRefreshRate()
        {
            this.SlowUpdateRadioButton.Checked = false;
            this.NormalUpdateRadioButton.Checked = false;
            this.ModerateUpdateRadioButton.Checked = false;
            this.FastUpdateRadioButton.Checked = false;

            switch (parent.RefreshRate)
            {
                case AutoUpdateRefreshRates.rate500mHz:
                    this.SlowUpdateRadioButton.Checked = true;
                    break;
                case AutoUpdateRefreshRates.rate1Hz:
                    this.ModerateUpdateRadioButton.Checked = true;
                    break;
                case AutoUpdateRefreshRates.rate10Hz:
                    this.NormalUpdateRadioButton.Checked = true;
                    break;
                case AutoUpdateRefreshRates.rate60Hz:
                    this.FastUpdateRadioButton.Checked = true;
                    break;
            }
        }
        private void SetTrackerSize()
        {
            this.SmallSizeRadioButton.Checked = false;
            this.MediumSizeRadioButton.Checked = false;
            this.LargeSizeRadioButton.Checked = false;
            this.XLSizeRadioButton.Checked = false;

            switch (parent.TrackerSize)
            {
                case TrackerPixelSizes.Small:
                    this.SmallSizeRadioButton.Checked = true;
                    break;
                case TrackerPixelSizes.Medium:
                    this.MediumSizeRadioButton.Checked = true;
                    break;
                case TrackerPixelSizes.Large:
                    this.LargeSizeRadioButton.Checked = true;
                    break;
                case TrackerPixelSizes.XL:
                    this.XLSizeRadioButton.Checked = true;
                    break;
            }
        }
        private void SetOpacity()
        {
            OpacityTrackBar.Value = (int)(100 * parent.Opacity);
        }

        #region TrackingOptionsButtons

        private void TrackTeleportersCheckbox_Click(object sender, EventArgs e)
        {
            parent.TrackTeleporters = TrackTeleportersCheckbox.Checked;
        }
        private void TrackTreesCheckbox_Click(object sender, EventArgs e)
        {
            parent.TrackTrees = TrackTreesCheckbox.Checked;
        }
        private void TrackShardsCheckbox_Click(object sender, EventArgs e)
        {
            parent.TrackShards = TrackShardsCheckbox.Checked;
        }
        private void TrackRelicsCheckbox_Click(object sender, EventArgs e)
        {
            parent.TrackRelics = TrackRelicsCheckbox.Checked;
        }
        private void TrackMapstonesCheckbox_Click(object sender, EventArgs e)
        {
            parent.TrackMapstones = TrackMapstonesCheckbox.Checked;
        }

        #endregion

        #region DisplayOptionsButtons

        private void DisplayGreyTreesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyTrees = DisplayGreyTreesCheckbox.Checked;
        }
        private void DisplayGreyTeleportersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyTeleporters = DisplayGreyTeleportersCheckbox.Checked;
        }
        private void DisplayExistingRelicsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyRelics = DisplayExistingRelicsCheckbox.Checked;
        }
        private void DisplayGreyShardsCheckboxx_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyShards = DisplayGreyShardsCheckbox.Checked;
        }

        #endregion

        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            BackgroundColorDialog.Color = parent.BackColor;
            if (BackgroundColorDialog.ShowDialog() == DialogResult.OK)
            {
                parent.BackColor = BackgroundColorDialog.Color;
            }
        }

        #region RefreshRateRadioButtons

        private void SlowUpdateRadioButton_Click(object sender, EventArgs e)
        {
            parent.RefreshRate = AutoUpdateRefreshRates.rate500mHz;
        }
        private void ModerateUpdateRadioButton_Click(object sender, EventArgs e)
        {
            parent.RefreshRate = AutoUpdateRefreshRates.rate1Hz;
        }
        private void NormalUpdateRadioButton_Click(object sender, EventArgs e)
        {
            parent.RefreshRate = AutoUpdateRefreshRates.rate10Hz;
        }
        private void FastUpdateRadioButton_Click(object sender, EventArgs e)
        {
            parent.RefreshRate = AutoUpdateRefreshRates.rate60Hz;
        }

        #endregion

        #region ImageSizeRadioButtons

        private void SmallSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.UpdateTrackerSize(TrackerPixelSizes.Small);
        }
        private void MediumSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.UpdateTrackerSize(TrackerPixelSizes.Medium);
        }
        private void LargeSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.UpdateTrackerSize(TrackerPixelSizes.Large);
        }
        private void XLSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.UpdateTrackerSize(TrackerPixelSizes.XL);
        }

        #endregion

        #region Opacity

        private void OpacityTrackBar_Scroll(object sender, EventArgs e)
        {
            parent.Opacity = (double)(OpacityTrackBar.Value / (decimal)100.0);
        }

        #endregion

        #region Mapstone

        private void MapstoneFontColorButton_Click(object sender, EventArgs e)
        {
            MapstoneFontColorDialog.Color = parent.FontColor;
            if (MapstoneFontColorDialog.ShowDialog() == DialogResult.OK)
            {
                parent.FontColor = MapstoneFontColorDialog.Color;
                parent.Refresh();
            }            
        }
        private void MapstoneFontButton_Click(object sender, EventArgs e)
        {
            MapstoneFontDialog.Font = parent.MapFont;
            if (MapstoneFontDialog.ShowDialog() == DialogResult.OK)
            {
                parent.MapFont = new Font(MapstoneFontDialog.Font.FontFamily, 24f, FontStyle.Bold);
                parent.Refresh();
            }
        }

        #endregion

        private void SettingsLayout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.FormOwnerClosing))
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }
    }
}
