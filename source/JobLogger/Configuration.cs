using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JobLogger
{
    public class ConfigurationLoggger : ConfigurationSection
    {
        public static ConfigurationLoggger GetConfiguration()
        {
            ConfigurationLoggger configurationLoggger =
                ConfigurationManager
                .GetSection("JobLoggerConfigurableModule")
                as ConfigurationLoggger;

            if (configurationLoggger != null)
                return configurationLoggger;

            return new ConfigurationLoggger();
        }

        
        [ConfigurationProperty("level", IsRequired = true)]
        public string Level
        {
            get
            {
                return this["level"] as string;
            }
           
        }
        [ConfigurationProperty("Loggertype")]
        public LoggertypeConfigElement Loggertype
        {
            get
            {
                return base["Loggertype"] as LoggertypeConfigElement;
            }
        }
    }

    
}
