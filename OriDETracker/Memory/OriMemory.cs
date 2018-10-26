using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace OriDE.Memory
{
    public partial class OriMemory
    {
        private static bool mono_debug = false;
        private static string BitfieldPtrString = mono_debug ? "B9EFBEADDEB8????????8908" : "B8????????C700EFBEADDE";
        private static int BitfieldsPtrOffset = BitfieldPtrString.IndexOf('?')/2;
        private static ProgramPointer TrackerBitfields = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, BitfieldPtrString, BitfieldsPtrOffset));
        public Process Program { get; set; }
        public bool IsHooked { get; set; } = false;
        private DateTime lastHooked;
        private static Skill[] AllSkills = new Skill[] { Skill.Sein, Skill.WallJump, Skill.ChargeFlame, Skill.Dash, Skill.DoubleJump, Skill.Bash, Skill.Stomp, Skill.Glide, Skill.Climb, Skill.ChargeJump, Skill.Grenade };

        public OriMemory()
        {
            lastHooked = DateTime.MinValue;
        }

        public Dictionary<string, bool> GetEvents()
        {
            Dictionary<string, bool> results = new Dictionary<string, bool>();
            foreach (var pair in events)
            {
                // results[pair.Key] = WorldEvents.Read<bool>(Program, pair.Value + 0x40);
            }
            return results;
        }
        public bool GetEvent(string name)
        {
            int offset = events[name];
            return false;
//            return WorldEvents.Read<bool>(Program, offset + 0x40);
        }
        public Dictionary<string, bool> GetKeys()
        {
            Dictionary<string, bool> results = new Dictionary<string, bool>();
            foreach (var pair in keys)
            {
              //  results[pair.Key] = WorldEvents.Read<bool>(Program, pair.Value);
            }
            return results;
        }
        public bool GetKey(string name)
        {
            int key = keys[name];
            return false;
            //                        return WorldEvents.Read<bool>(Program, key);
        }
        public Dictionary<string, bool> GetAbilities()
        {
            Dictionary<string, bool> results = new Dictionary<string, bool>();
            foreach (var pair in abilities)
            {
//                results[pair.Key] = SeinCharacter.Read<bool>(Program, 0x0, 0x4c, pair.Value * 4, 0x08);
            }
            return results;
        }

        public int TreeBitfield;
        public int RelicBitfield;
        public int MapstoneBitfield;
        public int TeleporterBitfield;
        public int KeyEventBitfield;

        public void GetBitfields()
        {
            TreeBitfield = TrackerBitfields.Read<int>(Program, 0x0);
            MapstoneBitfield = TrackerBitfields.Read<int>(Program, 0x4);
            TeleporterBitfield = TrackerBitfields.Read<int>(Program, 0x8);
            RelicBitfield = TrackerBitfields.Read<int>(Program, 0xc);
            KeyEventBitfield = TrackerBitfields.Read<int>(Program, 0x10);
        }

        public bool GetBit(int bitfield, int bit)
        {
            return (bitfield >> bit) % 2 == 1;
        }

        public bool HookProcess()
        {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > lastHooked.AddSeconds(1))
            {
                lastHooked = DateTime.Now;
                Process[] processes = Process.GetProcessesByName("OriDE");
                Program = processes.Length == 0 ? null : processes[0];
                if (Program != null && !Program.HasExited)
                {
                    MemoryReader.Update64Bit(Program);
                    IsHooked = true;
                }
            }

            return IsHooked;
        }
        public void AddLogItems(List<string> items)
        {
            foreach (string key in keys.Keys)
            {
                items.Add(key);
            }
            foreach (string key in events.Keys)
            {
                items.Add(key);
            }
            foreach (string key in abilities.Keys)
            {
                items.Add(key);
            }
        }
        public void Dispose()
        {
            if (Program != null) { this.Program.Dispose(); }
        }

        public static Dictionary<string, int> keys = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"Water Vein",   0},
            {"Gumon Seal",   1},
            {"Sunstone",     2},
        };
        public static Dictionary<string, int> events = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"Ginso Tree Entered",   0},
            {"Mist Lifted",          1},
            {"Clean Water",          2},
            {"Wind Restored",        3},
            {"Gumo Free",            4},
            {"Spirit Tree Reached",  5},
            {"Warmth Returned",      6},
            {"Darkness Lifted",      7}
        };
        public static Dictionary<string, int> abilities = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"Bash",                     5},
            {"Charge Flame",             6},
            {"Wall Jump",                7},
            {"Stomp",                    8},
            {"Double Jump",              9},
            {"Charge Jump",              10},
            {"Magnet",                   11},
            {"Ultra Magnet",             12},
            {"Climb",                    13},
            {"Glide",                    14},
            {"Spirit Flame",             15},
            {"Rapid Fire",               16},
            {"Soul Efficiency",          17},
            {"Water Breath",             18},
            {"Charge Flame Blast",       19},
            {"Charge Flame Burn",        20},
            {"Double Jump Upgrade",      21},
            {"Bash Upgrade",             22},
            {"Ultra Defense",            23},
            {"Health Efficiency",        24},
            {"Sense",                    25},
            {"Stomp Upgrade",            26},
            {"Quick Flame",              27},
            {"Map Markers",              28},
            {"Energy Efficiency",        29},
            {"Health Markers",           30},
            {"Energy Markers",           31},
            {"Ability Markers",          32},
            {"Rekindle",                 33},
            {"Regroup",                  34},
            {"Charge Flame Efficiency",  35},
            {"Ultra Soul Flame",         36},
            {"Soul Flame Efficiency",    37},
            {"Split Flame",              38},
            {"Spark Flame",              39},
            {"Cinder Flame",             40},
            {"Ultra Split Flame",        41},
            {"Light Grenade",            42},
            {"Dash",                     43},
            {"Grenade Upgrade",          44},
            {"Charge Dash",              45},
            {"Air Dash",                 46},
            {"Grenade Efficiency",       47}
        };
    }
    public enum PointerVersion
    {
        V1
    }
    public enum AutoDeref
    {
        None,
        Single,
        Double
    }
    public class ProgramSignature
    {
        public PointerVersion Version { get; set; }
        public string Signature { get; set; }
        public int Offset { get; set; }
        public ProgramSignature(PointerVersion version, string signature, int offset)
        {
            Version = version;
            Signature = signature;
            Offset = offset;
        }
        public override string ToString()
        {
            return Version.ToString() + " - " + Signature;
        }
    }
    public class ProgramPointer
    {
        private int lastID;
        private DateTime lastTry;
        private ProgramSignature[] signatures;
        private int[] offsets;
        public IntPtr Pointer { get; private set; }
        public PointerVersion Version { get; private set; }
        public AutoDeref AutoDeref { get; private set; }

        public ProgramPointer(AutoDeref autoDeref, params ProgramSignature[] signatures)
        {
            AutoDeref = autoDeref;
            this.signatures = signatures;
            lastID = -1;
            lastTry = DateTime.MinValue;
        }
        public ProgramPointer(AutoDeref autoDeref, params int[] offsets)
        {
            AutoDeref = autoDeref;
            this.offsets = offsets;
            lastID = -1;
            lastTry = DateTime.MinValue;
        }

        public T Read<T>(Process program, params int[] offsets) where T : struct
        {
            GetPointer(program);
            return program.Read<T>(Pointer, offsets);
        }
        public string Read(Process program, params int[] offsets)
        {
            GetPointer(program);
            return program.Read((IntPtr)program.Read<uint>(Pointer, offsets));
        }
        public byte[] ReadBytes(Process program, int length, params int[] offsets)
        {
            GetPointer(program);
            return program.Read(Pointer, length, offsets);
        }
        public void Write<T>(Process program, T value, params int[] offsets) where T : struct
        {
            GetPointer(program);
            program.Write<T>(Pointer, value, offsets);
        }
        public void Write(Process program, byte[] value, params int[] offsets)
        {
            GetPointer(program);
            program.Write(Pointer, value, offsets);
        }
        public IntPtr GetPointer(Process program)
        {
            if (program == null)
            {
                Pointer = IntPtr.Zero;
                lastID = -1;
                return Pointer;
            }
            else if (program.Id != lastID)
            {
                Pointer = IntPtr.Zero;
                lastID = program.Id;
            }

            if (Pointer == IntPtr.Zero && DateTime.Now > lastTry.AddSeconds(1))
            {
                lastTry = DateTime.Now;

                Pointer = GetVersionedFunctionPointer(program);
                if (Pointer != IntPtr.Zero)
                {
                    if (AutoDeref != AutoDeref.None)
                    {
                        Pointer = (IntPtr)program.Read<uint>(Pointer);
                        if (AutoDeref == AutoDeref.Double)
                        {
                            if (MemoryReader.is64Bit)
                            {
                                Pointer = (IntPtr)program.Read<ulong>(Pointer);
                            }
                            else
                            {
                                Pointer = (IntPtr)program.Read<uint>(Pointer);
                            }
                        }
                    }
                }
            }
            return Pointer;
        }
        private IntPtr GetVersionedFunctionPointer(Process program)
        {
            if (signatures != null)
            {
                MemorySearcher searcher = new MemorySearcher();
                searcher.MemoryFilter = delegate (MemInfo info) {
                    return (info.State & 0x1000) != 0 && (info.Protect & 0x40) != 0 && (info.Protect & 0x100) == 0;
                };
                for (int i = 0; i < signatures.Length; i++)
                {
                    ProgramSignature signature = signatures[i];

                    IntPtr ptr = searcher.FindSignature(program, signature.Signature);
                    if (ptr != IntPtr.Zero)
                    {
                        Version = signature.Version;
                        return ptr + signature.Offset;
                    }
                }
            }
            else
            {
                IntPtr ptr = (IntPtr)program.Read<uint>(program.MainModule.BaseAddress, offsets);
                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }
            }

            return IntPtr.Zero;
        }
    }
}