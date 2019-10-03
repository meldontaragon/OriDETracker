using System;
using System.Diagnostics;

namespace OriDE.Memory
{
    public partial class OriMemory
    {
        private static readonly bool mono_debug = false;
        private static readonly string BitfieldPtrString = mono_debug ? "B9EFBEADDEB8????????8908" : "B8????????C700EFBEADDE";
        private static readonly int BitfieldsPtrOffset = BitfieldPtrString.IndexOf('?') / 2;
        private static readonly ProgramPointer TrackerBitfields = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, BitfieldPtrString, BitfieldsPtrOffset));
        public Process Program { get; set; }
        public bool IsHooked { get; set; } = false;
        private DateTime lastHooked;
        private static readonly Skill[] AllSkills = new Skill[] { Skill.Sein, Skill.WallJump, Skill.ChargeFlame, Skill.Dash, Skill.DoubleJump, Skill.Bash, Skill.Stomp, Skill.Glide, Skill.Climb, Skill.ChargeJump, Skill.Grenade };

        public OriMemory()
        {
            lastHooked = DateTime.MinValue;
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
        public void Dispose()
        {
            if (Program != null) { this.Program.Dispose(); }
        }

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
        private readonly ProgramSignature[] signatures;
        private readonly int[] offsets;
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
                searcher.MemoryFilter = delegate (MemInfo info)
                {
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