using System;
using System.Configuration;
using ar.com.telecom.eva.CoreServices.Security.Authentication;
using ar.com.telecom.eva.CoreServices.Security.Authentication.Provider;
using ar.com.telecom.eva.CoreServices.Configuration;
using ar.com.telecom.eva.CoreServices.Properties;
using ar.com.telecom.eva.CoreServices.ExceptionHandling;

namespace ar.com.telecom.eva.CoreServices.Security.Authentication
{
    ///<summary>
    /// Retorna distintas instancias de AuthenticationManager
    /// <remarks> Los distintos nombres de Authentication Manager est�n definidos y configurados 
    /// por la configuraci�n del proyecto.</remarks>
    ///</summary>
    /// <example>
    /// Para instanciar una cacheManager espec�fico: 
    /// <code>
    /// ICacheManager cacheManager = CacheFactory.GetCacheManager("cache1");
    /// </code>
    /// <para>Donde <em>cache1</em> est� definido en la configuraci�n.</para>
    /// </example>
    public static class AuthenticationProviderFactory
    {
        ///<summary>
        /// <para>Retorna la implementaci�n de <see cref="ICacheManager"/> por defecto. 
        /// El nombre de �sta instancia y su configuraci�n est� en la configuraci�n del proyecto.</para>
        ///</summary>
        ///<returns>
        /// <para>  La instancia por defecto de AuthenticationProviderManager.</para>
        /// </returns>
        public static IAuthenticationProvider GetAuthenticationProviderManager()
        {
            AuthenticationConfigurationSection authenticationConfigurationSection = AuthenticationConfigurationSection.GetInstance();
            if (authenticationConfigurationSection != null)
            {
                string defaultAuthenticationProviderManagerName = authenticationConfigurationSection.defaultAuthenticatorManagerName;
                if (string.IsNullOrEmpty(defaultAuthenticationProviderManagerName))
                     throw new EvaTechnicalException ("", new   ConfigurationErrorsException(Resources.ERROR_AUTHENTICATORFACTORY_INVALIDDEFAULTCACHEMANAGERNAME));
                return GetAuthenticationProviderManager(defaultAuthenticationProviderManagerName);
            }
            else
            {
                ///TODO:Reemplazar por un mensaje de ausencia del cachemanager por defecto
                throw new EvaTechnicalException ("", new  ConfigurationErrorsException(Resources.ERROR_AUTHENTICATORFACTORY_INVALIDDEFAULTCACHEMANAGERNAME));
            }
        }

        ///<summary>
        /// <para> Retorna la implementaci�n pedida de <see cref="ICacheManager"/>.</para>
        ///</summary>
        ///<param name="cacheManagerName"><para>Nombre de la implementaci�n de <see cref="ICacheManager"/> pedida, definida en la configuraci�n.</para></param>
        ///<returns><para>La implementaci�n de <see cref="ICacheManager"/> pedida</para></returns>
        /// <exception cref="ArgumentException">cacheManagerName est� vac�o o es nulo</exception>
        public static IAuthenticationProvider GetAuthenticationProviderManager(string authenticationProviderManagerName)
        {
            if (string.IsNullOrEmpty(authenticationProviderManagerName))
                throw new EvaTechnicalException ("", new  ArgumentException(Resources.ERROR_EMPTYPARAMETERNAME, "authenticationProviderManagerName"));
            AuthenticationConfigurationSection authenticationConfigurationSection = AuthenticationConfigurationSection.GetInstance();
            AuthenticationManagerConfiguration authenticationManagerConfiguration =
                authenticationConfigurationSection.authenticationManagers[authenticationProviderManagerName];
            if (authenticationManagerConfiguration == null)
                 throw new EvaTechnicalException ("", new  ConfigurationErrorsException(
                    string.Format(Resources.ERROR_CONFIGURATIONNOTDEFINED, authenticationManagerConfiguration)));
            return (IAuthenticationProvider)ConfigurableObjectFactory.Create(
                                                    authenticationManagerConfiguration.Type, authenticationManagerConfiguration,
                                                        new object[] { authenticationProviderManagerName });
        }
    }
}
