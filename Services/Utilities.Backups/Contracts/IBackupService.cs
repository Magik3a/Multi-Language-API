namespace Utilities.Services
{
    public interface IBackupService
    {
        /// <summary>
        /// Create a database backup.
        /// </summary>
        /// <param name="backupFolder">The folder to put the backupFile in.</param>
        /// <param name="prefix">Prefix for the backup file name.</param>
        /// <param name="version">Version of the application (will be used in the filename).</param>
        /// <returns>The full path of the backup file</returns>
        string Backup(string backupFolder, string prefix, string version);

        /// <summary>
        /// Restores database from backup file.
        /// </summary>
        /// <param name="backupFilePath">The database backup file path</param>
        void Restore(string backupFilePath);
    }
}