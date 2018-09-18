using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerNetworkProvider networkProvider = new ServerNetworkProvider();
            PromptResponder responder = new PromptResponder(networkProvider);

            //TODO: Check threading on these
            networkProvider.Start();
            responder.Start();

            Console.ReadLine();
        }
    }
}
