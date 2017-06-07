using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OriDETracker
{
    public partial class Tracker : Form
    {
        public Tracker()
        {
            InitializeComponent();
        }

        private Boolean spiritflame = false;
        private Boolean sftree = false;
        private Boolean walljump = false;
        private Boolean wjtree = false;
        private Boolean chargeflame = false;
        private Boolean cftree = false;
        private Boolean doublejump = false;
        private Boolean djtree = false;
        private Boolean bash = false;
        private Boolean btree = false;
        private Boolean stomp = false;
        private Boolean stree = false;
        private Boolean glide = false;
        private Boolean gtree = false;
        private Boolean climb = false;
        private Boolean ctree = false;
        private Boolean chargejump = false;
        private Boolean cjtree = false;
        private Boolean lightgrenade = false;
        private Boolean lgtree = false;
        private Boolean dash = false;
        private Boolean dtree = false;

        private Boolean watervein = false;
        private Boolean cleanwater = false;
        private Boolean gumonseal = false;
        private Boolean windsrestored = false;
        private Boolean sunstone = false;

        private const int TOL = 25;

        private Image image_spiritflame = Image.FromFile(@"smspiritflame.png");


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset_all();
        }

        private void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Tracker_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.X;
            y = e.Y;

            handle_click(x, y);
        }

        private int Square(int x)
        {
            return x * x;
        }

        private void handle_click(int x, int y)
        {
            toggle(x, y);
        }

        private void toggle(int x, int y)
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
        }

        private void update_graphics(PaintEventArgs pea)
        {
            /* 
              1 spiritflame 165,78
              2 wjump 228,97
              3 cflame 267,144
              4 doublejump 273,205
              5 bash 249,263
              6 stomp 196,293
              7 glide 134,293
              8 climb 83,260
              9 cjump 54,202
              10 lightgrenade 66,143
              11 dash 106,95


              12 watervein 91,394
              13 gumonseal 159,386
              14 sunstone 226,386
              15 cleanwater 125,465
              16 winds 185,457
              */

            /*
             * pb_sein.Visible = spiritflame;
            pb_walljump.Visible = walljump;
            pb_cflame.Visible = chargeflame;
            pb_djump.Visible = doublejump;
            pb_bash.Visible = bash;
            pb_stomp.Visible = stomp;
            pb_glide.Visible = glide;
            pb_climb.Visible = climb;
            pb_cjump.Visible = chargejump;
            pb_grenade.Visible = lightgrenade;
            pb_dash.Visible = dash;       
            */
            if (spiritflame)
            {
                pea.Graphics.DrawImage(image_spiritflame, 165, 78);
            }
            
        }


        private void reset_all()
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
    }

        private void pb_skill_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox a;
            if (sender.Equals(a = pb_sein))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_walljump))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_djump))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_cflame))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_bash))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_stomp))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_glide))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_cjump))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_climb))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_grenade))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }
            else if (sender.Equals(a = pb_dash))
            {
                handle_click(e.X + a.Location.X, e.Y + a.Location.Y);
            }

        }

        private void Tracker_Paint(object sender, PaintEventArgs e)
        {
            update_graphics(e);
        }
    }
}
