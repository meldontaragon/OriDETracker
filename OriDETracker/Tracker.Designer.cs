namespace OriDETracker
{
    partial class Tracker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tracker));
            this.contextMenu_Tracker = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizerAllTreesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizerAllEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allSkillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseEventOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelBlank = new System.Windows.Forms.Label();
            this.contextMenu_Tracker.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu_Tracker
            // 
            this.contextMenu_Tracker.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.autoUpdateToolStripMenuItem,
            this.alwaysOnTopToolStripMenuItem,
            this.toolStripSeparator,
            this.settingsToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.layoutToolStripMenuItem});
            this.contextMenu_Tracker.Name = "contextMenu_Tracker";
            this.contextMenu_Tracker.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenu_Tracker.ShowCheckMargin = true;
            this.contextMenu_Tracker.ShowImageMargin = false;
            this.contextMenu_Tracker.Size = new System.Drawing.Size(152, 164);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.CheckOnClick = true;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.editToolStripMenuItem.Text = "Move";
            this.editToolStripMenuItem.ToolTipText = "Allows the form to be moved";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // autoUpdateToolStripMenuItem
            // 
            this.autoUpdateToolStripMenuItem.CheckOnClick = true;
            this.autoUpdateToolStripMenuItem.Name = "autoUpdateToolStripMenuItem";
            this.autoUpdateToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.autoUpdateToolStripMenuItem.Text = "Auto Update";
            this.autoUpdateToolStripMenuItem.Click += new System.EventHandler(this.autoUpdateToolStripMenuItem_Click);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Checked = true;
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on Top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(148, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomizerAllTreesToolStripMenuItem,
            this.randomizerAllEventsToolStripMenuItem,
            this.allSkillsToolStripMenuItem,
            this.allCellsToolStripMenuItem,
            this.reverseEventOrderToolStripMenuItem});
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.layoutToolStripMenuItem.Text = "Layout";
            // 
            // randomizerAllTreesToolStripMenuItem
            // 
            this.randomizerAllTreesToolStripMenuItem.Checked = true;
            this.randomizerAllTreesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomizerAllTreesToolStripMenuItem.Name = "randomizerAllTreesToolStripMenuItem";
            this.randomizerAllTreesToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.randomizerAllTreesToolStripMenuItem.Text = "Randomizer All Trees";
            this.randomizerAllTreesToolStripMenuItem.Click += new System.EventHandler(this.randomizerAllTreesToolStripMenuItem_Click);
            // 
            // randomizerAllEventsToolStripMenuItem
            // 
            this.randomizerAllEventsToolStripMenuItem.Name = "randomizerAllEventsToolStripMenuItem";
            this.randomizerAllEventsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.randomizerAllEventsToolStripMenuItem.Text = "Randomizer All Events";
            this.randomizerAllEventsToolStripMenuItem.Click += new System.EventHandler(this.randomizerAllEventsToolStripMenuItem_Click);
            // 
            // allSkillsToolStripMenuItem
            // 
            this.allSkillsToolStripMenuItem.Name = "allSkillsToolStripMenuItem";
            this.allSkillsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.allSkillsToolStripMenuItem.Text = "All Skills";
            this.allSkillsToolStripMenuItem.Click += new System.EventHandler(this.allSkillsToolStripMenuItem_Click);
            // 
            // allCellsToolStripMenuItem
            // 
            this.allCellsToolStripMenuItem.Name = "allCellsToolStripMenuItem";
            this.allCellsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.allCellsToolStripMenuItem.Text = "All Cells";
            this.allCellsToolStripMenuItem.Click += new System.EventHandler(this.allCellsToolStripMenuItem_Click);
            // 
            // reverseEventOrderToolStripMenuItem
            // 
            this.reverseEventOrderToolStripMenuItem.Name = "reverseEventOrderToolStripMenuItem";
            this.reverseEventOrderToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.reverseEventOrderToolStripMenuItem.Text = "Reverse Event Order";
            this.reverseEventOrderToolStripMenuItem.Click += new System.EventHandler(this.reverseEventOrderToolStripMenuItem_Click);
            // 
            // labelBlank
            // 
            this.labelBlank.AutoSize = true;
            this.labelBlank.Location = new System.Drawing.Point(35, 36);
            this.labelBlank.Name = "labelBlank";
            this.labelBlank.Size = new System.Drawing.Size(0, 13);
            this.labelBlank.TabIndex = 1;
            // 
            // Tracker
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(400, 584);
            this.ContextMenuStrip = this.contextMenu_Tracker;
            this.Controls.Add(this.labelBlank);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tracker";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Ori DE Tracker";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tracker_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Tracker_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Tracker_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tracker_MouseDown);
            this.contextMenu_Tracker.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenu_Tracker;
        private System.Windows.Forms.ToolStripMenuItem autoUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Label labelBlank;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizerAllTreesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizerAllEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allSkillsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allCellsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseEventOrderToolStripMenuItem;
    }
}

