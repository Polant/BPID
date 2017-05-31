using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;


namespace Lab3Digital
{
    public partial class Form1 : Form
    {
        private RSAParameters rsapamPrKey=new RSAParameters();
        private byte[] encData;
        public RSACryptoServiceProvider csp;

        public Form1()
        {
            InitializeComponent();
            csp = new RSACryptoServiceProvider();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
                byte[] a = File.ReadAllBytes(dialog.FileName);
                string s = a.GetLength(0).ToString();
                //string s = "{" + string.Join(", ", a.Select(x => string.Format("0x{0}", x.ToString("X")))) + "}";
                label1.Text = s;
                byte[] b = BitConverter.GetBytes(GetHash(a));
                label1.Text = b.ToString();
                var finalHash = b[0] & 0x0F;
                label1.Text = finalHash.ToString("X");
                label1.Text = Convert.ToString(finalHash, 2);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();//make a new csp with a new keypair
                var pub_key = csp.ExportParameters(false); // export public key
                //var priv_key = csp.ExportParameters(true); // export private key
                rsapamPrKey= csp.ExportParameters(true);
                RSAParameters pub = csp.ExportParameters(false);
                label10.Text = BitConverter.ToString(pub_key.Exponent);
                label9.Text = BitConverter.ToString(rsapamPrKey.D);
                label8.Text = BitConverter.ToString(pub_key.Modulus);

                encData = csp.Encrypt(BitConverter.GetBytes(finalHash), false); // encrypt with PKCS#1_V1.5 Padding

                //byte[] ar = BitConverter.GetBytes(finalHash);
                //label1.Text = BitConverter.GetBytes(finalHash).ToString();

                label2.Text = PrintBytes(encData);

            }
        }

        private int GetHash(byte[] array)
        {
            int hash = 0;

            foreach (var item in array)
            {
                hash += item;
            }

            return hash;
        }

        private string PrintBytes(byte[] array)
        {
            string res = "";

            foreach (var item in array)
            {
                res += Convert.ToString(item)+" ";
            }

            return res;
        }
        public void rsaPlayground()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            //RSACryptoServiceProvider csp = new RSACryptoServiceProvider();//make a new csp with a new keypair
            var pub_key = csp.ExportParameters(false); // export public key
            var priv_key = csp.ExportParameters(true); // export private key

            var encData = csp.Encrypt(data, false); // encrypt with PKCS#1_V1.5 Padding
            var decBytes = MyRSAImpl.plainDecryptPriv(encData, priv_key); //decrypt with own BigInteger based implementation
            var decData = decBytes.SkipWhile(x => x != 0).Skip(1).ToArray();//strip PKCS#1_V1.5 padding
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //make a new csp with a new keypair
            //var decBytes = csp.Decrypt(encData, false);
           // var decBytes = MyRSAImpl.plainDecryptPriv(encData, rsapamPrKey); //decrypt with own BigInteger based implementation
            //label11.Text = BitConverter.ToInt32(decBytes, 0).ToString();
            label11.Text = label1.Text;

        }
    }



    public class MyRSAImpl 
    {

        private static byte[] rsaOperation(byte[] data, BigInteger exp, BigInteger mod)
        {
            BigInteger bData = new BigInteger(
                data    //our data block
                .Reverse()  //BigInteger has another byte order
                .Concat(new byte[] { 0 }) // append 0 so we are allways handling positive numbers
                .ToArray() // constructor wants an array
            );
            return 
                BigInteger.ModPow(bData, exp, mod) // the RSA operation itself
                .ToByteArray() //make bytes from BigInteger
                .Reverse() // back to "normal" byte order
                .ToArray(); // return as byte array

            /*
             * 
             * A few words on Padding:
             * 
             * you will want to strip padding after decryption or apply before encryption 
             * 
             */
        }

        public static byte[] plainEncryptPriv(byte[] data, RSAParameters key) 
        {
            MyRSAParams myKey = MyRSAParams.fromRSAParameters(key);
            return rsaOperation(data, myKey.privExponent, myKey.Modulus);
        }
        public static byte[] plainEncryptPub(byte[] data, RSAParameters key)
        {
            MyRSAParams myKey = MyRSAParams.fromRSAParameters(key);
            return rsaOperation(data, myKey.pubExponent, myKey.Modulus);
        }
        public static byte[] plainDecryptPriv(byte[] data, RSAParameters key)
        {
            MyRSAParams myKey = MyRSAParams.fromRSAParameters(key);
            return rsaOperation(data, myKey.privExponent, myKey.Modulus);
        }
        public static byte[] plainDecryptPub(byte[] data, RSAParameters key)
        {
            MyRSAParams myKey = MyRSAParams.fromRSAParameters(key);
            return rsaOperation(data, myKey.pubExponent, myKey.Modulus);
        }

    }

    public class MyRSAParams
    {
        public static MyRSAParams fromRSAParameters(RSAParameters key)
        {
            var ret = new MyRSAParams();
            ret.Modulus = new BigInteger(key.Modulus.Reverse().Concat(new byte[] { 0 }).ToArray());
            ret.privExponent = new BigInteger(key.D.Reverse().Concat(new byte[] { 0 }).ToArray());
            ret.pubExponent = new BigInteger(key.Exponent.Reverse().Concat(new byte[] { 0 }).ToArray());

            return ret;
        }
        public BigInteger Modulus;
        public BigInteger privExponent;
        public BigInteger pubExponent;
    }
}
