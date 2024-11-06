using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using ZstdSharp.Unsafe;
using System.IO.Compression;

namespace AnimKit.Core.CodeWalker.Core.GameFiles
{
   public class rpfcompression
    {
        public static byte[] Decompress3(byte[] data, int offset = 0)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                memoryStream.Position = (long)offset;
                using (DeflateStream deflateStream = new DeflateStream((Stream)memoryStream, CompressionMode.Decompress))
                {
                    MemoryStream destination = new MemoryStream();
                    deflateStream.CopyTo((Stream)destination);
                    byte[] buffer = destination.GetBuffer();
                    byte[] numArray = new byte[destination.Length];
                    byte[] destinationArray = numArray;
                    int length = numArray.Length;
                    Array.Copy((Array)buffer, (Array)destinationArray, length);
                    return numArray;
                }
            }
        }

        public static byte[] Decompress(byte[] data, int decompressedSize)
        {
            byte[] outputBuffer = new byte[decompressedSize];
            
            
            //if ((long)Decompress1(data, (uint)data.Length, ref outputBuffer, (uint)decompressedSize) != (long)decompressedSize)
            //    throw new Exception("Decompression failed. Verification size does not match given size.");
            //    System.Diagnostics.Debug.WriteLine("Decompression failed. Verification size does not match given size.");
            Decompress1(data, (uint)data.Length, ref outputBuffer, (uint)decompressedSize);
            System.Diagnostics.Debug.WriteLine(BitConverter.ToString(outputBuffer));
            return outputBuffer;
        }
        public static uint Decompress1(
            byte[] buffer,
            uint bufferSize,
            ref byte[] outputBuffer,
            uint outputBufferSize)
        {   
            
            if (buffer.Length != 0 && bufferSize > 0U && outputBuffer.Length != 0 && outputBufferSize > 0U)
            {
                System.Diagnostics.Debug.WriteLine("Match");
                return (uint)OodleLZ_Decompress(buffer, (long)bufferSize, outputBuffer, (long)outputBufferSize, 0U, 0U,
                    0U, 0U, 0U, 0U, 0U, 0U, 0U, 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Dicklal");
                return 0U;
            }
        }
        [DllImport("C:\\Users\\sobek\\Documents\\animkitcopy\\AnimKit\\Core\\CodeWalker.Core\\oo2core_5_win64.dll")]
        public static extern int OodleLZ_Decompress(
            byte[] Buffer,
            long BufferSize,
            byte[] OutputBuffer,
            long OutputBufferSize,
            uint a,
            uint b,
            uint c,
            uint d,
            uint e,
            uint f,
            uint g,
            uint h,
            uint i,
            int ThreadModule);
    }
  
}

