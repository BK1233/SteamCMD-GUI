using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SteamCMD_GUI.Tests
{
    public class BackupManagerTests
    {
        [Fact]
        public void ListBackups_ReturnsEmptyList_WhenDirectoryDoesNotExist()
        {
            BackupManager manager = new BackupManager();
            string nonExistentPath = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_NonExistentDir_" + Guid.NewGuid());

            List<string> backups = manager.ListBackups(nonExistentPath);

            Assert.NotNull(backups);
            Assert.Empty(backups);
        }

        [Fact]
        public void ListBackups_ReturnsEmptyList_WhenDirectoryIsEmpty()
        {
            BackupManager manager = new BackupManager();
            string tempDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_TestEmpty_" + Guid.NewGuid());
            Directory.CreateDirectory(tempDir);

            try
            {
                List<string> backups = manager.ListBackups(tempDir);

                Assert.NotNull(backups);
                Assert.Empty(backups);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void ListBackups_ReturnsEmptyList_WhenNoBackupFilesExist()
        {
            BackupManager manager = new BackupManager();
            string tempDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_TestNoBackups_" + Guid.NewGuid());
            Directory.CreateDirectory(tempDir);

            try
            {
                File.WriteAllText(Path.Combine(tempDir, "regular_file.txt"), "content");
                File.WriteAllText(Path.Combine(tempDir, "backup_1234.txt"), "content");
                File.WriteAllText(Path.Combine(tempDir, "other_backup.zip"), "content");

                List<string> backups = manager.ListBackups(tempDir);

                Assert.NotNull(backups);
                Assert.Empty(backups);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void ListBackups_ReturnsOnlyMatchingBackupFiles_WhenBackupFilesExist()
        {
            BackupManager manager = new BackupManager();
            string tempDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_TestMatching_" + Guid.NewGuid());
            Directory.CreateDirectory(tempDir);

            try
            {
                File.WriteAllText(Path.Combine(tempDir, "backup_20230101120000.zip"), "content");
                File.WriteAllText(Path.Combine(tempDir, "backup_20230102120000.zip"), "content");
                File.WriteAllText(Path.Combine(tempDir, "other_file.zip"), "content");

                List<string> backups = manager.ListBackups(tempDir);

                Assert.NotNull(backups);
                Assert.Equal(2, backups.Count);
                Assert.Contains("backup_20230101120000.zip", backups);
                Assert.Contains("backup_20230102120000.zip", backups);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void CreateBackup_TriggersBackupCreated_WhenSuccessful()
        {
            BackupManager manager = new BackupManager();
            string sourceDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_CreateSource_" + Guid.NewGuid());
            string destDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_CreateDest_" + Guid.NewGuid());
            Directory.CreateDirectory(sourceDir);
            Directory.CreateDirectory(destDir);

            try
            {
                File.WriteAllText(Path.Combine(sourceDir, "test.txt"), "hello world");

                string? createdFilePath = null;
                manager.BackupCreated += (filePath) => createdFilePath = filePath;

                manager.CreateBackup(sourceDir, destDir);

                Assert.NotNull(createdFilePath);
                Assert.True(File.Exists(createdFilePath));
                Assert.StartsWith(destDir, createdFilePath);
            }
            finally
            {
                if (Directory.Exists(sourceDir)) Directory.Delete(sourceDir, true);
                if (Directory.Exists(destDir)) Directory.Delete(destDir, true);
            }
        }

        [Fact]
        public void CreateBackup_TriggersErrorOccurred_WhenSourceDirectoryDoesNotExist()
        {
            BackupManager manager = new BackupManager();
            string nonExistentSourceDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_NonExistentSource_" + Guid.NewGuid());
            string destDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_CreateDest_" + Guid.NewGuid());
            Directory.CreateDirectory(destDir);

            try
            {
                string? errorMessage = null;
                manager.ErrorOccurred += (msg) => errorMessage = msg;

                manager.CreateBackup(nonExistentSourceDir, destDir);

                Assert.NotNull(errorMessage);
                Assert.StartsWith("Backup creation failed:", errorMessage);
            }
            finally
            {
                if (Directory.Exists(destDir)) Directory.Delete(destDir, true);
            }
        }

        [Fact]
        public void CreateBackup_TriggersErrorOccurred_WhenDestinationDirectoryIsInvalid()
        {
            BackupManager manager = new BackupManager();
            string sourceDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_CreateSource_" + Guid.NewGuid());
            Directory.CreateDirectory(sourceDir);
            string invalidDestPath = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_InvalidDest\0" + Guid.NewGuid());

            try
            {
                string? errorMessage = null;
                manager.ErrorOccurred += (msg) => errorMessage = msg;

                manager.CreateBackup(sourceDir, invalidDestPath);

                Assert.NotNull(errorMessage);
                Assert.StartsWith("Backup creation failed:", errorMessage);
            }
            finally
            {
                if (Directory.Exists(sourceDir)) Directory.Delete(sourceDir, true);
            }
        }

        [Fact]
        public void RestoreBackup_TriggersBackupRestored_WhenSuccessful()
        {
            BackupManager manager = new BackupManager();
            string sourceDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_RestoreSource_" + Guid.NewGuid());
            string destDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_RestoreDest_" + Guid.NewGuid());
            string restoreDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_RestoreTarget_" + Guid.NewGuid());
            Directory.CreateDirectory(sourceDir);
            Directory.CreateDirectory(destDir);
            Directory.CreateDirectory(restoreDir);

            try
            {
                File.WriteAllText(Path.Combine(sourceDir, "file.txt"), "sample data");
                string? zipPath = null;
                manager.BackupCreated += (p) => zipPath = p;
                manager.CreateBackup(sourceDir, destDir);
                Assert.NotNull(zipPath);

                string? restoredPath = null;
                manager.BackupRestored += (path) => restoredPath = path;

                manager.RestoreBackup(zipPath, restoreDir);

                Assert.Equal(restoreDir, restoredPath);
                Assert.True(File.Exists(Path.Combine(restoreDir, "file.txt")));
            }
            finally
            {
                if (Directory.Exists(sourceDir)) Directory.Delete(sourceDir, true);
                if (Directory.Exists(destDir)) Directory.Delete(destDir, true);
                if (Directory.Exists(restoreDir)) Directory.Delete(restoreDir, true);
            }
        }

        [Fact]
        public void RestoreBackup_TriggersErrorOccurred_WhenBackupFileDoesNotExist()
        {
            BackupManager manager = new BackupManager();
            string nonExistentZip = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_NonExistentZip_" + Guid.NewGuid() + ".zip");
            string restoreDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_RestoreTarget_" + Guid.NewGuid());

            try
            {
                string? errorMessage = null;
                manager.ErrorOccurred += (msg) => errorMessage = msg;

                manager.RestoreBackup(nonExistentZip, restoreDir);

                Assert.NotNull(errorMessage);
                Assert.StartsWith("Backup restore failed:", errorMessage);
            }
            finally
            {
                if (Directory.Exists(restoreDir)) Directory.Delete(restoreDir, true);
            }
        }

        [Fact]
        public void DeleteBackup_TriggersBackupDeleted_WhenSuccessful()
        {
            BackupManager manager = new BackupManager();
            string tempDir = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_DeleteDir_" + Guid.NewGuid());
            Directory.CreateDirectory(tempDir);
            string tempFile = Path.Combine(tempDir, "backup_123.zip");
            File.WriteAllText(tempFile, "dummy");

            try
            {
                string? deletedPath = null;
                manager.BackupDeleted += (path) => deletedPath = path;

                manager.DeleteBackup(tempFile);

                Assert.Equal(tempFile, deletedPath);
                Assert.False(File.Exists(tempFile));
            }
            finally
            {
                if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            }
        }

        [Fact]
        public void DeleteBackup_TriggersErrorOccurred_WhenFilePathIsInvalid()
        {
            BackupManager manager = new BackupManager();
            string invalidPath = Path.Combine(Path.GetTempPath(), "SteamCMD_GUI_InvalidFile\0" + Guid.NewGuid());

            string? errorMessage = null;
            manager.ErrorOccurred += (msg) => errorMessage = msg;

            manager.DeleteBackup(invalidPath);

            Assert.NotNull(errorMessage);
            Assert.StartsWith("Backup deletion failed:", errorMessage);
        }
    }
}
