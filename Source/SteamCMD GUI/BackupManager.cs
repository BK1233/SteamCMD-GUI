using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace SteamCMD_GUI
{
    public class BackupManager
    {
        public event Action<string> BackupCreated;
        public event Action<string> BackupRestored;
        public event Action<string> BackupDeleted;
        public event Action<string> ErrorOccurred;

        public void CreateBackup(string sourcePath, string destinationPath)
        {
            try
            {
                string backupFileName = $"backup_{DateTime.Now:yyyyMMddHHmmss}.zip";
                string backupFilePath = Path.Combine(destinationPath, backupFileName);
                ZipFile.CreateFromDirectory(sourcePath, backupFilePath);
                BackupCreated?.Invoke(backupFilePath);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("Backup creation failed: " + ex.Message);
            }
        }

        public void RestoreBackup(string backupFilePath, string restorePath)
        {
            try
            {
                ZipFile.ExtractToDirectory(backupFilePath, restorePath);
                BackupRestored?.Invoke(restorePath);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("Backup restore failed: " + ex.Message);
            }
        }

        public void DeleteBackup(string backupFilePath)
        {
            try
            {
                File.Delete(backupFilePath);
                BackupDeleted?.Invoke(backupFilePath);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("Backup deletion failed: " + ex.Message);
            }
        }

        public List<string> ListBackups(string destinationPath)
        {
            List<string> backups = new List<string>();
            if (Directory.Exists(destinationPath))
            {
                foreach (string filePath in Directory.GetFiles(destinationPath, "backup_*.zip"))
                {
                    backups.Add(Path.GetFileName(filePath));
                }
            }
            return backups;
        }
    }
}
