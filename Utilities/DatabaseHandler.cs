using System;
using System.Data;
using System.Data.SqlClient;


namespace Notifications.Utilities
{
    //Created by Michal Tyburski 2018-05
    /// <summary>
    /// Class to execute SQL statements
    /// </summary>
    public class DatabaseHandler
    {
        private string _connectionString;

        public DatabaseHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a stored procedure which returns a table with data
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetData(string procedureName, SqlConnection connection = null, SqlParameter[] parameters = null)
        {
            try
            {
                SqlDataReader reader;
                DataTable result = new DataTable();
                //
                //If connection is null open it once, execute sp and close it
                if (connection == null)
                {
                    using (connection = new SqlConnection(_connectionString))
                    {
                        using (var command = new SqlCommand(procedureName, connection))
                        {
                            command.CommandTimeout = 120;
                            command.CommandType = CommandType.StoredProcedure;
                            if (parameters != null) command.Parameters.AddRange(parameters);
                            command.CommandText = procedureName;
                            if (connection.State != ConnectionState.Open) connection.Open();
                            reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                result.Load(reader);
                            }
                            reader.Close();
                            return result;
                        }
                    }
                }
                else//This case connection comes from method over this. Open connection once and exec sps as many times as needed
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandTimeout = 120;
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null) command.Parameters.AddRange(parameters);
                        command.CommandText = procedureName;
                        if (connection.State != ConnectionState.Open) connection.Open();
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            result.Load(reader);
                        }
                        reader.Close();
                        return result;
                    }
                }

            }
            catch (TimeoutException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot get data from database, sp {procedureName}  due to exception: {e.Message}");
            }

        }

        /// <summary>
        /// Executes a stored procedure which doesnt return data. Used for an updates / inserts
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public int ExecuteStoredProcedure(string procedureName, SqlConnection connection = null, SqlParameter[] parameters = null)
        {
            try
            {
                //If connection is null open it once, execute sp and close it
                if (connection == null)
                {
                    using (connection = new SqlConnection(_connectionString))
                    {
                        using (var command = new SqlCommand(procedureName, connection))
                        {
                            command.CommandTimeout = 120;
                            command.CommandType = CommandType.StoredProcedure;
                            if (parameters != null) command.Parameters.AddRange(parameters);
                            command.CommandText = procedureName;
                            if (connection.State != ConnectionState.Open) connection.Open();
                            var result = command.ExecuteScalar();
                            return int.Parse(result != null ? result.ToString() : "0");
                        }
                    }
                }
                else //This case connection comes from method over this. Used for loading huge amount of data. Open connection once and exec sps as many times as needed
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandTimeout = 120;
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null) command.Parameters.AddRange(parameters);
                        command.CommandText = procedureName;
                        if (connection.State != ConnectionState.Open) connection.Open();
                        var result = command.ExecuteScalar();
                        return int.Parse(result != null ? result.ToString() : "0");
                    }
                }
            }
            catch (TimeoutException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot execute procedure {procedureName} due to exception: {e.Message}");
            }
        }


        public static bool CheckConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("sp_CheckConnection", connection))
                    {
                        command.CommandTimeout = 120;
                        command.CommandType = CommandType.StoredProcedure;
                        if (connection.State != ConnectionState.Open) connection.Open();
                        var result = command.ExecuteScalar();
                        return int.Parse(result != null ? result.ToString() : "0") > 0;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
