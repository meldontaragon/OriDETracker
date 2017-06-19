using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OriDETracker
{
    public class TrackerPictureBox : PictureBox
    {
        public TrackerPictureBox(Form fm, Image img) : this(fm, img, img.Size)
        {

        }

        public TrackerPictureBox(Form fm, Image img, Size sz)
        {
            this.Parent = fm;
            this.Visible = false;

            this.Size = sz;
            this.Image = img;

            this.Location = new Point(0, 0);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            this.BackColor = Color.Transparent;
        }
    };
}
