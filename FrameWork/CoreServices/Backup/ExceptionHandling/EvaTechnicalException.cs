using System;
using System.Collections.Generic;
using System.Text;

namespace ar.com.telecom.eva.CoreServices.ExceptionHandling
{
    ///<summary>
    /// <para>Es una excepci�n adaptada para que el programador agregue informaci�n de contexto,
    /// para arrojar en Excepciones t�cnicas.
    ///  </para>
    ///</summary>
    public class EvaTechnicalException : EvaException
    {
       
        ///<summary>
        /// Inicializa la instancia.
        ///</summary>
        /// <param name="message">Mensaje descriptivo de la excepci�n.</param>
        /// <param name="context">Datos de contexto</param>
        ///<param name="innerException">Excepci�n  producida</param>        
        public EvaTechnicalException(string message, IDictionary<string, object> context, Exception innerException)
            : base(message, context, innerException)
        {
            
        }

        /// <summary>
        /// Inicializa la instancia. 
        /// </summary>
        /// <param name="message">Mensaje descriptivo de la excepci�n.</param>
        ///<param name="innerException">Excepci�n  producida</param>  
        public EvaTechnicalException(string message, Exception innerException)
            : base(message, null, innerException)
        {

        }
    }
}
