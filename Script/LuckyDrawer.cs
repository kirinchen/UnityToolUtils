using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace surfm.tool {
    public class LuckyDrawer<T> {

        private Dictionary<T, int> samples = new Dictionary<T, int>();

        public void add(T key, int count) {
            if (samples.ContainsKey(key)) {
                count = samples[key] + count;
                samples.Remove(key);
            }
            samples.Add(key, count);
        }

        public T draw() {
            int sign = Random.Range(1, getTotalCount() + 1);
            foreach (T key in samples.Keys) {
                int c = samples[key];
                sign -= c;
                if (sign <= 0) {
                    return key;
                }
            }
            throw new System.Exception("error sign=" + sign);
        }

        private int getTotalCount() {
            int ans = 0;
            foreach (int i in samples.Values) {
                ans += i;
            }
            return ans;
        }

        public enum TE {
            a, b, c
        }
        public static int test() {

            LuckyDrawer<TE> ld = new LuckyDrawer<TE>();
            int a = 0, b = 0, c = 0;
            ld.add(TE.a, 2);
            ld.add(TE.b, 2);
            ld.add(TE.c, 2);


            for (int i = 0; i < 99999; i++) {
                TE d = ld.draw();
                switch (d) {
                    case TE.a:
                        a++;
                        break;
                    case TE.b:
                        b++;
                        break;
                    case TE.c:
                        c++;
                        break;
                }
            }

            float total = a + b + c;
            float ra = a / total, rb = b / total, rc = c / total;
            Debug.Log(string.Format("ra={0} rb={1} rc={2} | a={3} b={4} c={5}", ra, rb, rc, a, b, c));
            return 0;
        }
    }
}
