using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using System.Xml;
using ar.com.telecom.eva.CoreServices.Data.Instrumentation;
using ar.com.telecom.eva.CoreServices.Properties;
using ar.com.telecom.eva.CoreServices.ExceptionHandling;

namespace ar.com.telecom.eva.CoreServices.Data.Databases
{
    /// <summary>
    /// <para> Este proveedor es una implementaci�n de la clase Database para SQL Server.</para>
    /// </summary>
    public class SQLDatabase : GenericDatabase
    {
        /// <summary>
        /// Inicializa la instancia.
        /// </summary>
        /// <param name="databaseName">Nombre de la base de datos en la configuraci�n.</param>
        /// <param name="connectionString">cadena de conexi�n a la base de datos.</param>
        public SQLDatabase(string databaseName, string connectionString)
            :base(databaseName,SqlClientFactory.Instance, Constants.SQLDatabaseProvider, connectionString)
        {


        }

        /// <summary>
        /// Inicializa la instancia.
        /// </summary>
        ///  <param name="databaseName">Nombre de la base de datos en la configuraci�n.</param>
        public SQLDatabase(string databaseName)
            :base(databaseName, SqlClientFactory.Instance, Constants.SQLDatabaseProvider)
        {
        }

        /// <summary>
        /// <para>Obtiene el s�mbolo usado por SQL Server para delimitar los par�metros.</para>
        /// </summary>
        /// <value>
        /// <para>El s�mbolo '@'.</para>
        /// </value>
        protected char ParameterToken
        {
            get { return '@'; }
        }

        /// <summary>
        /// <para>Ejecuta el <see cref="SqlCommand"/> y devuelve un <see cref="XmlReader"/> nuevo.</para>
        /// </summary>
        /// <param name="command">
        /// <para>El <see cref="SqlCommand"/> a ejecutar.</para>
        /// </param>
        /// <returns>
        /// <para>Un <see cref="XmlReader"/>.</para>
        /// </returns>
        public XmlReader ExecuteXmlReader(DbCommand command)
        {
            SqlCommand sqlCommand = CheckIfSqlCommand(command);

            DbConnectionWrapper connection = CreateConnectionWrapper(false);
            command.Connection = connection.Connection;
            return DoExecuteXmlReader(sqlCommand);
        }


        private XmlReader DoExecuteXmlReader(SqlCommand sqlCommand)
        {
            try
            {
                DateTime startTime = DateTime.Now;
                XmlReader reader = sqlCommand.ExecuteXmlReader();
                return reader;
            }
            catch 
            {

                if (!counterFails)
                {
                    try
                    {
                        InstrumentationProvider.CommandFailed();
                    }
                    catch
                    {
                        counterFails = true;
                        ar.com.telecom.eva.CoreServices.Logging.Logger.WriteInformation("Error en performance Counter", "Error en performance Counter", "ApplicationsGeneral");
                    }
                }
                throw;
            }
        }

        private static SqlCommand CheckIfSqlCommand(DbCommand command)
        {
            SqlCommand sqlCommand = command as SqlCommand;
            if (sqlCommand == null) throw new EvaTechnicalException ("", new  ArgumentException(Resources.ERROR_SQLDATABASE_NOTSQLCOMMAND, "command"));
            return sqlCommand;
        }

        /// <summary>
        /// Obtiene informaci�n de parametros para el comando especificado en el <see cref="DbCommand"/> 
        /// completa la colecci�n de par�metros. 
        /// </summary>
        /// <param name="discoveryCommand">El <see cref="DbCommand"/> a realizar el descubrimiento.</param>
        /// <remarks>El <see cref="DbCommand"/> debe ser un <see cref="SqlCommand"/>.</remarks>
        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            SqlCommandBuilder.DeriveParameters((SqlCommand)discoveryCommand);
        }

        /// <summary>
        /// Genera el nombre de par�metro para el proveedor de base de datos actual.
        /// </summary>
        /// <param name="name">El nombre del par�metro.</param>
        /// <returns>El nombre del par�metro adaptado para la base de datos actual.</returns>
        public override string BuildParameterName(string name)
        {
            if (name[0] != this.ParameterToken)
            {
                return name.Insert(0, new string(this.ParameterToken, 1));
            }
            return name;
        }


    }
}
