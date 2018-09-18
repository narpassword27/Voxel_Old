using System;
using System.Threading.Tasks;

namespace nodeTest
{
    public class Class1
    {
        public async Task<object> HelloWorld(object input)
        {
            return $"HelloWorld + {input}";
        }
    }
}
