namespace OriDETracker
{
    partial class SettingsLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialogBackground = new System.Windows.Forms.ColorDialog();
            this.OpacityLabel = new System.Windows.Forms.Label();
            this.BackgroundColorButton = new System.Windows.Forms.Button();
            this.OpacityTrackBar = new System.Windows.Forms.TrackBar();
            this.ImageSizeLabel = new System.Windows.Forms.Label();
            this.TrackShardsCheckbox = new System.Windows.Forms.CheckBox();
            this.MediumSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.LargeSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.ImageSizePanel = new System.Windows.Forms.Panel();
            this.XLSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.SmallSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.MapstoneFontButton = new System.Windows.Forms.Button();
            this.colorDialogFont = new System.Windows.Forms.ColorDialog();
            this.UpdateRatePanel = new System.Windows.Forms.Panel();
            this.SlowUpdateRadioButton = new System.Windows.Forms.RadioButton();
            this.ModerateUpdateRadioButton = new System.Windows.Forms.RadioButton();
            this.FastUpdateRadioButton = new System.Windows.Forms.RadioButton();
            this.NormalUpdateRadioButton = new System.Windows.Forms.RadioButton();
            this.UpdateRateLabel = new System.Windows.Forms.Label();
            this.TrackTeleportersCheckbox = new System.Windows.Forms.CheckBox();
            this.TrackTreesCheckbox = new System.Windows.Forms.CheckBox();
            this.TrackRelicsCheckbox = new System.Windows.Forms.CheckBox();
            this.TrackingLabel = new System.Windows.Forms.Label();
            this.TrackMapstonesCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).BeginInit();
            this.ImageSizePanel.SuspendLayout();
            this.UpdateRatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpacityLabel
            // 
            this.OpacityLabel.AutoSize = true;
            this.OpacityLabel.Location = new System.Drawing.Point(122, 10);
            this.OpacityLabel.Name = "OpacityLabel";
            this.OpacityLabel.Size = new System.Drawing.Size(43, 13);
            this.OpacityLabel.TabIndex = 8;
            this.OpacityLabel.Text = "Opacity";
            // 
            // BackgroundColorButton
            // 
            this.BackgroundColorButton.Location = new System.Drawing.Point(237, 112);
            this.BackgroundColorButton.Name = "BackgroundColorButton";
            this.BackgroundColorButton.Size = new System.Drawing.Size(104, 23);
            this.BackgroundColorButton.TabIndex = 9;
            this.BackgroundColorButton.Text = "Background Color";
            this.BackgroundColorButton.UseVisualStyleBackColor = true;
            this.BackgroundColorButton.Click += new System.EventHandler(this.BackgroundColorButton_Click);
            // 
            // OpacityTrackBar
            // 
            this.OpacityTrackBar.Location = new System.Drawing.Point(187, 1);
            this.OpacityTrackBar.Maximum = 100;
            this.OpacityTrackBar.Name = "OpacityTrackBar";
            this.OpacityTrackBar.Size = new System.Drawing.Size(250, 45);
            this.OpacityTrackBar.TabIndex = 11;
            this.OpacityTrackBar.Value = 100;
            this.OpacityTrackBar.Scroll += new System.EventHandler(this.OpacityTrackBarScroll_Scroll);
            // 
            // ImageSizeLabel
            // 
            this.ImageSizeLabel.AutoSize = true;
            this.ImageSizeLabel.Location = new System.Drawing.Point(122, 85);
            this.ImageSizeLabel.Name = "ImageSizeLabel";
            this.ImageSizeLabel.Size = new System.Drawing.Size(59, 13);
            this.ImageSizeLabel.TabIndex = 13;
            this.ImageSizeLabel.Text = "Image Size";
            // 
            // TrackShardsCheckbox
            // 
            this.TrackShardsCheckbox.AutoSize = true;
            this.TrackShardsCheckbox.Location = new System.Drawing.Point(12, 52);
            this.TrackShardsCheckbox.Name = "TrackShardsCheckbox";
            this.TrackShardsCheckbox.Size = new System.Drawing.Size(59, 17);
            this.TrackShardsCheckbox.TabIndex = 14;
            this.TrackShardsCheckbox.Text = "Shards";
            this.TrackShardsCheckbox.UseVisualStyleBackColor = true;
            this.TrackShardsCheckbox.CheckedChanged += new System.EventHandler(this.TrackShardsCheckbox_CheckedChanged);
            // 
            // MediumSizeRadioButton
            // 
            this.MediumSizeRadioButton.AutoSize = true;
            this.MediumSizeRadioButton.Location = new System.Drawing.Point(66, 5);
            this.MediumSizeRadioButton.Name = "MediumSizeRadioButton";
            this.MediumSizeRadioButton.Size = new System.Drawing.Size(62, 17);
            this.MediumSizeRadioButton.TabIndex = 15;
            this.MediumSizeRadioButton.Text = "Medium";
            this.MediumSizeRadioButton.UseVisualStyleBackColor = true;
            this.MediumSizeRadioButton.CheckedChanged += new System.EventHandler(this.MediumSizeRadioButton_CheckedChanged);
            // 
            // LargeSizeRadioButton
            // 
            this.LargeSizeRadioButton.AutoSize = true;
            this.LargeSizeRadioButton.Checked = true;
            this.LargeSizeRadioButton.Location = new System.Drawing.Point(129, 5);
            this.LargeSizeRadioButton.Name = "LargeSizeRadioButton";
            this.LargeSizeRadioButton.Size = new System.Drawing.Size(52, 17);
            this.LargeSizeRadioButton.TabIndex = 16;
            this.LargeSizeRadioButton.TabStop = true;
            this.LargeSizeRadioButton.Text = "Large";
            this.LargeSizeRadioButton.UseVisualStyleBackColor = true;
            this.LargeSizeRadioButton.CheckedChanged += new System.EventHandler(this.LargeSizeRadioButton_CheckedChanged);
            // 
            // ImageSizePanel
            // 
            this.ImageSizePanel.Controls.Add(this.XLSizeRadioButton);
            this.ImageSizePanel.Controls.Add(this.SmallSizeRadioButton);
            this.ImageSizePanel.Controls.Add(this.LargeSizeRadioButton);
            this.ImageSizePanel.Controls.Add(this.MediumSizeRadioButton);
            this.ImageSizePanel.Location = new System.Drawing.Point(187, 81);
            this.ImageSizePanel.Name = "ImageSizePanel";
            this.ImageSizePanel.Size = new System.Drawing.Size(250, 25);
            this.ImageSizePanel.TabIndex = 17;
            // 
            // XLSizeRadioButton
            // 
            this.XLSizeRadioButton.AutoSize = true;
            this.XLSizeRadioButton.Location = new System.Drawing.Point(192, 5);
            this.XLSizeRadioButton.Name = "XLSizeRadioButton";
            this.XLSizeRadioButton.Size = new System.Drawing.Size(38, 17);
            this.XLSizeRadioButton.TabIndex = 18;
            this.XLSizeRadioButton.Text = "XL";
            this.XLSizeRadioButton.UseVisualStyleBackColor = true;
            this.XLSizeRadioButton.CheckedChanged += new System.EventHandler(this.XLSizeRadioButton_CheckedChanged);
            // 
            // SmallSizeRadioButton
            // 
            this.SmallSizeRadioButton.AutoSize = true;
            this.SmallSizeRadioButton.Location = new System.Drawing.Point(3, 5);
            this.SmallSizeRadioButton.Name = "SmallSizeRadioButton";
            this.SmallSizeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.SmallSizeRadioButton.TabIndex = 17;
            this.SmallSizeRadioButton.Text = "Small";
            this.SmallSizeRadioButton.CheckedChanged += new System.EventHandler(this.SmallSizeRadioButton_CheckedChanged);
            // 
            // MapstoneFontButton
            // 
            this.MapstoneFontButton.Location = new System.Drawing.Point(109, 112);
            this.MapstoneFontButton.Name = "MapstoneFontButton";
            this.MapstoneFontButton.Size = new System.Drawing.Size(122, 23);
            this.MapstoneFontButton.TabIndex = 19;
            this.MapstoneFontButton.Text = "Mapstone Font Color";
            this.MapstoneFontButton.UseVisualStyleBackColor = true;
            this.MapstoneFontButton.Click += new System.EventHandler(this.MapstoneFontButton_Click);
            // 
            // UpdateRatePanel
            // 
            this.UpdateRatePanel.Controls.Add(this.SlowUpdateRadioButton);
            this.UpdateRatePanel.Controls.Add(this.ModerateUpdateRadioButton);
            this.UpdateRatePanel.Controls.Add(this.FastUpdateRadioButton);
            this.UpdateRatePanel.Controls.Add(this.NormalUpdateRadioButton);
            this.UpdateRatePanel.Location = new System.Drawing.Point(187, 38);
            this.UpdateRatePanel.Name = "UpdateRatePanel";
            this.UpdateRatePanel.Size = new System.Drawing.Size(250, 25);
            this.UpdateRatePanel.TabIndex = 21;
            // 
            // SlowUpdateRadioButton
            // 
            this.SlowUpdateRadioButton.AutoSize = true;
            this.SlowUpdateRadioButton.Location = new System.Drawing.Point(7, 4);
            this.SlowUpdateRadioButton.Name = "SlowUpdateRadioButton";
            this.SlowUpdateRadioButton.Size = new System.Drawing.Size(67, 17);
            this.SlowUpdateRadioButton.TabIndex = 18;
            this.SlowUpdateRadioButton.Text = "500 mHz";
            this.SlowUpdateRadioButton.UseVisualStyleBackColor = true;
            this.SlowUpdateRadioButton.CheckedChanged += new System.EventHandler(this.SlowUpdateRadioButton_CheckedChanged);
            // 
            // ModerateUpdateRadioButton
            // 
            this.ModerateUpdateRadioButton.AutoSize = true;
            this.ModerateUpdateRadioButton.Location = new System.Drawing.Point(80, 5);
            this.ModerateUpdateRadioButton.Name = "ModerateUpdateRadioButton";
            this.ModerateUpdateRadioButton.Size = new System.Drawing.Size(47, 17);
            this.ModerateUpdateRadioButton.TabIndex = 17;
            this.ModerateUpdateRadioButton.Text = "1 Hz";
            this.ModerateUpdateRadioButton.CheckedChanged += new System.EventHandler(this.ModerateUpdateRadioButton_CheckedChanged);
            // 
            // FastUpdateRadioButton
            // 
            this.FastUpdateRadioButton.AutoSize = true;
            this.FastUpdateRadioButton.Checked = true;
            this.FastUpdateRadioButton.Location = new System.Drawing.Point(192, 5);
            this.FastUpdateRadioButton.Name = "FastUpdateRadioButton";
            this.FastUpdateRadioButton.Size = new System.Drawing.Size(53, 17);
            this.FastUpdateRadioButton.TabIndex = 16;
            this.FastUpdateRadioButton.TabStop = true;
            this.FastUpdateRadioButton.Text = "60 Hz";
            this.FastUpdateRadioButton.UseVisualStyleBackColor = true;
            this.FastUpdateRadioButton.CheckedChanged += new System.EventHandler(this.FastUpdateRadioButton_CheckedChanged);
            // 
            // NormalUpdateRadioButton
            // 
            this.NormalUpdateRadioButton.AutoSize = true;
            this.NormalUpdateRadioButton.Location = new System.Drawing.Point(133, 5);
            this.NormalUpdateRadioButton.Name = "NormalUpdateRadioButton";
            this.NormalUpdateRadioButton.Size = new System.Drawing.Size(53, 17);
            this.NormalUpdateRadioButton.TabIndex = 15;
            this.NormalUpdateRadioButton.Text = "10 Hz";
            this.NormalUpdateRadioButton.UseVisualStyleBackColor = true;
            this.NormalUpdateRadioButton.CheckedChanged += new System.EventHandler(this.NormalUpdateRadioButton_CheckedChanged);
            // 
            // UpdateRateLabel
            // 
            this.UpdateRateLabel.AutoSize = true;
            this.UpdateRateLabel.Location = new System.Drawing.Point(116, 44);
            this.UpdateRateLabel.Name = "UpdateRateLabel";
            this.UpdateRateLabel.Size = new System.Drawing.Size(68, 13);
            this.UpdateRateLabel.TabIndex = 20;
            this.UpdateRateLabel.Text = "Update Rate";
            // 
            // TrackTeleportersCheckbox
            // 
            this.TrackTeleportersCheckbox.AutoSize = true;
            this.TrackTeleportersCheckbox.Location = new System.Drawing.Point(12, 75);
            this.TrackTeleportersCheckbox.Name = "TrackTeleportersCheckbox";
            this.TrackTeleportersCheckbox.Size = new System.Drawing.Size(79, 17);
            this.TrackTeleportersCheckbox.TabIndex = 22;
            this.TrackTeleportersCheckbox.Text = "Teleporters";
            this.TrackTeleportersCheckbox.UseVisualStyleBackColor = true;
            this.TrackTeleportersCheckbox.CheckedChanged += new System.EventHandler(this.TrackTeleportersCheckbox_CheckedChanged);
            // 
            // TrackTreesCheckbox
            // 
            this.TrackTreesCheckbox.AutoSize = true;
            this.TrackTreesCheckbox.Location = new System.Drawing.Point(12, 29);
            this.TrackTreesCheckbox.Name = "TrackTreesCheckbox";
            this.TrackTreesCheckbox.Size = new System.Drawing.Size(53, 17);
            this.TrackTreesCheckbox.TabIndex = 23;
            this.TrackTreesCheckbox.Text = "Trees";
            this.TrackTreesCheckbox.UseVisualStyleBackColor = true;
            this.TrackTreesCheckbox.CheckedChanged += new System.EventHandler(this.TrackTreesCheckbox_CheckedChanged);
            // 
            // TrackRelicsCheckbox
            // 
            this.TrackRelicsCheckbox.AutoSize = true;
            this.TrackRelicsCheckbox.Location = new System.Drawing.Point(12, 98);
            this.TrackRelicsCheckbox.Name = "TrackRelicsCheckbox";
            this.TrackRelicsCheckbox.Size = new System.Drawing.Size(55, 17);
            this.TrackRelicsCheckbox.TabIndex = 25;
            this.TrackRelicsCheckbox.Text = "Relics";
            this.TrackRelicsCheckbox.UseVisualStyleBackColor = true;
            this.TrackRelicsCheckbox.CheckedChanged += new System.EventHandler(this.TrackRelicsCheckbox_CheckedChanged);
            // 
            // TrackingLabel
            // 
            this.TrackingLabel.AutoSize = true;
            this.TrackingLabel.Location = new System.Drawing.Point(12, 10);
            this.TrackingLabel.Name = "TrackingLabel";
            this.TrackingLabel.Size = new System.Drawing.Size(88, 13);
            this.TrackingLabel.TabIndex = 26;
            this.TrackingLabel.Text = "Tracking Options";
            // 
            // TrackMapstonesCheckbox
            // 
            this.TrackMapstonesCheckbox.AutoSize = true;
            this.TrackMapstonesCheckbox.Location = new System.Drawing.Point(12, 121);
            this.TrackMapstonesCheckbox.Name = "TrackMapstonesCheckbox";
            this.TrackMapstonesCheckbox.Size = new System.Drawing.Size(78, 17);
            this.TrackMapstonesCheckbox.TabIndex = 27;
            this.TrackMapstonesCheckbox.Text = "Mapstones";
            this.TrackMapstonesCheckbox.UseVisualStyleBackColor = true;
            this.TrackMapstonesCheckbox.CheckedChanged += new System.EventHandler(this.TrackMapstonesCheckbox_CheckedChanged);
            // 
            // SettingsLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 145);
            this.Controls.Add(this.TrackMapstonesCheckbox);
            this.Controls.Add(this.TrackingLabel);
            this.Controls.Add(this.TrackRelicsCheckbox);
            this.Controls.Add(this.TrackTreesCheckbox);
            this.Controls.Add(this.TrackTeleportersCheckbox);
            this.Controls.Add(this.UpdateRatePanel);
            this.Controls.Add(this.UpdateRateLabel);
            this.Controls.Add(this.MapstoneFontButton);
            this.Controls.Add(this.ImageSizePanel);
            this.Controls.Add(this.TrackShardsCheckbox);
            this.Controls.Add(this.ImageSizeLabel);
            this.Controls.Add(this.OpacityTrackBar);
            this.Controls.Add(this.BackgroundColorButton);
            this.Controls.Add(this.OpacityLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 184);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 184);
            this.Name = "SettingsLayout";
            this.Text = "Tracker Layout";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsLayout_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).EndInit();
            this.ImageSizePanel.ResumeLayout(false);
            this.ImageSizePanel.PerformLayout();
            this.UpdateRatePanel.ResumeLayout(false);
            this.UpdateRatePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialogBackground;
        private System.Windows.Forms.Label OpacityLabel;
        private System.Windows.Forms.Button BackgroundColorButton;
        private System.Windows.Forms.TrackBar OpacityTrackBar;
        private System.Windows.Forms.Label ImageSizeLabel;
        private System.Windows.Forms.CheckBox TrackShardsCheckbox;
        private System.Windows.Forms.RadioButton MediumSizeRadioButton;
        private System.Windows.Forms.RadioButton LargeSizeRadioButton;
        private System.Windows.Forms.Panel ImageSizePanel;
        private System.Windows.Forms.Button MapstoneFontButton;
        private System.Windows.Forms.ColorDialog colorDialogFont;
        private System.Windows.Forms.RadioButton SmallSizeRadioButton;
        private System.Windows.Forms.RadioButton XLSizeRadioButton;
        private System.Windows.Forms.Panel UpdateRatePanel;
        private System.Windows.Forms.RadioButton SlowUpdateRadioButton;
        private System.Windows.Forms.RadioButton ModerateUpdateRadioButton;
        private System.Windows.Forms.RadioButton FastUpdateRadioButton;
        private System.Windows.Forms.RadioButton NormalUpdateRadioButton;
        private System.Windows.Forms.Label UpdateRateLabel;
        private System.Windows.Forms.CheckBox TrackTeleportersCheckbox;
        private System.Windows.Forms.CheckBox TrackTreesCheckbox;
        private System.Windows.Forms.CheckBox TrackRelicsCheckbox;
        private System.Windows.Forms.Label TrackingLabel;
        private System.Windows.Forms.CheckBox TrackMapstonesCheckbox;
    }
}