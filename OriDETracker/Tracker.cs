using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Threading;
using OriDE.Memory;

namespace OriDETracker
{
    public partial class Tracker : Form
    {
        public Tracker()
        {
            DoubleBuffered = true;

            //Log important things
            //Log = new Logger("OriDETracker-v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            //Log.WriteToLog("**INFO**  : Starting Tracker (v " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")");

            //Form for quickly editting things
            edit_form = new EditForm(this);
            edit_form.Visible = false;

            // Settings options for Refresh Rate and Tracker Size
            RefreshRate = TrackerSettings.Default.RefreshRate;
            TrackerSize = TrackerSettings.Default.Pixels;

            //Settings window display
            settings = new SettingsLayout(this);
            settings.Visible = false;

            InitializeComponent();

            //auto update boolean values
            started = false;
            paused = false;

            //load settings (except for those needed to initialize the settings window)
            display_shards = TrackerSettings.Default.Shards;
            current_layout = TrackerSettings.Default.Layout;
            display_shards = TrackerSettings.Default.Shards;
            TrackTeleporters = TrackerSettings.Default.Teleporters;
            TrackTrees = TrackerSettings.Default.Trees;
            font_color = TrackerSettings.Default.FontColoring;
            Opacity = TrackerSettings.Default.Opacity;
            BackColor = TrackerSettings.Default.Background;

            settings.RefreshOpacityBar();

            if (font_color == null)
            {
                //Log.WriteToLog("**INFO**  : Font Color is null, loading default font color instead");
                font_color = Color.White;
            }
            if (BackColor == null)
            {
                //Log.WriteToLog("**INFO**  : BackColor is null, loading default background color instead");
                BackColor = Color.Black;
            }

            //initialize the OriMemory module that Devil wrote
            mem = new OriMemory();
            //start the background loop
            th = new Thread(UpdateLoop);
            th.IsBackground = true;

            scaledSize = new Size(image_pixel_size, image_pixel_size);
            this.UpdateImages();
            this.ChangeLayout(current_layout);
            font_brush = new SolidBrush(font_color);

            this.ChangeShards();
            this.ChangeMapstone();

            //handles weird exceptions and lets me know if there are potential problems
            if (destroy == 1)
            {
                //Log.WriteToLog("**DEBUG** : Settings may need to be reset.");
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
                //Log.WriteToLog("**DEBUG** : Don't have preferred font so checking with user.");
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

        protected OriMemory mem
        {
            get;
            set;
        }
        protected Thread th;
        protected SettingsLayout settings;
        protected EditForm edit_form;
        //public Logger Log;

        public Color FontColor
        {
            get { return font_color; }
            set { font_color = value; font_brush = new SolidBrush(value); }
        }

        public TrackerPixelSizes TrackerSize
        {
            get { return tracker_size; }
            set { tracker_size = value; image_pixel_size = (int)value; }
        }

        public bool DisplayShards
        {
            get { return display_shards; }
            set { display_shards = value; }
        }

        public bool TrackTeleporters = TrackerSettings.Default.Teleporters;
        public bool TrackTrees = TrackerSettings.Default.Trees;

        public Font MapFont
        {
            set { map_font = value; }
        }

        public int MapstoneCount
        {
            get { return mapstone_count; }
            set { mapstone_count = value; }
        }

        public AutoUpdateRefreshRates RefreshRate
        {
            get { return refresh_rate; }
            set { refresh_rate = value; refresh_time = (int)(1000000.0f / ((float)value)); }
        }

        protected static int PIXEL_DEF = (int) TrackerPixelSizes.size640px;

        protected int image_pixel_size = PIXEL_DEF;
        protected TrackerPixelSizes tracker_size;


        private Dictionary<int, MapstoneText> mapstone_text_parameters = new Dictionary<int, MapstoneText>(){
            { (int) TrackerPixelSizes.size300px, new MapstoneText(140+6, 190+6, 14) },
            { (int) TrackerPixelSizes.size420px, new MapstoneText(195+9, 268+9, 18) },
            { (int) TrackerPixelSizes.size640px, new MapstoneText(304+13, 417+13, 24) },
            { (int) TrackerPixelSizes.size720px, new MapstoneText(342+15, 471+15, 28) }
        };

        protected Size scaledSize;
        protected bool display_shards = TrackerSettings.Default.Shards;
        protected TrackerLayout current_layout;
        protected Color font_color;

        public Brush font_brush;
        protected const int TOL = 25;
        protected bool auto_update = TrackerSettings.Default.AutoUpdate;
        protected bool draggable = TrackerSettings.Default.Draggable;
        protected int mapstone_count = 0;
        protected bool display_mapstone = false;
        protected Font map_font;
        protected AutoUpdateRefreshRates refresh_rate;
        protected int refresh_time;
        protected bool world_tour;
        protected bool force_trees;
        protected bool warmth_fragments;
        protected int current_frags;
        protected int max_frags;

        private int destroy = 1;

        #region FrameMoving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        #endregion

        #region LogicDictionary
        //general: Skills and Events
        public Dictionary<String, bool> haveSkill;
        public Dictionary<String, bool> haveEvent;

        //Relics
        public Dictionary<String, bool> relicExists;
        public Dictionary<String, bool> relicFound;

        public Dictionary<String, bool> teleportersActive;

        //All Trees
        public Dictionary<String, bool> hitTree;
        public Dictionary<String, bool> haveTree;

        //Shards
        public Dictionary<String, bool> haveShards;

        //Bits
        private Dictionary<String, int> treeBits;
        private Dictionary<String, int> skillBits;
        private Dictionary<String, int> teleporterBits;
        private Dictionary<String, int> mapstoneBits;
        private Dictionary<String, int> relicExistsBits;
        private Dictionary<String, int> relicFoundBits;

        /*
		   //All Events
		   private Dictionary<String, bool> haveEventLocation;
		   private Dictionary<String, bool> hitEventLocation;
		 */
        #endregion

        #region Images

        protected String DIR = @"Assets_750/";

        protected Image imageSpiritFlame;
        protected Image imageWallJump;
        protected Image imageChargeFlame;
        protected Image imageDoubleJump;
        protected Image imageBash;
        protected Image imageStomp;
        protected Image imageGlide;
        protected Image imageClimb;
        protected Image imageChargeJump;
        protected Image imageLightGrenade;
        protected Image imageDash;

        protected Image imageTreeSpiritFlame;
        protected Image imageTreeWallJump;
        protected Image imageTreeChargeFlame;
        protected Image imageTreeDoubleJump;
        protected Image imageTreeBash;
        protected Image imageTreeStomp;
        protected Image imageTreeChargeJump;
        protected Image imageTreeGlide;
        protected Image imageTreeClimb;
        protected Image imageTreeLightGrenade;
        protected Image imageTreeDash;

        protected Image imageWaterVein;
        protected Image imageGumonSeal;
        protected Image imageSunstone;
        protected Image imageCleanWater;
        protected Image imageWindRestored;
        protected Image imageWarmthReturned;

        protected Image imageGWaterVein;
        protected Image imageGGumonSeal;
        protected Image imageGSunstone;
        protected Image imageGCleanWater;
        protected Image imageGWindRestored;
        protected Image imageGWarmthReturned;

        protected Image imageWindRestoredRando;
        protected Image imageGWindRestoredRando;

        protected Image imageWaterVeinShard1;
        protected Image imageWaterVeinShard2;

        protected Image imageGumonSealShard1;
        protected Image imageGumonSealShard2;

        protected Image imageSunstoneShard1;
        protected Image imageSunstoneShard2;

        protected Image imageSkillWheel;
        protected Image imageSkillWheelDouble;
        protected Image imageSkillWheelTriple;
        protected Image imageBlackBackground;
        protected Image imageGSkills;
        protected Image imageGTrees;

        protected Image imageMapStone;

        public void UpdateImages()
        {
            DIR = "Assets_" + image_pixel_size.ToString() + @"/";

            imageSpiritFlame = Image.FromFile(DIR + @"SpiritFlame.png");
            imageWallJump = Image.FromFile(DIR + @"WallJump.png");
            imageChargeFlame = Image.FromFile(DIR + @"ChargeFlame.png");
            imageDoubleJump = Image.FromFile(DIR + @"DoubleJump.png");
            imageBash = Image.FromFile(DIR + @"Bash.png");
            imageStomp = Image.FromFile(DIR + @"Stomp.png");
            imageGlide = Image.FromFile(DIR + @"Glide.png");
            imageClimb = Image.FromFile(DIR + @"Climb.png");
            imageChargeJump = Image.FromFile(DIR + @"ChargeJump.png");
            imageLightGrenade = Image.FromFile(DIR + @"LightGrenade.png");
            imageDash = Image.FromFile(DIR + @"Dash.png");

            imageTreeSpiritFlame = Image.FromFile(DIR + @"TSpiritFlame.png");
            imageTreeWallJump = Image.FromFile(DIR + @"TWallJump.png");
            imageTreeChargeFlame = Image.FromFile(DIR + @"TChargeFlame.png");
            imageTreeDoubleJump = Image.FromFile(DIR + @"TDoubleJump.png");
            imageTreeBash = Image.FromFile(DIR + @"TBash.png");
            imageTreeStomp = Image.FromFile(DIR + @"TStomp.png");
            imageTreeChargeJump = Image.FromFile(DIR + @"TChargeJump.png");
            imageTreeGlide = Image.FromFile(DIR + @"TGlide.png");
            imageTreeClimb = Image.FromFile(DIR + @"TClimb.png");
            imageTreeLightGrenade = Image.FromFile(DIR + @"TLightGrenade.png");
            imageTreeDash = Image.FromFile(DIR + @"TDash.png");

            imageWaterVein = Image.FromFile(DIR + @"WaterVein.png");
            imageGumonSeal = Image.FromFile(DIR + @"GumonSeal.png");
            imageSunstone = Image.FromFile(DIR + @"Sunstone.png");
            imageCleanWater = Image.FromFile(DIR + @"CleanWater.png");
            imageWindRestored = Image.FromFile(DIR + @"WindRestored.png");
            imageWarmthReturned = Image.FromFile(DIR + @"WarmthReturned.png");

            imageGWaterVein = Image.FromFile(DIR + @"GWaterVein.png");
            imageGGumonSeal = Image.FromFile(DIR + @"GGumonSeal.png");
            imageGSunstone = Image.FromFile(DIR + @"GSunstone.png");
            imageGCleanWater = Image.FromFile(DIR + @"GCleanWater.png");
            imageGWindRestored = Image.FromFile(DIR + @"GWindRestored.png");
            imageGWarmthReturned = Image.FromFile(DIR + @"GWarmthReturned.png");

            imageSkillWheel = Image.FromFile(DIR + @"SkillRing_Single.png");
            imageSkillWheelDouble = Image.FromFile(DIR + @"SkillRing_Double.png");
            imageSkillWheelTriple = Image.FromFile(DIR + @"SkillRing_Triple.png");

            imageBlackBackground = Image.FromFile(DIR + @"BlackBackground.png");
            imageGSkills = Image.FromFile(DIR + @"GreySkillTree.png");
            imageGTrees = Image.FromFile(DIR + @"GreyTrees.png");

            imageMapStone = Image.FromFile(DIR + @"MapStone.png");

            imageWindRestoredRando = Image.FromFile(DIR + @"WindRestoredRando.png");
            imageGWindRestoredRando = Image.FromFile(DIR + @"GWindRestoredRando.png");

            imageWaterVeinShard1 = Image.FromFile(DIR + @"WaterVeinShard1.png");
            imageWaterVeinShard2 = Image.FromFile(DIR + @"WaterVeinShard2.png");

            imageGumonSealShard1 = Image.FromFile(DIR + @"GumonSealShard1.png");
            imageGumonSealShard2 = Image.FromFile(DIR + @"GumonSealShard2.png");

            imageSunstoneShard1 = Image.FromFile(DIR + @"SunstoneShard1.png");
            imageSunstoneShard2 = Image.FromFile(DIR + @"SunstoneShard2.png");
            foreach(string zone in new string[] { "Glades", "Grove", "Grotto", "Ginso", "Swamp", "Valley", "Misty", "Blackroot", "Sorrow", "Forlorn", "Horu"} )
            {
                relicExistImages[zone] = Image.FromFile(DIR + "Relics/Exist/" + zone + ".png");
                relicFoundImages[zone] = Image.FromFile(DIR + "Relics/Found/" + zone + ".png");
                if(zone != "Misty")
                {
                    teleporterImages[zone] = Image.FromFile(DIR + zone + "TP.png");
                }
            }

        }

        protected Dictionary<String, Image> teleporterImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicExistImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> relicFoundImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> skillImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> treeImages = new Dictionary<String, Image>();

        protected Dictionary<String, Image> eventImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> eventGreyImages = new Dictionary<String, Image>();

        protected Dictionary<String, Image> shardImages = new Dictionary<string, Image>();

        #endregion

        #region Hitbox
        //Game hitboxes for trees and events
        //private bool checkTreeHitbox = false;
        //private bool checkEventHitbox = false;

        /*
    private Dictionary<Skill, HitBox> treeHitboxes = new Dictionary<Skill, HitBox>(){
        {Skill.Sein,        new HitBox("-165,-262,1,2")},
        {Skill.WallJump,    new HitBox("-317,-301,5,6")},
        {Skill.ChargeFlame, new HitBox("-53,-153,5,6")},
        {Skill.Dash,        new HitBox("293,-251,5,6")},
        {Skill.DoubleJump,  new HitBox("785,-404,5,6")},
        {Skill.Bash,        new HitBox("532,334,5,6")},
        {Skill.Stomp,       new HitBox("859,-88,5,6")},
        {Skill.Glide,       new HitBox("-458,-13,5,6")},
        {Skill.Climb,       new HitBox("-1189,-95,5,6")},
        {Skill.ChargeJump,  new HitBox("-697,413,5,6")},
        {Skill.Grenade,     new HitBox("69,-373,5,6")}
    };

    //placeholder until I get the actual coordinates
    private Dictionary<String, HitBox> eventHitboxes = new Dictionary<String, HitBox>(){
        {"Water Vein",      new HitBox( "0,0,1,1")},
        {"Gumon Seal",      new HitBox( "0,0,1,1")},
        {"Sunstone",        new HitBox( "0,0,1,1")},
        {"Clean Water",     new HitBox( "0,0,1,1")},
        {"Wind Restored",   new HitBox( "0,0,1,1")},
        {"Warmth Returned", new HitBox( "0,0,1,1")},
    };
    */
        #endregion

        //points for mouse clicks (with certain tolerance defined by TOL)
        private Point mapstoneMousePoint = new Point(305, 343);
        private Dictionary<String, Point> eventMousePoint;

        #region SetLayout
        public void ChangeLayout(TrackerLayout layout)
        {
            this.current_layout = layout;
            switch (layout)
            {
                case TrackerLayout.AllCells:
                    SetLayoutAllCells();
                    break;
                case TrackerLayout.AllSkills:
                    SetLayoutAllSkills();
                    break;
                case TrackerLayout.ReverseEventOrder:
                    SetLayoutReverseEventOrder();
                    break;
                case TrackerLayout.RandomizerAllTrees:
                    SetLayoutRandomizerAllTrees();
                    break;
                case TrackerLayout.RandomizerAllEvents:
                    SetLayoutRandomizerAllEvents();
                    break;
                default:
                    break;
            }
        }
        private void SetLayoutRandomizerAllTrees()
        {
            SetLayoutDefaults();

            display_mapstone = true;
            ChangeMapstone();

            hitTree = new Dictionary<String, bool>(){
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

            haveTree = new Dictionary<String, bool>(){
                {"Spirit Flame",        false},
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

            eventImages = new Dictionary<String, Image>(){
                {"Water Vein",       imageWaterVein},
                {"Gumon Seal",       imageGumonSeal},
                {"Sunstone",         imageSunstone},
                {"Wind Restored",    imageWindRestoredRando},
                {"Clean Water",      imageCleanWater}
            };

            eventGreyImages = new Dictionary<String, Image>(){
                {"Water Vein",      imageGWaterVein},
                {"Gumon Seal",      imageGGumonSeal},
                {"Sunstone",        imageGSunstone},
                {"Wind Restored",   imageGWindRestoredRando},
                {"Clean Water",     imageGCleanWater}
            };

            shardImages = new Dictionary<string, Image>(){
                {"Water Vein 1",     imageWaterVeinShard1},
                {"Water Vein 2",     imageWaterVeinShard2},
                {"Gumon Seal 1",     imageGumonSealShard1},
                {"Gumon Seal 2",     imageGumonSealShard2},
                {"Sunstone 1",      imageSunstoneShard1},
                {"Sunstone 2",      imageSunstoneShard2},
            };

            treeImages = new Dictionary<String, Image>(){
                {"Spirit Flame",        imageTreeSpiritFlame},
                {"Wall Jump",    imageTreeWallJump},
                {"Charge Flame", imageTreeChargeFlame},
                {"Dash",        imageTreeDash},
                {"Double Jump",  imageTreeDoubleJump},
                {"Bash",        imageTreeBash},
                {"Stomp",       imageTreeStomp},
                {"Glide",       imageTreeGlide},
                {"Climb",       imageTreeClimb},
                {"Charge Jump",  imageTreeChargeJump},
                {"Grenade",     imageTreeLightGrenade}
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

            skillImages = new Dictionary<String, Image>(){
                {"Spirit Flame",        imageSpiritFlame},
                {"Wall Jump",    imageWallJump},
                {"Charge Flame", imageChargeFlame},
                {"Dash",        imageDash},
                {"Double Jump",  imageDoubleJump},
                {"Bash",        imageBash},
                {"Stomp",       imageStomp},
                {"Glide",       imageGlide},
                {"Climb",       imageClimb},
                {"Charge Jump",  imageChargeJump},
                {"Grenade",     imageLightGrenade}
            };

            eventImages = new Dictionary<String, Image>(){
                {"Water Vein",       imageWaterVein},
                {"Gumon Seal",      imageGumonSeal},
                {"Sunstone",        imageSunstone},
                {"Clean Water",     imageCleanWater},
                {"Wind Restored",    imageWindRestored},
                {"Warmth Returned", imageWarmthReturned}
            };

            eventGreyImages = new Dictionary<String, Image>(){
                {"Water Vein",      imageGWaterVein},
                {"Gumon Seal",      imageGGumonSeal},
                {"Sunstone",        imageGSunstone},
                { "Clean Water",     imageGCleanWater},
                {"Wind Restored",   imageGWindRestored},
                {"Warmth Returned", imageGWarmthReturned}
            };
        }

        private void SetLayoutRandomizerAllEvents()
        {
            SetLayoutDefaults();
        }
        private void SetLayoutAllSkills()
        {
            SetLayoutDefaults();
        }
        private void SetLayoutAllCells()
        {
            SetLayoutDefaults();
        }
        private void SetLayoutReverseEventOrder()
        {
            SetLayoutDefaults();
        }
        private Dictionary<String, Point> treeMouseLocation;
        private Dictionary<String, Point> skillMousePoint;
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

        protected void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.HardReset();
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

            //MessageBox.Show("X: " + x + "   Y: " + y);
            if (ToggleMouseClick(x, y))
            {
                //Log.WriteToLog("**INFO**  : Mouse Click at X: " + x + "   Y: " + y);

                bool tmp_auto_update = auto_update;
                //try turning off auto update for a moment
                if (tmp_auto_update)
                    this.TurnOffAutoUpdate();
                this.Refresh();
                if (tmp_auto_update)
                    this.TurnOnAutoUpdate();
            }
            //this.Invalidate();
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
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.edit_form.Show();
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
                        if (hitTree.ContainsKey(sk.Key))
                        {
                            hitTree[sk.Key] = haveTree[sk.Key];
                        }
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
                                if (display_shards)
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

                scaledSize = new Size(image_pixel_size, image_pixel_size);
                this.Size = scaledSize;
                Rectangle drawRect = new Rectangle(new Point(0, 0), scaledSize);

                #region Draw
                #region Skills

                g.DrawImage(imageGSkills, drawRect);
                foreach (KeyValuePair<String, bool> sk in haveSkill)
                {
                    edit_form.UpdateSkill(sk.Key, sk.Value);

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
                if(TrackTeleporters)
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
                if(TrackTrees)
                {
                    g.DrawImage(imageGTrees, drawRect);
                    foreach (KeyValuePair<String, bool> sk in haveTree)
                    {
                        edit_form.UpdateTree(sk.Key, sk.Value);

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
                    edit_form.UpdateEvent(ev.Key, ev.Value);

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
                if (display_shards)
                {
                    foreach (KeyValuePair<String, bool> ev in haveShards)
                    {
                        edit_form.UpdateShard(ev.Key, ev.Value);

                        if (ev.Value)
                        {
                            g.DrawImage(shardImages[ev.Key], drawRect);
                        }
                    }
                }
                #endregion
                if (display_mapstone)
                {
                    edit_form.UpdateMapstones(mapstone_count);

                    g.DrawImage(imageMapStone, drawRect);
                    if (font_brush == null)
                    {
                        font_brush = new SolidBrush(Color.White);
                    }
                    map_font = new Font(map_font.FontFamily, mapstone_text_parameters[image_pixel_size].TextSize, FontStyle.Bold);
                    g.DrawString(mapstone_count.ToString() + "/9", map_font, font_brush, new Point(mapstone_text_parameters[image_pixel_size].X, mapstone_text_parameters[image_pixel_size].Y));
                }
                #endregion

                g.DrawImage(imageSkillWheelDouble, drawRect);
            }
            catch //(Exception exc)
            {
                /*
                Log.WriteToLog("**ERROR** : Exception thrown, details follow below.");
                Log.WriteToLog("**INFO**  : Message = " + exc.Message.ToString());
                Log.WriteToLog("**INFO**  : Source = " + exc.Source.ToString());
                Log.WriteToLog("**INFO**  : Stack Trace = " + exc.StackTrace.ToString());
                Log.WriteToLog("**INFO**  : Target Site = " + exc.TargetSite.ToString());
                Log.WriteToLog("**INFO**  : Data = " + exc.Data.ToString());
                Log.WriteToLog("**INFO**  : " + exc.ToString());
                */
            }
            //Refresh();
        }

        protected void ClearAll()
        {
            //Log.WriteToLog("**INFO**  : Clearing the tracker.");
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
            for (int i = 0; i < hitTree.Count; i++)
            {
                hitTree[hitTree.ElementAt(i).Key] = false;
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
            edit_form.Clear();

            if (tmp_auto_update)
            {
                this.TurnOnAutoUpdate();
            }

            Refresh();
        }

        protected void SoftReset()
        {
            ClearAll();

            //Log.WriteToLog("**INFO**  : Performing soft reset.");

            this.settings.Visible = false;

            ChangeLayout(current_layout);
            if (auto_update && !TrackerSettings.Default.AutoUpdate)
            {
                TurnOffAutoUpdate();
            }
            auto_update = TrackerSettings.Default.AutoUpdate;
            this.autoUpdateToolStripMenuItem.Checked = TrackerSettings.Default.AutoUpdate;

            draggable = TrackerSettings.Default.Draggable;
            this.moveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;

        }

        protected void HardReset()
        {
            DialogResult res = MessageBox.Show("Your settings will be lost!", "Really reset?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            if (res == DialogResult.Cancel)
            {
                return;
            }
            this.SoftReset();

            settings.Reset();
            edit_form.Reset();

            TrackerSettings.Default.Reset();

            this.Opacity = 1.0;
            this.TrackerSize = (TrackerPixelSizes)PIXEL_DEF;

            this.RefreshRate = (AutoUpdateRefreshRates)1000;

            current_layout = TrackerLayout.RandomizerAllTrees;

            this.moveToolStripMenuItem.Checked = TrackerSettings.Default.Draggable;

            this.TopMost = TrackerSettings.Default.AlwaysOnTop;
            this.alwaysOnTopToolStripMenuItem.Checked = TrackerSettings.Default.AlwaysOnTop;

            this.display_shards = TrackerSettings.Default.Shards;

            this.BackColor = Color.Black;
            this.font_brush = new SolidBrush(Color.White);

            Refresh();
        }

        public void ChangeMapstone()
        {
            edit_form.ChangeMapstone(display_mapstone);
        }
        public void ChangeShards()
        {
            settings.ChangeShards(display_shards);
            edit_form.ChangeShards(display_shards);
        }

        #endregion

        #region AutoUpdate

        bool paused;
        bool started;
        protected void TurnOnAutoUpdate()
        {
            if (started && paused)
            {
                //Log.WriteToLog("**DEBUG** : Resuming auto update thread.");
                th.Resume();
                started = true;
                paused = false;
            }
            else if (!(started))
            {
                //Log.WriteToLog("**DEBUG** : Starting auto update thread.");
                th.Start();
                started = true;
                paused = false;
            }
            else
            {
                /*
                Log.WriteToLog("**ERROR** : Cannot start Auto Update if it is already running");
                Log.WriteToLog("**INFO**  : `paused` = " + paused.ToString());
                Log.WriteToLog("**INFO**  : `started` = " + started.ToString());
                */
            }
        }

        protected void TurnOffAutoUpdate()
        {
            if (!(paused) && started)
            {
                //Log.WriteToLog("**DEBUG** : Suspending auto update thread.");
                th.Suspend();
                started = true;
                paused = true;
            }
            else if (!(started) || paused)
            {
                /*
                Log.WriteToLog("**ERROR** : Cannot pause Auto Update if it is not running");
                Log.WriteToLog("**INFO**  : `paused` = " + paused.ToString());
                Log.WriteToLog("**INFO**  : `started` = " + started.ToString());
                */
            }
        }

        int DATA_SIZE = 10;
        public byte[] data;

        private void UpdateLoop()
        {
            bool lastHooked = false;
            while (true)
            {
                try
                {
                    bool hooked = mem.HookProcess();
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
                    Console.Out.WriteLine(exc);
                    /*
                    Log.WriteToLog("**ERROR** : Exception thrown, details follow below.");
                    Log.WriteToLog("**INFO**  : Message = " + exc.Message.ToString());
                    Log.WriteToLog("**INFO**  : Source = " + exc.Source.ToString());
                    Log.WriteToLog("**INFO**  : Stack Trace = " + exc.StackTrace.ToString());
                    Log.WriteToLog("**INFO**  : Target Site = " + exc.TargetSite.ToString());
                    Log.WriteToLog("**INFO**  : Data = " + exc.Data.ToString());
                    Log.WriteToLog("**INFO**  : " + exc.ToString());
                    */
                }
            }
        }

        private void UpdateValues()
        {
            try
            {
                mem.GetBitfields();
                UpdateSkills();
                CheckTrees();
                UpdateKeysEvents();
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
            catch (Exception err) {
                MessageBox.Show(err.StackTrace.ToString());

            }
        }

        private void UpdateSkills()
        {
            foreach (KeyValuePair<string, int> skill in skillBits)
            {
                haveSkill[skill.Key] = mem.GetBit(mem.TreeBitfield, skill.Value);
            }
        }
        private void CheckTrees()
        {
            foreach (KeyValuePair<string, int> tree in treeBits)
            {
                haveTree[tree.Key] = mem.GetBit(mem.TreeBitfield, tree.Value);
            }
        }
        private void UpdateRelics()
        {
            int bf = 0;
            if (world_tour)
                bf = mem.RelicBitfield;
            foreach (KeyValuePair<string, int> relic in relicExistsBits)
            {
                relicExists[relic.Key] = mem.GetBit(bf, relic.Value);
            }
            foreach (KeyValuePair<string, int> relic in relicFoundBits)
            {
                relicFound[relic.Key] = mem.GetBit(bf, relic.Value);
            }
        }

        private void UpdateKeysEvents()
        {
            int bf = mem.KeyEventBitfield;
            haveShards["Water Vein 1"] = mem.GetBit(bf, 0);
            haveShards["Water Vein 2"] = mem.GetBit(bf, 1);
            haveShards["Gumon Seal 1"] = mem.GetBit(bf, 3);
            haveShards["Gumon Seal 2"] = mem.GetBit(bf, 4);
            haveShards["Sunstone 1"] = mem.GetBit(bf, 6);
            haveShards["Sunstone 2"] = mem.GetBit(bf, 7);
            haveEvent["Water Vein"] = mem.GetBit(bf, 2);
            haveEvent["Gumon Seal"] = mem.GetBit(bf, 5);
            haveEvent["Sunstone"] = mem.GetBit(bf, 8);
            haveEvent["Clean Water"] = mem.GetBit(bf, 9);
            haveEvent["Wind Restored"] = mem.GetBit(bf, 10);
            force_trees = mem.GetBit(bf, 11);
            display_shards = mem.GetBit(bf, 12);
            warmth_fragments = mem.GetBit(bf, 13);
            world_tour = mem.GetBit(bf, 14);
        }

        private void UpdateWarmthFrags()
        {
            if (!warmth_fragments)
                return;
            current_frags = mem.MapstoneBitfield >> 9;
            max_frags = mem.TeleporterBitfield >> 10;
        }

        private void UpdateTeleporters()
        {
            foreach (KeyValuePair<string, int> tp in teleporterBits)
            {
                teleportersActive[tp.Key] = mem.GetBit(mem.TeleporterBitfield, tp.Value);
            }
        }


        private void UpdateMapstoneProgression()
        {
            int ms = 0;
            foreach(int bit in mapstoneBits.Values)
            {
                if (mem.GetBit(mem.MapstoneBitfield, bit))
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
            TrackerSettings.Default.Shards = display_shards;
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
