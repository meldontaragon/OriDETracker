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

    public enum TrackerPixelSizes
    {
        size300px = 312,
        size420px = 437,
        size640px = 667,
        size720px = 750
    }
    public enum AutoUpdateRefreshRates
    {
        rate500mHz = 500,
        rate1Hz = 1000,
        rate10Hz = 10000,
        rate60Hz = 60000  
    }
}
