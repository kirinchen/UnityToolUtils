using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace surfm.tool {
    public class CommUtils : MonoBehaviour {

        public static string getSha1(byte[] bytes) {
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes) {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}
