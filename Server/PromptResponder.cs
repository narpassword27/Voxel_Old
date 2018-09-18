using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voxel.Communication;

namespace Server
{
    internal class PromptResponder
    {
        private ServerNetworkProvider network;
        private Dictionary<string, Job> jobStatus;
        private const int jobDuration = 1000;

        internal PromptResponder(ServerNetworkProvider network)
        {
            this.network = network;
            this.jobStatus = new Dictionary<string, Job>();
        }

        internal void Start()
        {
            while (true)
            {
                if (network.Prompts.TryDequeue(out Prompt prompt))
                    SafeAddToJobs(prompt);

                TryResolveJob();
            }
            
        }

        internal void SafeAddToJobs(Prompt prompt)
        {
            if (!jobStatus.ContainsKey(prompt.SkillName))
            {
                jobStatus.Add(prompt.SkillName, new Job(prompt.SkillName, jobDuration));
                jobStatus[prompt.SkillName].AddPrompt(prompt);
            }
        }

        internal void TryResolveJob() =>
            jobStatus
            .Select(kvp => kvp.Value)
            .FirstOrDefault(j => j.JobTimeout)
            ?.GenerateResponses()
            ?.ForEach(r => network.Responses.Enqueue(r));
    }
}
