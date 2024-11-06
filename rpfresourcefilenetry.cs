using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimKit.Core.CodeWalker.Core.GameFiles
{
    public class rpfresourcefilenetry: rpffileentry
    {
        public static rpfresourcefilenetry Create(ref byte[] data, uint ver)
        {
            rpfresourcefilenetry resourceFileEntry = new rpfresourcefilenetry();
            if (BitConverter.ToUInt32(data, 0) == 943936338U)
            {
                int int32 = BitConverter.ToInt32(data, 4);
                resourceFileEntry.VirtualFlags = BitConverter.ToUInt32(data, 8);
                resourceFileEntry.PhysicalFlags = BitConverter.ToUInt32(data, 12);
                resourceFileEntry.CompressorId = (byte)((int32 >> 8 & 31) + 1);
                resourceFileEntry.EncryptionKeyId = (byte)((int32 >> 16 & (int)byte.MaxValue) - 1);
                if (data.Length > 16)
                {
                    int count = data.Length - 16;
                    byte[] dst = new byte[count];
                    Buffer.BlockCopy((Array)data, 16, (Array)dst, 0, count);
                    data = dst;

                }
            }
            else
            {
                resourceFileEntry.VirtualFlags = (uint)((data.Length & -16) + ((int)(ver >> 4) & 15));
                resourceFileEntry.PhysicalFlags = ver & 15U;
            }
            
            resourceFileEntry.IsResource = true;
            return resourceFileEntry;
        }
    }
}
