using System;
using System.Speech.Recognition;

namespace Voxel.Modules.Core
{
    class Core_Update : ISkill
    {
        public Grammar GetGrammar()
        {
            var gBuilder = new GrammarBuilder("Update");
            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Updating");
        }
    }
}
