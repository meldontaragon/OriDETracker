using System;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class SettingsLayout : Form
    {
        private readonly Tracker parent;
        public SettingsLayout(Tracker par)
        {
            InitializeComponent();

            parent = par;

            OpacityTrackBar.Value = (int)(100 * par.Opacity);

            SetRefreshRate();
            SetTrackerSize();
            SetTrackingOptions();

            this.Text = "Tracker Layer v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Refresh();
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
                    this.NormalUpdateRadioButton.Checked = true;
                    break;
                case AutoUpdateRefreshRates.rate10Hz:
                    this.ModerateUpdateRadioButton.Checked = true;
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
        private void SetTrackingOptions()
        {
            this.TrackShardsCheckbox.Checked = parent.TrackShards;
            this.TrackTeleportersCheckbox.Checked = parent.TrackTeleporters;
            this.TrackTreesCheckbox.Checked = parent.TrackTrees;
            this.TrackRelicsCheckbox.Checked = parent.TrackRelics;
            this.TrackMapstonesCheckbox.Checked = parent.TrackMapstones;
        }

        public void Reset()
        {
            RefreshOpacityBar();
            SetRefreshRate();
            SetTrackerSize();
            SetTrackingOptions();
        }

        private void SettingsLayout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.FormOwnerClosing))
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        #region ColorButtons
        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            if (BackgroundColorDialog.ShowDialog() == DialogResult.OK)
            {
                parent.BackColor = BackgroundColorDialog.Color;
            }
            parent.Refresh();
        }
        private void MapstoneFontColorButton_Click(object sender, EventArgs e)
        {
            if (MapstoneFontColorDialog.ShowDialog() == DialogResult.OK)
            {
                parent.FontColor = MapstoneFontColorDialog.Color;
            }
            parent.Refresh();
        }
        #endregion

        #region Opacity
        private void OpacityTrackBarScroll_Scroll(object sender, EventArgs e)
        {
            parent.Opacity = (double)(OpacityTrackBar.Value / (decimal)100.0);
            parent.Refresh();

            int tmp = OpacityTrackBar.Value;
            OpacityTrackBar.Value = tmp;
        }
        public void RefreshOpacityBar()
        {
            OpacityTrackBar.Value = (int)(100 * parent.Opacity);
        }
        #endregion

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
        internal void ChangeShards()
        {
            TrackShardsCheckbox.Checked = parent.TrackShards;
        }
        internal void ChangeTrees()
        {
            TrackShardsCheckbox.Checked = parent.TrackShards;
        }
        internal void ChangeRelics()
        {
            TrackShardsCheckbox.Checked = parent.TrackShards;
        }
        internal void ChangeTeleporters()
        {
            TrackShardsCheckbox.Checked = parent.TrackShards;
        }
        internal void ChangeMapstones()
        {
            TrackMapstonesCheckbox.Checked = parent.TrackMapstones;
        }
        #endregion

        #region ImageSizeRadioButtons
        private void SmallSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.Small;
            SetTrackerSize();
            parent.UpdateImages();
            parent.Refresh();
        }
        private void MediumSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.Medium;
            SetTrackerSize();
            parent.UpdateImages();
            parent.Refresh();
        }
        private void LargeSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.Large;
            SetTrackerSize();
            parent.UpdateImages();
            parent.Refresh();
        }
        private void XLSizeRadioButton_Click(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.XL;
            SetTrackerSize();
            parent.UpdateImages();
            parent.Refresh();
        }
        #endregion

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

        private void DisplayGreyTreesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyTrees = DisplayGreyTreesCheckbox.Checked;
        }

        private void DisplayInactiveTeleportersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyTeleporters = DisplayInactiveTeleportersCheckbox.Checked;
        }

        private void DisplayExistingRelicsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.DisplayEmptyRelics = DisplayExistingRelicsCheckbox.Checked;
        }

        private void MapstoneFontButton_Click(object sender, EventArgs e)
        {
            if (MapstoneFontDialog.ShowDialog() == DialogResult.OK)
            {
                parent.MapFont = MapstoneFontDialog.Font;
            }
            parent.Refresh();
        }
    }
}
