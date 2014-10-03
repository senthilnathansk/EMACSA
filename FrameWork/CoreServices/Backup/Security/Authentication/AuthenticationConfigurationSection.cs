using System.Configuration;
using ar.com.telecom.eva.CoreServices.Configuration;
using ar.com.telecom.eva.CoreServices.Properties;

namespace ar.com.telecom.eva.CoreServices.Security.Authentication
{
    class AuthenticationConfigurationSection : ConfigurationSection
    {

        #region Private Fields

        private static object lockSyncObject = new object();
        private static AuthenticationConfigurationSection authenticationConfigurationSection  = null;

        #endregion

        /// <summary>
        /// Retorna la instancia de la secci�n de configuraci�n
        /// </summary>
        /// <returns>Secci�n de configuracii�n para autenticaci�n</returns>
        internal static AuthenticationConfigurationSection GetInstance()
        {
            lock (lockSyncObject)
            {
                if (authenticationConfigurationSection == null)
                {
                    authenticationConfigurationSection =
                        (AuthenticationConfigurationSection)
                        ar.com.telecom.eva.CoreServices.Configuration.ConfigurationManager.GetSection(
                            Constants.AuthenticationConfigurationSectionName);
                }
            }
            return authenticationConfigurationSection;
        }

        [ConfigurationProperty("defaultAuthenticatorManagerName")]
        public string defaultAuthenticatorManagerName
        {
            get { return (string)base["defaultAuthenticatorManagerName"]; }
            set { base["defaultAuthenticatorManagerName"] = value; }
        }

        [ConfigurationProperty("authenticationManagers")]
        public GenericConfigurationElementCollection<AuthenticationManagerConfiguration> authenticationManagers
        {
            get { return (GenericConfigurationElementCollection<AuthenticationManagerConfiguration>)base["authenticationManagers"]; }
            set { base["authenticationManagers"] = value; }
        } 


    }
}
