using System;
using Newtonsoft.Json.Linq;

namespace Library.Jenkins
{
    public static class JobBuilder
    {
        public static JenkinsJob BuildJob(JToken jsonToken)
        {
            var jobType = GetJobType(jsonToken["_class"].ToString());
            var name = jsonToken["name"].ToString();
            var url = new Uri(jsonToken["url"].ToString());
            var status = GetStatus(jsonToken["color"]?.ToString());
            return new JenkinsJob(jobType, name, url, status);
        }

        private static JobType GetJobType(string className)
        {
            return className.EndsWith(".Folder") ? JobType.Folder : JobType.WorkflowJob;
        }

        private static JobStatus GetStatus(string color)
        {
            if (string.IsNullOrEmpty(color))
                return JobStatus.Disabled;
            if (color.EndsWith("_anime"))
                return JobStatus.InProgress;
            string str = color;
            if (str == "disabled" || str == "notbuilt")
                return JobStatus.Disabled;
            if (str == "aborted")
                return JobStatus.Aborted;
            if (str == "red")
                return JobStatus.Failure;
            if (str == "yellow")
                return JobStatus.Warning;
            if (str == "blue")
                return JobStatus.Success;
            throw new ArgumentOutOfRangeException(nameof(color), (object)color, "The specified color does not represent any status.");
        }
    }
}