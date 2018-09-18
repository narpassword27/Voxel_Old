using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Voxel.Communication
{
    public struct Prompt
    {
        public string SourceName;
        public string SourceIP;
        public string TargetName;
        public float Level;
        public string SkillName;

        public Prompt(string SkillName, string TargetName, float Level)
        {
            this.SourceName = Core.clientName;
            this.SourceIP = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();

            this.TargetName = TargetName;
            this.Level = Level;
            this.SkillName = SkillName;
        }
    }
}
