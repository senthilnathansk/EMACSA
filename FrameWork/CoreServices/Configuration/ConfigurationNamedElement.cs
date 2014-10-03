using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Gobbi.CoreServices.Configuration
{
    /// <summary>
    /// Representa un elemento de configuraci�n, el cual tiene una propiedad Name que adem�s es utilizada como clave de las colecciones.
    /// </summary>
    public class ConfigurationNamedElement : ConfigurationElement
    {
        /// <summary>
        /// Nombre del elemento de configuraci�n.
        /// </summary>
        [ConfigurationProperty("name",  Options= ConfigurationPropertyOptions.IsKey)]
        public string Name
        {
         get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Incializa una nueva instancia.
        /// </summary>
        public ConfigurationNamedElement()
        {

        }

    }
}
