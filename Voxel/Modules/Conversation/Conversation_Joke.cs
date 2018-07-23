using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Voxel.Modules.Conversation
{
    class Conversation_Joke : ISkill
    {
        public Grammar GetGrammar()
        {
            var gBuilder = new GrammarBuilder("Can a match box?");
            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string response = "I Don't know, but a tin can.";
            var ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
            ss.SelectVoice("Microsoft Server Speech Text to Speech Voice (en-US, ZiraPro)");

            ss.Speak(response);
        }
    }
}
