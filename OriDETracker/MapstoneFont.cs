using System;
using System.Drawing;

namespace OriDETracker
{
    internal class MapstoneFont : IDisposable
    {
        internal Font Font { get; }
        internal Brush Brush { get; }
        internal Point Location { get; }

        internal MapstoneFont(Font font, Brush brush, Point location)
        {
            Font = font;
            Brush = brush;
            Location = location;
        }

        public void Dispose()
        {
            Font?.Dispose();
        }
    }
}
