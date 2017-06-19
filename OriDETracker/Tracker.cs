using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OriDE.Memory;
using System.Threading;

namespace OriDETracker
{
    public enum TrackerLayout
    {
        RandomizerAllTrees,
        RandomizerAllEvents,
        AllSkills,
        AllCells,
        ReverseEventOrder
    }

    public partial class Tracker : Form
    {
        public Tracker()
        {
            InitializeComponent();

            scaling = Properties.Settings.Default.Scaling;
            current_layout = Properties.Settings.Default.Layout;
            this.Opacity = Properties.Settings.Default.Opacity;

            settings = new SettingsLayout(this);
            settings.Visible = false;

            mem = new OriDE.Memory.OriMemory();
            th = new Thread(UpdateLoop);
            th.IsBackground = true;

            this.ChangeLayout(current_layout);
        }

        protected OriMemory mem { get; set; }
        protected Thread th;
        protected SettingsLayout settings;
        protected TrackerLayout current_layout;

        public float Scaling { get { return scaling; } set { scaling = value; } }

        protected const int TOL = 25;
        protected bool auto_update = false;
        protected bool draggable = false;
        protected float scaling = 0.4f;
        private Size scaledSize = new Size(600, 600);

        protected static Size DEFAULTSIZE = new Size(600, 600);

        #region FrameMoving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        #endregion

        #region LogicDictionary
        //general: Skills and Events
        private Dictionary<Skill, bool> haveSkill;
        private Dictionary<String, bool> haveEvent;

        //All Trees
        private Dictionary<Skill, bool> hitTree;
        private Dictionary<Skill, bool> haveTree;

        /*
        //All Events
        private Dictionary<String, bool> haveEventLocation;
        private Dictionary<String, bool> hitEventLocation;
        */
        #endregion

        #region Images

        protected const String DIR = @"SmallAssets/";

        protected Image imageSpiritFlame  = Image.FromFile(DIR + @"SpiritFlame.png");
        protected Image imageWallJump     = Image.FromFile(DIR + @"WallJump.png");
        protected Image imageChargeFlame  = Image.FromFile(DIR + @"ChargeFlame.png");
        protected Image imageDoubleJump   = Image.FromFile(DIR + @"DoubleJump.png");
        protected Image imageBash         = Image.FromFile(DIR + @"Bash.png");
        protected Image imageStomp        = Image.FromFile(DIR + @"Stomp.png");
        protected Image imageGlide        = Image.FromFile(DIR + @"Glide.png");
        protected Image imageClimb        = Image.FromFile(DIR + @"Climb.png");
        protected Image imageChargeJump   = Image.FromFile(DIR + @"ChargeJump.png");
        protected Image imageLightGrenade = Image.FromFile(DIR + @"LightGrenade.png");
        protected Image imageDash         = Image.FromFile(DIR + @"Dash.png");

        /*
        protected Image imageGSpiritFlame   = Image.FromFile(DIR + @"GSpiritFlame.png");
        protected Image imageGWallJump      = Image.FromFile(DIR + @"GWallJump.png");
        protected Image imageGChargeFlame   = Image.FromFile(DIR + @"GChargeFlame.png");
        protected Image imageGDoubleJump    = Image.FromFile(DIR + @"GDoubleJump.png");
        protected Image imageGBash          = Image.FromFile(DIR + @"GBash.png");
        protected Image imageGStomp         = Image.FromFile(DIR + @"GStomp.png");
        protected Image imageGChargeJump    = Image.FromFile(DIR + @"GChargeJump.png");
        protected Image imageGClimb         = Image.FromFile(DIR + @"GClimb.png");
        protected Image imageGGlide         = Image.FromFile(DIR + @"GGlide.png");
        protected Image imageGLightGrenade  = Image.FromFile(DIR + @"GLightGrenade.png");
        protected Image imageGDash          = Image.FromFile(DIR + @"GDash.png");
        */

        protected Image imageTreeSpiritFlame  = Image.FromFile(DIR + @"TSpiritFlame.png");
        protected Image imageTreeWallJump     = Image.FromFile(DIR + @"TWallJump.png");
        protected Image imageTreeChargeFlame  = Image.FromFile(DIR + @"TChargeFlame.png");
        protected Image imageTreeDoubleJump   = Image.FromFile(DIR + @"TDoubleJump.png");
        protected Image imageTreeBash         = Image.FromFile(DIR + @"TBash.png");
        protected Image imageTreeStomp        = Image.FromFile(DIR + @"TStomp.png");
        protected Image imageTreeChargeJump   = Image.FromFile(DIR + @"TChargeJump.png");
        protected Image imageTreeGlide        = Image.FromFile(DIR + @"TGlide.png");
        protected Image imageTreeClimb        = Image.FromFile(DIR + @"TClimb.png");
        protected Image imageTreeLightGrenade = Image.FromFile(DIR + @"TLightGrenade.png");
        protected Image imageTreeDash         = Image.FromFile(DIR + @"TDash.png");

        /*
        protected Image imageGTreeSpiritFlame  = Image.FromFile(DIR + @"GTSpiritFlame.png");
        protected Image imageGTreeWallJump     = Image.FromFile(DIR + @"GTWallJump.png");
        protected Image imageGTreeChargeFlame  = Image.FromFile(DIR + @"GTChargeFlame.png");
        protected Image imageGTreeDoubleJump   = Image.FromFile(DIR + @"GTDoubleJump.png");
        protected Image imageGTreeBash         = Image.FromFile(DIR + @"GTBash.png");
        protected Image imageGTreeStomp        = Image.FromFile(DIR + @"GTStomp.png");
        protected Image imageGTreeChargeJump   = Image.FromFile(DIR + @"GTChargeJump.png");
        protected Image imageGTreeGlide        = Image.FromFile(DIR + @"GTGlide.png");
        protected Image imageGTreeClimb        = Image.FromFile(DIR + @"GTClimb.png");
        protected Image imageGTreeLightGrenade = Image.FromFile(DIR + @"GTLightGrenade.png");
        protected Image imageGTreeDash         = Image.FromFile(DIR + @"GTDash.png");
        */

        protected Image imageWaterVein      = Image.FromFile(DIR + @"WaterVein.png");
        protected Image imageGumonSeal      = Image.FromFile(DIR + @"GumonSeal.png");
        protected Image imageSunstone       = Image.FromFile(DIR + @"Sunstone.png");
        protected Image imageCleanWater     = Image.FromFile(DIR + @"CleanWater.png");
        protected Image imageWindRestored   = Image.FromFile(DIR + @"WindRestored.png");
        protected Image imageWarmthReturned = Image.FromFile(DIR + @"WarmthReturned.png");

        protected Image imageGWaterVein      = Image.FromFile(DIR + @"GWaterVein.png");
        protected Image imageGGumonSeal      = Image.FromFile(DIR + @"GGumonSeal.png");
        protected Image imageGSunstone       = Image.FromFile(DIR + @"GSunstone.png");
        protected Image imageGCleanWater     = Image.FromFile(DIR + @"GCleanWater.png");
        protected Image imageGWindRestored   = Image.FromFile(DIR + @"GWindRestored.png");
        protected Image imageGWarmthReturned = Image.FromFile(DIR + @"GWarmthReturned.png");

        protected Image imageSkillWheel =       Image.FromFile(DIR + @"SkillRing_Single.png");
        protected Image imageSkillWheelDouble = Image.FromFile(DIR + @"SkillRing_Double.png");
        protected Image imageSkillWheelTriple = Image.FromFile(DIR + @"SkillRing_Triple.png");

        protected Image imageBlackBackground = Image.FromFile(DIR + @"BlackBackground.png");
        protected Image imageGSkills = Image.FromFile(DIR + @"GreySkillTree.png");
        protected Image imageGTrees = Image.FromFile(DIR + @"GreyTrees.png");

        protected Dictionary<Skill, Image> skillImages = new Dictionary<Skill, Image>();
        //protected Dictionary<Skill, Image> skillGreyImages = new Dictionary<Skill, Image>();

        protected Dictionary<String, Image> eventImages = new Dictionary<String, Image>();
        protected Dictionary<String, Image> eventGreyImages = new Dictionary<String, Image>();

        protected Dictionary<Skill, Image> treeImages = new Dictionary<Skill, Image>();
        //protected Dictionary<Skill, Image> treeGreyImages = new Dictionary<Skill, Image>();

        protected Dictionary<Skill, TrackerPictureBox> skillPicBox = new Dictionary<Skill, TrackerPictureBox>();

        protected TrackerPictureBox test1;
        protected TrackerPictureBox test2;

        #endregion

        #region Hitbox
        //Game hitboxes for trees and events
        private bool checkTreeHitbox = false;
        private bool checkEventHitbox = false;

        private Dictionary<Skill, HitBox> treeHitboxes = new Dictionary<Skill, HitBox>()
        {
            { Skill.Sein,        new HitBox("-165,-262,1,2") },
            { Skill.WallJump,    new HitBox("-317,-301,5,6") },
            { Skill.ChargeFlame, new HitBox("-53,-153,5,6") },
            { Skill.Dash,        new HitBox("293,-251,5,6") },
            { Skill.DoubleJump,  new HitBox("785,-404,5,6") },
            { Skill.Bash,        new HitBox("532,334,5,6") },
            { Skill.Stomp,       new HitBox("859,-88,5,6") },
            { Skill.Glide,       new HitBox("-458,-13,5,6") },
            { Skill.Climb,       new HitBox("-1189,-95,5,6") },
            { Skill.ChargeJump,  new HitBox("-697,413,5,6") },
            { Skill.Grenade,     new HitBox("69,-373,5,6") }
        }; 
           
        //placeholder until I get the actual coordinates
        private Dictionary<String, HitBox> eventHitboxes = new Dictionary<String, HitBox>()
        {
            {"Water Vein",      new HitBox( "0,0,1,1") },
            {"Gumon Seal",      new HitBox( "0,0,1,1") },
            {"Sunstone",        new HitBox( "0,0,1,1") },
            {"Clear Water",     new HitBox( "0,0,1,1") },
            {"Wind Restored",   new HitBox( "0,0,1,1") },
            {"Warmth Returned", new HitBox( "0,0,1,1") },
        };
        #endregion        

        //points for mouse clicks (with certain tolerance defined by TOL)
        private Dictionary<Skill, Point> skillMousePoint = new Dictionary<Skill, Point>();
        private Dictionary<String, Point> eventMousePoint = new Dictionary<string, Point>();
        private Dictionary<Skill, Point> treeMouseLocation = new Dictionary<Skill, Point>();
        //private Dictionary<String, Point> eventMouseLocation;

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
            hitTree = new Dictionary<Skill, bool>()
            {
                { Skill.Sein,        false },
                { Skill.WallJump,    false },
                { Skill.ChargeFlame, false },
                { Skill.DoubleJump,  false },
                { Skill.Bash,        false },
                { Skill.Stomp,       false },
                { Skill.Glide,       false },
                { Skill.Climb,       false },
                { Skill.ChargeJump,  false },
                { Skill.Grenade,     false },
                { Skill.Dash ,       false }
            };

            haveTree = new Dictionary<Skill, bool>()
            {
                { Skill.Sein,        false },
                { Skill.WallJump,    false },
                { Skill.ChargeFlame, false },
                { Skill.DoubleJump,  false },
                { Skill.Bash,        false },
                { Skill.Stomp,       false },
                { Skill.Glide,       false },
                { Skill.Climb,       false },
                { Skill.ChargeJump,  false },
                { Skill.Grenade,     false },
                { Skill.Dash ,       false }
            };
            haveEvent = new Dictionary<String, bool>()
            {
                { "Water Vein",      false },
                { "Gumon Seal",      false },
                { "Sunstone",        false },
                { "Warmth Returned", false }, //this is actually clean water
                { "Wind Restored",   false }
            };

            eventImages = new Dictionary<String, Image>()
            {
                { "Water Vein",       imageWaterVein },
                { "Gumon Seal",      imageGumonSeal },
                { "Sunstone",        imageSunstone },
                { "Wind Restored",    imageWindRestored },
                { "Warmth Returned", imageCleanWater }
            };

            eventGreyImages = new Dictionary<String, Image>()
            {
                { "Water Vein",      imageGWaterVein },
                { "Gumon Seal",      imageGGumonSeal },
                { "Sunstone",        imageGSunstone },
                { "Wind Restored",   imageGWindRestored },
                { "Warmth Returned", imageGCleanWater}
            };

            treeImages = new Dictionary<Skill, Image>()
            {
                { Skill.Sein,        imageTreeSpiritFlame },
                { Skill.WallJump,    imageTreeWallJump },
                { Skill.ChargeFlame, imageTreeChargeFlame },
                { Skill.Dash,        imageTreeDash },
                { Skill.DoubleJump,  imageTreeDoubleJump },
                { Skill.Bash,        imageTreeBash },
                { Skill.Stomp,       imageTreeStomp },
                { Skill.Glide,       imageTreeGlide },
                { Skill.Climb,       imageTreeClimb },
                { Skill.ChargeJump,  imageTreeChargeJump },
                { Skill.Grenade,     imageTreeLightGrenade }
            };

            /*
            treeGreyImages = new Dictionary<Skill, Image>()
            {
                { Skill.Sein,        imageGTreeSpiritFlame },
                { Skill.WallJump,    imageGTreeWallJump },
                { Skill.ChargeFlame, imageGTreeChargeFlame },
                { Skill.Dash,        imageGTreeDash },
                { Skill.DoubleJump,  imageGTreeDoubleJump },
                { Skill.Bash,        imageGTreeBash },
                { Skill.Stomp,       imageGTreeStomp },
                { Skill.Glide,       imageGTreeGlide },
                { Skill.Climb,       imageGTreeClimb },
                { Skill.ChargeJump,  imageGTreeChargeJump },
                { Skill.Grenade,     imageGTreeLightGrenade }
            };
            */

            eventMousePoint = new Dictionary<string, Point>()
            {
                { "Water Vein", new Point(206, 240) },
                { "Gumon Seal", new Point(300, 202) },
                { "Sunstone",   new Point(393, 233) },
                { "Wind Restored", new Point(300, 404) },
                { "Warmth Returned", new Point(205, 343) }
            };

            checkTreeHitbox = true;
            checkEventHitbox = false;
        }

        private void SetLayoutDefaults()
        {
            SetMouseLocations();
            checkTreeHitbox = false;
            checkEventHitbox = false;

            #region Logic
            haveSkill = new Dictionary<Skill, bool>()
            {
                { Skill.Sein,        false },
                { Skill.WallJump,    false },
                { Skill.Dash ,       false },
                { Skill.ChargeFlame, false },
                { Skill.DoubleJump,  false },
                { Skill.Bash,        false },
                { Skill.Stomp,       false },
                { Skill.Glide,       false },
                { Skill.Climb,       false },
                { Skill.ChargeJump,  false },
                { Skill.Grenade,     false }
            };
            haveEvent = new Dictionary<String, bool>()
            {
                { "Water Vein",      false },
                { "Gumon Seal",      false },
                { "Sunstone",        false },
                { "Clean Water",     false },
                { "Warmth Returned", false },
                { "Wind Restored",   false }
            };

            #endregion         

            skillImages = new Dictionary<Skill, Image>()
            {
                { Skill.Sein,        imageSpiritFlame },
                { Skill.WallJump,    imageWallJump },
                { Skill.ChargeFlame, imageChargeFlame },
                { Skill.Dash,        imageDash },
                { Skill.DoubleJump,  imageDoubleJump },
                { Skill.Bash,        imageBash },
                { Skill.Stomp,       imageStomp },
                { Skill.Glide,       imageGlide },
                { Skill.Climb,       imageClimb },
                { Skill.ChargeJump,  imageChargeJump },
                { Skill.Grenade,     imageLightGrenade }
            };

            /*
            skillGreyImages = new Dictionary<Skill, Image>()
            {
                { Skill.Sein,        imageGSpiritFlame },
                { Skill.WallJump,    imageGWallJump },
                { Skill.ChargeFlame, imageGChargeFlame },
                { Skill.Dash,        imageGDash },
                { Skill.DoubleJump,  imageGDoubleJump },
                { Skill.Bash,        imageGBash },
                { Skill.Stomp,       imageGStomp },
                { Skill.Glide,       imageGGlide },
                { Skill.Climb,       imageGClimb },
                { Skill.ChargeJump,  imageGChargeJump },
                { Skill.Grenade,     imageGLightGrenade }
            };
            */

            eventImages = new Dictionary<String, Image>()
            {
                { "Water Vein",       imageWaterVein },
                { "Gumon Seal",      imageGumonSeal },
                { "Sunstone",        imageSunstone },
                { "Clean Water",     imageCleanWater},
                { "Wind Restored",    imageWindRestored },
                { "Warmth Returned", imageWarmthReturned }
            };

            eventGreyImages = new Dictionary<String, Image>()
            {
                { "Water Vein",      imageGWaterVein },
                { "Gumon Seal",      imageGGumonSeal },
                { "Sunstone",        imageGSunstone },
                { "Clean Water",     imageGCleanWater},
                { "Wind Restored",   imageGWindRestored },
                { "Warmth Returned", imageGWarmthReturned }
            };
        }

        private void SetLayoutRandomizerAllEvents()
        {

        }
        private void SetLayoutAllSkills()
        {

        }
        private void SetLayoutAllCells()
        {

        }
        private void SetLayoutReverseEventOrder()
        {

        }

        private void SetMouseLocations()
        {
            skillMousePoint = new Dictionary<Skill, Point>();
            treeMouseLocation = new Dictionary<Skill, Point>();

            for (int i = 0; i < 11; ++i)
            {
                skillMousePoint.Add((Skill) i, new Point((int)(300 + 194 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                    (int)(300 - 194 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }
            for (int i = 0; i < 11; ++i)
            {
                treeMouseLocation.Add((Skill) i, new Point((int)(300 + 267 * Math.Sin(2.0 * i * Math.PI / 11.0)),
                    (int)(300 - 267 * Math.Cos(2.0 * i * Math.PI / 11.0))));
            }

            eventMousePoint = new Dictionary<string, Point>()
            {
                { "Water Vein", new Point(206, 240) },
                { "Gumon Seal", new Point(300, 202) },
                { "Sunstone",   new Point(393, 233) },
                { "Clean Water", new Point(205, 343) },
                { "Wind Restored", new Point(300, 404) },
                { "Warmth Returned", new Point(391, 342) }
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
            ClearAll();

            settings.Reset();

            scaling = 1.0f;
            this.Opacity = 1.0;
            current_layout = TrackerLayout.RandomizerAllTrees;
            ChangeLayout(current_layout);

            auto_update = false;
            this.autoUpdateToolStripMenuItem.Checked = false;
            draggable = false;
            this.editToolStripMenuItem.Checked = false;

            this.TopMost = true;
            this.alwaysOnTopToolStripMenuItem.Checked = false;

            this.BackColor = SystemColors.ControlDarkDark;
            this.TransparencyKey = Color.Empty;

            Refresh();
        }
        protected void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auto_update = !(auto_update);
            //autoUpdateToolStripMenuItem.Checked = auto_update;

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
        protected void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draggable = !draggable;
            //editToolStripMenuItem.Checked = draggable;
        }
        protected void Tracker_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.X;
            y = e.Y;

            ToggleMouseClick(x, y);

            Refresh();
            this.Invalidate();
        }
        protected void Tracker_Paint(object sender, PaintEventArgs e)
        {
            //UpdateGraphics(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGraphics(e);
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }        
        #endregion  

        #region Graphics

        protected int Square(int a)
        {
            return a * a;
        }

        protected bool ToggleMouseClick(int x, int y)
        {
            //scaling = 1.5;
            int CUR_TOL = (int)(TOL * scaling);

            foreach (KeyValuePair<Skill, Point> sk in skillMousePoint)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X*scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= 2*CUR_TOL)
                {
                    if (haveSkill.ContainsKey(sk.Key))
                    {
                        haveSkill[sk.Key] = !haveSkill[sk.Key];
                        return true;
                    }
                }
            }

            foreach (KeyValuePair<Skill, Point> sk in treeMouseLocation)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= CUR_TOL)
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
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= CUR_TOL)
                {
                    if (haveEvent.ContainsKey(sk.Key))
                    {
                        haveEvent[sk.Key] = !haveEvent[sk.Key];
                        return true;
                    }
                }
            }
            return false;
        }

        protected void UpdateGraphics(PaintEventArgs pea)
        {
            //scaling = 1.5;
            //try
            {
                /*
                 * Drawing consists of the following steps:
                 * (1) The background on which everything is drawn (this can be user selected)
                 * (2) Drawing the Skills (either grayed out or colored in)
                 * (3) Drawing the Events (same)
                 * (4) Drawing the Tree locations
                 * (5) Putting the skill wheel on top
                 * */

                scaledSize = new Size((int)(DEFAULTSIZE.Width * scaling), (int)(DEFAULTSIZE.Height * scaling));

                this.Size = scaledSize;

                Rectangle drawRect = new Rectangle(new Point(0, 0), scaledSize);


                #region Draw

                #region Skills

                pea.Graphics.DrawImage(imageGSkills, drawRect);
                foreach (KeyValuePair<Skill, bool> sk in haveSkill)
                {
                    if (sk.Value)
                    {
                        pea.Graphics.DrawImage(skillImages[sk.Key], drawRect);
                    }
                    /*
                    else
                    {
                        pea.Graphics.DrawImage(skillGreyImages[sk.Key], drawRect);
                    }
                    */
                }

                #endregion


                #region Events

                foreach (KeyValuePair<String, bool> ev in haveEvent)
                {
                    if (ev.Value)
                    {
                        pea.Graphics.DrawImage(eventImages[ev.Key], drawRect);
                    }
                    else
                    {
                        pea.Graphics.DrawImage(eventGreyImages[ev.Key], drawRect);
                    }
                }
                #endregion


                #region Tree

                pea.Graphics.DrawImage(imageGTrees, drawRect);
                foreach (KeyValuePair<Skill, bool> sk in haveTree)
                {
                    if (sk.Value)
                    {
                        pea.Graphics.DrawImage(treeImages[sk.Key], drawRect);
                    }
                    /*
                    else
                    {
                        pea.Graphics.DrawImage(treeGreyImages[sk.Key], drawRect);
                    }
                    */
                }

                #endregion
                
                #endregion

                pea.Graphics.DrawImage(imageSkillWheelDouble, drawRect);

            }
        }

        protected void ClearAll()
        {
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
            Refresh();
        }

        #endregion

        #region AutoUpdate
        //these features need to be added
        bool paused = false;
        protected void TurnOnAutoUpdate()
        {
            if (paused)
            {
                th.Resume();
                paused = false;
            }
            else
            {
                th.Start();
            }
        }

        protected void TurnOffAutoUpdate()
        {
            th.Suspend();
            paused = true;
        }

        private bool CheckInGame(GameState state)
        {
            return (state != GameState.Logos && state != GameState.StartScreen && state != GameState.TitleScreen);
        }

        private bool CheckInGameWorld(GameState state)
        {
            return (CheckInGame(state) && state != GameState.Prologue && !mem.IsEnteringGame());
        }

        private void UpdateLoop()
        {
            bool lastHooked = false;
            while (true)
            {
                //try
                {
                    bool hooked = mem.HookProcess();
                    if (hooked)
                    {
                        UpdateValues();

                    }
                    if (lastHooked != hooked)
                    {
                        lastHooked = hooked;
                        //MessageBox.Show("Hooked: " + hooked.ToString());
                        this.Invoke((Action)delegate () { labelBlank.Visible = false; });
                    }
                    Thread.Sleep(12);
                }
                //catch { }
            }
        }

        private void UpdateValues()
        {
            if (CheckInGameWorld(mem.GetGameState()))
            {
                UpdateSkills();
                UpdateEvents();
                CheckTrees();           

                //the following works but is "incorrect"
                try
                {
                    this.Invalidate();
                    this.Update();
                }
                catch { }
            }
        }

        private void UpdateSkills()
        {
            Skill cur;
            for (int i = 0; i < haveSkill.Count; i++)
            {
                cur = haveSkill.ElementAt(i).Key;
                haveSkill[cur] = mem.GetAbility(GetSkillName(cur));
            }
        }

        private void UpdateEvents()
        {
            String cur;
            for (int i = 0; i < haveEvent.Count; i++)
            {
                cur = haveEvent.ElementAt(i).Key;
                switch (cur)
                {
                    case "Water Vein":
                    case "Gumon Seal":
                    case "Sunstone":
                        haveEvent[cur] = mem.GetKey(cur);
                        break;
                    case "Warmth Returned":
                    case "Wind Restored":
                    case "Clean Water":
                        haveEvent[cur] = mem.GetEvent(cur);
                        break;
                }
            }
        }

        private void CheckTrees()
        {
            HitBox ori = new HitBox(mem.GetCameraTargetPosition(), 0.68f, 1.15f, true);

            Skill tree_at = Skill.None;
            bool touchingAnyTree = false;
            foreach (KeyValuePair<Skill, HitBox> tree in treeHitboxes)
            {
                if (tree.Value.Intersects(ori))
                {
                    touchingAnyTree = true;
                    if (!mem.CanMove())
                    {
                        tree_at = tree.Key;
                        touchingAnyTree = false;
                    }
                    break;
                }
            }

            if (!touchingAnyTree && tree_at != Skill.None)
            {
                hitTree[tree_at] = true;
                haveTree[tree_at] = true;
            }

            //this loops over all trees and updates the have values to the hit values

			foreach (KeyValuePair<Skill, bool> skills in hitTree)
			{
				haveTree[skills.Key] = (hitTree[skills.Key] || skills.Value);
			}

        }

        private void CheckEventLocations()
        {
            HitBox ori = new HitBox(mem.GetCameraTargetPosition(), 0.68f, 1.15f, true);

            String event_at = "";
            bool touchingAnyEvent = false;
            foreach (KeyValuePair<String, HitBox> loc in eventHitboxes)
            {
                if (loc.Value.Intersects(ori))
                {
                    touchingAnyEvent = true;
                    if (!mem.CanMove())
                    {
                        event_at = loc.Key;
                        touchingAnyEvent = false;
                    }
                    break;
                }
            }
            
            if (!touchingAnyEvent && event_at != "")
            {
                //hitEventLocation[event_at] = true;
                //haveEventLocation[event_at] = true;
            }
            /*
            //this loops over all trees and updates the have values to the hit values
            //looking at this right now, I'm not exactly sure if it is working
            foreach (KeyValuePair<String, bool> trees in hitEventLocation)
            {
                haveEventLocation[trees.Key] = (hitEventLocation[trees.Key] || trees.Value);
            }
            */

        }

        private string GetSkillName(Skill sk)
        {
            switch (sk)
            {
                case Skill.Sein: return "Spirit Flame";
                case Skill.WallJump: return "Wall Jump";
                case Skill.ChargeFlame: return "Charge Flame";
                case Skill.Dash: return "Dash";
                case Skill.DoubleJump: return "Double Jump";
                case Skill.Bash: return "Bash";
                case Skill.Stomp: return "Stomp";
                case Skill.Glide: return "Glide";
                case Skill.Climb: return "Climb";
                case Skill.ChargeJump: return "Charge Jump";
                case Skill.Grenade: return "Light Grenade";
            }
            return "N/A";
        }

        #endregion

        private void Tracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Scaling = scaling;
            Properties.Settings.Default.Layout = current_layout;
            Properties.Settings.Default.Opacity = this.Opacity;
            Properties.Settings.Default.Save();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
            Refresh();
        }
    }
}
