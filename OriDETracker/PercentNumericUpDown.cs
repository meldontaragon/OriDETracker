using System.Windows.Forms;

namespace OriDETracker
{
    public class PercentNumericUpDown : NumericUpDown
    {
        protected override void UpdateEditText()
        {
            base.UpdateEditText();

            ChangingText = true;
            Text += "%";
        }
    }
}
