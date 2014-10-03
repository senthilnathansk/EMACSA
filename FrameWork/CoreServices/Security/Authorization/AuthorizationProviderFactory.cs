using System;
using System.Configuration;
using Gobbi.CoreServices.Security.Authorization;
//using Gobbi.CoreServices.Security.Authorization.Provider;
using Gobbi.CoreServices.Configuration;
using Gobbi.CoreServices.Properties;
using Gobbi.CoreServices.ExceptionHandling;


namespace Gobbi.CoreServices.Security.Authorization
{
    ///<summary>
    /// Retorna distintas instancias de AuthorizationManager
    /// <remarks> Los distintos nombres de Authorization Manager est�n definidos y configurados 
    /// por la configuraci�n del proyecto.</remarks>
    ///</summary>
    /// <example>
    /// Para instanciar una cacheManager espec�fico: 
    /// <code>
    /// ICacheManager cacheManager = CacheFactory.GetCacheManager("cache1");
    /// </code>
    /// <para>Donde <em>cache1</em> est� definido en la configuraci�n.</para>
    /// </example>
    public static class AuthorizationProviderFactory
    {
        ///<summary>
        /// <para>Retorna la implementaci�n de <see cref="IAuthorizationManager"/> por defecto. 
        /// El nombre de �sta instancia y su configuraci�n est� en la configuraci�n del proyecto.</para>
        ///</summary>
        ///<returns>
        /// <para>  La instancia por defecto de AuthorizationManager.</para>
        /// </returns>
        public static IAuthorizationProvider GetAuthorizationProviderManager()
        {
            AuthorizationConfigurationSection authorizationConfigurationSection = AuthorizationConfigurationSection.GetInstance();
            if (authorizationConfigurationSection != null)
            {
                string defaultAuthorizationProviderManagerName = authorizationConfigurationSection.defaultAuthorizationProviderName;
                if (string.IsNullOrEmpty(defaultAuthorizationProviderManagerName))
                     throw new GobbiTechnicalException ("", new   ConfigurationErrorsException(Resources.ERROR_AUTHENTICATORFACTORY_INVALIDDEFAULTCACHEMANAGERNAME));
                return GetAuthorizationProviderManager(defaultAuthorizationProviderManagerName);
            }
            else
            {
                ///TODO:Reemplazar por un mensaje de ausencia del cachemanager por defecto
                 throw new GobbiTechnicalException ("", new  ConfigurationErrorsException(Resources.ERROR_AUTHENTICATORFACTORY_INVALIDDEFAULTCACHEMANAGERNAME));
            }
        }

        ///<summary>
        /// <para> Retorna la implementaci�n pedida de <see cref="IAuthorizationManager"/>.</para>
        ///</summary>
        ///<param name="cacheManagerName"><para>Nombre de la implementaci�n de <see cref="IAuthorizationManager"/> pedida, definida en la configuraci�n.</para></param>
        ///<returns><para>La implementaci�n de <see cref="IAuthorizationManager"/> pedida</para></returns>
        /// <exception cref="ArgumentException">authorizationProviderManagerName est� vac�o o es nulo</exception>
        public static IAuthorizationProvider GetAuthorizationProviderManager(string authorizationProviderManagerName)
        {
            if (string.IsNullOrEmpty(authorizationProviderManagerName))
                throw new GobbiTechnicalException ("", new  ArgumentException(Resources.ERROR_EMPTYPARAMETERNAME, "authorizationProviderManagerName"));
            AuthorizationConfigurationSection authorizationConfigurationSection = AuthorizationConfigurationSection.GetInstance();
            AuthorizationManagerConfiguration authorizationManagerConfiguration =
                authorizationConfigurationSection.authorizationManagers[authorizationProviderManagerName];
            if (authorizationManagerConfiguration == null)
                 throw new GobbiTechnicalException ("", new  ConfigurationErrorsException(
                    string.Format(Resources.ERROR_CONFIGURATIONNOTDEFINED, authorizationManagerConfiguration)));
            return (IAuthorizationProvider)ConfigurableObjectFactory.Create(
                                                    authorizationManagerConfiguration.Type, authorizationManagerConfiguration,
                                                        new object[] { authorizationProviderManagerName });
        }
    }
}

