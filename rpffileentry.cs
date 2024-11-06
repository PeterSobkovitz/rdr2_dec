using CodeWalker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimKit.Core.CodeWalker.Core.GameFiles
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class rpffileentry
    {
        public ulong Data1 { get; set; }
        public byte EncryptionKeyId { get; set; }
        public byte Flags { get; set; }
        public uint VirtualFlags { get; set; }

        public uint PhysicalFlags { get; set; }
        public bool IsResource
        {
            get => ((int)this.Flags & 1) == 1;
            set => this.Flags = (byte)BitUtil.UpdateBit((uint)this.Flags, 0, value);
        }
        public byte CompressorId
        {
            get => (byte)(this.Data1 >> 59 & 31UL);
            set
            {
                this.Data1 = (ulong)((long)this.Data1 & 576460752303423487L | ((long)value & 31L) << 59);
            }
        }
        public uint VirtualSize => this.VirtualFlags & 4294967280U;

        public uint PhysicalSize => this.PhysicalFlags & 4294967280U;
        public uint DecompressedSize
        {
            get
            {
                return this.IsResource ? this.VirtualSize + this.PhysicalSize : this.VirtualFlags + this.PhysicalFlags;
            }
        }
    }
}
