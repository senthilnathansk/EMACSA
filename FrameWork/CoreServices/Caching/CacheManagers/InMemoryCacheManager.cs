using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using Gobbi.CoreServices.Caching.BackingStorages;
using Gobbi.CoreServices.Caching.CacheItemExpirations;
using Gobbi.CoreServices.Caching.Configuration;
using Gobbi.CoreServices.Caching.Scavenging;
using Gobbi.CoreServices.Configuration;

namespace Gobbi.CoreServices.Caching.CacheManagers
{
    /// <summary>
    /// Implementa todas las operaciones de <see cref="ICacheManager"/>, sin almacenamiento de resguardo.
    /// </summary>
    public class InMemoryCacheManager : ICacheManager, IConfigurable
    {
        static Dictionary<string, ICacheManager> cacheManagers = new Dictionary<string, ICacheManager>();
        private static object syncObject = new object();
        private bool isConfigured = true;
        private ICacheManager internalCacheManager;
        /// <summary>
        /// Inicializa la instancia.
        /// </summary>
        /// <param name="cacheManagerName">Nombre de la instancia de cache.</param>
        public InMemoryCacheManager(string cacheManagerName)
           
        {
            lock (syncObject)
            {
                if (!cacheManagers.ContainsKey(cacheManagerName))
                {
                    cacheManagers.Add(cacheManagerName, new CacheManager(cacheManagerName));
                    this.isConfigured = false;

                }
            }
            this.internalCacheManager= cacheManagers[cacheManagerName];
        }

        /// <summary>
        /// Entrega al objeto su elemento de configuraci�n.
        /// </summary>
        /// <param name="element">Elemento de configuraci�n para el objeto.</param>
        /// <remarks> Este m�todo es utilizado por <see cref="CacheFactory"/> no debe ser llamado externamente.</remarks>
        public void Configure(ConfigurationElement element)
        {
            if (!this.isConfigured)
            {
                CacheManagerConfiguration cacheManagerConfiguration = (CacheManagerConfiguration) element;
                this.Initialize(cacheManagerConfiguration.ExpirationPollFrequencyInSeconds,
                                                     cacheManagerConfiguration.MaximumElementsInCacheBeforeScavenging,
                                                     cacheManagerConfiguration.NumberToRemoveWhenScavenging,
                                                     new NullBackingStore());
                this.isConfigured = true;
            }
        }

        /// <summary>
        /// Obtiene el nombre de la instancia.
        /// </summary>
        public string Name
        {
            get { return this.internalCacheManager.Name; }
        }

        ///<summary>
        /// <para> Obtiene el n�mero de items actualmente en la instancia de cache.</para>
        ///</summary>        
        public int Count
        {
            get { return this.internalCacheManager.Count; }
        }

        ///<summary>
        /// <para> Obtiene el item identificado por la clave</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave para obtener el valor de la cache.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        public object this[string key]
        {
            get { return this.internalCacheManager[key]; }
            set { this.internalCacheManager[key] = value; }
        }

        ///<summary>
        /// <para> Retorna true si hay un �tem en la cache con clave provista. </para>
        ///</summary>
        ///<param name="key">
        /// <para>La clave a verificar.</para>
        /// </param>
        ///<returns>True si la clave provista est� en el cache.</returns>
        public bool Contains(string key)
        {
            return this.internalCacheManager.Contains(key);
        }

        ///<summary>
        /// <para> Agrega un nuevo �tem a la cache si existe otro �tem con la misma clave lo reemplaza.
        /// Items agregados con �ste m�todo no expiran y su prioridad ser� <seealso cref="CacheItemPriority.Normal"/>. </para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave identificatoria para este �tem.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        ///<param name="value">
        /// <para> Valor a guardar en la cache. Puede ser nulo.</para>
        /// </param>
        /// <returns>
        /// True si el �tem exist�a previamente y fue reemplazado.
        /// </returns>
        public bool Add(string key, object value)
        {
            return this.internalCacheManager.Add(key, value);
        }

        /// <summary>
        /// <para> Agrega un nuevo �tem a la cache si existe otro �tem con la misma clave lo reemplaza.</para>
        /// </summary>
        /// <param name="key">
        /// <para> Clave identificatoria para este �tem.</para>
        /// </param>
         /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
         /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        /// <param name="value">
        /// <para> Valor a guardar en la cache. Puede ser nulo.</para>
        /// </param>
        /// <param name="scavengingPriority">Prioridad para permanecer en memoria. 
         /// Uno de los valores de <see cref="CacheItemPriority"/>.</param>
         /// <param name="expirations">Vencimiento del �tem. 
         /// Un arreglo de objetos <see cref="ICacheItemExpiration"/>.</param>
        /// <returns>True si el �tem exist�a previamente y fue reemplazado.</returns>
        public bool Add(string key, object value, CacheItemPriority scavengingPriority,
                        params ICacheItemExpiration[] expirations)
        {
            return this.internalCacheManager.Add(key, value, scavengingPriority, expirations);
        }

        ///<summary>
        /// <para> Elimina el �tem dado de la cache. Si no hay ning�n �tem con la clave dada el m�todo no hace nada.</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave identificatoria del �tem a remover.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        public void Remove(string key)
        {
            this.internalCacheManager.Remove(key);
        }

        ///<summary>
        /// <para> Retorna el valor asociado a la clave dada.</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave del �tem a retornar del cache.</para>
        /// </param>
        ///<returns>
        /// <para> Valor almacenado en el cache.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        public object GetData(string key)
        {
            return this.internalCacheManager.GetData(key);
        }

        ///<summary>
        /// <para> Retorna el valor asociado a la clave dada.</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave del �tem a retornar del cache.</para>
        /// </param>
        ///<typeparam name="T">
        /// <para> Tipo de valor almacenado en el �tem.</para>
        /// </typeparam>
        ///<returns>
        /// <para> Valor almacenado en el cache.</para> 
        /// </returns>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        public T GetData<T>(string key)
        {
            return this.internalCacheManager.GetData<T>(key);
        }

        ///<summary>
        /// Retorna un <see cref="IDictionaryEnumerator"/> con los items de la cache. 
        ///</summary>
        ///<returns>
        /// <para> un <see cref="IDictionaryEnumerator"/> con los items de la cache. </para>
        /// </returns>
        public IDictionaryEnumerator Enumerator()
        {
            return this.internalCacheManager.Enumerator();
        }

        /// <summary>
        /// Obtiene una <see cref="Hashtable"/> conteniendo los �tems de cache actuales.
        /// </summary>
        public Hashtable CurrentCacheState
        {
            get { return this.internalCacheManager.CurrentCacheState; }
        }

        /// <summary>
        /// Inicializa la instancia con los valores de configuraci�n. No es para uso p�blico. P�blico para poder ser llamado
        /// desde <see cref="CacheFactory"/>.
        /// </summary>
        /// <param name="expirationPollFrequencyInSeconds">Frecuencia con la corre limpieza de la instancia.</param>
        /// <param name="maximumElementsInCacheBeforeScavenging">Catidad m�xima de �tems previo a realizar la limpieza del cache.</param>
        /// <param name="numberToRemoveWhenScavenging">Cantidad de �tems a remover tras realizar al terminar la limpieza.</param>
        /// <param name="backingStore">implementaci�n del almacenamiento de resguardo.</param>
        public void Initialize( int expirationPollFrequencyInSeconds,
                               int maximumElementsInCacheBeforeScavenging, int numberToRemoveWhenScavenging,
                               IBackingStore backingStore)
        {
            this.internalCacheManager.Initialize(expirationPollFrequencyInSeconds, maximumElementsInCacheBeforeScavenging, 
                numberToRemoveWhenScavenging, backingStore);
        }
    }
}
