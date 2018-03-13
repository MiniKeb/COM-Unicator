using System;
using System.Linq;
using System.Threading;
using Library.Serial;

namespace Dotnet.Arduino
{
    class Program
    {
        //RRRRROOOOOYYYYYAAAAAGGGGGTTTTTCCCCCLLLLLBBBBBVVVVVFFFFFPPPPP
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();

            using (var serialInformation = new SerialInformation("COM4"))
            {
                var rand = new Random();
                var current = 20;
                for(var i = 0; i < 600; i++){
                    var move = rand.Next(-2, 4);
                    Console.Write(move + " : ");
                    current += move;
                    var msg = String.Concat(Enumerable.Range(0, 60).Select(x => x < current ? 'V' : 'X').ToList());
                    Console.WriteLine(msg);
                    serialInformation.Write(msg);
                    Thread.Sleep(200);
                }

                // var colors = "ROYAGTCLBVFPW";


            }
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}
