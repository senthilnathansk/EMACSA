using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ar.com.telecom.eva.CoreServices.Caching.CacheManagers;

namespace ar.com.telecom.eva.CoreServices.Caching.BackingStorages
{
    /// <summary>
    /// Esta clase es usada cuando no se necesita almacenamiento de resguardo para la p�litica de resguardo elegida.
    /// La funci�n es proveer un almacenamiento de resguardo que no hace nada. Utilizado para tener un cache solo en memoria.
    /// Es usado por <see cref="InMemoryCacheManager"/>
    /// </summary>
    public class NullBackingStore : IBackingStore
    {
        /// <summary>
        /// Siempre devuelve 0
        /// </summary>
        public int Count
        {
            get { return 0; }
        }

        /// <summary>
        /// No usado.
        /// </summary>
        public NullBackingStore()
        {
        }

        /// <summary>
        /// No usado.
        /// </summary>
        /// <param name="newCacheItem">No usado.</param>
        public void Add(CacheItem newCacheItem)
        {
        }

        /// <summary>
        /// No usado.
        /// </summary>
        /// <param name="key">No usado.</param>
        public void Remove(string key)
        {
        }

        /// <summary>
        /// No usado.
        /// </summary>
        /// <param name="key">No usado.</param>
        /// <param name="timestamp">No usado.</param>
        public void UpdateLastAccessedTime(string key, DateTime timestamp)
        {
        }

        /// <summary>
        /// No usado.
        /// </summary>
        public void Flush()
        {
        }

        /// <summary>
        /// Siempre devuelve un Hastable vac�a.
        /// </summary>
        /// <returns>Hastable vac�a.</returns>
        public Hashtable Load()
        {
            return new Hashtable();
        }

        /// <summary>
        /// Implementaci�n vac�a.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
