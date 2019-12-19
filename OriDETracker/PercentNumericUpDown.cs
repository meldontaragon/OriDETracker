namespace OriDETracker
{    public class PercentNumericUpDown : System.Windows.Forms.NumericUpDown
    {
        protected override void UpdateEditText()
        {
            base.UpdateEditText();

            ChangingText = true;
            Text += "%";
        }
    }
}
