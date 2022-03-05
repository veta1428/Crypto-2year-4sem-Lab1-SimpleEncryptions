using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Helpers
{
    public static class HelperMath
    {
        static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        static int Max(int x, int y)
        {
            return x > y ? x : y;
        }

        public static int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);

                return GCD(max - min, min);
            }
        }

        public static int Invert(int a, int m)
        {
            if (a < 1 || m < 2)
                return -1;

            int u1 = m;
            int u2 = 0;
            int v1 = a;
            int v2 = 1;

            while (v1 != 0)
            {
                int q = u1 / v1;
                int t1 = u1 - q * v1;
                int t2 = u2 - q * v2;
                u1 = v1;
                u2 = v2;
                v1 = t1;
                v2 = t2;
            }

            return u1 == 1 ? (u2 + m) % m : -1;
        }
    }
}
