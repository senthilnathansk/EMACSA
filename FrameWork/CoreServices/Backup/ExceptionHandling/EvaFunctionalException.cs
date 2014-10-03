using System;
using System.Collections.Generic;
using System.Text;

namespace ar.com.telecom.eva.CoreServices.ExceptionHandling
{
    ///<summary>
    /// <para>Es una excepci�n adaptada para que el programador agregue informaci�n de contexto y un mensaje,
    /// para utilizar arrojando excepciones de negocio. </para>
    ///</summary>
    public class EvaFunctionalException : EvaException
    {
        ///<summary>
        /// <para>Inicializa la excepci�n.</para>
        ///</summary>
        ///<param name="message">
        /// <para>Mensaje descriptivo de la excepci�n.</para>
        /// </param>
        ///<param name="context">
        /// <para>Datos de contexto</para>
        /// </param>
        public EvaFunctionalException(string message, IDictionary<string, object> context)
            :
            base( message, context, null)
        {
            
        }

        /// <summary>
        /// Inicializa la excepci�n.
        /// </summary>
        /// <param name="message">
        /// <para>Mensaje descriptivo de la excepci�n.</para>
        /// </param>
        public EvaFunctionalException(string message)
            :
    base(message, null, null)
        {

        }
    }
}
