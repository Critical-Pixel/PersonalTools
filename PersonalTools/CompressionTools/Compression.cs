using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PersonalTools.CompressionTools
{
    class Compression
    {
            /// <summary>
            /// Compress a string
            /// </summary>
            /// <param name="text">String to be compressesed</param>
            /// <returns>Comressed string</returns>
            public static string Compress(string text)
            {
                return Compress(Encoding.UTF8.GetBytes(text));
            }

            /// <summary>
            /// Compresses the specified byte array.
            /// </summary>
            /// <param name="byteArray">The byte array.</param>
            /// <returns></returns>
            public static string Compress(byte[] byteArray)
            {
                byte[] buffer = byteArray;
                MemoryStream ms = new MemoryStream();
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(buffer, 0, buffer.Length);
                }

                ms.Position = 0;
                MemoryStream outStream = new MemoryStream();

                byte[] compressed = new byte[ms.Length];
                ms.Read(compressed, 0, compressed.Length);

                byte[] returnBuffer = new byte[compressed.Length + 4];
                System.Buffer.BlockCopy(compressed, 0, returnBuffer, 4, compressed.Length);
                System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, returnBuffer, 0, 4);
                return Convert.ToBase64String(returnBuffer);
            }

            /// <summary>
            /// Decompress a comressed string
            /// </summary>
            /// <param name="compressedText">Comressed string</param>
            /// <returns>decomressed string</returns>
            public static string Decompress(string compressedText)
            {
                byte[] firstBuffer = Convert.FromBase64String(compressedText);
                using (MemoryStream ms = new MemoryStream())
                {
                    int msgLength = BitConverter.ToInt32(firstBuffer, 0);
                    ms.Write(firstBuffer, 4, firstBuffer.Length - 4);

                    byte[] buffer = new byte[msgLength];

                    ms.Position = 0;
                    using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        zip.Read(buffer, 0, buffer.Length);
                    }

                    return Encoding.UTF8.GetString(buffer);
                }
            }
        
    }
}
