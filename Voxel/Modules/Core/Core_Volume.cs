using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Voxel.Modules.Core
{
    class Core_Volume : ISkill
    {
        public Grammar GetGrammar()
        {
            var gBuilder = new GrammarBuilder();

            var volume = new GrammarBuilder();
            volume.AppendDictation();
            var volumeKey = new SemanticResultKey("Volume", volume);

            gBuilder.Append("Volume");
            gBuilder += volumeKey;
            
            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string volume = e.Result.Semantics["Volume"].Value.ToString();

            int number = new string[] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" }
                .Select((s, i) => new { s, i })
                .FirstOrDefault(a => a.s == volume)?.i ?? -1;

            if (number > -1)
                Voxel.Core.SetVolume(number);
        }
    }
}
