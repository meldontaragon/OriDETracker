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
            this.labelScaling = new System.Windows.Forms.Label();
            this.labelOpacity = new System.Windows.Forms.Label();
            this.buttonBackgroundColor = new System.Windows.Forms.Button();
            this.percentNumericUpDown = new OriDETracker.PercentNumericUpDown();
            this.numericUpDownScaling = new OriDETracker.PercentNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.percentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaling)).BeginInit();
            this.SuspendLayout();
            // 
            // rbRandoTrees
            // 
            this.rbRandoTrees.AutoSize = true;
            this.rbRandoTrees.Location = new System.Drawing.Point(12, 12);
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
            this.rbRandoEvents.Location = new System.Drawing.Point(12, 35);
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
            this.rbOriAllSkills.Location = new System.Drawing.Point(12, 58);
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
            this.rbOriAllCells.Location = new System.Drawing.Point(12, 81);
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
            this.rbReverseEventOrder.Location = new System.Drawing.Point(12, 104);
            this.rbReverseEventOrder.Name = "rbReverseEventOrder";
            this.rbReverseEventOrder.Size = new System.Drawing.Size(125, 17);
            this.rbReverseEventOrder.TabIndex = 4;
            this.rbReverseEventOrder.TabStop = true;
            this.rbReverseEventOrder.Text = "Reverse Event Order";
            this.rbReverseEventOrder.UseVisualStyleBackColor = true;
            this.rbReverseEventOrder.CheckedChanged += new System.EventHandler(this.rbReverseEventOrder_CheckedChanged);
            // 
            // labelScaling
            // 
            this.labelScaling.AutoSize = true;
            this.labelScaling.Location = new System.Drawing.Point(12, 141);
            this.labelScaling.Name = "labelScaling";
            this.labelScaling.Size = new System.Drawing.Size(34, 13);
            this.labelScaling.TabIndex = 6;
            this.labelScaling.Text = "Scale";
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Location = new System.Drawing.Point(12, 167);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(43, 13);
            this.labelOpacity.TabIndex = 8;
            this.labelOpacity.Text = "Opacity";
            // 
            // buttonBackgroundColor
            // 
            this.buttonBackgroundColor.Location = new System.Drawing.Point(12, 191);
            this.buttonBackgroundColor.Name = "buttonBackgroundColor";
            this.buttonBackgroundColor.Size = new System.Drawing.Size(131, 23);
            this.buttonBackgroundColor.TabIndex = 9;
            this.buttonBackgroundColor.Text = "Background Color";
            this.buttonBackgroundColor.UseVisualStyleBackColor = true;
            this.buttonBackgroundColor.Click += new System.EventHandler(this.buttonBackgroundColor_Click);
            // 
            // percentNumericUpDown
            // 
            this.percentNumericUpDown.Location = new System.Drawing.Point(72, 165);
            this.percentNumericUpDown.Name = "percentNumericUpDown";
            this.percentNumericUpDown.Size = new System.Drawing.Size(59, 20);
            this.percentNumericUpDown.TabIndex = 7;
            this.percentNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.percentNumericUpDown.ValueChanged += new System.EventHandler(this.percentNumericUpDown_ValueChanged);
            // 
            // numericUpDownScaling
            // 
            this.numericUpDownScaling.Location = new System.Drawing.Point(72, 139);
            this.numericUpDownScaling.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownScaling.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownScaling.Name = "numericUpDownScaling";
            this.numericUpDownScaling.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownScaling.TabIndex = 5;
            this.numericUpDownScaling.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownScaling.ValueChanged += new System.EventHandler(this.numericUpDownScaling_ValueChanged);
            // 
            // SettingsLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 224);
            this.Controls.Add(this.buttonBackgroundColor);
            this.Controls.Add(this.labelOpacity);
            this.Controls.Add(this.percentNumericUpDown);
            this.Controls.Add(this.labelScaling);
            this.Controls.Add(this.numericUpDownScaling);
            this.Controls.Add(this.rbReverseEventOrder);
            this.Controls.Add(this.rbOriAllCells);
            this.Controls.Add(this.rbOriAllSkills);
            this.Controls.Add(this.rbRandoEvents);
            this.Controls.Add(this.rbRandoTrees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsLayout";
            this.Text = "Tracker Layout";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsLayout_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.percentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaling)).EndInit();
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
        private PercentNumericUpDown numericUpDownScaling;
        private System.Windows.Forms.Label labelScaling;
        private PercentNumericUpDown percentNumericUpDown;
        private System.Windows.Forms.Label labelOpacity;
        private System.Windows.Forms.Button buttonBackgroundColor;
    }
}