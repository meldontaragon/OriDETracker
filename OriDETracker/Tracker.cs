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
        protected OriMemory mem { get; set; }
        protected Thread th;
        protected SettingsLayout settings;

        public Tracker()
        {
            InitializeComponent();

            scaling = Properties.Settings.Default.Scaling;
            settings = new SettingsLayout(this);
            settings.Visible = false;

            mem = new OriDE.Memory.OriMemory();
            th = new Thread(UpdateLoop);
            th.IsBackground = true;

            labelLastPickup = new Label();
            labelLastPickup.Visible = false;
            labelLastPickup.BackColor = Color.White;
            labelLastPickup.TextAlign = ContentAlignment.TopLeft;
            labelLastPickup.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Bold);
            labelLastPickup.ForeColor = Color.Black;

            //this is here until the settings menu is complete
            this.layoutToolStripMenuItem.Visible = false;   
            SetLayoutRandomizerAllTrees();

        }

        #region FrameMoving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        #endregion

        public float Scaling { get { return scaling; } set { scaling = value; } }

        protected const int TOL = 25;
        protected bool auto_update = false;
        protected bool draggable = false;
        protected float scaling = 1.0f;
        protected TrackerLayout current_layout;

        #region LogicDictionary
        //general: Skills and Events
        private Dictionary<Skill, bool> haveSkill;
        private Dictionary<String, bool> haveEvent;

        //general HP/ENG/AC
        private int healthCells;
        private int abilityCells;
        private float energyCells;

        //All Trees
        private Dictionary<Skill, bool> hitTree;
        private Dictionary<Skill, bool> haveTree;

        //All Events
        private Dictionary<String, bool> haveEventLocation;
        private Dictionary<String, bool> hitEventLocation;
        #endregion

        #region Images

        protected static Image backgroundAllTrees = Image.FromFile(@"data/emptytracker.png");

        protected static Image image_sein = Image.FromFile(@"data\smspiritflame.png");
        protected static Image image_walljump = Image.FromFile(@"data\smwalljump.png");
        protected static Image image_chargeflame = Image.FromFile(@"data\smcflame.png");
        protected static Image image_doublejump = Image.FromFile(@"data\smdoublejump.png");
        protected static Image image_bash = Image.FromFile(@"data\smbash.png");
        protected static Image image_stomp = Image.FromFile(@"data\smstomp.png");
        protected static Image image_glide = Image.FromFile(@"data\smglide.png");
        protected static Image image_climb = Image.FromFile(@"data\smclimb.png");
        protected static Image image_chargejump = Image.FromFile(@"data\smcjump.png");
        protected static Image image_grenade = Image.FromFile(@"data\smlightgrenade.png");
        protected static Image image_dash = Image.FromFile(@"data\smdash.png");

        protected static Image image_watervein = Image.FromFile(@"data\smwatervein.png");
        protected static Image image_gumonseal = Image.FromFile(@"data\smgumonseal.png");
        protected static Image image_sunstone = Image.FromFile(@"data\smsunstone.png");
        protected static Image image_cleanwater = Image.FromFile(@"data\smcleanwater.png");
        protected static Image image_windrestored = Image.FromFile(@"data\smwinds.png");
        protected static Image image_warmthreturned;

        //protected static Image image_abilitycell;
        //protected static Image image_healthcell;
        //protected static Image image_energycell;

        //no category of the tracker uses different skill images
        private Dictionary<Skill, Image> skillImages = new Dictionary<Skill, Image>()
        {
            {Skill.Sein,        image_sein },
            {Skill.WallJump,    image_walljump },
            {Skill.ChargeFlame, image_chargeflame },
            {Skill.DoubleJump,  image_doublejump },
            {Skill.Bash,        image_bash },
            {Skill.Stomp,       image_stomp },
            {Skill.Glide,       image_glide },
            {Skill.Climb,       image_climb },
            {Skill.ChargeJump,  image_chargejump },
            {Skill.Grenade,     image_grenade },
            {Skill.Dash ,       image_dash }
        };

        private Dictionary<String, Image> eventImages;

        private Image healthImage;
        private Image energyImage;
        private Image abilityImage;

        private Image background;


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

        #region PointsAndRectangles
        //Rectangles to draw background for skill or event locations
        private Dictionary<String, Rectangle> eventLocationRectangles;
        private Dictionary<Skill, Rectangle> skillTreeRectangles;

        //General Skill and Event Locations on Tracker
        private Dictionary<Skill, Point> skillPointLocation;
        private Dictionary<String, Point> eventPointLocations;

        //points for mouse clicks (with certain tolerance defined by TOL)
        private Dictionary<Skill, Point> skillMousePoint;
        private Dictionary<String, Point> eventMousePoint;

        private Dictionary<Skill, Point> treeMouseLocation;
        private Dictionary<String, Point> eventMouseLocation;

        private bool drawInfo = false;

        private Dictionary<String, Point> infoImageLocations;
        private Dictionary<String, Point> infoTextLocations;

        private bool showLastPickup = false;
        private Point lastPickupLocation = new Point(87, 562);
        private Label labelLastPickup;
        #endregion

        #region SetLayout

        private void SetLayoutRandomizerAllTrees()
        {
            #region Logic
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
                { "Warmth Returned", false }, //this is actually clean water
                { "Wind Restored",   false }
            };

            #endregion
            checkTreeHitbox = true;
            checkEventHitbox = false;

            background = backgroundAllTrees;

            skillPointLocation = new Dictionary<Skill, Point>()
            {
                { Skill.Sein,        new Point(165, 78) },
                { Skill.WallJump,    new Point(228, 97) },
                { Skill.ChargeFlame, new Point(267, 144) },
                { Skill.Dash,        new Point(106, 95) },
                { Skill.DoubleJump,  new Point(273, 205) },
                { Skill.Bash,        new Point(249, 263) },
                { Skill.Stomp,       new Point(196, 293) },
                { Skill.Glide,       new Point(134, 293) },
                { Skill.Climb,       new Point(83, 260) },
                { Skill.ChargeJump,  new Point(54, 202) },
                { Skill.Grenade,     new Point(66, 143) }
            };

            eventPointLocations = new Dictionary<string, Point>()
            {
                {"Water Vein",       new Point(91, 394) },
                {"Gumon Seal",       new Point(159, 386) },
                { "Sunstone",        new Point(226, 386) },
                { "Warmth Returned", new Point(125, 465) }, //this is actually clean water
                { "Wind Restored",   new Point(185, 457) }
            };

            eventImages = new Dictionary<String, Image>()
            {
                {"Water Vein",      image_watervein },
                {"Warmth Returned", image_cleanwater }, //this is actually clean water
			    {"Wind Restored",   image_windrestored },
                {"Gumon Seal",      image_gumonseal },
                {"Sunstone",        image_sunstone }
            };

            skillTreeRectangles = new Dictionary<Skill, Rectangle>()
            {
                { Skill.Sein,        new Rectangle(201 - 25, 54 - 25, 50, 50) },
                { Skill.WallJump,    new Rectangle(296 - 25, 87 - 25, 50, 50) },
                { Skill.ChargeFlame, new Rectangle(356 - 25, 160 - 25, 50, 50) },
                { Skill.Dash,        new Rectangle(98 - 25, 85 - 25, 50, 50) },
                { Skill.DoubleJump,  new Rectangle(367 - 25, 247 - 25, 50, 50) },
                { Skill.Bash,        new Rectangle(321 - 25, 340 - 25, 50, 50) },
                { Skill.Stomp,       new Rectangle(246 - 25, 384 - 25, 50, 50) },
                { Skill.Glide,       new Rectangle(153 - 25, 385 - 25, 50, 50) },
                { Skill.Climb,       new Rectangle(78 - 25, 340 - 25, 50, 50) },
                { Skill.ChargeJump,  new Rectangle(33 - 25, 246 - 25, 50, 50) },
                { Skill.Grenade,     new Rectangle(48 - 25, 158 - 25, 50, 50) }
            };

            skillMousePoint = new Dictionary<Skill, Point>()
            {
                { Skill.Sein,        new Point(201, 114) },
                { Skill.WallJump,    new Point(264, 134) },
                { Skill.ChargeFlame, new Point(305, 181) },
                { Skill.Dash,        new Point(143, 131) },
                { Skill.DoubleJump,  new Point(313, 242) },
                { Skill.Bash,        new Point(286, 297) },
                { Skill.Stomp,       new Point(234, 333) },
                { Skill.Glide,       new Point(170, 330) },
                { Skill.Climb,       new Point(118, 296) },
                { Skill.ChargeJump,  new Point(93, 241) },
                { Skill.Grenade,     new Point(101, 180) }
            };

           treeMouseLocation = new Dictionary<Skill, Point>()
            {
                { Skill.Sein,        new Point(201, 54) },
                { Skill.WallJump,    new Point(296, 87) },
                { Skill.ChargeFlame, new Point(356, 160) },
                { Skill.Dash,        new Point(98, 85) },
                { Skill.DoubleJump,  new Point(367, 247) },
                { Skill.Bash,        new Point(321, 340) },
                { Skill.Stomp,       new Point(246, 384) },
                { Skill.Glide,       new Point(153, 385) },
                { Skill.Climb,       new Point(78, 340) },
                { Skill.ChargeJump,  new Point(33, 246) },
                { Skill.Grenade,     new Point(48, 158) }
            };

            eventMousePoint = new Dictionary<string, Point>()
            {
                {"Water Vein",      new Point(139, 438) },
                {"Gumon Seal",      new Point(206, 439) },
                {"Sunstone",        new Point(270, 439) },
                {"Warmth Returned", new Point(169, 509) }, //this is actually clean water
			    {"Wind Restored",   new Point(241, 509) }
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
            ResetAll();
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
            UpdateGraphics(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGraphics(e);
        }
        private void randomizerAllTreesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetLayoutCheck();
            this.randomizerAllEventsToolStripMenuItem.Checked = true;
        }
        private void randomizerAllEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetLayoutCheck();
            this.randomizerAllEventsToolStripMenuItem.Checked = true;
        }
        private void allSkillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetLayoutCheck();
            this.allSkillsToolStripMenuItem.Checked = true;
        }
        private void allCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetLayoutCheck();
            this.allCellsToolStripMenuItem.Checked = true;
        }
        private void reverseEventOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetLayoutCheck();
            this.reverseEventOrderToolStripMenuItem.Checked = true;
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }        
        #endregion  

        #region Graphics

        protected int Square(int x)
        {
            return x * x;
        }
        public void ChangeLayout(TrackerLayout layout)
        {
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

        protected bool ToggleMouseClick(int x, int y)
        {
            //scaling = 1.5;
            int CUR_TOL = (int)(TOL * scaling);

            foreach (KeyValuePair<Skill, Point> sk in skillMousePoint)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X*scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= CUR_TOL)
                {
                    haveSkill[sk.Key] = !haveSkill[sk.Key];
                    return true;
                }
            }

            foreach (KeyValuePair<Skill, Point> sk in treeMouseLocation)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= CUR_TOL)
                {
                    haveTree[sk.Key] = !haveTree[sk.Key];
                    return true;
                }
            }

            foreach (KeyValuePair<String, Point> sk in eventMousePoint)
            {
                if (Math.Sqrt(Square(x - (int)(sk.Value.X * scaling)) + Square(y - (int)(sk.Value.Y * scaling))) <= CUR_TOL)
                {
                    haveEvent[sk.Key] = !haveEvent[sk.Key];
                    return true;
                }
            }
            return false;
        }

        protected void UpdateGraphics(PaintEventArgs pea)
        {
            //scaling = 1.5;
            try
            {
                SolidBrush ellipse_brush = new SolidBrush(Color.White);

                #region DrawingforHitboxLocationChecks

                //draw tree location ellipses
                if (checkTreeHitbox)
                {
                    Dictionary<Skill, bool> tmp_tree = new Dictionary<Skill, bool>(haveTree);
                    foreach (KeyValuePair<Skill, bool> sk in tmp_tree)
                    {
                        if (sk.Value)
                        {
                            RectangleF newRect = new RectangleF((scaling*skillTreeRectangles[sk.Key].X), (scaling*skillTreeRectangles[sk.Key].Y), (scaling*skillTreeRectangles[sk.Key].Width), (scaling*skillTreeRectangles[sk.Key].Height));
                            //pea.Graphics.FillEllipse(ellipse_brush, skillTreeRectangles[sk.Key]);
                            pea.Graphics.FillEllipse(ellipse_brush, newRect);
                        }
                    }
                }

                //draw event location ellipses (not currently implemented);
                if (checkEventHitbox)
                {
                    Dictionary<String, bool> tmp_loc = new Dictionary<String, bool>(haveEventLocation);
                    foreach (KeyValuePair<String, bool> ev in tmp_loc)
                    {
                        if (ev.Value)
                        {
                            RectangleF newRect = new RectangleF((scaling * eventLocationRectangles[ev.Key].X), (scaling * eventLocationRectangles[ev.Key].Y), 
                                (scaling * eventLocationRectangles[ev.Key].Width), (scaling * eventLocationRectangles[ev.Key].Height));
                            //pea.Graphics.FillEllipse(ellipse_brush, eventLocationRectangles[ev.Key]);
                            pea.Graphics.FillEllipse(ellipse_brush, newRect);
                        }
                    }
                }
                #endregion  

                Size newSize = new Size((int)Math.Ceiling(background.Size.Width * scaling), (int)Math.Ceiling(background.Size.Height * scaling));
                pea.Graphics.DrawImage(background, new Rectangle(new Point(0, 0), newSize));
                this.Size = newSize;

                #region DrawingSkillsAndEvents
                Dictionary<Skill, bool> tmp = new Dictionary<Skill, bool>(haveSkill);
                foreach (KeyValuePair<Skill, bool> sk in tmp)
                {
                    if (sk.Value)
                    {
                        RectangleF newRect = new RectangleF((skillPointLocation[sk.Key].X * scaling), (skillPointLocation[sk.Key].Y * scaling),
                            (skillImages[sk.Key].Size.Width * scaling), (skillImages[sk.Key].Size.Height * scaling));
                        //pea.Graphics.DrawImage(skillImages[sk.Key], new Rectangle(skillPointLocation[sk.Key], skillImages[sk.Key].Size));
                        pea.Graphics.DrawImage(skillImages[sk.Key], newRect);

                    }
                }

                Dictionary<String, bool> tmp_ev = new Dictionary<string, bool>(haveEvent);
                foreach (KeyValuePair<String, bool> ev in tmp_ev)
                {
                    if (ev.Value)
                    {
                        RectangleF newRect = new RectangleF((eventPointLocations[ev.Key].X * scaling), (eventPointLocations[ev.Key].Y * scaling),
                            (eventImages[ev.Key].Size.Width * scaling), (eventImages[ev.Key].Size.Height * scaling));
                        //pea.Graphics.DrawImage(eventImages[ev.Key], new Rectangle(eventPointLocations[ev.Key], eventImages[ev.Key].Size));
                        pea.Graphics.DrawImage(eventImages[ev.Key], newRect);
                    }
                }
                #endregion

                if (drawInfo)
                {
                    pea.Graphics.DrawImage(energyImage,  new Rectangle(infoImageLocations["Energy"], energyImage.Size));
                    pea.Graphics.DrawImage(healthImage,  new Rectangle(infoImageLocations["Health"], healthImage.Size));
                    pea.Graphics.DrawImage(abilityImage, new Rectangle(infoImageLocations["Ability"], abilityImage.Size));
                }
            }
            catch { }
        }

        protected void ResetAll()
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
            settings.Reset();
            scaling = 1.0f;

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
                hitEventLocation[event_at] = true;
                haveEventLocation[event_at] = true;
            }

            //this loops over all trees and updates the have values to the hit values
            //looking at this right now, I'm not exactly sure if it is working
            foreach (KeyValuePair<String, bool> trees in hitEventLocation)
            {
                haveEventLocation[trees.Key] = (hitEventLocation[trees.Key] || trees.Value);
            }

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

        private void UnsetLayoutCheck()
        {
            this.randomizerAllEventsToolStripMenuItem.Checked = false;
            this.randomizerAllTreesToolStripMenuItem.Checked = false;
            this.allSkillsToolStripMenuItem.Checked = false;
            this.allCellsToolStripMenuItem.Checked = false;
            this.reverseEventOrderToolStripMenuItem.Checked = false;
        }

        private void Tracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Scaling = scaling;
            Properties.Settings.Default.Save();
        }
    }
}
