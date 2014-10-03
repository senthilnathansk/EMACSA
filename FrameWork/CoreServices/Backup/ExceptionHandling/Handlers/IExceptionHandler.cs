using System;
using System.Collections.Generic;
using System.Text;
using ar.com.telecom.eva.CoreServices.Configuration;

namespace ar.com.telecom.eva.CoreServices.ExceptionHandling.Handlers
{
    /// <summary>
    /// Interface con los metodos que implementan los distintos manejadores de excepci�n.
    /// </summary>
    public interface IExceptionHandler : IConfigurable
    {
        /// <summary>
        /// M�todo que deben implementar todos los manejadores de excepciones.
        /// Entrega la excepci�n para hacer las operaciones.
        /// </summary>
        /// <param name="ex">La <see cref="EvaException"/> a procesar.</param>
        /// <returns>Una <see cref="EvaException"/>.</returns>
        EvaException HandleException(EvaException ex);
    }
}
