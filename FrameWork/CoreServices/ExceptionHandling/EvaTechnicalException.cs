using System;
using System.Collections.Generic;
using System.Text;

namespace Gobbi.CoreServices.ExceptionHandling
{
    ///<summary>
    /// <para>Es una excepci�n adaptada para que el programador agregue informaci�n de contexto,
    /// para arrojar en Excepciones t�cnicas.
    ///  </para>
    ///</summary>
    public class GobbiTechnicalException : EvaException
    {
       
        ///<summary>
        /// Inicializa la instancia.
        ///</summary>
        /// <param name="message">Mensaje descriptivo de la excepci�n.</param>
        /// <param name="context">Datos de contexto</param>
        ///<param name="innerException">Excepci�n  producida</param>        
        public GobbiTechnicalException(string message, IDictionary<string, object> context, Exception innerException)
            : base(message, context, innerException)
        {
            
        }

        /// <summary>
        /// Inicializa la instancia. 
        /// </summary>
        /// <param name="message">Mensaje descriptivo de la excepci�n.</param>
        ///<param name="innerException">Excepci�n  producida</param>  
        public GobbiTechnicalException(string message, Exception innerException)
            : base(message, null, innerException)
        {

        }
    }
}
