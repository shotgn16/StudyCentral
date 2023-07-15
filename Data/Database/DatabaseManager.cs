using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace StudyCentralV2.Data.Database
{
    public class DatabaseManager
    {
        public string _connectionString { get; set; }

        public DatabaseManager(string connectionString) 
        {
            _connectionString = connectionString;    
        }

        public async Task<MySqlConnection> CreateConnection(MySqlConnection connection = null)
        {
            try
            {
                connection = new MySqlConnection(_connectionString);
            }

            catch (Exception ex)
            {
                // Log
            }

            return connection;
        }

        public async Task<bool> CheckDatabaseExists(string dbName, bool returnValue = false)
        {
            using (var connection = await CreateConnection())
            {
                await connection.OpenAsync();

                string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{dbName}'";

                using (var command = new MySqlCommand(query, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalarAsync());

                    if (count > 0) { returnValue = true; }
                }
                await connection.CloseAsync();
            }

            return returnValue;
        }

        public async Task CreateDatabase(string query)
        {
            using (var connection = await CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
                
                await connection.DisposeAsync();
            }
        }

        //Will check if DB exists and if not, create it then check again. - If returned true, the database exists!
        public async Task<bool> CheckAndCreateDatabase(string dbName, string query, bool returnValue = false)
        {
            if (!await CheckDatabaseExists(dbName))
            {
                await CreateDatabase(query);
            }

            if (await CheckDatabaseExists(dbName))
            {
                returnValue = true;
            }

            return returnValue;
        }

        public async Task CheckTableExists(string dbName, string tableName)
        {
            try
            {
                using (var connection = await CreateConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand($"SELECT * FROM information_schema.tables WHERE table_schema = {dbName} AND table_name = '{tableName}'", connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows) //Table does NOT exist!
                            {
                                
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Log
            }
        }

        public async Task CreateTable(string query)
        {
            using (var connection = await CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }

                await connection.DisposeAsync();
            }
        }
    }
}
