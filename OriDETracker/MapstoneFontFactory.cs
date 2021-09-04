using System.Collections.Generic;
using System.Drawing;

namespace OriDETracker
{
    internal static class MapstoneFontFactory
    {
        private static readonly Dictionary<TrackerPixelSizes, MapstoneText> mapstone_text_parameters = new Dictionary<TrackerPixelSizes, MapstoneText>()
        {
            { TrackerPixelSizes.Small, new MapstoneText(140+6, 190+6, 14) },
            { TrackerPixelSizes.Medium, new MapstoneText(195+9, 268+9, 18) },
            { TrackerPixelSizes.Large, new MapstoneText(304+13, 417+13, 24) },
            { TrackerPixelSizes.XL, new MapstoneText(342+15, 471+15, 28) }
        };

        internal static MapstoneFont Create(TrackerPixelSizes size, string fontFamily, Brush fontBrush)
        {
            var mapstoneText = mapstone_text_parameters[size];
            var font = new Font(fontFamily, mapstoneText.Size, FontStyle.Bold);
            return new MapstoneFont(font, fontBrush, new Point(mapstoneText.X, mapstoneText.Y));
        }
    }
}
