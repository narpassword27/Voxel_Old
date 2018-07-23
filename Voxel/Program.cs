using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voxel
{
    class Program
    {
        static void Main(string[] args)
        {
            ResponseEngine sre = new ResponseEngine();
            sre.Prepare();
            sre.Start();

            Console.ReadLine();
        }
    }
}
