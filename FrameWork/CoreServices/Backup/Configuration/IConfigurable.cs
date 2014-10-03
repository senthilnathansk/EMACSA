using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ar.com.telecom.eva.CoreServices.Configuration
{
    /// <summary>
    /// Est� interfaz es implementada por objetos que se configuran a trav�s de elementos almacenados en la configuraci�n de la
    /// aplicaci�n.
    /// </summary>
    public interface IConfigurable 
    {
        /// <summary>
        /// Entrega al objeto su elemento de configuraci�n.
        /// </summary>
        /// <param name="element">Elemento de configuraci�n para el objeto.</param>
        void Configure(ConfigurationElement element);
    }
}
