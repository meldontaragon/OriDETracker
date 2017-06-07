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
    public partial class Tracker : Form
    {
        protected OriMemory mem { get; set; }
        protected Thread th;

        public Tracker()
        {
            InitializeComponent();
            mem = new OriDE.Memory.OriMemory();
            th = new Thread(UpdateLoop);
            th.IsBackground = true;
        }

        #region FrameMoving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        
        private void Tracker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && draggable)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion  

        protected const int TOL = 25;
        protected bool auto_update = false;
        protected bool draggable = false;
        //private bool back_drawn = false;

        private int level;
        private int xp;
        private float hp;
        private float eng;
        private int ability;
        private int keystones;
        private int mapstones;         

        #region BooleanValues
        protected Boolean spiritflame = false;
        protected Boolean walljump = false;
        protected Boolean chargeflame = false;
        protected Boolean doublejump = false;
        protected Boolean bash = false;
        protected Boolean stomp = false;
        protected Boolean glide = false;
        protected Boolean gtree = false;
        protected Boolean climb = false;
        protected Boolean chargejump = false;
        protected Boolean lightgrenade = false;
        protected Boolean dash = false;

        protected Boolean sftree = false;
        protected Boolean wjtree = false;
        protected Boolean cftree = false;
        protected Boolean djtree = false;
        protected Boolean btree = false;
        protected Boolean stree = false;
        protected Boolean ctree = false;
        protected Boolean cjtree = false;
        protected Boolean lgtree = false;
        protected Boolean dtree = false;


        protected Boolean watervein = false;
        protected Boolean cleanwater = false;
        protected Boolean gumonseal = false;
        protected Boolean windsrestored = false;
        protected Boolean sunstone = false;
        #endregion

        #region Images
        protected Image background = Image.FromFile(@"data/emptytracker.png");

        protected Image image_spiritflame = Image.FromFile(@"data\smspiritflame.png");
        protected Image image_walljump = Image.FromFile(@"data\smwalljump.png");
        protected Image image_cflame = Image.FromFile(@"data\smcflame.png");
        protected Image image_doublejump = Image.FromFile(@"data\smdoublejump.png");
        protected Image image_bash = Image.FromFile(@"data\smbash.png");
        protected Image image_stomp = Image.FromFile(@"data\smstomp.png");
        protected Image image_glide = Image.FromFile(@"data\smglide.png");
        protected Image image_climb = Image.FromFile(@"data\smclimb.png");
        protected Image image_cjump = Image.FromFile(@"data\smcjump.png");
        protected Image image_lightgrenade = Image.FromFile(@"data\smlightgrenade.png");
        protected Image image_dash = Image.FromFile(@"data\smdash.png");

        protected Image image_watervein = Image.FromFile(@"data\smwatervein.png");
        protected Image image_gumonseal = Image.FromFile(@"data\smgumonseal.png");
        protected Image image_sunstone = Image.FromFile(@"data\smsunstone.png");
        protected Image image_cleanwater = Image.FromFile(@"data\smcleanwater.png");
        protected Image image_winds = Image.FromFile(@"data\smwinds.png");
        #endregion

        #region Points
        protected Point point_spiritflame = new Point(165, 78);
        protected Point point_walljump = new Point(228, 97);
        protected Point point_cflame = new Point(267, 144);
        protected Point point_doublejump = new Point(273, 205);
        protected Point point_bash = new Point(249, 263);
        protected Point point_stomp = new Point(196, 293);
        protected Point point_glide = new Point(134, 293);
        protected Point point_climb = new Point(83, 260);
        protected Point point_cjump = new Point(54, 202);
        protected Point point_lightgrenade = new Point(66, 143);
        protected Point point_dash = new Point(106, 95);

        protected Point point_watervein = new Point(91, 394);
        protected Point point_gumonseal = new Point(159, 386);
        protected Point point_sunstone = new Point(226, 386);
        protected Point point_cleanwater = new Point(125, 465);
        protected Point point_winds = new Point(185, 457);

        #endregion

        protected void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset_all();
            Refresh();
        }

        protected void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auto_update = !(auto_update);
            //autoUpdateToolStripMenuItem.Checked = auto_update;

            if (auto_update)
            {
                turn_on_auto_update();
            }
            else
            {
                turn_off_auto_update();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draggable = !draggable;
            //editToolStripMenuItem.Checked = draggable;
        }
        
        #region Graphics
        protected void Tracker_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.X;
            y = e.Y;

            toggle(x, y);

            Refresh();
            this.Invalidate();
        }

        protected int Square(int x)
        {
            return x * x;
        }

        protected bool toggle(int x, int y)
        {
            if (Math.Sqrt(Square(x - 201) + Square(y - 114)) <= TOL)
            {
                spiritflame = !spiritflame;
            }
            else if (Math.Sqrt(Square(x - 264) + Square(y - 134)) <= TOL)
            {
                walljump = !walljump;
            }
            else if (Math.Sqrt(Square(x - 305) + Square(y - 181)) <= TOL)
            {
                chargeflame = !chargeflame;
            }
            else if (Math.Sqrt(Square(x - 313) + Square(y - 242)) <= TOL)
            {
                doublejump = !doublejump;
            }
            else if (Math.Sqrt(Square(x - 286) + Square(y - 297)) <= TOL)
            {
                bash = !bash;
            }
            else if (Math.Sqrt(Square(x - 234) + Square(y - 333)) <= TOL)
            {
                stomp = !stomp;
            }
            else if (Math.Sqrt(Square(x - 170) + Square(y - 330)) <= 27)
            {
                glide = !glide;
            }
            else if (Math.Sqrt(Square(x - 118) + Square(y - 296)) <= TOL)
            {
                climb = !climb;
            }
            else if (Math.Sqrt(Square(x - 93) + Square(y - 241)) <= TOL)
            {
                chargejump = !chargejump;
            }
            else if (Math.Sqrt(Square(x - 101) + Square(y - 180)) <= TOL)
            {
                lightgrenade = !lightgrenade;
            }
            else if (Math.Sqrt(Square(x - 143) + Square(y - 131)) <= TOL)
            {
                dash = !dash;
            }
            else if (Math.Sqrt(Square(x - 139) + Square(y - 438)) <= TOL)
            {
                watervein = !watervein;
            }
            else if (Math.Sqrt(Square(x - 206) + Square(y - 439)) <= TOL)
            {
                gumonseal = !gumonseal;
            }
            else if (Math.Sqrt(Square(x - 270) + Square(y - 439)) <= TOL)
            {
                sunstone = !sunstone;
            }
            else if (Math.Sqrt(Square(x - 169) + Square(y - 509)) <= TOL)
            {
                cleanwater = !cleanwater;
            }
            else if (Math.Sqrt(Square(x - 241) + Square(y - 509)) <= TOL)
            {
                windsrestored = !windsrestored;
            }
            else if (Math.Sqrt(Square(x - 201) + Square(y - 54)) <= TOL)
            {
                sftree = !sftree;
            }
            else if (Math.Sqrt(Square(x - 296) + Square(y - 87)) <= TOL)
            {
                wjtree = !wjtree;
            }
            else if (Math.Sqrt(Square(x - 356) + Square(y - 160)) <= TOL)
            {
                cftree = !cftree;
            }
            else if (Math.Sqrt(Square(x - 367) + Square(y - 247)) <= TOL)
            {
                djtree = !djtree;
            }
            else if (Math.Sqrt(Square(x - 321) + Square(y - 340)) <= TOL)
            {
                btree = !btree;
            }
            else if (Math.Sqrt(Square(x - 246) + Square(y - 384)) <= TOL)
            {
                stree = !stree;
            }
            else if (Math.Sqrt(Square(x - 153) + Square(y - 385)) <= TOL)
            {
                gtree = !gtree;
            }
            else if (Math.Sqrt(Square(x - 78) + Square(y - 340)) <= TOL)
            {
                ctree = !ctree;
            }
            else if (Math.Sqrt(Square(x - 33) + Square(y - 246)) <= TOL)
            {
                cjtree = !cjtree;
            }
            else if (Math.Sqrt(Square(x - 48) + Square(y - 158)) <= TOL)
            {
                lgtree = !lgtree;
            }
            else if (Math.Sqrt(Square(x - 98) + Square(y - 85)) <= TOL)
            {
                dtree = !dtree;
            }
            else
            {
                return true;
            }
            return false;
        }

        protected void update_graphics(PaintEventArgs pea)
        {
            SolidBrush ellipse_brush = new SolidBrush(Color.White);

            if (sftree) pea.Graphics.FillEllipse(ellipse_brush, 201 - 25, 54 - 25, 50, 50);
            if (wjtree) pea.Graphics.FillEllipse(ellipse_brush, 296 - 25, 87 - 25, 50, 50);
            if (cftree) pea.Graphics.FillEllipse(ellipse_brush, 356 - 25, 160 - 25, 50, 50);
            if (djtree) pea.Graphics.FillEllipse(ellipse_brush, 367 - 25, 247 - 25, 50, 50);
            if (btree) pea.Graphics.FillEllipse(ellipse_brush, 321 - 25, 340 - 25, 50, 50);
            if (stree) pea.Graphics.FillEllipse(ellipse_brush, 246 - 25, 384 - 25, 50, 50);
            if (gtree) pea.Graphics.FillEllipse(ellipse_brush, 153 - 25, 385 - 25, 50, 50);
            if (ctree) pea.Graphics.FillEllipse(ellipse_brush, 78 - 25, 340 - 25, 50, 50);
            if (cjtree) pea.Graphics.FillEllipse(ellipse_brush, 33 - 25, 246 - 25, 50, 50);
            if (lgtree) pea.Graphics.FillEllipse(ellipse_brush, 48 - 25, 158 - 25, 50, 50);
            if (dtree) pea.Graphics.FillEllipse(ellipse_brush, 98 - 25, 85 - 25, 50, 50);

            //if (!back_drawn)
            {
                pea.Graphics.DrawImage(background, new Rectangle(new Point(0, 0), background.Size));
            }

            #region DrawSkills
            if (spiritflame)
            {
                pea.Graphics.DrawImage(image_spiritflame, new Rectangle(point_spiritflame, image_spiritflame.Size));
                //new Point[] { point_spiritflame, SKILL_SIZE });
            }
            if (walljump)
            {
                pea.Graphics.DrawImage(image_walljump, new Rectangle(point_walljump, image_walljump.Size));
            }
            if (chargeflame)
            {
                pea.Graphics.DrawImage(image_cflame, new Rectangle(point_cflame, image_cflame.Size));
            }
            if (doublejump)
            {
                pea.Graphics.DrawImage(image_doublejump, new Rectangle(point_doublejump, image_doublejump.Size));
            }
            if (bash)
            {
                pea.Graphics.DrawImage(image_bash, new Rectangle(point_bash, image_bash.Size));
            }
            if (stomp)
            {
                pea.Graphics.DrawImage(image_stomp, new Rectangle(point_stomp, image_stomp.Size));
            }
            if (glide)
            {
                pea.Graphics.DrawImage(image_glide, new Rectangle(point_glide, image_glide.Size));
            }
            if (climb)
            {
                pea.Graphics.DrawImage(image_climb, new Rectangle(point_climb, image_climb.Size));
            }
            if (chargejump)
            {
                pea.Graphics.DrawImage(image_cjump, new Rectangle(point_cjump, image_cjump.Size));
            }
            if (lightgrenade)
            {
                pea.Graphics.DrawImage(image_lightgrenade, new Rectangle(point_lightgrenade, image_lightgrenade.Size));
            }
            if (dash)
            {
                pea.Graphics.DrawImage(image_dash, new Rectangle(point_dash, image_dash.Size));
            }
            #endregion

            #region DrawEvents
            if (watervein)
            {
                pea.Graphics.DrawImage(image_watervein, new Rectangle(point_watervein, image_watervein.Size));
            }
            if (gumonseal)
            {
                pea.Graphics.DrawImage(image_gumonseal, new Rectangle(point_gumonseal, image_gumonseal.Size));
            }
            if (sunstone)
            {
                pea.Graphics.DrawImage(image_sunstone, new Rectangle(point_sunstone, image_sunstone.Size));
            }
            if (cleanwater)
            {
                pea.Graphics.DrawImage(image_cleanwater, new Rectangle(point_cleanwater, image_cleanwater.Size));
            }
            if (windsrestored)
            {
                pea.Graphics.DrawImage(image_winds, new Rectangle(point_winds, image_winds.Size));
            }
            #endregion

            /*
            if (sftree) pea.Graphics.DrawEllipse(Pens.White, 201 - 25, 54 - 25, 50, 50);
            if (wjtree) pea.Graphics.DrawEllipse(Pens.White, 296 - 25, 87 - 25, 50, 50);
            if (cftree) pea.Graphics.DrawEllipse(Pens.White, 356 - 25, 160 - 25, 50, 50);
            if (djtree) pea.Graphics.DrawEllipse(Pens.White, 367 - 25, 247 - 25, 50, 50);
            if (btree) pea.Graphics.DrawEllipse(Pens.White, 321 - 25, 340 - 25, 50, 50);
            if (stree) pea.Graphics.DrawEllipse(Pens.White, 246 - 25, 384 - 25, 50, 50);
            if (gtree) pea.Graphics.DrawEllipse(Pens.White, 153 - 25, 385 - 25, 50, 50);
            if (ctree) pea.Graphics.DrawEllipse(Pens.White, 78 - 25, 340 - 25, 50, 50);
            if (cjtree) pea.Graphics.DrawEllipse(Pens.White, 33 - 25, 246 - 25, 50, 50);
            if (lgtree) pea.Graphics.DrawEllipse(Pens.White, 48 - 25, 158 - 25, 50, 50);
            if (dtree) pea.Graphics.DrawEllipse(Pens.White, 98 - 25, 85 - 25, 50, 50);
            */

        }

        protected void reset_all()
        {

            spiritflame = false;
            sftree = false;
            walljump = false;
            wjtree = false;
            chargeflame = false;
            cftree = false;
            doublejump = false;
            djtree = false;
            bash = false;
            btree = false;
            stomp = false;
            stree = false;
            glide = false;
            gtree = false;
            climb = false;
            ctree = false;
            chargejump = false;
            cjtree = false;
            lightgrenade = false;
            lgtree = false;
            dash = false;
            dtree = false;

            watervein = false;
            cleanwater = false;
            gumonseal = false;
            windsrestored = false;
            sunstone = false;

            Refresh();
        }

        protected void Tracker_Paint(object sender, PaintEventArgs e)
        {
            update_graphics(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            update_graphics(e);
        }
        #endregion


        #region AutoUpdate
        //these features need to be added
        bool paused = false;
        protected void turn_on_auto_update()
        {
            if (paused) 
            {
                th.Resume();
            }
            else
            {
                th.Start();
            }
        }

        protected void turn_off_auto_update()
        {
            th.Suspend();
            paused = true;
        }
        
        private bool CheckInGame(GameState state)
        {
            return state != GameState.Logos && state != GameState.StartScreen && state != GameState.TitleScreen;
        }

        private bool CheckInGameWorld(GameState state)
        {
            return CheckInGame(state) && state != GameState.Prologue && !mem.IsEnteringGame();
        }

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
                        MessageBox.Show("Hooked: " + hooked.ToString());
                        //this.Invoke((Action)delegate () { lblNote.Visible = !hooked; });
                    }
                    Thread.Sleep(12);
                }
                catch { }
            }          
        }

        private void UpdateValues()
        {
            if ( CheckInGameWorld(mem.GetGameState()) )
            {
                level = mem.GetCurrentLevel();
                xp = mem.GetExperience();               
                hp = mem.GetCurrentHP();
                eng = mem.GetCurrentEN();
                ability = mem.GetAbilityCells();
                keystones = mem.GetKeyStones();
                mapstones = mem.GetMapStones();                              

                UpdateSkills();
                UpdateEvents();
                CheckTrees();
            }
            //still need to update the graphics here
        }

        private void UpdateSkills()
        {
            spiritflame = mem.GetAbility("Spirit Flame");
            walljump = mem.GetAbility("Wall Jump");
            chargeflame = mem.GetAbility("Charge Flame");
            doublejump = mem.GetAbility("Double Jump");
            bash = mem.GetAbility("Bash");
            stomp = mem.GetAbility("Stomp");
            glide = mem.GetAbility("Glide");
            climb = mem.GetAbility("Climb");
            chargejump = mem.GetAbility("Charge Jump");
            lightgrenade = mem.GetAbility("Light Grenade");
            dash = mem.GetAbility("Dash");
        }
        
        private void UpdateEvents()
        {
            watervein = mem.GetKey("Water Vein");
            gumonseal = mem.GetKey("Gumon Seal");
            sunstone = mem.GetKey("Sunstone");

            cleanwater = mem.GetEvent("Warmth Returned");
            windsrestored = mem.GetEvent("Wind Restored");
        }

        private void CheckTrees()
        {
            PointF pos = mem.GetCameraTargetPosition();
            HitBox ori = new HitBox(pos, 0.68f, 1.15f, true);
        }

        #endregion
    }
}
