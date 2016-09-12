using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLogger
{
    public class LoggerExceptionLevel: Exception
    {
        public LoggerExceptionLevel()
            : base("Error or Warning or Message must be specified")
        {

           
    
        }
    }

    
}
