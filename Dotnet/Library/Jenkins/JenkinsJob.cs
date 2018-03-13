using System;

namespace Library.Jenkins
{
    public class JenkinsJob
    {
        public JobType Type { get; }

        public string Name { get; }

        public Uri Url { get; }

        public JobStatus JobStatus { get; }

        public JenkinsJob(JobType type, string name, Uri url, JobStatus jobStatus)
        {
            Type = type;
            Name = name;
            Url = url;
            JobStatus = jobStatus;
        }
    }
}