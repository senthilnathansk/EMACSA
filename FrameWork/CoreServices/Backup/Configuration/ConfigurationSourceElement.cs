using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace ar.com.telecom.eva.CoreServices.Configuration
{
    /// <summary>
    /// Permite especificar un proveedor de configuraci�n.
    /// </summary>
    public class ConfigurationSourceElement : DynamicConfigurationElement
    {
        #region Private Fields
        
        #endregion

        #region Ctor.
        /// <summary>
        /// Crea un nuevo �tem de configuraci�n <em>Source</em> con los atributos necesarios.
        /// </summary>
        public ConfigurationSourceElement()
        {
            
            Properties.Add(new ConfigurationProperty("type", typeof(Type)));
            Properties.Add(new ConfigurationProperty("sections", typeof(GenericConfigurationElementCollection<ConfigurationNamedElement>)));
        }
        #endregion

        #region Public Properties


        /// <summary>
        /// Especifica el tipo del proveedor de configuraci�n asociado.
        /// </summary>
        [ConfigurationProperty("type")]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Type
        {
            get { return (Type)base["type"]; }
            set { base["type"] = value; }
        }

        /// <summary>
        /// Secciones de configuraci�n asociadas a este proveedor.
        /// </summary>
        [ConfigurationProperty("sections")]
        public GenericConfigurationElementCollection<ConfigurationNamedElement> Sections
        {
            get { return (GenericConfigurationElementCollection<ConfigurationNamedElement>)base["sections"]; }
            set { base["sections"] = value; }
        }
        #endregion

    }
}
