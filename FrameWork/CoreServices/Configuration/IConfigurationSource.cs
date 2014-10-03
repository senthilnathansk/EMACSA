using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Gobbi.CoreServices.Configuration
{
    ///<summary>
    /// <para> La interface debe ser implementado por todo fuente de configuraci�n.</para>
    ///</summary>
    public interface IConfigurationSource : IConfigurable
    {
        /// <summary>
        /// <para> Dado el nombre de la secci�n retorna la secci�n de configuraci�n correspondiente. </para>
        /// </summary>
        /// <param name="sectionName">Nombre de la secci�n.</param>
        /// <returns>La secci�n de configuraci�n pedida.</returns>
        object GetSection(string sectionName);
    }
}
