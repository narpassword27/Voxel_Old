using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voxel.Communication;

namespace Server
{
    internal class Job
    {
        private Stopwatch sw;
        private readonly int jobTimeout;
        internal List<Prompt> Prompts;
        internal string JobName;

        internal Job(string JobName, int jobTimeout)
        {
            this.JobName = JobName;
            this.jobTimeout = jobTimeout;

            sw = new Stopwatch();
            Prompts = new List<Prompt>();

            sw.Start();
        }

        internal void AddPrompt(Prompt prompt)
        {
            Prompts.Add(prompt);
        }

        internal bool JobTimeout => sw.ElapsedMilliseconds >= jobTimeout;

        internal List<Response> GenerateResponses()
        {
            var maxLevel = Prompts.Max(p => p.Level);

            return Prompts
                .Select(p =>
                {
                    ServerNetworkProvider.TryResolveDeviceName(p.TargetName, out string ipAddress);
                    return new Response(p, ipAddress, p.Level == maxLevel ? ResponseAuth.Op : ResponseAuth.NoOp);
                }).ToList();
        }
    }
}
