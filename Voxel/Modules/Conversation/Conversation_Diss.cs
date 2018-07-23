using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Voxel.Modules.Conversation
{
    class Conversation_Diss : ISkill
    {
        public Grammar GetGrammar()
        {
            return new Grammar(new GrammarBuilder("What do you think about alexa?"));
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string response = "If I need to be browbeaten over the fact that I don't subscribe to spot-if-eye, I'll just disable ad blocker.";
            var ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
            ss.SelectVoice("Microsoft Server Speech Text to Speech Voice (en-US, ZiraPro)");

            ss.Speak(response);
        }
    }
}
