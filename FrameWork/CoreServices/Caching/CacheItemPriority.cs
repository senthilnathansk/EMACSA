using System;
using System.Collections.Generic;
using System.Text;

namespace Gobbi.CoreServices.Caching
{
    ///<summary>
    /// Establece la prioridad del item para permanecer en la cache.
    /// <remarks> Las instancias de Cache deben circunstalcialmente remover �tems aplicando
    /// distintos algoritmos. La prioridad es un par�metro usado por dichos algoritmos.</remarks>
    /// 
    ///</summary>
    public enum CacheItemPriority
    {

        ///<summary>
        /// Identificador de prioridad Baja.
        ///</summary>
        Low = 1,
        ///<summary>
        /// identificador de prioridad Normal.
        ///</summary>
        Normal,
        ///<summary>
        /// Identificador de prioridad alta.
        ///</summary>
        High,

        ///<summary>
        /// Identificador de No Removible.
        ///</summary>
        NotRemovable
    }
}
