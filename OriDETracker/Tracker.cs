using OriDE.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class Tracker : Form
    {
        public Tracker()
        {
            DoubleBuffered = true;

            // Settings options for Refresh Rate and Tracker Size
            RefreshRate = TrackerSettings.Default.RefreshRate;
            TrackerSize = TrackerSettings.Default.Pixels;

            //Settings window display
            settings = new SettingsLayout(this)
            {
                Visible = false
            };

            InitializeComponent();
            this.moveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;
            this.moveToolStripMenuItem.CheckState = TrackerSettings.Default.Draggable ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
            this.autoUpdateToolStripMenuItem.Checked = TrackerSettings.Default.AutoUpdate;
            this.autoUpdateToolStripMenuItem.CheckState = TrackerSettings.Default.AutoUpdate ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
            this.alwaysOnTopToolStripMenuItem.Checked = TrackerSettings.Default.AlwaysOnTop;
            this.alwaysOnTopToolStripMenuItem.CheckState = TrackerSettings.Default.AlwaysOnTop ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
            this.TopMost = TrackerSettings.Default.AlwaysOnTop;

            //auto update boolean values
            started = false;
            paused = false;

            //load settings (except for those needed to initialize the settings window)
            track_shards = TrackerSettings.Default.Shards;
            current_layout = TrackerSettings.Default.Layout;
            track_shards = TrackerSettings.Default.Shards;
            TrackTeleporters = TrackerSettings.Default.Teleporters;
            TrackTrees = TrackerSettings.Default.Trees;
            font_color = TrackerSettings.Default.FontColoring;
            Opacity = TrackerSettings.Default.Opacity;
            BackColor = TrackerSettings.Default.Background;

            settings.RefreshOpacityBar();

            if (font_color == null)
            {
                font_color = Color.White;
            }
            if (BackColor == null)
            {
                BackColor = Color.Black;
            }

            //initialize the OriMemory module that Devil wrote
            Mem = new OriMemory();
            //start the background loop
            th = new Thread(UpdateLoop)
            {
                IsBackground = true
            };

            scaled_size = new Size(image_pixel_size, image_pixel_size);
            this.UpdateImages();
            this.ChangeLayout();
            font_brush = new SolidBrush(font_color);

            this.ChangeShards();
            this.ChangeMapstone();

            //handles weird exceptions and lets me know if there are potential problems
            if (destroy == 1)
            {
                this.SoftReset();
            }

            int needFont = 1;
            foreach (FontFamily ff in FontFamily.Families)
            {
                if (ff.Name.ToLower() == "amatic sc")
                {
                    map_font = new Font(new FontFamily("Amatic SC"), 24f, FontStyle.Bold);
                    needFont = 0;
                    break;
                }
            }
            if (needFont == 1)
            {
                MessageBox.Show("Please install the included fonts: Amatic SC and Amatic SC Bold");
                if (this.fontDialog_mapstone.ShowDialog() == DialogResult.OK)
                {
                    map_font = fontDialog_mapstone.Font;
                    map_font = new Font(fontDialog_mapstone.Font.FontFamily, 24f, FontStyle.Bold);
                }
                else
                {
                    map_font = new Font(FontFamily.GenericSansSerif, 24f, FontStyle.Bold);
                }
            }
        }

        #region PrivateVariables
        protected static int PIXEL_DEF = (int)TrackerPixelSizes.size640px;
        protected int image_pixel_size = PIXEL_DEF;

        protected Size scaled_size;
        protected TrackerPixelSizes tracker_size;

        protected TrackerLayout current_layout;

        protected Color font_color;
        protected Brush font_brush;
        protected Font map_font;


        protected AutoUpdateRefreshRates refresh_rate;
        protected int refresh_time;

        protected bool display_mapstone = false;
        protected int mapstone_count = 0;

        protected bool world_tour;
        protected bool force_trees;
        protected bool warmth_fragments;
        protected int current_frags;
        protected int max_frags;

        protected bool draggable = TrackerSettings.Default.Draggable;
        protected bool auto_update = TrackerSettings.Default.AutoUpdate;

        protected bool track_teleporters = TrackerSettings.Default.Teleporters;
        protected bool track_trees = TrackerSettings.Default.Trees;
        protected bool track_shards = TrackerSettings.Default.Shards;

        protected OriMemory Mem
        {
            get;
            set;
        }

        protected Thread th;
        protected SettingsLayout settings;

        private readonly Dictionary<int, MapstoneText> mapstone_text_parameters = new Dictionary<int, MapstoneText>(){
            { (int) TrackerPixelSizes.size300px, new MapstoneText(140+6, 190+6, 14) },
            { (int) TrackerPixelSizes.size420px, new MapstoneText(195+9, 268+9, 18) },
            { (int) TrackerPixelSizes.size640px, new MapstoneText(304+13, 417+13, 24) },
            { (int) TrackerPixelSizes.size720px, new MapstoneText(342+15, 471+15, 28) }
        };

        private readonly int destroy = 1;

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
            set { map_font = value; }
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
            set { track_shards = value; }
        }
        public bool TrackTeleporters
        {
            get { return track_teleporters; }
            set { track_teleporters = value; }
        }
        public bool TrackTrees
        {
            get { return track_trees; }
            set { track_trees = value; }
        }

        public int MapstoneCount
        {
            get { return mapstone_count; }
            set { mapstone_count = value; }
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
        //Skills, Trees, Events, Shards, Teleporters, and Relics
        protected Dictionary<String, bool> haveSkill;
        protected Dictionary<String, bool> haveTree;
        protected Dictionary<String, bool> haveEvent;
        protected Dictionary<String, bool> haveShards;
        protected Dictionary<String, bool> teleportersActive;
        protected Dictionary<String, bool> relicExists;
        protected Dictionary<String, bool> relicFound;

        //Bits
        private Dictionary<String, int> treeBits;
        private Dictionary<String, int> skillBits;
        private Dictionary<String, int> teleporterBits;
        private Dictionary<String, int> mapstoneBits;
        private Dictionary<String, int> relicExistsBits;
        private Dictionary<String, int> relicFoundBits;
        #endregion

        #region Images
        protected String DIR = @"Assets_750/";

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
        protected Dictionary<String, Image> teleporterImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicExistImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicFoundImages = new Dictionary<String, Image>();
        
        public void UpdateImages()
        {
            var image_collection = typeof(Tracker).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f => f.FieldType == typeof(Image));
            foreach (var img in image_collection)
            {
                var v = (Image)img.GetValue(this);
                v?.Dispose();
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
                    teleporterImages[zone] = Image.FromFile(DIR + zone + "TP.png");
                }
            }
        }
        #endregion

        //points for mouse clicks (with certain tolerance defined by TOL)
        private const int TOL = 25;
        private Point mapstoneMousePoint = new Point(305, 343);
        private Dictionary<String, Point> eventMousePoint;
        private Dictionary<String, Point> treeMouseLocation;
        private Dictionary<String, Point> skillMousePoint;

        #region SetLayout
        public void ChangeLayout()
        {
            this.current_layout = TrackerLayout.RandomizerAllTrees;
            SetLayoutRandomizerAllTrees();                 
        }
        private void SetLayoutRandomizerAllTrees()
        {
            SetLayoutDefaults();

            display_mapstone = true;
            ChangeMapstone();

            haveTree = new Dictionary<String, bool>(){
                {"Spirit Flame", false},
                {"Wall Jump",    false},
                {"Charge Flame", false},
                {"Double Jump",  false},
                {"Bash",        false},
                {"Stomp",       false},
                {"Glide",       false},
                {"Climb",       false},
                {"Charge Jump",  false},
                {"Grenade",     false},
                {"Dash",       false}
            };
            haveEvent = new Dictionary<String, bool>(){
                {"Water Vein",      false},
                {"Gumon Seal",      false},
                {"Sunstone",        false},
                {"Clean Water",     false},
                {"Wind Restored",   false}
            };

            haveShards = new Dictionary<string, bool>(){
                {"Water Vein 1",     false},
                {"Water Vein 2",     false},
                {"Gumon Seal 1",     false},
                {"Gumon Seal 2",     false},
                {"Sunstone 1",      false},
                {"Sunstone 2",      false},
            };

            eventMousePoint = new Dictionary<string, Point>(){
                {"Water Vein", new Point(221+13, 258+13)},
                {"Gumon Seal", new Point(328+13, 215+13)},
                {"Sunstone",   new Point(428+13, 257+13)},
                {"Wind Restored", new Point(423+13, 365+13)},
                {"Clean Water", new Point(220+13, 360+13)}
            };

            //checkTreeHitbox = true;
            //checkEventHitbox = false;
        }
        private void SetLayoutDefaults()
        {
            SetMouseLocations();
            //checkTreeHitbox = false;
            //checkEventHitbox = false;

            #region Logic
            haveSkill = new Dictionary<String, bool>(){
                {"Spirit Flame",        false},
                {"Wall Jump",    false},
                {"Dash",       false},
                {"Charge Flame", false},
                {"Double Jump",  false},
                {"Bash",        false},
                {"Stomp",       false},
                {"Glide",       false},
                {"Climb",       false},
                {"Charge Jump",  false},
                {"Grenade",     false}
            };
            haveEvent = new Dictionary<String, bool>(){
                {"Water Vein",      false},
                {"Gumon Seal",      false},
                {"Sunstone",        false},
                {"Clean Water",     false},
                {"Warmth Returned", false},
                {"Wind Restored",   false}
            };
            relicExists = new Dictionary<string, bool>()
            {
                {"Glades", false},
                {"Grove", false},
                {"Grotto", false},
                {"Blackroot", false},
                {"Swamp", false},
                {"Ginso", false},
                {"Valley", false},
                {"Misty", false},
                {"Forlorn", false},
                {"Sorrow", false},
                {"Horu", false}
            };
            relicFound = new Dictionary<string, bool>()
            {
                {"Glades", false},
                {"Grove", false},
                {"Grotto", false},
                {"Blackroot", false},
                {"Swamp", false},
                {"Ginso", false},
                {"Valley", false},
                {"Misty", false},
                {"Forlorn", false},
                {"Sorrow", false},
                {"Horu", false}
            };
            teleportersActive = new Dictionary<string, bool>()
            {
                {"Grove", false},
                {"Swamp", false},
                {"Grotto", false},
                {"Valley", false},
                {"Forlorn", false},
                {"Sorrow", false},
                {"Ginso", false},
                {"Horu", false},
                {"Blackroot", false},
                {"Glades", false},
            };
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
            string[] skills = new string[] { "Spirit Flame", "Wall Jump", "Charge Flame", "Double Jump", "Bash", "Stomp", "Glide", "Climb", "Charge Jump", "Grenade", "Dash" };
            for (int i = 0; i < 11; ++i)
            {
                skillMousePoint.Add(skills[i], new Point((int)(320 + 13 + 205 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                                                         (int)(320 + 13 - 205 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }
            for (int i = 0; i < 11; ++i)
            {
                treeMouseLocation.Add(skills[i], new Point((int)(320 + 13 + 286 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                                                           (int)(320 + 13 - 286 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }

            eventMousePoint = new Dictionary<string, Point>(){
                {"Water Vein", new Point(206+13, 240+13)},
                {"Gumon Seal", new Point(328+13, 215+13)},
                {"Sunstone",   new Point(428+13, 257+13)},
                {"Clean Water", new Point(205+13, 343+13)},
                {"Wind Restored", new Point(300+13, 404+13)},
                {"Warmth Returned", new Point(391+13, 342+13)}
            };
        }
        #endregion

        #region EventHandlers
        private void Tracker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && draggable)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        protected void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auto_update = !(auto_update);

            if (auto_update)
            {
                TurnOnAutoUpdate();
            }
            else
            {
                TurnOffAutoUpdate();
            }
        }
        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }
        protected void moveToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            Refresh();
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

            if (display_mapstone && (Math.Sqrt(Square(x - (int)(mapstoneMousePoint.X * mouse_scaling)) + Square(y - (int)(mapstoneMousePoint.Y * mouse_scaling))) <= 2 * CUR_TOL))
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

                scaled_size = new Size(image_pixel_size, image_pixel_size);
                this.Size = scaled_size;
                Rectangle drawRect = new Rectangle(new Point(0, 0), scaled_size);

                #region Draw
                #region Skills

                g.DrawImage(imageGSkills, drawRect);
                foreach (KeyValuePair<String, bool> sk in haveSkill)
                {
                    if (sk.Value)
                    {
                        g.DrawImage(skillImages[sk.Key], drawRect);
                    }
                }
                #endregion

                #region Relic
                foreach (KeyValuePair<String, bool> relic in relicExists)
                {
                    if (relic.Value)
                    {
                        g.DrawImage(relicExistImages[relic.Key], drawRect);
                    }
                }
                foreach (KeyValuePair<String, bool> relic in relicFound)
                {
                    if (relic.Value)
                    {
                        g.DrawImage(relicFoundImages[relic.Key], drawRect);
                    }
                }
                #endregion

                #region Teleporters
                if (TrackTeleporters)
                {
                    foreach (KeyValuePair<String, bool> tp in teleportersActive)
                    {
                        if (tp.Value)
                        {
                            g.DrawImage(teleporterImages[tp.Key], drawRect);
                        }
                    }
                }
                #endregion

                #region Tree
                if (TrackTrees)
                {
                    g.DrawImage(imageGTrees, drawRect);
                    foreach (KeyValuePair<String, bool> sk in haveTree)
                    {
                        if (sk.Value)
                        {
                            g.DrawImage(treeImages[sk.Key], drawRect);
                        }
                    }
                }

                #endregion

                #region Events

                foreach (KeyValuePair<String, bool> ev in haveEvent)
                {
                    if (ev.Value)
                    {
                        g.DrawImage(eventImages[ev.Key], drawRect);
                    }
                    else
                    {
                        g.DrawImage(eventGreyImages[ev.Key], drawRect);
                    }
                }
                #endregion

                #region Shards
                if (track_shards)
                {
                    foreach (KeyValuePair<String, bool> ev in haveShards)
                    {
                        if (ev.Value)
                        {
                            g.DrawImage(shardImages[ev.Key], drawRect);
                        }
                    }
                }
                #endregion

                #region Mapstone
                if (display_mapstone)
                {
                    g.DrawImage(imageMapStone, drawRect);
                    if (font_brush == null)
                    {
                        font_brush = new SolidBrush(Color.White);
                    }
                    map_font = new Font(map_font.FontFamily, mapstone_text_parameters[image_pixel_size].TextSize, FontStyle.Bold);
                    g.DrawString(mapstone_count.ToString() + "/9", map_font, font_brush, new Point(mapstone_text_parameters[image_pixel_size].X, mapstone_text_parameters[image_pixel_size].Y));
                }
                #endregion
                #endregion

                g.DrawImage(imageSkillWheelDouble, drawRect);
            }
            catch (Exception exc)
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
            mapstone_count = 0;

            if (tmp_auto_update)
            {
                this.TurnOnAutoUpdate();
            }

            Refresh();
        }
        protected void SoftReset()
        {
            ClearAll();

            this.settings.Visible = false;

            ChangeLayout();
            if (auto_update && !TrackerSettings.Default.AutoUpdate)
            {
                TurnOffAutoUpdate();
            }
            auto_update = TrackerSettings.Default.AutoUpdate;
            this.autoUpdateToolStripMenuItem.Checked = TrackerSettings.Default.AutoUpdate;

            draggable = TrackerSettings.Default.Draggable;
            this.moveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;
        }
        public void ChangeMapstone()
        {
        }
        public void ChangeShards()
        {
            settings.ChangeShards(track_shards);
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
            if (!(paused) && started)
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
                        this.Invoke((Action)delegate () { labelBlank.Visible = false; });
                    }
                    Thread.Sleep((int)refresh_time);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.StackTrace.ToString());
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
                MessageBox.Show(exc.StackTrace.ToString());
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
            force_trees = Mem.GetBit(bf, 11);
            track_shards = Mem.GetBit(bf, 12);
            warmth_fragments = Mem.GetBit(bf, 13);
            world_tour = Mem.GetBit(bf, 14);
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
            if (world_tour)
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
            if (!warmth_fragments)
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

        private void Tracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            TrackerSettings.Default.FontColoring = font_color;
            TrackerSettings.Default.Background = BackColor;
            TrackerSettings.Default.RefreshRate = RefreshRate;
            TrackerSettings.Default.Layout = current_layout;
            TrackerSettings.Default.Opacity = Opacity;
            TrackerSettings.Default.Shards = track_shards;
            TrackerSettings.Default.Teleporters = TrackTeleporters;
            TrackerSettings.Default.Trees = TrackTrees;
            TrackerSettings.Default.Pixels = TrackerSize;
            TrackerSettings.Default.AlwaysOnTop = this.TopMost;
            TrackerSettings.Default.Draggable = draggable;
            TrackerSettings.Default.AutoUpdate = auto_update;

            TrackerSettings.Default.Save();
        }
    }
}
