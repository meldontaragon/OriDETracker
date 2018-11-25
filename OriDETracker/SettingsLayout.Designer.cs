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
            this.rbRandoTrees = new System.Windows.Forms.RadioButton();
            this.rbRandoEvents = new System.Windows.Forms.RadioButton();
            this.rbOriAllSkills = new System.Windows.Forms.RadioButton();
            this.rbOriAllCells = new System.Windows.Forms.RadioButton();
            this.rbReverseEventOrder = new System.Windows.Forms.RadioButton();
            this.labelOpacity = new System.Windows.Forms.Label();
            this.buttonBackgroundColor = new System.Windows.Forms.Button();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_shards = new System.Windows.Forms.CheckBox();
            this.rb_420 = new System.Windows.Forms.RadioButton();
            this.rb_640 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_720 = new System.Windows.Forms.RadioButton();
            this.rb_300 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_mapstone_font = new System.Windows.Forms.Button();
            this.colorDialogFont = new System.Windows.Forms.ColorDialog();
            this.numericUpDownOpacity = new OriDETracker.PercentNumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rb_500_mHz = new System.Windows.Forms.RadioButton();
            this.rb_1_hz = new System.Windows.Forms.RadioButton();
            this.rb_60_hz = new System.Windows.Forms.RadioButton();
            this.rb_10_hz = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_teleporters = new System.Windows.Forms.CheckBox();
            this.cb_trees = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbRandoTrees
            // 
            this.rbRandoTrees.AutoSize = true;
            this.rbRandoTrees.Location = new System.Drawing.Point(5, 5);
            this.rbRandoTrees.Name = "rbRandoTrees";
            this.rbRandoTrees.Size = new System.Drawing.Size(125, 17);
            this.rbRandoTrees.TabIndex = 0;
            this.rbRandoTrees.TabStop = true;
            this.rbRandoTrees.Text = "Randomizer All Trees";
            this.rbRandoTrees.UseVisualStyleBackColor = true;
            this.rbRandoTrees.CheckedChanged += new System.EventHandler(this.rbRandoTrees_CheckedChanged);
            // 
            // rbRandoEvents
            // 
            this.rbRandoEvents.AutoSize = true;
            this.rbRandoEvents.Location = new System.Drawing.Point(5, 28);
            this.rbRandoEvents.Name = "rbRandoEvents";
            this.rbRandoEvents.Size = new System.Drawing.Size(131, 17);
            this.rbRandoEvents.TabIndex = 1;
            this.rbRandoEvents.TabStop = true;
            this.rbRandoEvents.Text = "Randomizer All Events";
            this.rbRandoEvents.UseVisualStyleBackColor = true;
            this.rbRandoEvents.CheckedChanged += new System.EventHandler(this.rbRandoEvents_CheckedChanged);
            // 
            // rbOriAllSkills
            // 
            this.rbOriAllSkills.AutoSize = true;
            this.rbOriAllSkills.Location = new System.Drawing.Point(5, 51);
            this.rbOriAllSkills.Name = "rbOriAllSkills";
            this.rbOriAllSkills.Size = new System.Drawing.Size(63, 17);
            this.rbOriAllSkills.TabIndex = 2;
            this.rbOriAllSkills.TabStop = true;
            this.rbOriAllSkills.Text = "All Skills";
            this.rbOriAllSkills.UseVisualStyleBackColor = true;
            this.rbOriAllSkills.CheckedChanged += new System.EventHandler(this.rbOriAllSkills_CheckedChanged);
            // 
            // rbOriAllCells
            // 
            this.rbOriAllCells.AutoSize = true;
            this.rbOriAllCells.Location = new System.Drawing.Point(5, 74);
            this.rbOriAllCells.Name = "rbOriAllCells";
            this.rbOriAllCells.Size = new System.Drawing.Size(61, 17);
            this.rbOriAllCells.TabIndex = 3;
            this.rbOriAllCells.TabStop = true;
            this.rbOriAllCells.Text = "All Cells";
            this.rbOriAllCells.UseVisualStyleBackColor = true;
            this.rbOriAllCells.CheckedChanged += new System.EventHandler(this.rbOriAllCells_CheckedChanged);
            // 
            // rbReverseEventOrder
            // 
            this.rbReverseEventOrder.AutoSize = true;
            this.rbReverseEventOrder.Location = new System.Drawing.Point(5, 97);
            this.rbReverseEventOrder.Name = "rbReverseEventOrder";
            this.rbReverseEventOrder.Size = new System.Drawing.Size(125, 17);
            this.rbReverseEventOrder.TabIndex = 4;
            this.rbReverseEventOrder.TabStop = true;
            this.rbReverseEventOrder.Text = "Reverse Event Order";
            this.rbReverseEventOrder.UseVisualStyleBackColor = true;
            this.rbReverseEventOrder.CheckedChanged += new System.EventHandler(this.rbReverseEventOrder_CheckedChanged);
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Location = new System.Drawing.Point(152, 10);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(43, 13);
            this.labelOpacity.TabIndex = 8;
            this.labelOpacity.Text = "Opacity";
            // 
            // buttonBackgroundColor
            // 
            this.buttonBackgroundColor.Location = new System.Drawing.Point(364, 124);
            this.buttonBackgroundColor.Name = "buttonBackgroundColor";
            this.buttonBackgroundColor.Size = new System.Drawing.Size(104, 23);
            this.buttonBackgroundColor.TabIndex = 9;
            this.buttonBackgroundColor.Text = "Background Color";
            this.buttonBackgroundColor.UseVisualStyleBackColor = true;
            this.buttonBackgroundColor.Click += new System.EventHandler(this.buttonBackgroundColor_Click);
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Location = new System.Drawing.Point(267, 1);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(200, 45);
            this.trackBarOpacity.TabIndex = 11;
            this.trackBarOpacity.Value = 100;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Image Size";
            // 
            // cb_shards
            // 
            this.cb_shards.Checked = TrackerSettings.Default.Shards;
            this.cb_shards.AutoSize = true;
            this.cb_shards.Location = new System.Drawing.Point(6, 128);
            this.cb_shards.Name = "cb_shards";
            this.cb_shards.Size = new System.Drawing.Size(59, 17);
            this.cb_shards.TabIndex = 14;
            this.cb_shards.Text = "Shards";
            this.cb_shards.UseVisualStyleBackColor = true;
            this.cb_shards.CheckedChanged += new System.EventHandler(this.cb_shards_CheckedChanged);
            // 
            // rb_420
            // 
            this.rb_420.AutoSize = true;
            this.rb_420.Location = new System.Drawing.Point(66, 5);
            this.rb_420.Name = "rb_420";
            this.rb_420.Size = new System.Drawing.Size(62, 17);
            this.rb_420.TabIndex = 15;
            this.rb_420.Text = "Medium";
            this.rb_420.UseVisualStyleBackColor = true;
            this.rb_420.CheckedChanged += new System.EventHandler(this.rb_400_CheckedChanged);
            // 
            // rb_640
            // 
            this.rb_640.AutoSize = true;
            this.rb_640.Checked = true;
            this.rb_640.Location = new System.Drawing.Point(129, 5);
            this.rb_640.Name = "rb_640";
            this.rb_640.Size = new System.Drawing.Size(52, 17);
            this.rb_640.TabIndex = 16;
            this.rb_640.TabStop = true;
            this.rb_640.Text = "Large";
            this.rb_640.UseVisualStyleBackColor = true;
            this.rb_640.CheckedChanged += new System.EventHandler(this.rb_600_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_720);
            this.panel1.Controls.Add(this.rb_300);
            this.panel1.Controls.Add(this.rb_640);
            this.panel1.Controls.Add(this.rb_420);
            this.panel1.Location = new System.Drawing.Point(218, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 25);
            this.panel1.TabIndex = 17;
            // 
            // rb_720
            // 
            this.rb_720.AutoSize = true;
            this.rb_720.Location = new System.Drawing.Point(192, 5);
            this.rb_720.Name = "rb_720";
            this.rb_720.Size = new System.Drawing.Size(38, 17);
            this.rb_720.TabIndex = 18;
            this.rb_720.Text = "XL";
            this.rb_720.UseVisualStyleBackColor = true;
            this.rb_720.CheckedChanged += new System.EventHandler(this.rb_720_CheckedChanged);
            // 
            // rb_300
            // 
            this.rb_300.AutoSize = true;
            this.rb_300.Location = new System.Drawing.Point(3, 5);
            this.rb_300.Name = "rb_300";
            this.rb_300.Size = new System.Drawing.Size(50, 17);
            this.rb_300.TabIndex = 17;
            this.rb_300.Text = "Small";
            this.rb_300.CheckedChanged += new System.EventHandler(this.rb_300_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbReverseEventOrder);
            this.panel2.Controls.Add(this.rbOriAllCells);
            this.panel2.Controls.Add(this.rbOriAllSkills);
            this.panel2.Controls.Add(this.rbRandoTrees);
            this.panel2.Controls.Add(this.rbRandoEvents);
            this.panel2.Location = new System.Drawing.Point(6, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 123);
            this.panel2.TabIndex = 18;
            // 
            // button_mapstone_font
            // 
            this.button_mapstone_font.Location = new System.Drawing.Point(218, 124);
            this.button_mapstone_font.Name = "button_mapstone_font";
            this.button_mapstone_font.Size = new System.Drawing.Size(122, 23);
            this.button_mapstone_font.TabIndex = 19;
            this.button_mapstone_font.Text = "Mapstone Font Color";
            this.button_mapstone_font.UseVisualStyleBackColor = true;
            this.button_mapstone_font.Click += new System.EventHandler(this.button_mapstone_font_Click);
            // 
            // numericUpDownOpacity
            // 
            this.numericUpDownOpacity.Location = new System.Drawing.Point(202, 8);
            this.numericUpDownOpacity.Name = "numericUpDownOpacity";
            this.numericUpDownOpacity.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownOpacity.TabIndex = 7;
            this.numericUpDownOpacity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownOpacity.ValueChanged += new System.EventHandler(this.percentNumericUpDown_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rb_500_mHz);
            this.panel3.Controls.Add(this.rb_1_hz);
            this.panel3.Controls.Add(this.rb_60_hz);
            this.panel3.Controls.Add(this.rb_10_hz);
            this.panel3.Location = new System.Drawing.Point(218, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 25);
            this.panel3.TabIndex = 21;
            // 
            // rb_500_mHz
            // 
            this.rb_500_mHz.AutoSize = true;
            this.rb_500_mHz.Location = new System.Drawing.Point(7, 4);
            this.rb_500_mHz.Name = "rb_500_mHz";
            this.rb_500_mHz.Size = new System.Drawing.Size(67, 17);
            this.rb_500_mHz.TabIndex = 18;
            this.rb_500_mHz.Text = "500 mHz";
            this.rb_500_mHz.UseVisualStyleBackColor = true;
            this.rb_500_mHz.CheckedChanged += new System.EventHandler(this.rb_500_mHz_CheckedChanged);
            // 
            // rb_1_hz
            // 
            this.rb_1_hz.AutoSize = true;
            this.rb_1_hz.Location = new System.Drawing.Point(80, 5);
            this.rb_1_hz.Name = "rb_1_hz";
            this.rb_1_hz.Size = new System.Drawing.Size(47, 17);
            this.rb_1_hz.TabIndex = 17;
            this.rb_1_hz.Text = "1 Hz";
            this.rb_1_hz.CheckedChanged += new System.EventHandler(this.rb_1_hz_CheckedChanged);
            // 
            // rb_60_hz
            // 
            this.rb_60_hz.AutoSize = true;
            this.rb_60_hz.Checked = true;
            this.rb_60_hz.Location = new System.Drawing.Point(192, 5);
            this.rb_60_hz.Name = "rb_60_hz";
            this.rb_60_hz.Size = new System.Drawing.Size(53, 17);
            this.rb_60_hz.TabIndex = 16;
            this.rb_60_hz.TabStop = true;
            this.rb_60_hz.Text = "60 Hz";
            this.rb_60_hz.UseVisualStyleBackColor = true;
            this.rb_60_hz.CheckedChanged += new System.EventHandler(this.rb_60_hz_CheckedChanged);
            // 
            // rb_10_hz
            // 
            this.rb_10_hz.AutoSize = true;
            this.rb_10_hz.Location = new System.Drawing.Point(133, 5);
            this.rb_10_hz.Name = "rb_10_hz";
            this.rb_10_hz.Size = new System.Drawing.Size(53, 17);
            this.rb_10_hz.TabIndex = 15;
            this.rb_10_hz.Text = "10 Hz";
            this.rb_10_hz.UseVisualStyleBackColor = true;
            this.rb_10_hz.CheckedChanged += new System.EventHandler(this.rb_10_hz_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Update Rate";
            // 
            // cb_teleporters
            // 
            this.cb_teleporters.AutoSize = true;
            this.cb_teleporters.Checked = TrackerSettings.Default.Teleporters;
            this.cb_teleporters.Location = new System.Drawing.Point(67, 128);
            this.cb_teleporters.Name = "cb_teleporters";
            this.cb_teleporters.Size = new System.Drawing.Size(79, 17);
            this.cb_teleporters.TabIndex = 22;
            this.cb_teleporters.Text = "Teleporters";
            this.cb_teleporters.UseVisualStyleBackColor = true;
            this.cb_teleporters.CheckedChanged += new System.EventHandler(this.cb_teleporters_CheckedChanged);
            // 
            // cb_trees
            // 
            this.cb_trees.AutoSize = true;
            this.cb_trees.Checked = TrackerSettings.Default.Trees;
            this.cb_trees.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_trees.Location = new System.Drawing.Point(150, 128);
            this.cb_trees.Name = "cb_trees";
            this.cb_trees.Size = new System.Drawing.Size(53, 17);
            this.cb_trees.TabIndex = 23;
            this.cb_trees.Text = "Trees";
            this.cb_trees.UseVisualStyleBackColor = true;
            this.cb_trees.CheckedChanged += new System.EventHandler(this.cb_trees_CheckedChanged);
            // 
            // SettingsLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 158);
            this.Controls.Add(this.cb_trees);
            this.Controls.Add(this.cb_teleporters);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_mapstone_font);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cb_shards);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarOpacity);
            this.Controls.Add(this.buttonBackgroundColor);
            this.Controls.Add(this.labelOpacity);
            this.Controls.Add(this.numericUpDownOpacity);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(494, 197);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(494, 197);
            this.Name = "SettingsLayout";
            this.Text = "Tracker Layout";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsLayout_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpacity)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialogBackground;
        private System.Windows.Forms.RadioButton rbRandoTrees;
        private System.Windows.Forms.RadioButton rbRandoEvents;
        private System.Windows.Forms.RadioButton rbOriAllSkills;
        private System.Windows.Forms.RadioButton rbOriAllCells;
        private System.Windows.Forms.RadioButton rbReverseEventOrder;
        private PercentNumericUpDown numericUpDownOpacity;
        private System.Windows.Forms.Label labelOpacity;
        private System.Windows.Forms.Button buttonBackgroundColor;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_shards;
        private System.Windows.Forms.RadioButton rb_420;
        private System.Windows.Forms.RadioButton rb_640;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_mapstone_font;
        private System.Windows.Forms.ColorDialog colorDialogFont;
        private System.Windows.Forms.RadioButton rb_300;
        private System.Windows.Forms.RadioButton rb_720;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rb_500_mHz;
        private System.Windows.Forms.RadioButton rb_1_hz;
        private System.Windows.Forms.RadioButton rb_60_hz;
        private System.Windows.Forms.RadioButton rb_10_hz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_teleporters;
        private System.Windows.Forms.CheckBox cb_trees;
    }
}