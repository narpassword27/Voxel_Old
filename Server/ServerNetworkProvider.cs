using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Voxel.Communication;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Threading;
using System.Net;

namespace Server
{
    internal class ServerNetworkProvider
    {
        //TODO: Poll network for devices
        internal static Dictionary<string, string> IPAddressesByDeviceName;


        internal ConcurrentQueue<Prompt> Prompts;
        internal ConcurrentQueue<Response> Responses;

        private IPAddress ip;
        private TcpListener server;
        private const int portNumber = 8000;

        internal ServerNetworkProvider()
        {
            Prompts = new ConcurrentQueue<Prompt>();
            Responses = new ConcurrentQueue<Response>();
            IPAddressesByDeviceName = new Dictionary<string, string>();

            ip = Dns.GetHostEntry("localhost").AddressList[0];
            server = new TcpListener(ip, portNumber);
            //client = default(TcpClient);
            //clients = new ConcurrentDictionary<string, TcpClient>();

            try
            {
                server.Start();
                Console.WriteLine("Server Started...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A fatal error has occurred:\r\n{ex.ToString()}");
            }

        }

        internal async void Start()
        {
            while (true)
            {
                await ReceivePrompt();
                await SendResponse();
            }
        }


        private async Task SendResponse()
        {
            if (Responses.Any() && Responses.TryDequeue(out Response response))
            {
                using (TcpClient client = new TcpClient(response.TargetIP, portNumber))
                {
                    var serialized = new JavaScriptSerializer().Serialize(response);
                    var encoded = Encoding.ASCII.GetBytes(serialized);

                    NetworkStream stream = client.GetStream();

                    await stream.WriteAsync(encoded, 0, encoded.Length);
                }
            }
        }


        private async Task ReceivePrompt()
        {
            using (TcpClient client = await server.AcceptTcpClientAsync())
            {
                byte[] receivedBuffer = new byte[512];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedBuffer, 0, receivedBuffer.Length);

                var str = Encoding.ASCII.GetString(receivedBuffer);
                var prompt = new JavaScriptSerializer().Deserialize<Prompt>(str);

                Prompts.Enqueue(prompt);
            }
        }


        internal static bool TryResolveDeviceName(string DeviceName, out string IPAddress)
        {
            if (IPAddressesByDeviceName.ContainsKey(DeviceName))
            {
                IPAddress = IPAddressesByDeviceName[DeviceName];
                return true;
            }
            else
            {
                IPAddress = null;
                return false;
            }
        }
    }
}
