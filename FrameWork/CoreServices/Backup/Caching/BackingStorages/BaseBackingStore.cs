using System;
using System.Collections;

namespace ar.com.telecom.eva.CoreServices.Caching.BackingStorages
{
    /// <summary>
    /// Clase base para almacenamientos de respaldo. Contiene la implementaci�n de pol�ticas comunes y rutinas
    /// �tiles para todos los los almacenamientos de respaldo.
    /// </summary>
    public abstract class BaseBackingStore : IBackingStore, IDisposable
    {
        /// <summary>
        /// Incializa una nueva instancia.
        /// </summary>
        public BaseBackingStore()
        {

        }

        /// <summary>
        /// Destructor para BaseBackingStore
        /// </summary>
        ~BaseBackingStore()
        {
            Dispose(false);
        }

        /// <summary>
        /// Esta implementaci�n es suficiente para cualquier clase que no necesita comportamiento en su destructor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// M�todo usado para la descarga del objeto.
        /// </summary>
        /// <param name="disposing">True si es llamado durante la descarga. False si fue llamado desde el destructor.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Cantidad de objetos almacenados en el almacenamiento de resguardo.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Elimina un �tem con la clave dada del almacenamiento de respaldo.
        /// </summary>
        /// <param name="key">Clave a eliminar. No puede ser null.</param>
        public void Remove(string key)
        {
            Remove(key.GetHashCode());
        }

        /// <summary>
        /// Elimina un �tem con la clave de almacenamiento dada del almacenamiento de respaldo.
        /// </summary>
        /// <param name="storageKey">Clave �nica para el �tem de cache a ser eliminado</param>
        protected abstract void Remove(int storageKey);

        /// <summary>
        /// Actualiza la fecha y hora de �ltimo acceso para el �tem de cache.
        /// </summary>
        /// <param name="key">Clave a actualizar</param>
        /// <param name="timestamp">Hora a la cual el �tem fue actualizado.</param>
        public void UpdateLastAccessedTime(string key, DateTime timestamp)
        {
            UpdateLastAccessedTime(key.GetHashCode(), timestamp);
        }

        /// <summary>
        /// Actualiza la fecha y hora de �ltimo acceso para el �tem de cache referido por �sta clave de identificaci�n �nica.
        /// </summary>
        /// <param name="storageKey">Clave �nica de almacenamiento para el �tem de cache.</param>
        /// <param name="timestamp">Hora a la cual el �tem fue actualizado.</param>
        protected abstract void UpdateLastAccessedTime(int storageKey, DateTime timestamp);

        /// <summary>
        /// Limpia todos los �tems de cache desde el almacenamiento de respaldo. 
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// <p>
        /// Este m�todo es responsable de agregar a �tem de cache al almacenamiento de respaldo. Este metodo debe ser 
        /// exitoso aun cuando existe un �tem con la misma clave.
        /// </p> 
        /// </summary>
        /// <param name="newCacheItem">Item de cache a ser agregado.</param>
        public virtual void Add(CacheItem newCacheItem)
        {
            try
            {
                RemoveOldItem(newCacheItem.Key.GetHashCode());
                AddNewItem(newCacheItem.Key.GetHashCode(), newCacheItem);
            }
            catch
            {
                Remove(newCacheItem.Key.GetHashCode());
                throw;
            }
        }

        /// <summary>
        /// Carga todos los �tems de cache del mecanismo de persistencia implementado.
        /// </summary>
        /// <returns>Hashtable conteniento todos los �tems de cache existentes.</returns>
        public virtual Hashtable Load()
        {
            return LoadDataFromStore();
        }

        /// <summary>
        /// Elimina un �tem existente almacenado en un medio de persistencia almacenado con la misma clave que un nuevo �tem.
        /// </summary>
        /// <param name="storageKey">Clave �nica para el �tem de cache.</param>
        protected abstract void RemoveOldItem(int storageKey);

        /// <summary>
        /// Agrega un nuevo �tem al almacenamiento de persistenca.
        /// </summary>
        /// <param name="storageKey">Clave �nica para el �tem de cache.</param>
        /// <param name="newItem">Item a ser agregado al cache. deber�a no ser null.</param>
        protected abstract void AddNewItem(int storageKey, CacheItem newItem);

        /// <summary>
        /// Responsable por cargar los �tems del medio de persistencia. Este m�todo no deber�a hacer
        /// filtro para eliminar �tems expirados.
        /// </summary>
        /// <returns>Hashtable de todos los �tems cargados del medio de persistencia.</returns>
        protected abstract Hashtable LoadDataFromStore();
    }
}
