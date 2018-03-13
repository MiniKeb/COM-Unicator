using System;
using System.IO.Ports;
using System.Linq;

namespace Library.Serial
{
    public sealed class SerialInformation : IDisposable
    {
        private readonly SerialPort serialPort;

        private Action<string> onReading;

        public SerialInformation(string port)
        {
            if(!SerialPort.GetPortNames().Contains(port))
                throw new ArgumentOutOfRangeException(nameof(port), $"The specified port ({port}) does not exists.");

            serialPort = new SerialPort(port)
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };

            onReading = message => {};

        }
        
        public void Write(string message){
            if (!serialPort.IsOpen)
                serialPort.Open();

            serialPort.Write(message);
        }

        public void ReadWith(Action<string> onReading)
        {
            this.onReading = onReading;
            serialPort.DataReceived += SerialPortDataReceived;

            if(!serialPort.IsOpen)
                serialPort.Open();
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var input = ((SerialPort)sender).ReadLine();

            onReading(input);
        }

        public void Dispose()
        {
            if(serialPort.IsOpen)
                serialPort.Close();

            serialPort?.Dispose();
        }
    }  
}
