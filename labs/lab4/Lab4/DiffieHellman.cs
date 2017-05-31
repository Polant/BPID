using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace Lab4
{
    class DiffieHellman
    {
        public BigInteger p, g, a, A, B, key;

        Random rand = new Random();
        private BigInteger calculate(BigInteger val, BigInteger pow, BigInteger mod)
        {
            return BigInteger.ModPow(val, pow, mod);
        }
        public BigInteger getPublicData()
        {
            A = calculate(g, a, p);
            return A;
        }
        public BigInteger getPrivateKey()
        {
            key = calculate(B, a, p);
            return key;
        }
        public BigInteger genSecret()
        {
            a = new BigInteger(54);//BigIntegerC.genPseudoPrime(512, 20, rand).ToByteArray());
            return a;
        }
        public void setPublicData(string arr)
        {
            B = new BigInteger(stringToBytes(arr));
        }

        public void generateData()
        {
            p = new BigInteger(761);//BigIntegerC.genPseudoPrime(512, 20, rand).ToByteArray());
            g = new BigInteger(6);//BigIntegerC.genPseudoPrime(512, 20, rand).ToByteArray());
            a = new BigInteger(48);//BigIntegerC.genPseudoPrime(512, 20, rand).ToByteArray());
            getPublicData();
        }

        public void generateData(string str)
        {
            genSecret();
            string[] stringSeparators = new string[] { "||" };
            string[] temp = str.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            g = new BigInteger(stringToBytes(temp[0]));
            p = new BigInteger(stringToBytes(temp[1]));
            B = new BigInteger(stringToBytes(temp[2]));
        }
        public string sendPublicData()
        {
            string res = "";
            res += bytesToString(g.ToByteArray());
            res += "||";
            res += bytesToString(p.ToByteArray());
            res += "||";
            res += bytesToString(A.ToByteArray());
            return res;
        }
        public static string bytesToString(byte[] arr)
        {
            String test = "";
            foreach (byte b in arr)
            {
                test += Convert.ToUInt32(b).ToString();
                test += "|";
            }
            return test;
            //return System.Text.Encoding.UTF8.GetString(arr);
        }

        public static byte[] stringToBytes(string str)
        {
            string[] spl = str.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            byte[] res = new byte[spl.Length];
            for (int i = 0; i < res.Length; i++ )
            {
                res[i] = Convert.ToByte(Convert.ToUInt32(spl[i]));
            }
            return res;
        }

        public string print(BigInteger bi)
        {
            string res = "";
            for (int i = 0; i < bi.ToByteArray().Length; i++)
            {
                res += Convert.ToInt32(bi.ToByteArray()[i]).ToString();
            }
            return res;
        }
        
        public BigInteger getPrimitiveRoot()
        {
            BigInteger res = new BigInteger();
            BigInteger temp = p - 1;
            BigInteger[] variants = new BigInteger[BigIntegerC.primesBelow2000.Length];
            int pos = 0;
            while (temp > 1)
            {
                for (int i = 0; i < BigIntegerC.primesBelow2000.Length; i++)
                {
                    if (temp % BigIntegerC.primesBelow2000[i] == 0)
                    {
                        variants[pos] = BigIntegerC.primesBelow2000[i];
                        while (temp % BigIntegerC.primesBelow2000[i] == 0) temp /= BigIntegerC.primesBelow2000[i];
                    }
                }
            }
            bool isPrimR = false;
            for (int i = 0; i < BigIntegerC.primesBelow2000.Length; i++)
            {
                foreach (BigInteger bi in variants)
                {
                    if (BigInteger.ModPow(BigIntegerC.primesBelow2000[0], bi, p) == 1)
                    {
                        isPrimR = false;
                        break;
                    }
                    else
                    {
                        res = bi;
                        isPrimR = true;
                    }
                }
                if (isPrimR == true)
                {
                    break;
                }
            }
            return res;
        }

    }
}
