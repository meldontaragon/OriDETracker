using System.Configuration;

namespace OriDETracker
{
    public class TrackerSettings : ApplicationSettingsBase
    {
        private static readonly TrackerSettings MainSettings = (TrackerSettings)ApplicationSettingsBase.Synchronized(new TrackerSettings());

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
        [DefaultSettingValueAttribute("true")]
        public bool Mapstones
        {
            get
            {
                return ((bool)(this["Mapstones"]));
            }
            set
            {
                this["Mapstones"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool Teleporters
        {
            get
            {
                return ((bool)(this["Teleporters"]));
            }
            set
            {
                this["Teleporters"] = value;
            }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool Trees
        {
            get
            {
                return ((bool)(this["Trees"]));
            }
            set
            {
                this["Trees"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool Relics
        {
            get
            {
                return ((bool)(this["Relics"]));
            }
            set
            {
                this["Relics"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool DisplayEmptyRelics
        {
            get
            {
                return ((bool)(this["DisplayEmptyRelics"]));
            }
            set
            {
                this["DisplayEmptyRelics"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool DisplayEmptyTeleporters
        {
            get
            {
                return ((bool)(this["DisplayEmptyTeleporters"]));
            }
            set
            {
                this["DisplayEmptyTeleporters"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool DisplayEmptyTrees
        {
            get
            {
                return ((bool)(this["DisplayEmptyTrees"]));
            }
            set
            {
                this["DisplayEmptyTrees"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool AlwaysOnTop
        {
            get
            {
                return ((bool)(this["AlwaysOnTop"]));
            }
            set
            {
                this["AlwaysOnTop"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool AutoUpdate
        {
            get
            {
                return ((bool)(this["AutoUpdate"]));
            }
            set
            {
                this["AutoUpdate"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool Draggable
        {
            get
            {
                return ((bool)(this["Draggable"]));
            }
            set
            {
                this["Draggable"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("Medium")]
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
        [DefaultSettingValueAttribute("")]
        public System.Drawing.FontFamily MapFont
        {
            get
            {
                return ((System.Drawing.FontFamily)(this["MapFont"]));
            }
            set
            {
                this["MapFont"] = value;
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

