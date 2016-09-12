using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLogger
{
    public class LoggerExceptionType : Exception
    {
        public LoggerExceptionType() : base("Invalid configuration")
        {

           
    
        }
    }
}
