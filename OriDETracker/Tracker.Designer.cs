using System;
using System.Collections.Generic;
using System.Drawing;

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
            this.font_brush?.Dispose();
            this.map_font?.Dispose();

            this.imageSkillWheelDouble?.Dispose();
            this.imageBlackBackground?.Dispose();
            this.imageGSkills?.Dispose();
            this.imageGTrees?.Dispose();
            this.imageGTeleporters?.Dispose();
            this.imageMapStone?.Dispose();

            foreach (KeyValuePair<String, Image> k in skillImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in treeImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in eventImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in eventGreyImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in shardImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in teleporterImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in relicExistImages)
            {
                k.Value?.Dispose();
            }
            foreach (KeyValuePair<String, Image> k in relicFoundImages)
            {
                k.Value?.Dispose();
            }

            this.settings?.Dispose();

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
            this.TrackerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelBlank = new System.Windows.Forms.Label();
            this.MapStoneFontDialog = new System.Windows.Forms.FontDialog();
            this.TrackerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrackerContextMenu
            // 
            this.TrackerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveToolStripMenuItem,
            this.AutoUpdateToolStripMenuItem,
            this.AlwaysOnTopToolStripMenuItem,
            this.ToolStripSeparator,
            this.SettingsToolStripMenuItem,
            this.ToolStripSeparator2,
            this.ClearToolStripMenuItem,
            this.CloseToolStripMenuItem});
            this.TrackerContextMenu.Name = "contextMenu_Tracker";
            this.TrackerContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TrackerContextMenu.ShowCheckMargin = true;
            this.TrackerContextMenu.ShowImageMargin = false;
            this.TrackerContextMenu.Size = new System.Drawing.Size(151, 148);
            // 
            // MoveToolStripMenuItem
            // 
            this.MoveToolStripMenuItem.CheckOnClick = true;
            this.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem";
            this.MoveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.MoveToolStripMenuItem.Text = "Draggable";
            this.MoveToolStripMenuItem.ToolTipText = "Allows the form to be moved";
            this.MoveToolStripMenuItem.Click += new System.EventHandler(this.MoveToolStripMenuItem_Click);
            // 
            // AutoUpdateToolStripMenuItem
            // 
            this.AutoUpdateToolStripMenuItem.CheckOnClick = true;
            this.AutoUpdateToolStripMenuItem.Name = "AutoUpdateToolStripMenuItem";
            this.AutoUpdateToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.AutoUpdateToolStripMenuItem.Text = "Auto Update";
            this.AutoUpdateToolStripMenuItem.Click += new System.EventHandler(this.AutoUpdateToolStripMenuItem_Click);
            // 
            // AlwaysOnTopToolStripMenuItem
            // 
            this.AlwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.AlwaysOnTopToolStripMenuItem.Name = "AlwaysOnTopToolStripMenuItem";
            this.AlwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.AlwaysOnTopToolStripMenuItem.Text = "Always on Top";
            this.AlwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.AlwaysOnTopToolStripMenuItem_Click);
            // 
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(147, 6);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.SettingsToolStripMenuItem.Text = "Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.ClearToolStripMenuItem.Text = "Clear Tracker";
            this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CloseToolStripMenuItem.Text = "Close";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // LabelBlank
            // 
            this.LabelBlank.AutoSize = true;
            this.LabelBlank.Location = new System.Drawing.Point(35, 36);
            this.LabelBlank.Name = "LabelBlank";
            this.LabelBlank.Size = new System.Drawing.Size(0, 13);
            this.LabelBlank.TabIndex = 1;
            // 
            // Tracker
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.ContextMenuStrip = this.TrackerContextMenu;
            this.Controls.Add(this.LabelBlank);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tracker";
            this.Opacity = 0D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Ori DE Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tracker_FormClosing);
            this.Load += new System.EventHandler(this.Tracker_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Tracker_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tracker_MouseDown);
            this.TrackerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip TrackerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AutoUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.Label LabelBlank;
        private System.Windows.Forms.FontDialog MapStoneFontDialog;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    }
}

