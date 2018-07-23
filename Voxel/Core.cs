using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voxel
{
    static class Core
    {
        static int audioVolume = 10;
        static int voiceVolume = 10;

        public static void SetVolume(int volume)
        {
            audioVolume = volume;
            voiceVolume = volume;
            Console.WriteLine($"Volume {volume}");
        }
    }
}
