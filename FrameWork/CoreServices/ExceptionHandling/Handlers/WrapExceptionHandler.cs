using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Gobbi.CoreServices.Configuration;

namespace Gobbi.CoreServices.ExceptionHandling.Handlers
{
    /// <summary>
    /// Encapsula la excepci�n actual con una nueva del tipo especificado.
    /// </summary>
    public class WrapExceptionHandler : IExceptionHandler
    {
        private string exceptionMessage;
        private Type wrapExceptionType;

        /// <summary>
        /// Inicializa una nueva instancia.
        /// </summary>
        public WrapExceptionHandler()
        {

        }

        /// <summary>
        /// M�todo que deben implementar todos los manejadores de excepciones.
        /// Entrega la excepci�n para hacer las operaciones.
        /// 
        /// </summary>
        /// <param name="ex">La <see cref="EvaException"/> a procesar.</param>
        /// <returns>Una <see cref="EvaException"/>.</returns>
        public EvaException HandleException(EvaException ex)
        {
            object[] extraParameters = { exceptionMessage, ex.Context, ex };
            return (EvaException)Activator.CreateInstance(wrapExceptionType, extraParameters);   
        }


        /// <summary>
        /// Entrega al objeto su elemento de configuraci�n.
        /// </summary>
        /// <param name="element">Elemento de configuraci�n para el objeto.</param>
        public void Configure(ConfigurationElement element)
        {
            DynamicConfigurationElement dce = (DynamicConfigurationElement)element;
            this.exceptionMessage = dce.GetPropertyValue("exceptionMessage");
            wrapExceptionType = Type.GetType(dce.GetPropertyValue("wrapExceptionType"));
        }
    }
}
