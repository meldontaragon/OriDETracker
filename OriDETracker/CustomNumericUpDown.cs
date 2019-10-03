using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriDETracker
{
    public partial class CustomNumericUpDown : System.Windows.Forms.NumericUpDown
    {
        public CustomNumericUpDown()
        {
            InitializeComponent();
        }

        public CustomNumericUpDown(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void UpdateEditText()
        {
            base.UpdateEditText();

            ChangingText = true;
            Text += "%";
        }
    }
}
