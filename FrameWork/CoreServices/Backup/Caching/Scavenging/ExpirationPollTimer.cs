using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ar.com.telecom.eva.CoreServices.Properties;
using ar.com.telecom.eva.CoreServices.ExceptionHandling;

namespace ar.com.telecom.eva.CoreServices.Caching.Scavenging
{
    /// <summary>
    /// Representa un temporizador de control de vencimientos.
    /// </summary>
    public sealed class ExpirationPollTimer : IDisposable
    {
        private Timer pollTimer;

        /// <summary>
        /// Incializa una nueva instancia.
        /// </summary>
        public ExpirationPollTimer()
        {

        }
        /// <summary>
        /// Inicia el temporizador.
        /// </summary>
        /// <param name="callbackMethod">M�todo a llamar cuando termina un ciclo.</param>
        /// <param name="pollCycleInMilliseconds">Tiempo en milisegundos entre cada llamada.</param>
        public void StartPolling(TimerCallback callbackMethod, int pollCycleInMilliseconds)
        {
            if (callbackMethod == null)
            {
                throw new EvaTechnicalException ("", new ArgumentNullException("callbackMethod"));
            }
            if (pollCycleInMilliseconds <= 0)
            {
                throw new EvaTechnicalException ("", new  ArgumentException(Resources.ERROR_EXPIRATIONPOLLTIMER_INVALIDTIME, "pollCycleInMilliseconds"));
            }

            pollTimer = new Timer(callbackMethod, null, pollCycleInMilliseconds, pollCycleInMilliseconds);
        }

        /// <summary>
        /// Detiene el temporizador.
        /// </summary>
        public void StopPolling()
        {
            if (pollTimer == null)
            {
                throw new EvaTechnicalException ("", new  InvalidOperationException(Resources.ERROR_EXPIRATIONPOLLTIMER_INVALIDSTOPOPERATION));
            }

            pollTimer.Dispose();
            pollTimer = null;
        }

        void IDisposable.Dispose()
        {
            if (pollTimer != null) pollTimer.Dispose();
        }
    }
}
