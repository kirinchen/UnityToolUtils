using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
namespace surfm.tool {
    public class CommUtils : MonoBehaviour {

        public static string getSha1(string s) {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            return getSha1(bytes);
        }

        public static string getSha1(byte[] bytes) {
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes) {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }


        public static string encryptAES(string k, string byteKey, string source) {
            AesCryptoServiceProvider des = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(k);
            byte[] iv = Encoding.ASCII.GetBytes(byteKey);
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
            des.Key = key;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write)) {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

        public static string decryptAES(string k, string byteKey, string encrypt) {
            AesCryptoServiceProvider des = new AesCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(k);
            byte[] iv = Encoding.ASCII.GetBytes(byteKey);
            des.Key = key;
            des.IV = iv;

            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        public static string encryptDES(string k, string byteKey, string source) {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(k);
            byte[] iv = Encoding.ASCII.GetBytes(byteKey);
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
            des.Key = key;
            des.IV = iv;
            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write)) {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

        public static string decryptDES(string k, string byteKey, string encrypt) {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(k);
            byte[] iv = Encoding.ASCII.GetBytes(byteKey);
            des.Key = key;
            des.IV = iv;

            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }


        public static string encodeBase64(string plain) {
            if (string.IsNullOrEmpty(plain)) return string.Empty;
            byte[] k = Encoding.UTF8.GetBytes(plain);
            return Convert.ToBase64String(k);
        }

        public static string decodeBase64(string base64) {
            if (string.IsNullOrEmpty(base64)) return string.Empty;
            byte[] data = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(data);
        }


        public static T newInstance<T>(string strFullyQualifiedName) {
            Type t = Type.GetType(strFullyQualifiedName);
            return (T)Activator.CreateInstance(t);
        }

        internal static bool isListEqua<T>(List<T> la, List<T> lb) {
            if (la == lb) return true;
            if (la == null || lb == null) return false;
            if (la.Count != lb.Count) return false;
            for (int i = 0; i < la.Count; i++) {
                if (!la[i].Equals(lb[i])) {
                    return false;
                }
            }
            return true;
        }


        public static void writeEnum(string clzName, List<string> ers, string fileDir) {
            string fc = "public enum {0}  @  {1}  $ ";
            string es = "\n";
            foreach (string s in ers) {
                es += s + ", \n";
            }

            string outputS = string.Format(fc, clzName, es);
            outputS = outputS.Replace('@', '{');
            outputS = outputS.Replace('$', '}');
            System.IO.File.WriteAllText(fileDir + clzName + ".cs", outputS);
        }

        public static string getContentByClip(string plain, string startClip, string endClip) {
            string preg = string.Format(@"\{0}([^)]*)\{1}", startClip, endClip);
            //string preg = @"\[([^)]*)\]";
            Regex reg = new Regex(preg);
            Match m = reg.Match(plain);
            if (m.Success)
                return m.Result("$1");

            return string.Empty;
        }

        public static object convert(object source,Type type) {
            string json= JsonConvert.SerializeObject(source, ObscuredValueConverter.DEFAULT);
            return JsonConvert.DeserializeObject(json,type, ObscuredValueConverter.DEFAULT);
        }

        public static E optMap<T, E>(Dictionary<T, E> map, T key,E _de = default) {
            if (map.ContainsKey(key)) return map[key];
            return _de;
        }


        public static List<Type> listExtends(Type root) {
            return Assembly.GetAssembly(root).GetTypes()
              .Where(x => root.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }

    }


}
