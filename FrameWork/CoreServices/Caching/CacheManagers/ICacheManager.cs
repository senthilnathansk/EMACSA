using System;
using System.Collections;
using Gobbi.CoreServices.Caching;
using Gobbi.CoreServices.Caching.BackingStorages;
using Gobbi.CoreServices.Caching.CacheItemExpirations;

namespace Gobbi.CoreServices.Caching.CacheManagers
{
    ///<summary>
    /// <para>Esta es la interface con el usuario de los diversos CacheManagers Implementados. 
    /// Todas las operaciones de cache son realizadas a trav�s de  �sta interface.</para>
    ///</summary>
    /// <example>
    /// Genera una nueva entrada entrada en el cacheManager o reemplaza la entrada existenta,
    /// indicando que el �tem expira a los 5 minutos de cada hora y que su prioridad es Alta.
    /// Al finalizar clavePreexistente indicar� True si el �tem fue insertado, de lo contrario False.
    /// <code>
    ///       clavePreexistente = cacheManager.Add("timeExpiration", producto, CacheItemPriority.High,
    ///                         new ICacheItemExpiration[] {new ExtendedFormatTime("5 * * * *")});
    /// </code>
    /// </example>
    public interface  ICacheManager
    {
        /// <summary>
        /// Nombre de la instancia de cache.
        /// </summary>
        string Name
        { get;}

        ///<summary>
        /// <para> Retorna el n�mero de items actualmente en la instancia de cache.</para>
        ///</summary>
         int Count
         {get;}

        ///<summary>
        /// <para> Retorna el item identificado por la clave</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave para obtener el valor de la cache.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        object this[string key]
        { get;
            set;
        }

        ///<summary>
        /// <para> Retorna true si hay un �tem en la cache con clave provista. </para>
        ///</summary>
        ///<param name="key">
        /// <para>La clave a verificar.</para>
        /// </param>
        ///<returns>True si la clave provista est� en el cache.</returns>
        bool Contains(string key);

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
         bool Add(string key, object value);

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
        bool Add(string key, object value, CacheItemPriority scavengingPriority, params ICacheItemExpiration[] expirations);

        ///<summary>
        /// <para> Remueve el �tem dado de la cache. Si no hay ning�n �tem con la clave dada el m�todo no hace nada.</para>
        ///</summary>
        ///<param name="key">
        /// <para> Clave identificatoria del �tem a remover.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">La clave provista es nula.</exception>
        /// <exception cref="ArgumentException">La clave provista es una cadena vac�a.</exception>
        void Remove(string key);

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
        object GetData(string key);

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
        T GetData<T>(string key);

        ///<summary>
        /// Retorna un <see cref="IDictionaryEnumerator"/> con los items de la cache. 
        ///</summary>
        ///<returns>
        /// <para> un <see cref="IDictionaryEnumerator"/> con los items de la cache. </para>
        /// </returns>
        IDictionaryEnumerator Enumerator();

        /// <summary>
        /// Obtiene una <see cref="Hashtable"/> conteniendo los �tems de cache actuales.
        /// </summary>
        Hashtable CurrentCacheState
        {
            get;
        }

        /// <summary>
        /// Inicializa la instancia con los valores de configuraci�n. No es para uso p�blico. P�blico para poder ser llamado
        /// desde <see cref="CacheFactory"/>.
        /// </summary>
        /// <param name="expirationPollFrequencyInSeconds">Frecuencia con la corre limpieza de la instancia.</param>
        /// <param name="maximumElementsInCacheBeforeScavenging">Catidad m�xima de �tems previo a realizar la limpieza del cache.</param>
        /// <param name="numberToRemoveWhenScavenging">Cantidad de �tems a remover tras realizar al terminar la limpieza.</param>
        /// <param name="backingStore">implementaci�n del almacenamiento de resguardo.</param>
        void Initialize( int expirationPollFrequencyInSeconds, int maximumElementsInCacheBeforeScavenging,
                        int numberToRemoveWhenScavenging, IBackingStore backingStore);
    }
}