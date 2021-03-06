using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Gobbi.CoreServices.Configuration;
using System.ComponentModel;
using System.Configuration;

namespace Gobbi.CoreServices.Logging.Configuration
{
    internal class LogSurceConfigurationElement : DynamicConfigurationElement
    {
        [ConfigurationProperty("switchValue")]
        public SourceLevels SourceLevels
        {

            get { return (SourceLevels)base["switchValue"]; }
            set { base["switchValue"] = value; }
        }

        [ConfigurationProperty("destinationListeners")]
        public GenericConfigurationElementCollection<ConfigurationNamedElement> Listeners
        {
            get
            {

                return (GenericConfigurationElementCollection<ConfigurationNamedElement>)base["destinationListeners"];
            }
            set { base["destinationListeners"] = value; }
        }
    }
}
