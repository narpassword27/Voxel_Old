using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace Voxel.Modules.Media
{
    class Media_Youtube : ISkill
    {
        public Media_Youtube()
        {
        }

        public Grammar GetGrammar()
        {
            GrammarBuilder gBuilder = new GrammarBuilder();

            var video = new GrammarBuilder();
            video.AppendDictation();
            var videoKey = new SemanticResultKey("Video", video);

            gBuilder.Append("Play");
            gBuilder += videoKey;
            gBuilder.Append("from youtube");

            return new Grammar(gBuilder);
        }

        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string video = e.Result.Semantics["Video"].Value.ToString();

            //C:\Users\Darren\AppData\Local\Google\Chrome\User Data\Default\Extensions\cfhdojbkjhnklbpkdaibdccddilifddb\3.0.3_0

            ChromeOptions options = new ChromeOptions();
            options.AddArgument(@"load-extension=C:\Users\Darren\AppData\Local\Google\Chrome\User Data\Default\Extensions\cfhdojbkjhnklbpkdaibdccddilifddb\3.0.3_0");
            //options.AddArgument("headless");

            ChromeDriver webDriver = new ChromeDriver(options);

            webDriver.SwitchTo().Window(webDriver.WindowHandles[0]);

            webDriver.WindowHandles.Skip(1).ToList().ForEach(t =>
            {
                webDriver.SwitchTo().Window(t);
                webDriver.Close();
            });

            webDriver.SwitchTo().Window(webDriver.WindowHandles[0]);

            webDriver.Navigate().GoToUrl(@"http://www.youtube.com");








            webDriver.FindElementById("search").SendKeys(video);
            System.Threading.Thread.Sleep(500);
            webDriver.FindElementById("search-icon-legacy").Click();
            System.Threading.Thread.Sleep(1500);
            webDriver.FindElementsById("video-title").First(v => v.Enabled && v.Displayed && v.Text.ToLower().Contains(video)).Click();

        }
    }
}
