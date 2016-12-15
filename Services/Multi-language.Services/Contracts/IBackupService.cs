namespace Multi_language.Services
{
    public interface IBackupService
    {
        void Initialiaze(string ConnectionString, string DatabaseName);

        /// <summary>
        /// Create a database backup.
        /// </summary>
        /// <param name="backupFolder">The folder to put the backupFile in.</param>
        /// <param name="prefix">Prefix for the backup file name.</param>
        /// <param name="version">Version of the application (will be used in the filename).</param>
        /// <param name="suffix">It will be added to the end of the name</param>
        /// <returns>The full path of the backup file</returns>
        string Backup(string backupFolder, string prefix, string version, string suffix);

        /// <summary>
        /// Restores database from backup file.
        /// </summary>
        /// <param name="backupFilePath">The database backup file path</param>
        /// <param name="dbName">Database name</param>
        /// <param name="logsName">Database logs name</param>
        void Restore(string backupFilePath, string dbName, string logsName);
    }
}