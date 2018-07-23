using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Reflection;

namespace Voxel
{
    class ResponseEngine
    {
        SpeechRecognitionEngine wake;
        SpeechRecognitionEngine commands;
        SpeechSynthesizer ss;

        internal ResponseEngine()
        {
            wake = new SpeechRecognitionEngine();
            commands = new SpeechRecognitionEngine();
            ss = new SpeechSynthesizer();
        }

        internal void Prepare()
        {
            wake.SetInputToDefaultAudioDevice();
            commands.SetInputToDefaultAudioDevice();

            var skills = Assembly.GetCallingAssembly().GetTypes()
                .Where(t => !t.IsInterface && t.FullName.StartsWith("Voxel.Modules") && !t.Attributes.HasFlag(TypeAttributes.NestedPrivate | TypeAttributes.Sealed))
                .Select(t => new
                {
                    Instance = t.GetConstructors().First().Invoke(new object[] { }) as Modules.ISkill,
                    Name = t.Name
                })
                .ToList();

            foreach (var skill in skills)
            {
                Grammar skillGrammar = skill.Instance.GetGrammar();
                skillGrammar.Name = skill.Name;
                skillGrammar.SpeechRecognized += skill.Instance.SpeechRecognized;

                commands.LoadGrammar(skillGrammar);
            }


            wake.LoadGrammarAsync(new Grammar(new GrammarBuilder("voxel")) { Name = "Wake"});
            wake.SpeechRecognized += Wake_SpeechRecognized;
            wake.InitialSilenceTimeout = TimeSpan.FromSeconds(0);
            wake.BabbleTimeout = TimeSpan.FromSeconds(0);

            commands.RecognizeCompleted += Commands_RecognizeCompleted;
            commands.InitialSilenceTimeout = TimeSpan.FromSeconds(3);
            commands.BabbleTimeout = TimeSpan.FromSeconds(10);
            commands.EndSilenceTimeout = TimeSpan.FromSeconds(2);



            //ss.SetOutputToDefaultAudioDevice();
            //ss.SelectVoice("Microsoft Server Speech Text to Speech Voice (en-US, ZiraPro)");
        }

        public void Start()
        {
            wake.RecognizeAsync(RecognizeMode.Single);
            Console.WriteLine("Ready");
        }

        private void Wake_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            commands.RecognizeAsync(RecognizeMode.Single);
            Console.WriteLine("Woken");
        }

        private void Commands_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            commands.RecognizeAsyncStop();
            wake.RecognizeAsync(RecognizeMode.Single);
            Console.WriteLine("Sleep");
        }
    }
}
