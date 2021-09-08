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
        [DefaultSettingValue(nameof(AutoUpdateRefreshRates.rate10Hz))]
        public AutoUpdateRefreshRates RefreshRate
        {
            get
            {
                return (AutoUpdateRefreshRates)this[nameof(RefreshRate)];
            }
            set
            {
                this[nameof(RefreshRate)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1")]
        public double Opacity
        {
            get
            {
                return (double)this[nameof(Opacity)];
            }
            set
            {
                this[nameof(Opacity)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool Shards
        {
            get
            {
                return (bool)this[nameof(Shards)];
            }
            set
            {
                this[nameof(Shards)] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Mapstones
        {
            get
            {
                return (bool)this[nameof(Mapstones)];
            }
            set
            {
                this[nameof(Mapstones)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Teleporters
        {
            get
            {
                return (bool)this[nameof(Teleporters)];
            }
            set
            {
                this[nameof(Teleporters)] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Trees
        {
            get
            {
                return (bool)this[nameof(Trees)];
            }
            set
            {
                this[nameof(Trees)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool Relics
        {
            get
            {
                return (bool)this[nameof(Relics)];
            }
            set
            {
                this[nameof(Relics)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool DisplayEmptyRelics
        {
            get
            {
                return (bool)this[nameof(DisplayEmptyRelics)];
            }
            set
            {
                this[nameof(DisplayEmptyRelics)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool DisplayEmptyTeleporters
        {
            get
            {
                return (bool)this[nameof(DisplayEmptyTeleporters)];
            }
            set
            {
                this[nameof(DisplayEmptyTeleporters)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool DisplayEmptyTrees
        {
            get
            {
                return (bool)this[nameof(DisplayEmptyTrees)];
            }
            set
            {
                this[nameof(DisplayEmptyTrees)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool DisplayEmptyShards
        {
            get
            {
                return (bool)this[nameof(DisplayEmptyShards)];
            }
            set
            {
                this[nameof(DisplayEmptyShards)] = value;
            }
        }        

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool AlwaysOnTop
        {
            get
            {
                return (bool)this[nameof(AlwaysOnTop)];
            }
            set
            {
                this[nameof(AlwaysOnTop)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool AutoUpdate
        {
            get
            {
                return (bool)this[nameof(AutoUpdate)];
            }
            set
            {
                this[nameof(AutoUpdate)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool Draggable
        {
            get
            {
                return (bool)this[nameof(Draggable)];
            }
            set
            {
                this[nameof(Draggable)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue(nameof(TrackerPixelSizes.Medium))]
        public TrackerPixelSizes Pixels
        {
            get
            {
                return (TrackerPixelSizes)this[nameof(Pixels)];
            }
            set
            {
                this[nameof(Pixels)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string MapFontFamilyName
        {
            get
            {
                return (string)this[nameof(MapFontFamilyName)];
            }
            set
            {
                this[nameof(MapFontFamilyName)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue(nameof(Color.White))]
        public Color MapFontColor
        {
            get
            {
                return (Color)this[nameof(MapFontColor)];
            }
            set
            {
                this[nameof(MapFontColor)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue(nameof(Color.Black))]
        public Color Background
        {
            get
            {
                return (Color)this[nameof(Background)];
            }
            set
            {
                this[nameof(Background)] = value;
            }
        }
    }
}

