using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Library.Jenkins
{
    public sealed class JenkinsApiClient : IDisposable
    {
        private readonly HttpClient client;

        public JenkinsApiClient()
        {
            client = new HttpClient();
        }

        public IEnumerable<JenkinsJob> GetAllJobs(string jenkinsUrl)
        {
            var jenkinsTeamUri = new Uri(jenkinsUrl);

            var allJobs = GetJobs(jenkinsTeamUri);

            return allJobs.AsEnumerable();
        }

        private IEnumerable<JenkinsJob> GetJobs(Uri baseUrl)
        {
            var queryUri = new Uri(baseUrl, "api/json");

            var document = client.GetStringAsync(queryUri).Result;
            dynamic json = JObject.Parse(document);
            JArray jobs = JArray.Parse(json.jobs.ToString());


            return jobs
                .Select(JobBuilder.BuildJob)
                .Aggregate(
                    new List<JenkinsJob>(), 
                    (acc, job) => acc.Union(job.Type == JobType.WorkflowJob ? new[]{job} : GetJobs(job.Url)).ToList());
        }


        public void Dispose()
        {
            client?.Dispose();
        }
    }
}