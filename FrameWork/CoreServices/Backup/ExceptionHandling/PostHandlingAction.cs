

namespace ar.com.telecom.eva.CoreServices.ExceptionHandling
{
    /// <summary>
    /// Determina que acci�n debe ocurrir luego de que la excepci�n es manejada. 
    /// </summary>
    public enum PostHandlingAction
    {
        /// <summary>
        /// Indica que no debe volver a arrojarse.
        /// </summary>
        None = 0,
        /// <summary>
        /// Nitifica al llamante que se recomienda volver a arrojarla.
        /// </summary>
        NotifyRethrow = 1,
        /// <summary>
        /// Arroja la excepci�n luego que la excepci�n fue manejada.
        /// </summary>
        ThrowNewException = 2
    }
}
