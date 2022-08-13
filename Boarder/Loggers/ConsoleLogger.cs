using System;
using System.Collections.Generic;
using System.Text;

namespace Boarder.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string output)
        {
            Console.WriteLine(output);
        }

      
    }
}
