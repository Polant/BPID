using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VigenereCipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vigenere MyVigenere = new Vigenere();
            textBox2.Text = MyVigenere.Encrypt(textBox1.Text, textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vigenere MyVigenere = new Vigenere();
            textBox1.Text = MyVigenere.Decrypt(textBox2.Text, textBox3.Text);
        }
    }
}
