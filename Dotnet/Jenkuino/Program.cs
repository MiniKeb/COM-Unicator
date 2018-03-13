using System;
using System.Collections.Generic;
using System.Linq;
using Library.Jenkins;

namespace Jenkuino
{
    public enum Color
    {
        Off = 'X',
        Red = 'R',
        Orange = 'O',
        Yellow = 'Y',
        AppleGreen = 'A',
        Green = 'G',
        Turquoise = 'T',
        Cyan = 'C',
        LightBlue = 'L',
        Blue = 'B',
        Violet = 'V',
        Fushia = 'F',
        Pink = 'P',
        White = 'W'
    }

    class Program
    {
        private const string JenkinsTeamUri = "http://myJenkinsUri";

        private static readonly Dictionary<JobStatus, Color> Mapping = new Dictionary<JobStatus, Color>()
        {
            { JobStatus.Disabled, Color.Turquoise },
            { JobStatus.InProgress, Color.Turquoise },
            { JobStatus.Aborted, Color.Pink },
            { JobStatus.Failure, Color.Red },
            { JobStatus.Warning, Color.Orange },
            { JobStatus.Success, Color.Green }
        };

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            using (var api = new JenkinsApiClient())
            {
                var message = string.Concat(api
                    .GetAllJobs(JenkinsTeamUri)
                    .Select(j => (char) Mapping[j.JobStatus])
                    .ToList());
                    
                
                var allJobs = api.GetAllJobs(JenkinsTeamUri);
                foreach (var job in allJobs)
                {
                    Console.WriteLine(job.Name +" => "+ job.JobStatus);
                }

                Console.WriteLine(message);
                Console.WriteLine($"Fin : {allJobs.Count()} Jobs.");
                
            }

            Console.ReadLine();
        }

        
    }
}
