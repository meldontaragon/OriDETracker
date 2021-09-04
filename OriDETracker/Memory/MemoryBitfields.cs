namespace OriDE.Memory
{
    public struct MemoryBitfields
    {
        public static MemoryBitfields Empty => new MemoryBitfields(0, 0, 0, 0, 0);
        public int TreeBitfield { get; set; }
        public int MapstoneBitfield { get; set; }
        public int TeleporterBitfield { get; set; }
        public int RelicBitfield { get; set; }        
        public int KeyEventBitfield { get; set; }

        public MemoryBitfields(int treeBitfield, int mapstoneBitField, int teleporterBitfield, int relicBitfield, int keyEventBitfield)
        {
            TreeBitfield = treeBitfield;
            MapstoneBitfield = mapstoneBitField;
            TeleporterBitfield = teleporterBitfield;
            RelicBitfield = relicBitfield;
            KeyEventBitfield = keyEventBitfield;
        }
    }
}