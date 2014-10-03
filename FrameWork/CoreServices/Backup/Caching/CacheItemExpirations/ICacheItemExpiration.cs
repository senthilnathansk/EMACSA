using System;
using System.Collections.Generic;
using System.Text;

namespace ar.com.telecom.eva.CoreServices.Caching.CacheItemExpirations
{
    /// <summary>
    /// Es el contrato que deben cumplir los elementos de expiration. 
    /// <remarks> Permite agregar nuevos tipos de expiraciones de �tems.</remarks>
    /// </summary>
    public interface ICacheItemExpiration
    {
        /// <summary>
        /// Especif�ca si el �tem ha expirado o no.
        /// </summary>
        /// <returns>Devuelte true si el item ha expirado, de lo contrario false.</returns>
        bool HasExpired();

        /// <summary>
        /// Llamado para notificar que el CacheItem al cual �sta expiraci�n pertenece ha sido usado por el usuario.
        /// </summary>
        void Notify();

        /// <summary>
        /// Llamado para dar la oportunidad de inicializar a la expiraci�n a partir de la informaci�n contenida en el CacheItem.
        /// </summary>
        /// <param name="owningCacheItem">CacheItem due�a del objeto de expiraci�n actual.</param>
        void Initialize(CacheItem owningCacheItem);
    }
}
