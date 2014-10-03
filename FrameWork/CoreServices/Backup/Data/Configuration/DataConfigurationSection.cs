using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using ar.com.telecom.eva.CoreServices.Configuration;

namespace ar.com.telecom.eva.CoreServices.Data
{
    /// <summary>
    /// Permite el parseo de la secci�n XML de configuraci�n.
    /// </summary>
    public class DataConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Inicializa una nueva instancia.
        /// </summary>
        public DataConfigurationSection()
        {

        }

        /// <summary>
        /// Permite especificar origen de configuraci�n predeterminado.
        /// </summary>
        [ConfigurationProperty("defaultDatabase")]
        public string DefaultDatabase
        {
            get { return (string)base["defaultDatabase"]; }
            set { base["defaultDatabase"] = value; }
        }

        /// <summary>
        /// Permite enumerar las secciones de configuraci�n asociadas a un proveedor.
        /// </summary>
        [ConfigurationProperty("databases")]
        public GenericConfigurationElementCollection<DataConfigurationElement> Databases
        {
            get { return (GenericConfigurationElementCollection<DataConfigurationElement>)base["databases"]; }
            set { base["databases"] = value; }
        }

        
    }
}
