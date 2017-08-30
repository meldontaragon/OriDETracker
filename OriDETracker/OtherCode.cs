using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class MapstoneText
    {
        public int X;
        public int Y;
        public int TextSize;

        public MapstoneText(int a, int b, int size)
        {
            X = a;
            Y = b;
            TextSize = size;
        }
    }
    public enum TrackerPixelSizes
    {
        size300px = 300,
        size420px = 420,
        size640px = 640,
        size720px = 720
    }
    public enum AutoUpdateRefreshRates
    {
        rate500mHz = 500,
        rate1Hz = 1000,
        rate10Hz = 10000,
        rate60Hz = 60000  
    }
}
