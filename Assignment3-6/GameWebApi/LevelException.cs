using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi
{
    public class LevelException : Exception
    {
        public LevelException(string message) : base(message)
        {
        }
    }
}
