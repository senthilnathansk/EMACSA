using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Text;
using ar.com.telecom.eva.CoreServices.Logging.Configuration;

namespace ar.com.telecom.eva.CoreServices.Common.Instrumentation
{
    /// <summary>
    /// Genera los Performance counters. Al registrarse el assembly en el sistema.
    /// </summary>
    [RunInstaller(true)]
    public class InstrumentationInstaller : Installer
    {
        #region Constants

        private const string General = "Services Invocation Framework";

        #endregion

        #region Private Vars

        private System.ComponentModel.Container components;
        private static bool EnablePerformanceCounters;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public InstrumentationInstaller()
        {
            LoggingConfigurationSection lcs = LoggingConfigurationSection.GetInstance();
            if (lcs != null)
                EnablePerformanceCounters = lcs.PerformanceCountersEnabled;
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        /// <summary>
        /// Descarga los objetos instanciados por la componente
        /// </summary>
        /// <param name="disposing">indica si se deben descargar los objetos</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (components != null)
                        components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Events


        /// <summary>
        /// Crea los performance counters con sus categor�as.
        /// </summary>
        /// <param name="stateSaver"> Es pasado por el sistema e indicia el estado de la instalaci�n</param>
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            CreateCachingPerformanceCounters();
            CreateDataPerformanceCounters();
            CreateExceptionHandlingPerformanceCounters();
            CreateLoggingPerformanceCounters();

        }

        /// <summary>
        /// Elimina los performance counters con sus categor�as.
        /// </summary>
        /// <param name="savedState">Es pasado por el sistema e indicia el estado de la instalaci�n</param>
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            DeletePerformanceCounters();
        }

        #endregion


        internal static void CreateDataPerformanceCounters()
        {

            if (EnablePerformanceCounters)
            {
                //Si no existe la categoria para los contadores generales la creo
                if (!PerformanceCounterCategory.Exists(Constants.DataCategory))
                {
                    CounterCreationDataCollection counters = new CounterCreationDataCollection();

                    CounterCreationData commandExecutedTotal = new CounterCreationData();
                    commandExecutedTotal.CounterName = Constants.DataCommandExecutedTotalName;
                    commandExecutedTotal.CounterHelp = "N�mero total de comandos ejecutados";
                    commandExecutedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(commandExecutedTotal);

                    CounterCreationData commandExecutedPerSec = new CounterCreationData();
                    commandExecutedPerSec.CounterName = Constants.DataCommandExecutedPerSecName;
                    commandExecutedPerSec.CounterHelp = "N�mero de comandos ejecutados por segundo";
                    commandExecutedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(commandExecutedPerSec);

                    CounterCreationData averageCommandExecutedTotal = new CounterCreationData();
                    averageCommandExecutedTotal.CounterName = Constants.DataAverageCommandExecutedTotalName;
                    averageCommandExecutedTotal.CounterHelp = "Duraci�n promedia de los comandos ejecutados";
                    averageCommandExecutedTotal.CounterType = PerformanceCounterType.AverageTimer32;
                    counters.Add(averageCommandExecutedTotal);

                    CounterCreationData averageCommandExecutedTotalBase = new CounterCreationData();
                    averageCommandExecutedTotalBase.CounterName = Constants.DataAverageCommandExecutedTotalBaseName;
                    averageCommandExecutedTotalBase.CounterHelp = "Base de duraci�n promedio de los comandos ejecutados";
                    averageCommandExecutedTotalBase.CounterType = PerformanceCounterType.AverageBase;
                    counters.Add(averageCommandExecutedTotalBase);

                    CounterCreationData commandFailedTotal = new CounterCreationData();
                    commandFailedTotal.CounterName = Constants.DataCommandFailedTotalName;
                    commandFailedTotal.CounterHelp = "N�mero total de comandos que fallaron en su ejecuci�n.";
                    commandFailedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(commandFailedTotal);

                    CounterCreationData commandFailedPerSec = new CounterCreationData();
                    commandFailedPerSec.CounterName = Constants.DataCommandFailedPerSecName;
                    commandFailedPerSec.CounterHelp = "N�mero de comandos que fallaron en su ejecuci�n por segundo";
                    commandFailedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(commandFailedPerSec);

                    CounterCreationData connectionOpenedTotal = new CounterCreationData();
                    connectionOpenedTotal.CounterName = Constants.DataConnectionOpenedTotalName;
                    connectionOpenedTotal.CounterHelp = "N�mero total de conexiones abiertas";
                    connectionOpenedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(connectionOpenedTotal);

                    CounterCreationData connectionOpenedPerSec = new CounterCreationData();
                    connectionOpenedPerSec.CounterName = Constants.DataConnectionOpenedPerSecName;
                    connectionOpenedPerSec.CounterHelp = "N�mero de conexiones abiertas por segundo";
                    connectionOpenedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(connectionOpenedPerSec);

                    CounterCreationData connectionFailedTotal = new CounterCreationData();
                    connectionFailedTotal.CounterName = Constants.DataConnectionFailedTotalName;
                    connectionFailedTotal.CounterHelp =
                        "N�mero total de conexiones que fallaron al intentar abrirse";
                    connectionFailedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(connectionFailedTotal);

                    CounterCreationData connectionFailedPerSec = new CounterCreationData();
                    connectionFailedPerSec.CounterName = Constants.DataConnectionFailedPerSecName;
                    connectionFailedPerSec.CounterHelp =
                        "N�mero de conexiones que fallaron al intentar abrirse por segundo";
                    connectionFailedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(connectionFailedPerSec);

                    //Creo la categoria con los contadores previamente mencioandos
                    PerformanceCounterCategory.Create(Constants.DataCategory, Constants.DataCategoryHelp,
                        PerformanceCounterCategoryType.MultiInstance, counters);
                }
            }
        }
        /*
        internal static void CreateConfigurationPerformanceCounters()
        {
            //Si no existe la categoria para los contadores generales la creo
            if (!PerformanceCounterCategory.Exists(Constants.ConfigurationCategory))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection();

                CounterCreationData configurationSectionRetrievedTotal = new CounterCreationData();
                configurationSectionRetrievedTotal.CounterName = Constants.ConfigurationConfigurationSectionRetrievedTotalName;
                configurationSectionRetrievedTotal.CounterHelp =
                    "N�mero total de secciones configuraci�n cargadas desde almacenamiento satisfactoriamente.";
                configurationSectionRetrievedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(configurationSectionRetrievedTotal);

                CounterCreationData configurationSectionRetrievedPerSecond = new CounterCreationData();
                configurationSectionRetrievedPerSecond.CounterName =
                    Constants.ConfigurationConfigurationSectionRetrievedPerSecondName;
                configurationSectionRetrievedPerSecond.CounterHelp =
                    "N�mero de secciones configuraci�n cargadas desde almacenamiento satisfactoriamente por segundo.";
                configurationSectionRetrievedPerSecond.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                counters.Add(configurationSectionRetrievedPerSecond);

                CounterCreationData configurationSectionRetrieveFailedTotal = new CounterCreationData();
                configurationSectionRetrieveFailedTotal.CounterName =
                    Constants.ConfigurationConfigurationSectionRetrieveFailedTotalName;
                configurationSectionRetrieveFailedTotal.CounterHelp =
                    "N�mero total de secciones configuraci�n que no se pudieron cargadas desde almacenamiento satisfactoriamente.";
                configurationSectionRetrieveFailedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(configurationSectionRetrieveFailedTotal);

                CounterCreationData dynamicPropertyRetrievedTotal = new CounterCreationData();
                dynamicPropertyRetrievedTotal.CounterName = Constants.ConfigurationDynamicPropertyRetrievedTotalName;
                dynamicPropertyRetrievedTotal.CounterHelp =
                    "N�mero total de propiedades din�micas que se obtuvieron desde los elementos de configuraci�n.";
                dynamicPropertyRetrievedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(dynamicPropertyRetrievedTotal);

                CounterCreationData dynamicPropertyRetrieveFailedTotal = new CounterCreationData();
                dynamicPropertyRetrieveFailedTotal.CounterName =
                    Constants.ConfigurationDynamicPropertyRetrieveFailedTotalName;
                dynamicPropertyRetrieveFailedTotal.CounterHelp =
                    "N�mero total de propiedades din�micas que no se obtuvieron desde los elementos de configuraci�n.";
                dynamicPropertyRetrieveFailedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(dynamicPropertyRetrieveFailedTotal);


                //Creo la categoria con los contadores previamente mencioandos
                PerformanceCounterCategory.Create(Constants.ConfigurationCategory, Constants.ConfigurationCategoryHelp, PerformanceCounterCategoryType.MultiInstance, counters);
            }

        }
        */
        internal static void CreateExceptionHandlingPerformanceCounters()
        {
            if (EnablePerformanceCounters)
            {
                //Si no existe la categoria para los contadores generales la creo
                if (!PerformanceCounterCategory.Exists(Constants.ExceptionHandlingCategory))
                {
                    CounterCreationDataCollection counters = new CounterCreationDataCollection();

                    CounterCreationData exceptionHandledTotal = new CounterCreationData();
                    exceptionHandledTotal.CounterName = Constants.ExceptionHandlingExceptionHandledTotalName;
                    exceptionHandledTotal.CounterHelp =
                        "N�mero total de excepciones manejadas";
                    exceptionHandledTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(exceptionHandledTotal);

                    CounterCreationData exceptionHandledPerSec = new CounterCreationData();
                    exceptionHandledPerSec.CounterName =
                        Constants.ExceptionHandlingExceptionHandledPerSecName;
                    exceptionHandledPerSec.CounterHelp =
                        "N�mero de excepciones manejadas por segundo";
                    exceptionHandledPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(exceptionHandledPerSec);

                    CounterCreationData exceptionHandlerExecutedTotal = new CounterCreationData();
                    exceptionHandlerExecutedTotal.CounterName = Constants.ExceptionHandlingExceptionHandlerExecutedTotalName;
                    exceptionHandlerExecutedTotal.CounterHelp =
                        "N�mero total de manejadores de excepci�n ejecutados";
                    exceptionHandlerExecutedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(exceptionHandlerExecutedTotal);

                    CounterCreationData exceptionHandlerExecutedPerSec = new CounterCreationData();
                    exceptionHandlerExecutedPerSec.CounterName =
                        Constants.ExceptionHandlingExceptionHandlerExecutedPerSecName;
                    exceptionHandlerExecutedPerSec.CounterHelp =
                        "N�mero de manejadores de excepci�n ejecutados por segundo";
                    exceptionHandlerExecutedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(exceptionHandlerExecutedPerSec);

                    CounterCreationData exceptionHandlingErrorOccurredInvalidConfigurationTotal = new CounterCreationData();
                    exceptionHandlingErrorOccurredInvalidConfigurationTotal.CounterName =
                        Constants.ExceptionHandlingExceptionHandlingErrorOccurredInvalidConfigurationTotalName;
                    exceptionHandlingErrorOccurredInvalidConfigurationTotal.CounterHelp =
                        "N�mero total de errores por configuraci�n incorrecta de manejadores de excepci�n";
                    exceptionHandlingErrorOccurredInvalidConfigurationTotal.CounterType =
                        PerformanceCounterType.NumberOfItems32;
                    counters.Add(exceptionHandlingErrorOccurredInvalidConfigurationTotal);

                    CounterCreationData exceptionHandlingErrorOccurredInvalidConfigurationPerSec = new CounterCreationData();
                    exceptionHandlingErrorOccurredInvalidConfigurationPerSec.CounterName =
                        Constants.ExceptionHandlingExceptionHandlingErrorOccurredInvalidConfigurationPerSecName;
                    exceptionHandlingErrorOccurredInvalidConfigurationPerSec.CounterHelp =
                        "N�mero de errores por configuraci�n incorrecta de manejadores de excepci�n por segundo";
                    exceptionHandlingErrorOccurredInvalidConfigurationPerSec.CounterType =
                        PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(exceptionHandlingErrorOccurredInvalidConfigurationPerSec);

                    //Creo la categoria con los contadores previamente mencioandos
                    PerformanceCounterCategory.Create(Constants.ExceptionHandlingCategory,
                                                      Constants.ExceptionHandlingCategoryHelp,
                                                      PerformanceCounterCategoryType.MultiInstance, counters);
                }
            }
        }

        internal static void CreateLoggingPerformanceCounters()
        {
            if (EnablePerformanceCounters)
            {
                //Si no existe la categoria para los contadores generales la creo
                if (!PerformanceCounterCategory.Exists(Constants.LoggingCategory))
                {
                    CounterCreationDataCollection counters = new CounterCreationDataCollection();

                    CounterCreationData traceListenerEntryWrittenTotal = new CounterCreationData();
                    traceListenerEntryWrittenTotal.CounterName = Constants.LoggingTraceListenerEntryWrittenTotalName;
                    traceListenerEntryWrittenTotal.CounterHelp =
                        "N�mero total de registros creados por los TraceListener en los medio de almacenamieto";
                    traceListenerEntryWrittenTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(traceListenerEntryWrittenTotal);

                    CounterCreationData traceListenerEntryWrittenPerSec = new CounterCreationData();
                    traceListenerEntryWrittenPerSec.CounterName = Constants.LoggingTraceListenerEntryWrittenPerSecName;
                    traceListenerEntryWrittenPerSec.CounterHelp =
                        "N�mero total de registros creados por los TraceListener en los medio de almacenamieto por seg";
                    traceListenerEntryWrittenPerSec.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(traceListenerEntryWrittenPerSec);

                    CounterCreationData failureLoggingErrorTotal = new CounterCreationData();
                    failureLoggingErrorTotal.CounterName = Constants.LoggingFailureLoggingErrorTotalName;
                    failureLoggingErrorTotal.CounterHelp =
                        "N�mero total fallas de los TraceListener al escribir la entrada al medio de almacenamieto";
                    failureLoggingErrorTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(failureLoggingErrorTotal);

                    CounterCreationData failureLoggingErrorPerSec = new CounterCreationData();
                    failureLoggingErrorPerSec.CounterName = Constants.LoggingFailureLoggingErrorPerSecName;
                    failureLoggingErrorPerSec.CounterHelp =
                        "N�mero fallas de los TraceListener al escribir la entrada al medio de almacenamieto por segundo";
                    failureLoggingErrorPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(failureLoggingErrorPerSec);

                    CounterCreationData configurationFailureTotal = new CounterCreationData();
                    configurationFailureTotal.CounterName = Constants.LoggingConfigurationFailureTotalName;
                    configurationFailureTotal.CounterHelp =
                        "N�mero total de errores producidos al intentar obtener valores de configuraci�n";
                    configurationFailureTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(configurationFailureTotal);

                    CounterCreationData configurationFailurePerSec = new CounterCreationData();
                    configurationFailurePerSec.CounterName = Constants.LoggingConfigurationFailurePerSecName;
                    configurationFailurePerSec.CounterHelp =
                        "N�mero de errores producidos al intentar obtener valores de configuraci�n por segundo";
                    configurationFailurePerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(configurationFailurePerSec);

                    CounterCreationData logEventRaisedTotal = new CounterCreationData();
                    logEventRaisedTotal.CounterName = Constants.LoggingLogEventRaisedTotalName;
                    logEventRaisedTotal.CounterHelp =
                        "N�mero total de entradas de log generadas por la aplicaci�n, antes de ser filtradas y almacenadas";
                    logEventRaisedTotal.CounterType =
                        PerformanceCounterType.NumberOfItems32;
                    counters.Add(logEventRaisedTotal);

                    CounterCreationData logEventRaisedPerSec = new CounterCreationData();
                    logEventRaisedPerSec.CounterName = Constants.LoggingLogEventRaisedPerSecName;
                    logEventRaisedPerSec.CounterHelp =
                        "N�mero de entradas de log generadas por la aplicaci�n, antes de ser filtradas y almacenadas por segundo";
                    logEventRaisedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(logEventRaisedPerSec);

                    CounterCreationData threadWriterStatusErrorTotal = new CounterCreationData();
                    threadWriterStatusErrorTotal.CounterName = Constants.LoggingThreadWriterStatusErrorTotalName;
                    threadWriterStatusErrorTotal.CounterHelp =
                        "N�mero total de fallos producidos en el thread escrituras de entradas de log";
                    threadWriterStatusErrorTotal.CounterType =
                        PerformanceCounterType.NumberOfItems32;
                    counters.Add(threadWriterStatusErrorTotal);

                    CounterCreationData threadWriterStatusErrorPerSec = new CounterCreationData();
                    threadWriterStatusErrorPerSec.CounterName = Constants.LoggingThreadWriterStatusErrorPerSecName;
                    threadWriterStatusErrorPerSec.CounterHelp =
                        "N�mero total de fallos producidos en el thread escrituras de entradas de log";
                    threadWriterStatusErrorPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(threadWriterStatusErrorPerSec);

                    //Creo la categoria con los contadores previamente mencionados
                    PerformanceCounterCategory.Create(Constants.LoggingCategory,
                                                      Constants.LoggingCategoryHelp,
                                                      PerformanceCounterCategoryType.MultiInstance, counters);
                }
            }
        }

        internal static void CreateCachingPerformanceCounters()
        {
            if (EnablePerformanceCounters)
            {
                //Si no existe la categoria para los contadores generales la creo
                if (!PerformanceCounterCategory.Exists(Constants.CachingCategory))
                {
                    CounterCreationDataCollection counters = new CounterCreationDataCollection();

                    CounterCreationData cachingCacheUpdatedTotal = new CounterCreationData();
                    cachingCacheUpdatedTotal.CounterName = Constants.CachingCacheUpdatedTotalName;
                    cachingCacheUpdatedTotal.CounterHelp =
                        "N�mero total de actualizaciones a �tems de las distintas instancias de cache";
                    cachingCacheUpdatedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(cachingCacheUpdatedTotal);

                    CounterCreationData cachingCacheUpdatedPerSec = new CounterCreationData();
                    cachingCacheUpdatedPerSec.CounterName = Constants.CachingCacheUpdatedPerSecName;
                    cachingCacheUpdatedPerSec.CounterHelp = "N�mero de actualizaciones a �tems  de cache por segundo";
                    cachingCacheUpdatedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(cachingCacheUpdatedPerSec);

                    CounterCreationData cacheAccessedTotal = new CounterCreationData();
                    cacheAccessedTotal.CounterName = Constants.CachingCacheAccessedTotalName;
                    cacheAccessedTotal.CounterHelp = "N�mero total de �tems de cache pedidos";
                    cacheAccessedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(cacheAccessedTotal);

                    CounterCreationData cacheAccessedPerSec = new CounterCreationData();
                    cacheAccessedPerSec.CounterName = Constants.CachingCacheAccessedPerSecName;
                    cacheAccessedPerSec.CounterHelp = "N�mero de �tems de cache pedidos por segundo";
                    cacheAccessedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(cacheAccessedPerSec);

                    CounterCreationData cacheExpiredTotal = new CounterCreationData();
                    cacheExpiredTotal.CounterName = Constants.CachingCacheExpiredTotalName;
                    cacheExpiredTotal.CounterHelp =
                        "N�mero total de �tems que fueron quitados por haber vencido";
                    cacheExpiredTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(cacheExpiredTotal);

                    CounterCreationData cacheExpiredPerSec = new CounterCreationData();
                    cacheExpiredPerSec.CounterName = Constants.CachingcacheExpiredPerSecName;
                    cacheExpiredPerSec.CounterHelp =
                        "N�mero total de �tems que fueron quitados por haber vencido por segundo";
                    cacheExpiredPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(cacheExpiredPerSec);

                    CounterCreationData cacheFailedTotal = new CounterCreationData();
                    cacheFailedTotal.CounterName = Constants.CachingCacheFailedTotalName;
                    cacheFailedTotal.CounterHelp =
                        "N�mero total fallos ocurridos en las intancias de cache";
                    cacheFailedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(cacheFailedTotal);

                    CounterCreationData cacheFailedPerSec = new CounterCreationData();
                    cacheFailedPerSec.CounterName = Constants.CachingCacheFailedPerSecName;
                    cacheFailedPerSec.CounterHelp =
                        "N�mero de fallos ocurridos en las intancias de cache por segundo";
                    cacheFailedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(cacheFailedPerSec);

                    CounterCreationData cacheScavengedTotal = new CounterCreationData();
                    cacheScavengedTotal.CounterName = Constants.CachingCacheScavengedTotalName;
                    cacheScavengedTotal.CounterHelp =
                        "N�mero total barridos de limpieza hechos a a las intancias de cache";
                    cacheScavengedTotal.CounterType = PerformanceCounterType.NumberOfItems32;
                    counters.Add(cacheScavengedTotal);

                    CounterCreationData cacheScavengedPerSec = new CounterCreationData();
                    cacheScavengedPerSec.CounterName = Constants.CachingCacheScavengedPerSecName;
                    cacheScavengedPerSec.CounterHelp =
                        "N�mero total barridos de limpieza hechos a a las intancias de cache por segundo";
                    cacheScavengedPerSec.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                    counters.Add(cacheScavengedPerSec);

                    //Creo la categoria con los contadores previamente mencionados
                    PerformanceCounterCategory.Create(Constants.CachingCategory,
                                                      Constants.CachingCategoryHelp,
                                                      PerformanceCounterCategoryType.MultiInstance, counters);
                }
            }
        }

        internal static void  DeletePerformanceCounters(string categoryName)
        {
            if (EnablePerformanceCounters)
            {
                if (PerformanceCounterCategory.Exists(categoryName))
                    PerformanceCounterCategory.Delete(categoryName);
            }
        }

        internal static void DeletePerformanceCounters()
        {
            if (EnablePerformanceCounters)
            {
                DeletePerformanceCounters(Constants.CachingCategory);
                DeletePerformanceCounters(Constants.ConfigurationCategory);
                DeletePerformanceCounters(Constants.DataCategory);
                DeletePerformanceCounters(Constants.ExceptionHandlingCategory);
                DeletePerformanceCounters(Constants.LoggingCategory);
            }
        }


    }
}
