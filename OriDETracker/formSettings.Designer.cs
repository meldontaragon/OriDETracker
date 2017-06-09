namespace OriDETracker
{
    partial class formSettings
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.rbRandoTrees = new System.Windows.Forms.RadioButton();
            this.rbRandoEvents = new System.Windows.Forms.RadioButton();
            this.rbOriAllSkills = new System.Windows.Forms.RadioButton();
            this.rbOriAllCells = new System.Windows.Forms.RadioButton();
            this.rbReverseEventOrder = new System.Windows.Forms.RadioButton();
            this.labelTracking = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbRandoTrees
            // 
            this.rbRandoTrees.AutoSize = true;
            this.rbRandoTrees.Location = new System.Drawing.Point(12, 25);
            this.rbRandoTrees.Name = "rbRandoTrees";
            this.rbRandoTrees.Size = new System.Drawing.Size(125, 17);
            this.rbRandoTrees.TabIndex = 0;
            this.rbRandoTrees.TabStop = true;
            this.rbRandoTrees.Text = "Randomizer All Trees";
            this.rbRandoTrees.UseVisualStyleBackColor = true;
            // 
            // rbRandoEvents
            // 
            this.rbRandoEvents.AutoSize = true;
            this.rbRandoEvents.Location = new System.Drawing.Point(12, 48);
            this.rbRandoEvents.Name = "rbRandoEvents";
            this.rbRandoEvents.Size = new System.Drawing.Size(131, 17);
            this.rbRandoEvents.TabIndex = 1;
            this.rbRandoEvents.TabStop = true;
            this.rbRandoEvents.Text = "Randomizer All Events";
            this.rbRandoEvents.UseVisualStyleBackColor = true;
            // 
            // rbOriAllSkills
            // 
            this.rbOriAllSkills.AutoSize = true;
            this.rbOriAllSkills.Location = new System.Drawing.Point(12, 71);
            this.rbOriAllSkills.Name = "rbOriAllSkills";
            this.rbOriAllSkills.Size = new System.Drawing.Size(63, 17);
            this.rbOriAllSkills.TabIndex = 2;
            this.rbOriAllSkills.TabStop = true;
            this.rbOriAllSkills.Text = "All Skills";
            this.rbOriAllSkills.UseVisualStyleBackColor = true;
            // 
            // rbOriAllCells
            // 
            this.rbOriAllCells.AutoSize = true;
            this.rbOriAllCells.Location = new System.Drawing.Point(12, 94);
            this.rbOriAllCells.Name = "rbOriAllCells";
            this.rbOriAllCells.Size = new System.Drawing.Size(61, 17);
            this.rbOriAllCells.TabIndex = 3;
            this.rbOriAllCells.TabStop = true;
            this.rbOriAllCells.Text = "All Cells";
            this.rbOriAllCells.UseVisualStyleBackColor = true;
            // 
            // rbReverseEventOrder
            // 
            this.rbReverseEventOrder.AutoSize = true;
            this.rbReverseEventOrder.Location = new System.Drawing.Point(12, 117);
            this.rbReverseEventOrder.Name = "rbReverseEventOrder";
            this.rbReverseEventOrder.Size = new System.Drawing.Size(125, 17);
            this.rbReverseEventOrder.TabIndex = 4;
            this.rbReverseEventOrder.TabStop = true;
            this.rbReverseEventOrder.Text = "Reverse Event Order";
            this.rbReverseEventOrder.UseVisualStyleBackColor = true;
            // 
            // labelTracking
            // 
            this.labelTracking.AutoSize = true;
            this.labelTracking.Location = new System.Drawing.Point(12, 9);
            this.labelTracking.Name = "labelTracking";
            this.labelTracking.Size = new System.Drawing.Size(79, 13);
            this.labelTracking.TabIndex = 5;
            this.labelTracking.Text = "Tracker Format";
            // 
            // formSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 261);
            this.Controls.Add(this.labelTracking);
            this.Controls.Add(this.rbReverseEventOrder);
            this.Controls.Add(this.rbOriAllCells);
            this.Controls.Add(this.rbOriAllSkills);
            this.Controls.Add(this.rbRandoEvents);
            this.Controls.Add(this.rbRandoTrees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.RadioButton rbRandoTrees;
        private System.Windows.Forms.RadioButton rbRandoEvents;
        private System.Windows.Forms.RadioButton rbOriAllSkills;
        private System.Windows.Forms.RadioButton rbOriAllCells;
        private System.Windows.Forms.RadioButton rbReverseEventOrder;
        private System.Windows.Forms.Label labelTracking;
    }
}