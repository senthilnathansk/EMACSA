using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using System.Text;


namespace ar.com.telecom.eva.CoreServices.Configuration
{
    /// <summary>
    /// Contiene los m�todos para obtener la <see cref="ConfigurationSection"/> desde el provider especificado en la configuraci�n.
    /// </summary>
    /// <example>
    /// Obtiene la secci�n de configuraci�n pedida del proveedor definido en la configuraci�n de 
    /// la aplicaci�n para esa configuraci�n.
    /// <code>
    ///  ConfigurationSection section2 = 
    ///            (ConfigurationSection)ar.com.telecom.eva.CoreServices.Configuration.ConfigurationManager.GetSection("testingSection1");
    /// </code>
    /// </example>
    public static class ConfigurationManager
    {
        private static Dictionary<string, IConfigurationSource> configurationSourceDictionary = new Dictionary<string, IConfigurationSource>();
        private static object syncObject = new object();
        private static ConfigurationSourceElement defaultElement;

        /// <summary>
        /// <para> Retorna la configuraci�n pedida desde el provider especificado en la configuraci�n. </para>
        /// </summary>
        /// <param name="sectionName">Nombre de la secci�n</param>
        /// <returns>La configuraci�n pedida.</returns>
        public static object GetSection(string sectionName)
        {
            ConfigurationSourceElement sourceElement = null;
                object configurationSection;
                if (TryGetConfigurationSourceElementFromConfigurationFile(sectionName, out sourceElement))
                {
                    IConfigurationSource sourceObject = GetOrCreateConfigurationSourceFromElement(sourceElement);
                    configurationSection =  sourceObject.GetSection(sectionName);
                }
                else
                    configurationSection=  SystemConfigurationManager.GetSection(sectionName);
                return configurationSection;
        }

        #region Private Methods
        private static IConfigurationSource GetOrCreateConfigurationSourceFromElement(ConfigurationSourceElement sourceElement)
        {
            //
            // No existen operaciones de eliminaci�n, as� que esto puede realizarse fuera del lock
            //
            if (configurationSourceDictionary.ContainsKey(sourceElement.Name))
                return configurationSourceDictionary[sourceElement.Name];

            lock (syncObject) 
            {
                if (configurationSourceDictionary.ContainsKey(sourceElement.Name))
                    return configurationSourceDictionary[sourceElement.Name];

                IConfigurationSource sourceObject = (IConfigurationSource)Activator.CreateInstance(sourceElement.Type);
                sourceObject.Configure(sourceElement);

                configurationSourceDictionary.Add(sourceElement.Name, sourceObject);
                return sourceObject;
            }
        }

        private static bool TryGetConfigurationSourceElementFromConfigurationFile(string sectionName, out ConfigurationSourceElement element)
        {
            element = null;
            ConfigurationSourcesSection configurationSourcesSection =
                           (ConfigurationSourcesSection)SystemConfigurationManager.GetSection(Constants.ConfigurationSourceSectionName);

            if (configurationSourcesSection == null) return false;

            foreach (ConfigurationSourceElement source in configurationSourcesSection.Sources)
                foreach (ConfigurationNamedElement section in source.Sections)
                    if (section.Name == sectionName)
                    {
                        element = source;
                        return true;
                    }
            if (TryGetDefaultConfigurationSourceElement(configurationSourcesSection, out element))
                return true;
            else
                return false;
        }

        private static bool  TryGetDefaultConfigurationSourceElement(ConfigurationSourcesSection configurationSourcesSection, out ConfigurationSourceElement element)
        {
            string selectedSource = configurationSourcesSection.SelectedSource;
            if (defaultElement!= null)
            {
                element = defaultElement;
                return true;
            }
            if (!string.IsNullOrEmpty(selectedSource))
                foreach (ConfigurationSourceElement source in configurationSourcesSection.Sources)
                    if (source.Name == selectedSource)
                    {
                        defaultElement = source;
                        element = source;
                        return true;
                    }
            element = null;
            return false;
        }
        #endregion
    }
}
