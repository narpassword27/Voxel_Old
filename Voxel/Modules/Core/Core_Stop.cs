using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Voxel.Modules.Core
{
    class Core_Stop : ISkill
    {
        public Grammar GetGrammar()
        {
            var gBuilder = new GrammarBuilder("Stop");
            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Stopping");
        }
    }
}
