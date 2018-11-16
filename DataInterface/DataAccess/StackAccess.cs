using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using DataInterface.DataModel;
    using DataInterface.SQL;

    /// <summary>
    /// Class for handling database access to the list of currently available stacks
    /// </summary>
    public class StackAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string MainDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the list of existing MSE Stacks and pass it back to the logic layer as a DataTable
        /// </summary>
        public DataTable GetStacks(int stackType)
        {
            DataTable dataTable = new DataTable();

            try
            {
                log.Debug($"Get Stacks DBconn: {MainDBConnectionString}");

                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetStacksList;
                            cmd.Parameters.Add("@StackType", SqlDbType.Int).Value = stackType;
                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackAccess Exception occurred: " + ex.Message);
                //log.Debug("StackAccess Exception occurred", ex);
            }

            return dataTable;
        }

        /// <summary>
        /// Method to save playlist elements for a specified playlist to the DB
        /// </summary>
        public void SaveStack(StackModel stackMetadata)
        {
            //Save out the top-level metadata
            try
            {
                log.Debug("SaveStack");

                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            SqlTransaction transaction;
                            // Start a local transaction.
                            transaction = connection.BeginTransaction("Save stack metadata");

                            // Must assign both transaction object and connection 
                            // to Command object for a pending local transaction
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            try
                            {
                                //Specify base command
                                cmd.CommandText = SQLCommands.sqlSaveStack;
                                //Set parameters
                                cmd.Parameters.Add("@ixStackID", SqlDbType.Float).Value = stackMetadata.ixStackID;
                                cmd.Parameters.Add("@StackName", SqlDbType.Text).Value = stackMetadata.StackName;
                                cmd.Parameters.Add("@StackType", SqlDbType.Int).Value = stackMetadata.StackType;
                                cmd.Parameters.Add("@ShowName", SqlDbType.Text).Value = stackMetadata.ShowName;
                                cmd.Parameters.Add("@ConceptID", SqlDbType.Int).Value = stackMetadata.ConceptID;
                                cmd.Parameters.Add("@ConceptName", SqlDbType.Text).Value = stackMetadata.ConceptName;
                                cmd.Parameters.Add("@Notes", SqlDbType.Text).Value = stackMetadata.Notes;

                                sqlDataAdapter.SelectCommand = cmd;
                                sqlDataAdapter.SelectCommand.Connection = connection;
                                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                                // Execute stored proc to store top-level metadata
                                sqlDataAdapter.SelectCommand.ExecuteNonQuery();

                                //Attempt to commit the transaction
                                transaction.Commit();
                                log.Debug("SaveStack complete");

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                log.Error("StackAccess Exception occurred: " + ex.Message);

                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackAccess Exception occurred: " + ex.Message);
                //log.Debug("StackAccess Exception occurred", ex);
            }
        }

        /// <summary>
        /// Method to delete a stack from the database - uses a cascade delete constraint on the stack elements table
        /// </summary>
        public void DeleteStack_DB(Double stackID)
        {
            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            SqlTransaction transaction;
                            // Start a local transaction.
                            transaction = connection.BeginTransaction("Delete Stack Metadata");

                            // Must assign both transaction object and connection 
                            // to Command object for a pending local transaction
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            try
                            {
                                //Specify base command
                                cmd.CommandText = SQLCommands.sqlDeleteStack;
                                //Set parameters
                                cmd.Parameters.Add("@StackID", SqlDbType.Float).Value = stackID;

                                sqlDataAdapter.SelectCommand = cmd;
                                sqlDataAdapter.SelectCommand.Connection = connection;
                                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                                // Execute stored proc to store top-level metadata
                                sqlDataAdapter.SelectCommand.ExecuteNonQuery();

                                //Attempt to commit the transaction
                                transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackAccess Exception occurred: " + ex.Message);
                //log.Debug("StackAccess Exception occurred", ex);
            }
        }

        /// <summary>
        /// Check for an existing stack by ID
        /// </summary>
        public Double CheckIfStackExists_DB(String stackName)
        {
            DataTable dataTable = new DataTable();
            Double stackID = -1;

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlCheckIfStackExists;
                            //Set parameters
                            cmd.Parameters.Add("@StackName", SqlDbType.NVarChar).Value = stackName;

                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                DataRow row = dataTable.Rows[0];
                                stackID = Convert.ToDouble(row["ixStackID"] ?? 0);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackAccess Exception occurred: " + ex.Message);
                //log.Debug("StackAccess Exception occurred", ex);
            }
            return stackID;
        }
        #endregion
    }
}
