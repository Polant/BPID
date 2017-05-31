using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;

        DiffieHellman df = new DiffieHellman();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("127.0.0.1", 8888);
            label1.Text = "Client Socket Program - Server Connected ...";
            //NetworkStream serverStream = clientSocket.GetStream();
            //byte[] outStream = System.Text.Encoding.UTF8.GetBytes("Hello from new client" + "$");
            //serverStream.Write(outStream, 0, outStream.Length);
            //serverStream.Flush();

            //byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize + 1];
            //serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            //string returndata = System.Text.Encoding.UTF8.GetString(inStream);
            //msg("Data from Server : " + returndata);

        }


        public void msg(string mesg)
        {
            textBox1.Text = textBox2.Text + Environment.NewLine + " >> " + mesg + "\r\n";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(textBox2.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize + 1];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.UTF8.GetString(inStream);
            msg("Data from Server : " + returndata);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            df.generateData();
            string pd = df.sendPublicData();
            df.getPublicData();
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(pd + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize + 1];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.UTF8.GetString(inStream);
            df.setPublicData(returndata);
            msg("Data from Server : " + returndata);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize + 1];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.UTF8.GetString(inStream);
            DiffieHellman df = new DiffieHellman();
            df.generateData(returndata);
            msg("g: " + df.g);
            msg("p: " + df.p);
            msg("a: " + df.a);
            msg("B: " + df.B);
            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(DiffieHellman.bytesToString(df.A.ToByteArray()) + "$");
            serverStream.Write(outStream, 0, outStream.Length);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            df.getPrivateKey();
            msg("Private key: " + df.key);
        }
    }
}
