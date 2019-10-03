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

            if (parent.TrackerSize == TrackerPixelSizes.size420px)
            {
                this.MediumSizeRadioButton.Checked = true;
                this.LargeSizeRadioButton.Checked = false;
                this.SmallSizeRadioButton.Checked = false;
                this.XLSizeRadioButton.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size640px)
            {
                this.MediumSizeRadioButton.Checked = false;
                this.LargeSizeRadioButton.Checked = true;
                this.SmallSizeRadioButton.Checked = false;
                this.XLSizeRadioButton.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size300px)
            {
                this.MediumSizeRadioButton.Checked = false;
                this.LargeSizeRadioButton.Checked = false;
                this.SmallSizeRadioButton.Checked = true;
                this.XLSizeRadioButton.Checked = false;
            }
            else if (parent.TrackerSize == TrackerPixelSizes.size720px)
            {
                this.MediumSizeRadioButton.Checked = false;
                this.LargeSizeRadioButton.Checked = false;
                this.SmallSizeRadioButton.Checked = false;
                this.XLSizeRadioButton.Checked = true;
            }
            else
            {
                parent.TrackerSize = TrackerPixelSizes.size640px;

                this.MediumSizeRadioButton.Checked = false;
                this.LargeSizeRadioButton.Checked = true;
                this.SmallSizeRadioButton.Checked = false;
                this.XLSizeRadioButton.Checked = false;
            }

            if (parent.RefreshRate == (AutoUpdateRefreshRates)500)
            {
                this.SlowUpdateRadioButton.Checked = true;
                this.NormalUpdateRadioButton.Checked = false;
                this.ModerateUpdateRadioButton.Checked = false;
                this.FastUpdateRadioButton.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)1000)
            {
                this.SlowUpdateRadioButton.Checked = false;
                this.NormalUpdateRadioButton.Checked = false;
                this.ModerateUpdateRadioButton.Checked = true;
                this.FastUpdateRadioButton.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)10000)
            {
                this.SlowUpdateRadioButton.Checked = false;
                this.NormalUpdateRadioButton.Checked = true;
                this.ModerateUpdateRadioButton.Checked = false;
                this.FastUpdateRadioButton.Checked = false;
            }
            else if (parent.RefreshRate == (AutoUpdateRefreshRates)60000)
            {
                this.SlowUpdateRadioButton.Checked = false;
                this.NormalUpdateRadioButton.Checked = false;
                this.ModerateUpdateRadioButton.Checked = false;
                this.FastUpdateRadioButton.Checked = true;
            }
            else
            {
                parent.RefreshRate = (AutoUpdateRefreshRates)10000;

                this.SlowUpdateRadioButton.Checked = false;
                this.NormalUpdateRadioButton.Checked = true;
                this.ModerateUpdateRadioButton.Checked = false;
                this.FastUpdateRadioButton.Checked = false;
            }

            this.TrackShardsCheckbox.Checked = parent.TrackShards;
            this.TrackTeleportersCheckbox.Checked = parent.TrackTeleporters;
            this.TrackTreesCheckbox.Checked = parent.TrackTrees;
            this.TrackRelicsCheckbox.Checked = parent.TrackRelics;

            this.Text = "Tracker Layer v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Refresh();
        }

        public void Reset()
        {
            OpacityTrackBar.Value = 100;
            SmallSizeRadioButton.Checked = false;
            MediumSizeRadioButton.Checked = false;
            LargeSizeRadioButton.Checked = true;
            XLSizeRadioButton.Checked = false;
            TrackShardsCheckbox.Checked = false;
            TrackTeleportersCheckbox.Checked = false;
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
            if (colorDialogBackground.ShowDialog() == DialogResult.OK)
            {
                parent.BackColor = colorDialogBackground.Color;
            }
            parent.Refresh();
        }
        private void MapstoneFontButton_Click(object sender, EventArgs e)
        {
            if (colorDialogFont.ShowDialog() == DialogResult.OK)
            {
                parent.FontColor = colorDialogFont.Color;
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
        private void TrackShardsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackShards = TrackShardsCheckbox.Checked;
        }
        private void TrackTeleportersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackTeleporters = TrackTeleportersCheckbox.Checked;
        }
        private void TrackTreesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackTrees = TrackTreesCheckbox.Checked;
        }
        private void TrackMapstonesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackMapstones = TrackMapstonesCheckbox.Checked;
        }
        private void TrackRelicsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackRelics = TrackRelicsCheckbox.Checked;
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
        private void XLSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size720px;
            parent.UpdateImages();
            parent.Refresh();
        }
        private void LargeSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size640px;
            parent.UpdateImages();
            parent.Refresh();
        }
        private void MediumSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size420px;
            parent.UpdateImages();
            parent.Refresh();
        }
        private void SmallSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.TrackerSize = TrackerPixelSizes.size300px;
            parent.UpdateImages();
            parent.Refresh();
        }
        #endregion

        #region RefreshRateRadioButtons
        private void ModerateUpdateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)1000;
        }
        private void NormalUpdateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)10000;
        }
        private void FastUpdateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)60000;
        }
        private void SlowUpdateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            parent.RefreshRate = (AutoUpdateRefreshRates)500;
        }
        #endregion


    }
}
