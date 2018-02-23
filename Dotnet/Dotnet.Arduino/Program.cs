using System;
using System.Threading;
using Dotnet.Arduino.SerialUsb;

namespace Dotnet.Arduino
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
    
            SerialInformation.GetPorts();
 
            var serialInformation = new SerialInformation();
        
            serialInformation.ReadFromPort();

            var message = string.Empty;
            while(message != "quit"){
                Console.WriteLine("You : ");
                message = Console.ReadLine();
                serialInformation.Write(message);
                Thread.Sleep(2000);
            }
        
            Console.WriteLine("[EXIT]");
            Console.ReadKey();
        
            serialInformation.SerialPort.Close();
        }
    }
}
