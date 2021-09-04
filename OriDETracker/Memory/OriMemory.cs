using System;
using System.Diagnostics;

namespace OriDE.Memory
{
    public class OriMemory : IDisposable
    {
        private static readonly bool mono_debug = false;
        private static readonly string BitfieldPtrString = mono_debug ? "B9EFBEADDEB8????????8908" : "B8????????C700EFBEADDE";
        private static readonly int BitfieldsPtrOffset = BitfieldPtrString.IndexOf('?') / 2;
        private static readonly ProgramPointer TrackerBitfields = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, BitfieldPtrString, BitfieldsPtrOffset));
        private Process Program;
        private bool IsHooked;
        private DateTime lastHooked;

        public MemoryBitfields GetBitfields()
        {
            var bitfields = TrackerBitfields.ReadBytes(Program, 20);
            return new MemoryBitfields
            {
                TreeBitfield = BitConverter.ToInt32(bitfields, 0),
                MapstoneBitfield = BitConverter.ToInt32(bitfields, 4),
                TeleporterBitfield = BitConverter.ToInt32(bitfields, 8),
                RelicBitfield = BitConverter.ToInt32(bitfields, 12),
                KeyEventBitfield = BitConverter.ToInt32(bitfields, 16)
            };
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
            Program?.Dispose();
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
        public PointerVersion Version { get; private set; }
        public string Signature { get; private set; }
        public int Offset { get; private set; }

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

        public byte[] ReadBytes(Process program, int length)
        {
            GetPointer(program);
            return program.Read(Pointer, length);
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
                MemorySearcher searcher = new MemorySearcher
                {
                    MemoryFilter = delegate (MemInfo info)
                    {
                        return (info.State & 0x1000) != 0 && (info.Protect & 0x40) != 0 && (info.Protect & 0x100) == 0;
                    }
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

            return IntPtr.Zero;
        }
    }
}