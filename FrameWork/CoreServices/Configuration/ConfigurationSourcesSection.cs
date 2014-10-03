using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Gobbi.CoreServices.Configuration
{
    /// <summary>
    /// Permite el parseo de la secci�n XML de configuraci�n.
    /// </summary>
    public class ConfigurationSourcesSection : ConfigurationSection
    {
        /// <summary>
        /// Inicializa una nueva instancia.
        /// </summary>
        public ConfigurationSourcesSection()
        {

        }

        /// <summary>
        /// Permite especificar origen de configuraci�n predeterminado.
        /// </summary>
        [ConfigurationProperty("selectedSource")]
        public string SelectedSource
        {
            get { return (string)base["selectedSource"]; }
            set { base["selectedSource"] = value; }
        }

        /// <summary>
        /// Permite enumerar las secciones de configuraci�n asociadas a un proveedor.
        /// </summary>
        [ConfigurationProperty("sources")]
        public GenericConfigurationElementCollection<ConfigurationSourceElement> Sources
        {
            get { return (GenericConfigurationElementCollection<ConfigurationSourceElement>)base["sources"]; }
            set { base["sources"] = value; }
        }
    }
}
