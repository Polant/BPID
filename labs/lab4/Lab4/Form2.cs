using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DiffieHellman dfAlice = new DiffieHellman();
        DiffieHellman dfBob = new DiffieHellman();
        string publicData1 = ""; //for p, g, A
        string publicData2 = ""; //for B

        private void button2_Click(object sender, EventArgs e)
        {
            dfAlice.generateData();
            richTextBox1.Text += Environment.NewLine + "g: " + dfAlice.print(dfAlice.g);
            richTextBox1.Text += Environment.NewLine + "p: " + dfAlice.print(dfAlice.p);
            richTextBox1.Text += Environment.NewLine + "A: " + dfAlice.print(dfAlice.A);
            publicData1 = dfAlice.sendPublicData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (/*df.g == null || df.p == null*/ publicData1 == "") richTextBox1.Text += Environment.NewLine + "There are no generated p and g";
            else
            {
                dfAlice.generateData(publicData1);
                richTextBox1.Text += Environment.NewLine + "g: " + dfAlice.print(dfAlice.g);
                richTextBox1.Text += Environment.NewLine + "p: " + dfAlice.print(dfAlice.p);
                richTextBox1.Text += Environment.NewLine + "B: " + dfAlice.print(dfAlice.B);
                richTextBox1.Text += Environment.NewLine + "A: " + dfAlice.print(dfAlice.getPublicData());
                publicData2 = DiffieHellman.bytesToString(dfAlice.A.ToByteArray());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dfAlice.B == null || dfAlice.B == 0)
            {
                dfAlice.setPublicData(publicData2);
                richTextBox1.Text += Environment.NewLine + "B: " + dfAlice.print(dfAlice.B);
            }
            dfAlice.getPrivateKey();
            richTextBox1.Text += Environment.NewLine + "Key: " + dfAlice.print(dfAlice.key);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dfBob.generateData();
            richTextBox2.Text += Environment.NewLine + "g: " + dfBob.print(dfBob.g);
            richTextBox2.Text += Environment.NewLine + "p: " + dfBob.print(dfBob.p);
            richTextBox2.Text += Environment.NewLine + "A: " + dfBob.print(dfBob.A);
            publicData1 = dfBob.sendPublicData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (/*df.g == null || df.p == null*/ publicData1 == "") richTextBox2.Text += Environment.NewLine + "There are no generated p and g";
            else
            {
                dfBob.generateData(publicData1);
                richTextBox2.Text += Environment.NewLine + "g: " + dfBob.print(dfBob.g);
                richTextBox2.Text += Environment.NewLine + "p: " + dfBob.print(dfBob.p);
                richTextBox2.Text += Environment.NewLine + "B: " + dfBob.print(dfBob.B);
                richTextBox2.Text += Environment.NewLine + "A: " + dfBob.print(dfBob.getPublicData());
                publicData2 = DiffieHellman.bytesToString(dfBob.A.ToByteArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dfBob.B == null || dfBob.B == 0)
            {
                dfBob.setPublicData(publicData2);
                richTextBox2.Text += Environment.NewLine + "B: " + dfBob.print(dfBob.B);
            }
            dfBob.getPrivateKey();
            richTextBox2.Text += Environment.NewLine + "Key: " + dfBob.print(dfBob.key);
        }
    }
}
