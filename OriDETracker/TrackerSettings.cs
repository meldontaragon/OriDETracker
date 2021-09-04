using System.Configuration;
using System.Drawing;

namespace OriDETracker
{
    public class TrackerSettings : ApplicationSettingsBase
    {
        private static readonly TrackerSettings MainSettings = (TrackerSettings)Synchronized(new TrackerSettings());

        public static TrackerSettings Default
        {
            get
            {
                return MainSettings;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("rate10Hz")]
        public AutoUpdateRefreshRates RefreshRate
        {
            get
            {
                return (AutoUpdateRefreshRates)this["RefreshRate"];
            }
            set
            {
                this["RefreshRate"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1")]
        public double Opacity
        {
            get
            {
                return (double)this["Opacity"];
            }
            set
            {
                this["Opacity"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool Shards
        {
            get
            {
                return (bool)this["Shards"];
            }
            set
            {
                this["Shards"] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Mapstones
        {
            get
            {
                return (bool)this["Mapstones"];
            }
            set
            {
                this["Mapstones"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Teleporters
        {
            get
            {
                return (bool)this["Teleporters"];
            }
            set
            {
                this["Teleporters"] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Trees
        {
            get
            {
                return (bool)this["Trees"];
            }
            set
            {
                this["Trees"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool Relics
        {
            get
            {
                return (bool)this["Relics"];
            }
            set
            {
                this["Relics"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool DisplayEmptyRelics
        {
            get
            {
                return (bool)this["DisplayEmptyRelics"];
            }
            set
            {
                this["DisplayEmptyRelics"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool DisplayEmptyTeleporters
        {
            get
            {
                return (bool)this["DisplayEmptyTeleporters"];
            }
            set
            {
                this["DisplayEmptyTeleporters"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool DisplayEmptyTrees
        {
            get
            {
                return (bool)this["DisplayEmptyTrees"];
            }
            set
            {
                this["DisplayEmptyTrees"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool DisplayEmptyShards
        {
            get
            {
                return (bool)this["DisplayEmptyShards"];
            }
            set
            {
                this["DisplayEmptyShards"] = value;
            }
        }        

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool AlwaysOnTop
        {
            get
            {
                return (bool)this["AlwaysOnTop"];
            }
            set
            {
                this["AlwaysOnTop"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool AutoUpdate
        {
            get
            {
                return (bool)this["AutoUpdate"];
            }
            set
            {
                this["AutoUpdate"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Draggable
        {
            get
            {
                return (bool)this["Draggable"];
            }
            set
            {
                this["Draggable"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Medium")]
        public TrackerPixelSizes Pixels
        {
            get
            {
                return (TrackerPixelSizes)this["Pixels"];
            }
            set
            {
                this["Pixels"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public FontFamily MapFont
        {
            get
            {
                return (FontFamily)this["MapFont"];
            }
            set
            {
                this["MapFont"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("White")]
        public Color FontColoring
        {
            get
            {
                return (Color)this["FontColoring"];
            }
            set
            {
                this["FontColoring"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Black")]
        public Color Background
        {
            get
            {
                return (Color)this["Background"];
            }
            set
            {
                this["Background"] = value;
            }
        }
    }
}

