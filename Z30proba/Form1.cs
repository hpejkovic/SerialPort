using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Threading;

namespace Z30proba
{
    public partial class Form1 : Form
    {
        static bool _continue;
        static SerialPort _serialPort;
        Thread readThread;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string name;
            string message;
            this.textBox2.AcceptsReturn = true;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);
            _serialPort.Parity = SetPortParity(_serialPort.Parity);
            _serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);
            _serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
            _serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.DataReceived += _serialPort_DataReceived;

            _serialPort.Open();
            _continue = true;
            readThread.Start();

            //Console.Write("Name: ");
            //name = Console.ReadLine();

            //textBox1.AppendText("Type QUIT to exit\n");
            _serialPort.WriteLine("");

            //while (_continue)
            //{
            //    message = Console.ReadLine();

            //    if (stringComparer.Equals("quit", message))
            //    {
            //        _continue = false;
            //    }
            //    else
            //    {

            //        _serialPort.WriteLine(message);
            //        //Console.WriteLine(String.Format("<{0}>: {1}", name, message));
            //    }
            //}

            //
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                
            }
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                
                this.textBox2.AppendText(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.ResetText();
            _serialPort.WriteLine(textBox1.Lines.Last());
        }

        private static void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string message = ((System.IO.Ports.SerialPort)sender).ReadExisting();
            //Console.Write(message);
        }

        public void Read()
        {
          
            while (_continue)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    SetText(message+"\r\n");
                }
                catch (TimeoutException) { }
            }

        }

        // Display Port values and prompt user to enter a port.
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            //Console.WriteLine("Available Ports:");
            //foreach (string s in SerialPort.GetPortNames())
            //{
            //    Console.WriteLine("   {0}", s);
            //}


            //Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            //portName = Console.ReadLine();
            portName = "COM4";

            Console.WriteLine(portName);
            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }
        // Display BaudRate values and prompt user to enter a value.
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;

            //Console.Write("Baud Rate(default:{0}): ", defaultPortBaudRate);
            baudRate = "115200";

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }
            Console.WriteLine(baudRate);
            return int.Parse(baudRate);
        }

        // Display PortParity values and prompt user to enter a value.
        public static Parity SetPortParity(Parity defaultPortParity)
        {
            string parity;

            //Console.WriteLine("Available Parity options:");
            //foreach (string s in Enum.GetNames(typeof(Parity)))
            //{
            //    Console.WriteLine("   {0}", s);
            //}

            //Console.Write("Enter Parity value (Default: {0}):", defaultPortParity.ToString(), true);
            parity = defaultPortParity.ToString();

            if (parity == "")
            {
                parity = defaultPortParity.ToString();
            }
            Console.WriteLine(parity);
            return (Parity)Enum.Parse(typeof(Parity), parity, true);
        }
        // Display DataBits values and prompt user to enter a value.
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            string dataBits;

            //Console.Write("Enter DataBits value (Default: {0}): ", defaultPortDataBits);
            dataBits = defaultPortDataBits.ToString();

            if (dataBits == "")
            {
                dataBits = defaultPortDataBits.ToString();
            }

            Console.WriteLine(dataBits);
            return int.Parse(dataBits.ToUpperInvariant());
        }

        // Display StopBits values and prompt user to enter a value.
        public static StopBits SetPortStopBits(StopBits defaultPortStopBits)
        {
            string stopBits;

            //Console.WriteLine("Available StopBits options:");
            //foreach (string s in Enum.GetNames(typeof(StopBits)))
            //{
            //    Console.WriteLine("   {0}", s);
            //}

            //Console.Write("Enter StopBits value (None is not supported and \n" +
            // "raises an ArgumentOutOfRangeException. \n (Default: {0}):", defaultPortStopBits.ToString());
            stopBits = defaultPortStopBits.ToString();

            if (stopBits == "")
            {
                stopBits = defaultPortStopBits.ToString();
            }
            Console.WriteLine(stopBits);
            return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
        }
        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            //Console.WriteLine("Available Handshake options:");
            //foreach (string s in Enum.GetNames(typeof(Handshake)))
            //{
            //    Console.WriteLine("   {0}", s);
            //}

            //Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = defaultPortHandshake.ToString();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }
            Console.WriteLine(handshake);
            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //readThread.Join();
            //_serialPort.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _continue = false;
            readThread.Join();
            _serialPort.Close();
        }
    }
}
