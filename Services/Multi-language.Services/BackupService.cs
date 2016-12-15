using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Multi_language.Services
{

    public class BackupService : IBackupService
    {
        private static string connectionString;
        private static string databaseName;


        public void Initialiaze(string ConnectionString, string DatabaseName)
        {

            connectionString = ConnectionString;
            databaseName = DatabaseName;
        }
        /// <inheritdoc />
        public string Backup(string backupFolder, string prefix, string version, string suffix)
        {
            var fileName = GetFileName(prefix, version, suffix, "bak");

            var backupPath = Path.Combine(backupFolder, fileName);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var backUpScript = string.Format(
                      @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' 
                        WITH NOFORMAT, 
                        INIT, 
                        NAME = N'{0}_backup', 
                        SKIP, 
                        NOREWIND, 
                        NOUNLOAD,  
                        STATS = 10", databaseName, backupPath);

                ExecuteNonQuery(connection, backUpScript);
            }
            return backupPath;
        }

        /// <summary>
        /// Creates a filename using the different parts specified. If null or empty, the part will be omitted.
        /// Filename is constructed as follows: prefix_ntfy_version_datetime_suffix.bak
        /// </summary>
        /// <param name="prefix">Prefix that is added at the start of the filename.</param>
        /// <param name="version">The version of the core service.</param>
        /// <param name="suffix">Suffix that is added at the end of the filename.</param>
        /// <param name="extension">The filename extension (without '.')</param>
        /// <returns>The filename with all the required parts (separated by an underscore)</returns>
        private string GetFileName(string prefix, string version, string suffix, string extension)
        {
            if (string.IsNullOrWhiteSpace(extension) || extension.StartsWith("."))
            {
                throw new ArgumentException(nameof(extension));
            }
            var filename = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                filename.Append($"{prefix}_");
            }
            filename.Append("ntfy_");
            if (!string.IsNullOrWhiteSpace(version))
            {
                filename.Append($"{version}_");
            }
            filename.Append(DateTime.UtcNow.ToString("yyyyMMdd_hhmmss"));
            if (!string.IsNullOrWhiteSpace(suffix))
            {
                filename.Append($"_{suffix}");
            }
            filename.Append($".{extension}");
            return filename.ToString();
        }

        /// <inheritdoc />
        public void Restore(string backupFilePath, string dbName, string logsName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Getting exclusive access to database in case there are other connections.
                SetSingleUserDatabase(connection);

                try
                {
                    // Getting the correct path to mdf file. Check link for more info http://stackoverflow.com/questions/10741281/error-restoring-database-backup
                    var findDbPathScript =
                        @"SELECT SUBSTRING(physical_name, 1,
                        CHARINDEX(N'master.mdf',
                        LOWER(physical_name)) - 1) Database_File_Location
                        FROM master.sys.master_files
                        WHERE database_id = 1 AND FILE_ID = 1";

                    var dbFilePath = ExecuteScalar(connection, findDbPathScript);

                    var mdfFilePath = dbFilePath + dbName;
                    var logsFilePath = dbFilePath + logsName;

                    var restoreScript =
                          $@"use master 
                            RESTORE DATABASE [{databaseName}] FROM  DISK = N'{backupFilePath}' 
                            WITH  FILE = 1, 
                            MOVE N'{dbName}' TO N'{mdfFilePath}',
                            MOVE N'{logsName}' TO N'{logsFilePath}',
                            REPLACE, 
                            RECOVERY, 
                            NOUNLOAD,
                            STATS = 10";

                    ExecuteNonQuery(connection, restoreScript);
                }
                finally
                {
                    // Rollback access type.
                    SetMultiUserDatabase(connection);
                }
            }
        }

        /// <summary>
        /// Set single-user control for database.
        /// </summary>
        /// <param name="connection">Server connection.</param>
        private void SetSingleUserDatabase(SqlConnection connection)
        {
            var singleUserScript = string.Format(
                  @"USE master 
                    ALTER DATABASE [{0}] SET SINGLE_USER 
                    WITH ROLLBACK IMMEDIATE;", databaseName);

            ExecuteNonQuery(connection, singleUserScript);
        }

        /// <summary>
        /// Set multi-user control for database.
        /// </summary>
        /// <param name="connection">Server connection.</param>
        private void SetMultiUserDatabase(SqlConnection connection)
        {
            var sqlScript = string.Format(
                  @"USE master 
                    ALTER DATABASE [{0}] SET MULTI_USER 
                    WITH ROLLBACK IMMEDIATE", databaseName);

            ExecuteNonQuery(connection, sqlScript);
        }

        /// <summary>
        /// Helper function to execute a non-query with commandTimeout of 5 minutes.
        /// </summary>
        /// <param name="connection">Server connection.</param>
        /// <param name="sqlScript">The sql script to execute.</param>
        private void ExecuteNonQuery(SqlConnection connection, string sqlScript)
        {
            using (var cmd = new SqlCommand(sqlScript, connection))
            {
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
        }

        private string ExecuteScalar(SqlConnection connection, string sqlScript)
        {
            using (var cmd = new SqlCommand(sqlScript, connection))
            {
                cmd.CommandTimeout = 300;
                return (string)cmd.ExecuteScalar();
            }
        }
    }
}
