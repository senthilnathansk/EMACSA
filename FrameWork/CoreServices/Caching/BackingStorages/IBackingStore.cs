using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gobbi.CoreServices.Caching.BackingStorages
{
    /// <summary>
    /// <P>Esta interface define el contrato que deben implementar todos los almacenamientos de respaldo.
    /// Cada implementaci�n debe interactuar con un mecanismo de persistencia para almacenar y recuparar CacheItems.
    /// La implementaci�n debe funcionar de forma transaccional de forma que si se produce un error los datos siempre
    /// queden consistentes.</P>
    /// </summary>
    public interface IBackingStore : IDisposable
    {
        /// <summary>
        /// N�mero de objetos almacenados en el almacenamiento de resguardo.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// <p>
        /// Este m�todo es responsable de agregar CacheItem al BackingStorage. Este m�todo debe ser existoso inclusive si un
        /// �tem con la misma clave ya existe. Si el m�todo falla no debe agregarse el �tem.
        /// </p>
        /// </summary>
        /// <param name="newCacheItem">CacheItem a ser agregado</param>
        void Add(CacheItem newCacheItem);

        /// <summary>
        /// Elimina un �tem con la clave dada del almacenamiento de resguardo.
        /// </summary>
        /// <param name="key">Clave a eliminar. No puede ser null.</param>
        void Remove(string key);

        /// <summary>
        /// Actualiza la fecha y hora del �ltimo acceso para el �tem de cache.
        /// </summary>
        /// <param name="key">Clave a actualizar.</param>
        /// <param name="timestamp">Fecha y hora a la cual el �tem es actualizdo.</param>
        void UpdateLastAccessedTime(string key, DateTime timestamp);

        /// <summary>
        /// Limpia todos los CacheItems del almacenamiento de resguardo.
        /// </summary>
        void Flush();

        /// <summary>
        /// Carga todos los CacheItems desde el almacenamiento de resguardo.
        /// </summary>
        /// <returns>Hashtable conteniendo todos los CacheItems existentes.</returns>
        Hashtable Load();
    }
}
