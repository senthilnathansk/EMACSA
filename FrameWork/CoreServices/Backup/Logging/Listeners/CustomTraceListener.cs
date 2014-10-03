using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using ar.com.telecom.eva.CoreServices.Configuration;

namespace ar.com.telecom.eva.CoreServices.Logging.Listeners
{
    /// <summary>
    /// <para>Representa un TraceListener Base para ser utilizado para guardar guardar las entradas de Log. 
    /// Todos los TraceListeners deben heredar de �sta clase.</para>
    /// <remarks>Es un punto de extensi�n para agregar mas TraceListeners.</remarks>
    /// </summary>
    public abstract class CustomTraceListener : TraceListener, IConfigurable
    {
        private string name;

        /// <summary>
        /// Nombre de la instancia, es clave identificatoria. Se obtiene de la configuraci�n.
        /// </summary>
        public new string Name
        {
            get { return name; }
        }

        private string initializeData;
        /// <summary>
        /// Datos de inicializaci�n. Se obtiene de la configuraci�n. Es opcional seg�n la implementaci�n del TraceListener.
        /// </summary>
        public string InitializeData
        {
            get { return initializeData; }
        }

        /// <summary>
        /// Inicializa la instancia.
        /// </summary>
        /// <param name="name">Nombre de la instancia.</param>
        public CustomTraceListener(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Inicializa la instancia.
        /// </summary>
        /// <param name="name">Nombre de la instancia.</param>
        /// <param name="initializeData">Datos adicionales de inicializaci�n.</param>
        /// <remarks>Este constructor se llama solo en el caso que se haya especificado el valor para <paramref name="initializeData"/>
        /// en la configuraci�n.</remarks>
        public CustomTraceListener(string name, string initializeData)
        {
            this.name = name;
            this.initializeData = initializeData;
        }

        /// <summary>
        /// Graba el la entrada de log.
        /// Debe ser implementado en la clase que hereda.
        /// </summary>
        /// <param name="log">Entrada de log a grabar.</param>
        public abstract void Write(LogEntry log);

        /// <summary>
        /// Escribe un mensaje.
        /// <remarks> Este m�todo no debe ser llamado.</remarks>
        /// </summary>
        /// <param name="message">Mensaje a grabar.</param>
        public override void Write(string message)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Escribe un mensaje.
        /// <remarks> Este m�todo no debe ser llamado.</remarks>
        /// </summary>
        /// <param name="message">Mensaje a grabar.</param>
        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }



        #region IConfigurable Members

        /// <summary>
        /// Entrega al objeto su elemento de configuraci�n.
        /// </summary>
        /// <param name="element">Elemento de configuraci�n para el objeto.</param>
        public abstract void Configure(ConfigurationElement element);

        #endregion

        /// <summary>
        /// Compara las instancias por su nombre (clave identificatoria.)
        /// </summary>
        /// <param name="obj">objeto a comparar.</param>
        /// <returns>True si los objetos son iguales</returns>
        public override bool Equals(object obj)
        {
            if (obj is CustomTraceListener)
                return this.name.Equals(((CustomTraceListener) obj).name);
            return base.Equals(obj);
        }

        /// <summary>
        /// Retorna el c�digo hash identificador de la instancia. 
        /// </summary>
        /// <returns> c�digo hash identificador de la instancia</returns>
        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }
    }
}
