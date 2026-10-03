using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using SteamCMD_GUI;

namespace SteamCMD_GUI.Tests
{
    public class UpdateManagerTests
    {
        [Fact]
        public async Task ExtractZip_OffloadedToTaskRun_FreesCallingThread()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);
            string zipPath = Path.Combine(tempDir, "test.zip");
            string extractDir = Path.Combine(tempDir, "extracted");
            Directory.CreateDirectory(extractDir);

            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                for (int i = 0; i < 20; i++)
                {
                    var entry = archive.CreateEntry($"file_{i}.bin");
                    using (var stream = entry.Open())
                    {
                        byte[] buffer = new byte[1024 * 1024]; // 1MB
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }

            // Record calling thread ID
            int callingThreadId = Thread.CurrentThread.ManagedThreadId;
            int extractionThreadId = -1;

            await Task.Run(() =>
            {
                extractionThreadId = Thread.CurrentThread.ManagedThreadId;
                ZipFile.ExtractToDirectory(zipPath, extractDir, overwriteFiles: true);
            });

            Assert.NotEqual(-1, extractionThreadId);
            Assert.NotEqual(callingThreadId, extractionThreadId);

            Directory.Delete(tempDir, true);
        }

        [Fact]
        public async Task DownloadCommunityMod_ExtractsZipAndRaisesEvents()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            var updateManager = new UpdateManager("dummy");
            bool completedRaised = false;
            string completedMod = "";

            updateManager.UpdateCompleted += (mod) =>
            {
                completedRaised = true;
                completedMod = mod;
            };

            // Test non-TF2C path
            await updateManager.DownloadCommunityMod("CustomMod", tempDir);

            Assert.True(completedRaised);
            Assert.Equal("CustomMod", completedMod);

            Directory.Delete(tempDir, true);
        }
    }
}
