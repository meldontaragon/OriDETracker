using OriDE.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class Tracker : Form
    {
        public Tracker()
        {
            InitializeComponent();

            // Apply settings options for Refresh Rate (and refresh_time) 
            RefreshRate = TrackerSettings.Default.RefreshRate;

            // Apply settings options Tracker Size using UpdateTrackerSize
            UpdateTrackerSize(TrackerSettings.Default.Pixels);

            // Settings options for what to track
            track_shards = TrackerSettings.Default.Shards;
            track_relics = TrackerSettings.Default.Relics;
            track_teleporters = TrackerSettings.Default.Teleporters;
            track_trees = TrackerSettings.Default.Trees;
            track_mapstones = TrackerSettings.Default.Mapstones;

            // Settings for what to display
            display_empty_relics = TrackerSettings.Default.DisplayEmptyRelics;
            display_empty_trees = TrackerSettings.Default.DisplayEmptyTrees;
            display_empty_teleporters = TrackerSettings.Default.DisplayEmptyTeleporters;

            // Load the default logic options, bitfields, and mouse mappings
            SetDefaults();

            // Load settings for this form
            this.MoveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;
            this.AutoUpdateToolStripMenuItem.Checked = TrackerSettings.Default.AutoUpdate;
            this.AlwaysOnTopToolStripMenuItem.Checked = TrackerSettings.Default.AlwaysOnTop;
            this.TopMost = TrackerSettings.Default.AlwaysOnTop;

            // Other Settings
            font_color = TrackerSettings.Default.FontColoring;
            font_family = TrackerSettings.Default.MapFont;
            Opacity = TrackerSettings.Default.Opacity;
            BackColor = TrackerSettings.Default.Background;

            // Auto update boolean values
            auto_update = TrackerSettings.Default.AutoUpdate;

            if (font_color == null)
                font_color = Color.White;
            if (BackColor == null)
                BackColor = Color.Black;

            font_brush = new SolidBrush(font_color);

            bool need_font, found_fount = false;

            if (font_family == null)
                need_font = true;
            else
                need_font = false;

            if (need_font)
                // first looks for amatic sc
                foreach (FontFamily ff in FontFamily.Families)
                {
                    if (ff.Name.ToLower() == "amatic sc")
                    {
                        font_family = new FontFamily("Amatic SC");
                        found_fount = true;
                        break;
                    }
                }
            // if not found then ask for a font
            if (need_font && !found_fount)
            {
                MessageBox.Show("It is recommended to install and use the included fonts: Amatic SC and Amatic SC Bold");
                if (this.MapStoneFontDialog.ShowDialog() == DialogResult.OK)
                {
                    font_family = MapStoneFontDialog.Font.FontFamily;
                }
                else
                {
                    font_family = FontFamily.GenericSansSerif;
                }
            }
            // finally load font
            map_font = new Font(font_family, 24f, FontStyle.Bold);

            // Initialize the OriMemory module that Devil/Eiko/Sigma wrote
            Mem = new OriMemory();

            // Initialize background update loop
            th = new Thread(UpdateLoop)
            {
                IsBackground = true
            };

            // Settings window display
            settings = new SettingsLayout(this)
            {
                Visible = false
            };
        }

        #region PrivateVariables
        protected static int PIXEL_DEF = 667;
        protected int image_pixel_size;

        protected TrackerPixelSizes tracker_size;

        protected Color font_color;
        protected FontFamily font_family;
        protected Brush font_brush;
        protected Font map_font;

        protected AutoUpdateRefreshRates refresh_rate;
        protected int refresh_time;

        protected bool mode_shards;
        protected bool mode_force_trees;
        protected bool mode_world_tour;
        protected bool mode_warmth_fragments;
        protected bool mode_force_maps;

        protected int current_frags;
        protected int max_frags;

        protected bool draggable = TrackerSettings.Default.Draggable;
        protected bool auto_update = TrackerSettings.Default.AutoUpdate;

        protected bool track_teleporters = TrackerSettings.Default.Teleporters;
        protected bool track_trees = TrackerSettings.Default.Trees;
        protected bool track_shards = TrackerSettings.Default.Shards;
        protected bool track_relics = TrackerSettings.Default.Relics;
        protected bool track_mapstones = TrackerSettings.Default.Mapstones;

        protected bool display_empty_relics = TrackerSettings.Default.DisplayEmptyRelics;
        protected bool display_empty_trees = TrackerSettings.Default.DisplayEmptyTrees;
        protected bool display_empty_teleporters = TrackerSettings.Default.DisplayEmptyTeleporters;

        protected OriMemory Mem
        {
            get;
            set;
        }

        protected Thread th;
        protected SettingsLayout settings;

        private readonly Dictionary<int, MapstoneText> mapstone_text_parameters = new Dictionary<int, MapstoneText>(){
            { (int) TrackerPixelSizes.Small, new MapstoneText(140+6, 190+6, 14) },
            { (int) TrackerPixelSizes.Medium, new MapstoneText(195+9, 268+9, 18) },
            { (int) TrackerPixelSizes.Large, new MapstoneText(304+13, 417+13, 24) },
            { (int) TrackerPixelSizes.XL, new MapstoneText(342+15, 471+15, 28) }
        };

        private readonly string[] skill_list = { "Spirit Flame", "Wall Jump", "Charge Flame", "Double Jump", "Bash", "Stomp", "Glide", "Climb", "Charge Jump", "Grenade", "Dash" };
        private readonly string[] event_list = { "Water Vein", "Gumon Seal", "Sunstone", "Clean Water", "Wind Restored" };
        private readonly string[] zone_list = { "Glades", "Grove", "Grotto", "Ginso", "Swamp", "Valley", "Misty", "Blackroot", "Sorrow", "Forlorn", "Horu" };
        #endregion

        #region PublicProperties
        public Color FontColor
        {
            get { return font_color; }
            set { font_color = value; font_brush = new SolidBrush(value); }

        }
        public Font MapFont
        {
            get { return map_font; }
            set { font_family = value.FontFamily; map_font = new Font(font_family, 24f, FontStyle.Bold); }
        }
        public TrackerPixelSizes TrackerSize
        {
            get { return tracker_size; }
            set { tracker_size = value; image_pixel_size = (int)value; }
        }

        public AutoUpdateRefreshRates RefreshRate
        {
            get { return refresh_rate; }
            set { refresh_rate = value; refresh_time = (int)(1000000.0f / ((float)value)); }
        }

        public bool TrackShards
        {
            get { return track_shards; }
            set { track_shards = value; this.Refresh(); }
        }
        public bool TrackTeleporters
        {
            get { return track_teleporters; }
            set { track_teleporters = value; this.Refresh(); }
        }
        public bool TrackTrees
        {
            get { return track_trees; }
            set { track_trees = value; this.Refresh(); }
        }
        public bool TrackRelics
        {
            get { return track_relics; }
            set { track_relics = value; this.SetRelicDefaults(); this.Refresh(); }
        }
        public bool TrackMapstones
        {
            get { return track_mapstones; }
            set { track_mapstones = value; this.Refresh(); }
        }
        public int MapstoneCount
        {
            get { return mapstone_count; }
            set { mapstone_count = value; }
        }

        public bool DisplayEmptyRelics
        {
            get { return display_empty_relics; }
            set { display_empty_relics = value; this.Refresh(); }
        }
        public bool DisplayEmptyTrees
        {
            get { return display_empty_trees; }
            set { display_empty_trees = value; this.Refresh(); }
        }
        public bool DisplayEmptyTeleporters
        {
            get { return display_empty_teleporters; }
            set { display_empty_teleporters = value; this.Refresh(); }
        }
        #endregion

        #region FrameMoving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        #endregion

        #region LogicDictionary
        // not a dictionary but "logic"
        protected int mapstone_count = 0;

        //Skills, Trees, Events, Shards, Teleporters, and Relics
        protected ConcurrentDictionary<String, bool> haveSkill = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> haveTree = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> haveEvent = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> haveShards = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> teleportersActive = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> teleportersInactive = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> relicExists = new ConcurrentDictionary<string, bool>();
        protected ConcurrentDictionary<String, bool> relicFound = new ConcurrentDictionary<string, bool>();

        //Bits
        private Dictionary<String, int> treeBits;
        private Dictionary<String, int> skillBits;
        private Dictionary<String, int> teleporterBits;
        private Dictionary<String, int> mapstoneBits;
        private Dictionary<String, int> relicExistsBits;
        private Dictionary<String, int> relicFoundBits;
        #endregion

        #region Images
        protected String DIR = @"Assets_667/";

        protected Image imageSkillWheelDouble;
        protected Image imageBlackBackground;
        protected Image imageGSkills;
        protected Image imageGTrees;
        protected Image imageMapStone;

        protected Dictionary<String, Image> skillImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> treeImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> eventImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> eventGreyImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> shardImages = new Dictionary<string, Image>();
        protected Dictionary<String, Image> teleporterActiveImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> teleporterInactiveImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicExistImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicFoundImages = new Dictionary<String, Image>();

        private void DisposeImages()
        {
            imageSkillWheelDouble?.Dispose();
            imageBlackBackground?.Dispose();
            imageGSkills?.Dispose();
            imageGTrees?.Dispose();
            imageMapStone?.Dispose();

            foreach (string skill in skill_list)
            {
                skillImages[skill]?.Dispose();
                treeImages[skill]?.Dispose();
            }

            foreach (string ev in event_list)
            {
                eventImages[ev]?.Dispose();
                eventGreyImages[ev]?.Dispose();

                if (ev == "Water Vein" || ev == "Gumon Seal" || ev == "Sunstone")
                {
                    shardImages[ev + " 1"]?.Dispose();
                    shardImages[ev + " 2"]?.Dispose();
                }
            }

            foreach (string zone in zone_list)
            {
                relicExistImages[zone]?.Dispose();
                relicFoundImages[zone]?.Dispose();

                if (zone != "Misty")
                {
                    teleporterActiveImages[zone]?.Dispose();
                }
            }
        }

        private void UpdateImages()
        {
            // On startup, no tracker images are stored in dictionaries
            if (imageSkillWheelDouble != null)
            {
                DisposeImages();
            }

            DIR = "Assets_" + image_pixel_size.ToString() + @"/";

            imageSkillWheelDouble = Image.FromFile(DIR + @"SkillRing_Double.png");
            imageBlackBackground = Image.FromFile(DIR + @"BlackBackground.png");
            imageGSkills = Image.FromFile(DIR + @"GreySkillTree.png");
            imageGTrees = Image.FromFile(DIR + @"GreyTrees.png");
            imageMapStone = Image.FromFile(DIR + @"MapStone.png");

            foreach (string skill in skill_list)
            {
                skillImages[skill] = Image.FromFile(DIR + skill.Replace(" ", String.Empty) + @".png");
                treeImages[skill] = Image.FromFile(DIR + "T" + skill.Replace(" ", String.Empty) + @".png");
            }

            foreach (string ev in event_list)
            {
                eventImages[ev] = Image.FromFile(DIR + ev.Replace(" ", String.Empty) + @".png");
                eventGreyImages[ev] = Image.FromFile(DIR + "G" + ev.Replace(" ", String.Empty) + @".png");

                if (ev == "Water Vein" || ev == "Gumon Seal" || ev == "Sunstone")
                {
                    shardImages[ev + " 1"] = Image.FromFile(DIR + ev.Replace(" ", String.Empty) + @"Shard1.png");
                    shardImages[ev + " 2"] = Image.FromFile(DIR + ev.Replace(" ", String.Empty) + @"Shard2.png");
                }
            }

            foreach (string zone in zone_list)
            {
                relicExistImages[zone] = Image.FromFile(DIR + "Relics/Exist/" + zone + ".png");
                relicFoundImages[zone] = Image.FromFile(DIR + "Relics/Found/" + zone + ".png");

                if (zone != "Misty")
                {
                    teleporterActiveImages[zone] = Image.FromFile(DIR + zone + "TP.png");
                }
            }
        }
        #endregion

        #region SetLayout
        //points for mouse clicks (with certain tolerance defined by TOL)
        private const int TOL = 25;
        private Point mapstoneMousePoint = new Point(305, 343);
        private Dictionary<String, Point> eventMousePoint;
        private Dictionary<String, Point> treeMouseLocation;
        private Dictionary<String, Point> skillMousePoint;

        private void SetDefaults()
        {
            SetMouseLocations();
            SetBitDefaults();
            SetSkillDefaults();
            SetEventDefaults();
            SetRelicDefaults();
        }
        private void SetSkillDefaults()
        {
            //haveTree and haveSkill Dictionaries
            foreach (var sk in skill_list)
            {
                haveTree[sk] = false;
                haveSkill[sk] = false;
            }
        }
        private void SetEventDefaults()
        {
            //haveEvent and haveShard Dictionaries
            foreach (var ev in event_list)
            {
                haveEvent[ev] = false;
                if (ev == "Water Vein" || ev == "Gumon Seal" || ev == "Sunstone")
                {
                    haveShards[ev + " 1"] = false;
                    haveShards[ev + " 2"] = false;
                }
            }
        }
        private void SetRelicDefaults()
        {
            //relicExists, relicFound, and teleporterActive Dictionaries
            foreach (var zn in zone_list)
            {
                relicExists[zn] = !auto_update;
                relicFound[zn] = false;
                if (zn != "Misty")
                {
                    teleportersActive[zn] = false;
                }
            }
        }
        private void SetBitDefaults()
        {
            #region Bits
            treeBits = new Dictionary<string, int>() {
                { "Spirit Flame", 0},
                { "Wall Jump", 1},
                { "Charge Flame", 2},
                { "Double Jump", 3},
                { "Bash", 4},
                { "Stomp", 5},
                { "Glide", 6},
                { "Climb", 7},
                { "Charge Jump", 8},
                { "Grenade", 9},
                { "Dash", 10}
            };
            skillBits = new Dictionary<string, int>() {
                { "Spirit Flame", 11},
                { "Wall Jump", 12},
                { "Charge Flame", 13},
                { "Double Jump", 14},
                { "Bash", 15},
                { "Stomp", 16},
                { "Glide", 17},
                { "Climb", 18},
                { "Charge Jump", 19},
                { "Grenade", 20},
                { "Dash", 21}
            };
            relicFoundBits = new Dictionary<string, int>() {
                {"Glades", 0},
                {"Grove", 1},
                {"Grotto", 2},
                {"Blackroot", 3},
                {"Swamp", 4},
                {"Ginso", 5},
                {"Valley", 6},
                {"Misty", 7},
                {"Forlorn", 8},
                {"Sorrow", 9},
                {"Horu", 10}
            };
            relicExistsBits = new Dictionary<string, int>() {
                {"Glades", 11},
                {"Grove", 12},
                {"Grotto", 13},
                {"Blackroot", 14},
                {"Swamp", 15},
                {"Ginso", 16},
                {"Valley", 17},
                {"Misty", 18},
                {"Forlorn", 19},
                {"Sorrow", 20},
                {"Horu", 21}
            };
            mapstoneBits = new Dictionary<string, int>()
            {
                {"Glades", 0},
                {"Blackroot", 1},
                {"Grove", 2},
                {"Grotto", 3},
                {"Swamp", 4},
                {"Valley", 5},
                {"Forlorn", 6},
                {"Sorrow", 7},
                {"Horu", 8},
            };
            teleporterBits = new Dictionary<string, int>()
            {
                {"Grove", 0},
                {"Swamp", 1},
                {"Grotto", 2},
                {"Valley", 3},
                {"Forlorn", 4},
                {"Sorrow", 5},
                {"Ginso", 6},
                {"Horu", 7},
                {"Blackroot", 8},
                {"Glades", 9}
            };
            #endregion
        }
        private void SetMouseLocations()
        {
            skillMousePoint = new Dictionary<String, Point>();
            treeMouseLocation = new Dictionary<String, Point>();

            for (int i = 0; i < 11; ++i)
            {
                skillMousePoint.Add(skill_list[i], new Point((int)(320 + 13 + 205 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                                                         (int)(320 + 13 - 205 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }
            for (int i = 0; i < 11; ++i)
            {
                treeMouseLocation.Add(skill_list[i], new Point((int)(320 + 13 + 286 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                                                           (int)(320 + 13 - 286 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }

            eventMousePoint = new Dictionary<string, Point>(){
                {"Water Vein", new Point(221+13, 258+13)},
                {"Gumon Seal", new Point(328+13, 215+13)},
                {"Sunstone",   new Point(428+13, 257+13)},
                {"Wind Restored", new Point(423+13, 365+13)},
                {"Clean Water", new Point(220+13, 360+13)}
            };
        }
        #endregion

        #region EventHandlers
        private void Tracker_Load(object sender, EventArgs e)
        {
            // Start background update loop when the tracker is loaded
            // Avoid modified collection exception of dictionaries conflicted between init and update loop
            if (auto_update)
            {
                this.TurnOnAutoUpdate();
            }
        }

        private void Tracker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && draggable)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        protected void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected void AutoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auto_update = !auto_update;

            if (auto_update)
            {
                TurnOnAutoUpdate();
            }
            else
            {
                TurnOffAutoUpdate();
            }
        }
        private void AlwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = AlwaysOnTopToolStripMenuItem.Checked;
        }
        protected void MoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draggable = !draggable;
        }
        protected void Tracker_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;

            x = e.X;
            y = e.Y;

            if (ToggleMouseClick(x, y))
            {
                bool tmp_auto_update = auto_update;
                //try turning off auto update for a moment
                if (tmp_auto_update)
                    this.TurnOffAutoUpdate();
                this.Refresh();
                if (tmp_auto_update)
                    this.TurnOnAutoUpdate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGraphics(e.Graphics);
        }
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void Tracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop background update loop before closing
            // Avoid an update on disposed objects
            if (auto_update)
            {
                this.TurnOffAutoUpdate();
            }

            TrackerSettings.Default.FontColoring = font_color;
            TrackerSettings.Default.MapFont = font_family;
            TrackerSettings.Default.Background = BackColor;
            TrackerSettings.Default.RefreshRate = refresh_rate;
            TrackerSettings.Default.Opacity = Opacity;

            TrackerSettings.Default.Shards = track_shards;
            TrackerSettings.Default.Teleporters = track_teleporters;
            TrackerSettings.Default.Trees = track_trees;
            TrackerSettings.Default.Relics = track_relics;
            TrackerSettings.Default.Mapstones = track_mapstones;

            TrackerSettings.Default.DisplayEmptyRelics = display_empty_relics;
            TrackerSettings.Default.DisplayEmptyTrees = display_empty_trees;
            TrackerSettings.Default.DisplayEmptyTeleporters = display_empty_teleporters;

            TrackerSettings.Default.Pixels = tracker_size;
            TrackerSettings.Default.AlwaysOnTop = this.TopMost;
            TrackerSettings.Default.Draggable = draggable;
            TrackerSettings.Default.AutoUpdate = auto_update;

            TrackerSettings.Default.Save();

            Mem?.Dispose();
        }
        #endregion

        #region Graphics
        protected int Square(int a)
        {
            return a * a;
        }
        protected bool ToggleMouseClick(int x, int y)
        {
            double mouse_scaling = ((image_pixel_size * 1.0) / PIXEL_DEF);
            int CUR_TOL = (int)(TOL * mouse_scaling);

            if (track_mapstones && (Math.Sqrt(Square(x - (int)(mapstoneMousePoint.X * mouse_scaling)) + Square(y - (int)(mapstoneMousePoint.Y * mouse_scaling))) <= 2 * CUR_TOL))
            {
                mapstone_count += 1;
                if (mapstone_count > 9)
                {
                    mapstone_count = 0;
                }
                return true;
            }

            foreach (KeyValuePair<String, Point> sk in skillMousePoint)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * mouse_scaling)) + Square(y - (int)(sk.Value.Y * mouse_scaling))) <= 2 * CUR_TOL)
                {
                    if (haveSkill.ContainsKey(sk.Key))
                    {
                        haveSkill[sk.Key] = !haveSkill[sk.Key];
                        return true;
                    }
                }
            }

            foreach (KeyValuePair<String, Point> sk in treeMouseLocation)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * mouse_scaling)) + Square(y - (int)(sk.Value.Y * mouse_scaling))) <= CUR_TOL)
                {
                    if (haveTree.ContainsKey(sk.Key))
                    {
                        haveTree[sk.Key] = !haveTree[sk.Key];
                        return true;
                    }
                }
            }

            foreach (KeyValuePair<String, Point> sk in eventMousePoint)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * mouse_scaling)) + Square(y - (int)(sk.Value.Y * mouse_scaling))) <= CUR_TOL)
                {
                    if (haveEvent.ContainsKey(sk.Key))
                    {
                        switch (sk.Key)
                        {
                            case "Water Vein":
                            case "Gumon Seal":
                            case "Sunstone":
                                if (track_shards)
                                {
                                    if (haveEvent[sk.Key])
                                    {
                                        haveShards[sk.Key + " 1"] = false;
                                        haveShards[sk.Key + " 2"] = false;
                                        haveEvent[sk.Key] = false;
                                    }
                                    else if (haveShards[sk.Key + " 2"])
                                    {
                                        haveShards[sk.Key + " 1"] = true;
                                        haveShards[sk.Key + " 2"] = true;
                                        haveEvent[sk.Key] = true;
                                    }
                                    else if (haveShards[sk.Key + " 1"])
                                    {
                                        haveShards[sk.Key + " 1"] = true;
                                        haveShards[sk.Key + " 2"] = true;
                                        haveEvent[sk.Key] = false;
                                    }
                                    else
                                    {
                                        haveShards[sk.Key + " 1"] = true;
                                        haveShards[sk.Key + " 2"] = false;
                                        haveEvent[sk.Key] = false;
                                    }
                                }
                                else
                                {
                                    haveEvent[sk.Key] = !haveEvent[sk.Key];
                                }
                                break;
                            case "Warmth Returned":
                            case "Wind Restored":
                            case "Clean Water":
                                haveEvent[sk.Key] = !haveEvent[sk.Key];
                                break;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        protected void UpdateGraphics(Graphics g)
        {
            try
            {
                /*
                 * Drawing consists of the following steps:
                 * (1) The background on which everything is drawn (this can be user selected)
                 * (2) Drawing the Skills (either grayed out or colored in)
                 * (3) Drawing the Events (same)
                 * (4) Drawing the Tree locations
                 * (5) Putting the skill wheel on top
                 * */

                #region Draw
                #region Skills

                g.DrawImage(imageGSkills, ClientRectangle);
                foreach (KeyValuePair<String, bool> sk in haveSkill)
                {
                    if (sk.Value)
                    {
                        g.DrawImage(skillImages[sk.Key], ClientRectangle);
                    }
                }
                #endregion

                #region Relic
                /* Relics are drawn if:
                 * (a) auto_update and world tour are on
                 * (b) track_relics is on and display_emptry_relics is on
                 */
                if ((auto_update && mode_world_tour) || (track_relics && display_empty_relics))
                {
                    foreach (KeyValuePair<String, bool> relic in relicExists)
                    {
                        if (relic.Value)
                        {
                            g.DrawImage(relicExistImages[relic.Key], ClientRectangle);
                        }
                    }
                    foreach (KeyValuePair<String, bool> relic in relicFound)
                    {
                        if (relic.Value)
                        {
                            g.DrawImage(relicFoundImages[relic.Key], ClientRectangle);
                        }
                    }
                }
                #endregion

                #region Teleporters
                /* Teleporters are drawn if:
                 * (a) track_teleporters is on
                 * (*) only drawn grey teleporters if display_emptry_teleporters is set
                 */
                if (track_teleporters)
                {
                    foreach (KeyValuePair<String, bool> tp in teleportersActive)
                    {
                        if (tp.Value)
                        {
                            g.DrawImage(teleporterActiveImages[tp.Key], ClientRectangle);
                        }
                    }
                    if (display_empty_teleporters)
                    {
                        foreach (KeyValuePair<String, bool> tp in teleportersInactive)
                        {
                            if (tp.Value)
                            {
                                g.DrawImage(teleporterInactiveImages[tp.Key], ClientRectangle);
                            }
                        }
                    }

                }
                #endregion

                #region Tree
                /* Trees are drawn if:
                 * (a) track_trees is on
                 * (b) mode_force_trees is on
                 * (*) only draw grey trees if display_empty_trees is on
                 */
                if (track_trees || mode_force_trees)
                {
                    if (display_empty_trees)
                    {
                        g.DrawImage(imageGTrees, ClientRectangle);
                    }
                    foreach (KeyValuePair<String, bool> sk in haveTree)
                    {
                        if (sk.Value)
                        {
                            g.DrawImage(treeImages[sk.Key], ClientRectangle);
                        }
                    }
                }
                #endregion

                #region Events
                foreach (KeyValuePair<String, bool> ev in haveEvent)
                {
                    if (ev.Value)
                    {
                        g.DrawImage(eventImages[ev.Key], ClientRectangle);
                    }
                    else
                    {
                        g.DrawImage(eventGreyImages[ev.Key], ClientRectangle);
                    }
                }
                #endregion

                #region Shards
                /* Shards are drawn if
                 * (a) Shards mode is active
                 * (b) track_shards is on (manual only)
                 */
                if (track_shards || mode_shards)
                {
                    foreach (KeyValuePair<String, bool> ev in haveShards)
                    {
                        if (ev.Value)
                        {
                            g.DrawImage(shardImages[ev.Key], ClientRectangle);
                        }
                    }
                }
                #endregion

                #region Mapstone
                /* Mapstone count is displayed if
                 * (a) track_mapstones is on
                 * (b) mode all maps is on
                 */
                if (track_mapstones || mode_force_maps)
                {
                    g.DrawImage(imageMapStone, ClientRectangle);
                    if (font_brush == null)
                    {
                        font_brush = new SolidBrush(Color.White);
                    }
                    map_font = new Font(font_family, mapstone_text_parameters[image_pixel_size].TextSize, FontStyle.Bold);
                    g.DrawString(mapstone_count.ToString() + "/9", map_font, font_brush, new Point(mapstone_text_parameters[image_pixel_size].X, mapstone_text_parameters[image_pixel_size].Y));
                }
                #endregion
                #endregion

                g.DrawImage(imageSkillWheelDouble, ClientRectangle);
            }
            catch
            {

            }
        }
        protected void ClearAll()
        {
            bool tmp_auto_update = this.auto_update;
            if (tmp_auto_update)
            {
                this.TurnOffAutoUpdate();
            }

            for (int i = 0; i < haveSkill.Count; i++)
            {
                haveSkill[haveSkill.ElementAt(i).Key] = false;
            }
            for (int i = 0; i < haveTree.Count; i++)
            {
                haveTree[haveTree.ElementAt(i).Key] = false;
            }
            for (int i = 0; i < haveEvent.Count; i++)
            {
                haveEvent[haveEvent.ElementAt(i).Key] = false;
            }
            for (int i = 0; i < haveShards.Count; i++)
            {
                haveShards[haveShards.ElementAt(i).Key] = false;
            }
            for (int i = 0; i < teleportersActive.Count; i++)
            {
                teleportersActive[teleportersActive.ElementAt(i).Key] = false;
            }
            for (int i = 0; i < relicFound.Count; i++)
            {
                relicFound[relicFound.ElementAt(i).Key] = false;
            }
            mapstone_count = 0;

            Refresh();

            if (tmp_auto_update)
            {
                this.TurnOnAutoUpdate();
            }
        }
        protected void SoftReset()
        {
            ClearAll();

            this.settings.Visible = false;
            this.settings.Reset();

            SetDefaults();
            if (auto_update && !TrackerSettings.Default.AutoUpdate)
            {
                TurnOffAutoUpdate();
            }
            auto_update = TrackerSettings.Default.AutoUpdate;
            this.AutoUpdateToolStripMenuItem.Checked = TrackerSettings.Default.AutoUpdate;

            draggable = TrackerSettings.Default.Draggable;
            this.MoveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;
        }
        #endregion

        #region AutoUpdate
        bool paused;
        bool started;
        protected void TurnOnAutoUpdate()
        {
            if (started && paused)
            {
                th.Resume();
                started = true;
                paused = false;
            }
            else if (!(started))
            {
                th.Start();
                started = true;
                paused = false;
            }
        }
        protected void TurnOffAutoUpdate()
        {
            if (!paused && started)
            {
                th.Suspend();
                started = true;
                paused = true;
            }
        }

        private void UpdateLoop()
        {
            bool lastHooked = false;
            while (true)
            {
                try
                {
                    bool hooked = Mem.HookProcess();
                    if (hooked)
                    {
                        UpdateValues();
                    }
                    if (lastHooked != hooked)
                    {
                        lastHooked = hooked;
                        this.Invoke((Action)delegate () { LabelBlank.Visible = false; });
                    }
                    Thread.Sleep((int)refresh_time);
                }
                catch (Exception exc)
                {
                    if (MessageBox.Show(exc.StackTrace.ToString() + "\nWould you like to abort and soft reset the tracker?", "Exception Occured", MessageBoxButtons.AbortRetryIgnore) == DialogResult.Abort)
                        SoftReset();
                }
            }
        }
        private void UpdateValues()
        {
            try
            {
                Mem.GetBitfields();
                UpdateSkills();
                UpdateTrees();
                UpdateEvents();
                UpdateRelics();
                UpdateTeleporters();
                UpdateWarmthFrags();
                UpdateMapstoneProgression();

                //the following works but is "incorrect"
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Refresh(); }));
                }
                else
                    this.Refresh();
            }
            catch (Exception exc)
            {
                if (MessageBox.Show(exc.StackTrace.ToString() + "\nWould you like to abort and soft reset the tracker?", "Exception Occured", MessageBoxButtons.AbortRetryIgnore) == DialogResult.Abort)
                    SoftReset();
            }
        }

        private void UpdateSkills()
        {
            foreach (KeyValuePair<string, int> skill in skillBits)
            {
                haveSkill[skill.Key] = Mem.GetBit(Mem.TreeBitfield, skill.Value);
            }
        }
        private void UpdateTrees()
        {
            foreach (KeyValuePair<string, int> tree in treeBits)
            {
                haveTree[tree.Key] = Mem.GetBit(Mem.TreeBitfield, tree.Value);
            }
        }
        private void UpdateEvents()
        {
            int bf = Mem.KeyEventBitfield;
            haveShards["Water Vein 1"] = Mem.GetBit(bf, 0);
            haveShards["Water Vein 2"] = Mem.GetBit(bf, 1);
            haveShards["Gumon Seal 1"] = Mem.GetBit(bf, 3);
            haveShards["Gumon Seal 2"] = Mem.GetBit(bf, 4);
            haveShards["Sunstone 1"] = Mem.GetBit(bf, 6);
            haveShards["Sunstone 2"] = Mem.GetBit(bf, 7);
            haveEvent["Water Vein"] = Mem.GetBit(bf, 2);
            haveEvent["Gumon Seal"] = Mem.GetBit(bf, 5);
            haveEvent["Sunstone"] = Mem.GetBit(bf, 8);
            haveEvent["Clean Water"] = Mem.GetBit(bf, 9);
            haveEvent["Wind Restored"] = Mem.GetBit(bf, 10);
            mode_force_trees = Mem.GetBit(bf, 11);
            mode_shards = Mem.GetBit(bf, 12);
            mode_warmth_fragments = Mem.GetBit(bf, 13);
            mode_world_tour = Mem.GetBit(bf, 14);
        }
        private void UpdateTeleporters()
        {
            foreach (KeyValuePair<string, int> tp in teleporterBits)
            {
                teleportersActive[tp.Key] = Mem.GetBit(Mem.TeleporterBitfield, tp.Value);
            }
        }
        private void UpdateRelics()
        {
            int bf = 0;
            if (mode_world_tour)
                bf = Mem.RelicBitfield;
            foreach (KeyValuePair<string, int> relic in relicExistsBits)
            {
                relicExists[relic.Key] = Mem.GetBit(bf, relic.Value);
            }
            foreach (KeyValuePair<string, int> relic in relicFoundBits)
            {
                relicFound[relic.Key] = Mem.GetBit(bf, relic.Value);
            }
        }
        private void UpdateWarmthFrags()
        {
            if (!mode_warmth_fragments)
                return;
            current_frags = Mem.MapstoneBitfield >> 9;
            max_frags = Mem.TeleporterBitfield >> 10;
        }
        private void UpdateMapstoneProgression()
        {
            int ms = 0;
            foreach (int bit in mapstoneBits.Values)
            {
                if (Mem.GetBit(Mem.MapstoneBitfield, bit))
                    ms++;
            }
            mapstone_count = ms;
        }
        #endregion

        internal void UpdateTrackerSize(TrackerPixelSizes trackerSize)
        {
            TrackerSize = trackerSize;
            UpdateImages();
            Size = new Size(image_pixel_size, image_pixel_size);
        }
    }
}
