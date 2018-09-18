using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Voxel.Communication
{
    public struct Response
    {
        public string SourceName;
        public string TargetName;
        public string TargetIP;
        public string SkillName;
        public ResponseAuth Auth;

        public Response(Prompt Prompt, string TargetIP, ResponseAuth Auth)
        {
            SourceName = Prompt.SourceName;
            TargetName = Prompt.TargetName;
            this.TargetIP = TargetIP;
            SkillName = Prompt.SkillName;
            this.Auth = Auth;
        }
    }
}
