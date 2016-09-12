using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JobLogger
{
    public class LoggertypeConfigElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("Type")]
        public string Type
        {
            get
            {
                return base["Type"] as string;
            }

        }

        [ConfigurationProperty("KeySource")]
        public string KeySource
        {
            get
            {
                return base["KeySource"] as string;
            }

        }
    }
}
