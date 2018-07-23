using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace Voxel.Modules
{
    interface ISkill
    {
        Grammar GetGrammar();
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e);
    }
}
