using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Voxel.Modules.Core
{
    class Core_Mute : ISkill
    {
        public Grammar GetGrammar()
        {
            var gBuilder = new GrammarBuilder("Mute");
            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Voxel.Core.SetVolume(0);
        }
    }
}
