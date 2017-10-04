using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Drawing;

namespace OriDETracker
{
    public class TrackerSettings : ApplicationSettingsBase
    {
      
        private static TrackerSettings MainSettings = (TrackerSettings) ApplicationSettingsBase.Synchronized(new TrackerSettings());

        
        public static TrackerSettings Default
        {
            get
            {
                return MainSettings;
            }
        }
        

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("rate10Hz")]
        public AutoUpdateRefreshRates RefreshRate
        {
            get
            {
                return ((AutoUpdateRefreshRates)(this["RefreshRate"]));
            }
            set
            {
                this["RefreshRate"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("1")]
        public double Opacity
        {
            get
            {
                return ((double)(this["Opacity"]));
            }
            set
            {
                this["Opacity"] = value;
            }
        }


        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool Shards
        {
            get
            {
                return ((bool)(this["Shards"]));
            }
            set
            {
                this["Shards"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("size640px")]
        public TrackerPixelSizes Pixels
        {
            get
            {
                return ((TrackerPixelSizes)(this["Pixels"]));
            }
            set
            {
                this["Pixels"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("RandomizerAllTrees")]
        public TrackerLayout Layout
        {
            get
            {
                return ((TrackerLayout)(this["Layout"]));
            }
            set
            {
                this["Layout"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("White")]
        public System.Drawing.Color FontColoring
        {
            get
            {
                return ((System.Drawing.Color)(this["FontColoring"]));
            }
            set
            {
                this["FontColoring"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("Black")]
        public System.Drawing.Color Background
        {
            get
            {
                return ((System.Drawing.Color)(this["Background"]));
            }
            set
            {
                this["Background"] = value;
            }
        }
    }
}

