using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriDETracker
{
    public class PercentNumericUpDown : System.Windows.Forms.NumericUpDown
    {
        protected override void UpdateEditText()
        {
            base.UpdateEditText();

            ChangingText = true;
            Text += "%";
        }
    }
}
