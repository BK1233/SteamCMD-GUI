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
    }
}
