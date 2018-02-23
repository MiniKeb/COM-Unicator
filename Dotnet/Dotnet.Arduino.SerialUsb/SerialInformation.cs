using System;
using System.IO.Ports;
using System.Diagnostics;

namespace Dotnet.Arduino.SerialUsb
{
    public class SerialInformation
    {
        public string OutputLine { get; set; }

        public SerialPort SerialPort { get; set; }

        public static void GetPorts()
        {
            Console.WriteLine("Serial ports available:");
            Console.WriteLine("-----------------------");
            foreach (var portName in SerialPort.GetPortNames())
            {
                Console.WriteLine(portName);
            }
        }

        public void Write(string message){
            this.SerialPort.Write(message);
        }

        public void ReadFromPort()
        {
            // Initialise the serial port on COM4.
            // obviously we would normally parameterise this, but
            // this is for demonstration purposes only.
            this.SerialPort = new SerialPort("COM4")
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };

            // Subscribe to the DataReceived event.
            this.SerialPort.DataReceived += SerialPortDataReceived;
            
            // Now open the port.
            this.SerialPort.Open();

            // while(true){
            //     var message = this.SerialPort.ReadLine();
            //     Console.WriteLine(message);
            // }
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;

            // Read the data that's in the serial buffer.
            // var serialdata = serialPort.ReadExisting();
            var serialdata = serialPort.ReadLine();

            // Write to debug output.
            Console.WriteLine(serialdata);
        }
    }  
}
