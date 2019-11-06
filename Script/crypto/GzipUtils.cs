using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using BestHTTP.Decompression.Zlib;
using System;

namespace surfm.tool {
    public static class GzipUtils  {


        public static string compressGzip(this string plain) {
            byte[] byteArray = Encoding.ASCII.GetBytes(plain);
            MemoryStream stream = new MemoryStream(byteArray);
            using (GZipStream gs = new GZipStream(stream, CompressionMode.Compress)) {
                gs.Write(byteArray, 0, byteArray.Length);
                gs.Close();
                string compressedBase64 = Convert.ToBase64String(stream.ToArray());
                return compressedBase64;
            }
        }

    }
}