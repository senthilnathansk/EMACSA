using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using ar.com.telecom.eva.CoreServices.Properties;
using ar.com.telecom.eva.CoreServices.ExceptionHandling;


namespace ar.com.telecom.eva.CoreServices.Configuration
{
    /// <summary>
    /// Implementa la lectura de <see cref="ConfigurationSection"/> desde archivos del Sistema de Archivos.
    /// </summary>
    public class FileConfigurationSource : IConfigurationSource
    {
        #region Constants
        /// <summary>
        /// Nombre del atributo de configuraci�n con el path completo al archivo externo
        /// </summary>
        private const string FileNameConfigurationAttributeName = "fileName";
        #endregion

        #region Private Fields
        /// <summary>
        /// Archivo externo de configuraci�n
        /// </summary>
        private string fileName;

        /// <summary>
        /// Configuraci�n externa
        /// </summary>
        private System.Configuration.Configuration externalConfiguration;
        #endregion

        /// <summary>
        /// Inicializa una nueva instancia.
        /// </summary>
        public FileConfigurationSource()
        {

        }

        /// <summary>
        /// Inicializa el proveedor de configuraci�n en funci�n de par�metros de configuraci�n.
        /// </summary>
        /// <param name="configurationElement">Elemento de configuraci�n.</param>
        public void Init(ConfigurationSourceElement configurationElement)
        {
            if(configurationElement == null)
                throw new EvaTechnicalException ("", new   ArgumentNullException("configurationElement"));

            fileName = configurationElement.GetPropertyValue(FileNameConfigurationAttributeName);
            
            if(string.IsNullOrEmpty(fileName))
               throw new EvaTechnicalException ("", new ConfigurationErrorsException(Resources.ERROR_FILECONFIGURATIONSOURCE_FILENAME_NOTSPECIFIEDINCONFIGURATION));
            if(!File.Exists(fileName))
                throw new EvaTechnicalException ("", new FileNotFoundException(string.Format(Resources.ERROR_FILECONFIGURATIONSOURCE_FILENOTFOUND, fileName), fileName));

            externalConfiguration = SystemConfigurationManager.OpenExeConfiguration(fileName);
        }

        /// <summary>
        /// <para> Dado el nombre de la secci�n retorna la secci�n de configuraci�n correspondiente. </para>
        /// </summary>
        /// <param name="sectionName">Nombre de la secci�n.</param>
        /// <returns>La secci�n de configuraci�n pedida.</returns>
        public object GetSection(string sectionName)
        {
            return externalConfiguration.GetSection(sectionName);
        }

        /// <summary>
        /// Archivo externo de configuraci�n
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Permite configurar al objeto con su elemento de configuraci�n correspondiente.
        /// </summary>
        /// <param name="element">Elemento de configuraci�n.</param>
        public void Configure(ConfigurationElement element)
        {
            this.Init((ConfigurationSourceElement) element);
        }
    }
}
