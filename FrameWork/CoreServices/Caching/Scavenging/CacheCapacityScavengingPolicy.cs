using System;
using System.Collections.Generic;
using System.Text;

namespace Gobbi.CoreServices.Caching.Scavenging
{
    /// <summary>
    /// La pol�tica de limpieza de la cache est� basada en la capacidad.
    /// </summary>
    public class CacheCapacityScavengingPolicy
    {
        private readonly int maximumElementsInCacheBeforeScavenging;

        /// <summary>
        /// Incializa una nueva instancia de <see cref="CacheCapacityScavengingPolicy"/> con la cantidad m�xima de �tems antes de realizar la limpieza.
        /// </summary>
        /// <param name="maximumElementsInCacheBeforeScavenging">The proxy to the latest configuration data.</param>
        public CacheCapacityScavengingPolicy(int maximumElementsInCacheBeforeScavenging)
        {
            this.maximumElementsInCacheBeforeScavenging = maximumElementsInCacheBeforeScavenging;
        }

        /// <summary>
        /// Obtiene la m�xima catidad items antes de comenzar con la limpieza de la cache.
        /// </summary>
        /// <value>
        /// La m�xima catidad items antes de comenzar con la limpieza de la cache.
        /// </value>
        public int MaximumItemsAllowedBeforeScavenging
        {
            get { return this.maximumElementsInCacheBeforeScavenging; }
        }

        /// <summary>
        /// Determina si es necesario limpiar la cache.
        /// </summary>
        /// <param name="currentCacheItemCount">La cantidad de actual de objetos <see cref="CacheItem"/> en la cache.</param>
        /// <returns>
        /// <see langword="true"/> si es necesario correr la limpieza de la cache; de lo contrario, <see langword="false"/>.
        /// </returns>
        public bool IsScavengingNeeded(int currentCacheItemCount)
        {
            return currentCacheItemCount > MaximumItemsAllowedBeforeScavenging;
        }
    }
}
