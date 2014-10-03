using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using ar.com.telecom.eva.CoreServices.ExceptionHandling;
using ar.com.telecom.eva.CoreServices.Security.Role;

namespace ar.com.telecom.eva.CoreServices.Security.Principal
{
    /// <summary>
    /// Contiene informaci�n del usuario que est� ejecutando
    /// </summary>
    public class EvaPrincipal : IPrincipal
    {
        #region Attributes
        private string[] roles;
        private EvaIdentity identity;
        #endregion

        #region Ctor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="identity">Identidad de usuario</param>
        /// <param name="roles">Roles a los que pertenece</param>
        public EvaPrincipal(EvaIdentity identity, string[] roles)
        {
            this.identity = identity;
            this.roles = roles;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="identity">Identidad de usuario</param>
        /// <param name="channel">Canal de operaciones</param>
        public EvaPrincipal(EvaIdentity identity)
            : this(identity, null)
        {
        }

        #endregion

        #region IPrincipal Members

        /// <summary>
        /// Identidad del usuario que est� corriendo
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        /// <summary>
        /// Roles del usuario
        /// </summary>
        protected string[] Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        /// <summary>
        /// Verifica que el usuario est� en el role indicado
        /// </summary>
        /// <param name="role">Role a verificar</param>
        /// <exception cref="RolesNotDefinedException">Si no se han definido roles</exception>
        /// <returns>True si pudo verificar </returns>
        public virtual bool IsInRole(string role)
        {
            if (this.Roles == null)
            {
                try
                {
                    IRoleProvider roleProvider = RoleProviderFactory.GetRoleProviderManager();
                    this.roles = roleProvider.GetRoles(this);
                    // Obtener roles de la base de datos
                    //this.Roles = System.Web.Security.Roles.GetRolesForUser(this.Identity.Name);
                }
                catch (Exception ex)
                {
                     throw new EvaTechnicalException("Falla al obtener los roles del usuario", ex);
                }
            }
            return VerifyRole(role);
        }

        /// <summary>
        /// Verifica que si usuario est� incluido en alguno de los roles.
        /// </summary>
        /// <param name="role">Rol a verificar</param>
        /// <returns><see langword="true"/> si se encuentra entre los roles.</returns>
        protected bool VerifyRole(string role)
        {
            foreach (string rol in Roles)
            {
                if (rol == role)
                    return true;
            }
            return false;
        }

        #endregion
    }

    /// <summary>
    /// Excepci�n arrojada cuando no se pueden verificar los roles
    /// </summary>
    public class RolesNotDefinedException : Exception
    {

    }

    /// <summary>
    /// Excepci�n arrojada cuando no se pueden obtener los roles del usuario
    /// </summary>
    public class RolProviderNotDefinedException : Exception
    {

    }
}
