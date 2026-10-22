// <copyright file="FileSystemServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="FileSystemService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FileSystemServiceTests
    {
        /// <summary>
        /// Verifies that <c>FileExists</c> returns <c>false</c> when the specified
        /// file does not exist.
        /// </summary>
        [Fact]
        public void FileExists_WhenFileDoesNotExist_ReturnsFalse()
        {
            FileSystemService sut = new();

            bool result = sut.FileExists(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt"));

            Assert.False(result);
        }

        /// <summary>
        /// Verifies that <c>FileExists</c> returns <c>true</c> when the specified
        /// file exists.
        /// </summary>
        [Fact]
        public void FileExists_WhenFileExists_ReturnsTrue()
        {
            using TempDirectory tempDirectory = new();
            FileSystemService sut = new();
            string filePath = Path.Combine(tempDirectory.DirectoryPath, "test.txt");
            File.WriteAllText(filePath, "test");

            bool result = sut.FileExists(filePath);

            Assert.True(result);
        }

        private sealed class TempDirectory : IDisposable
        {
            public TempDirectory()
            {
                this.DirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(this.DirectoryPath);
            }

            public string DirectoryPath { get; }

            public void Dispose()
            {
                if (Directory.Exists(this.DirectoryPath))
                {
                    Directory.Delete(this.DirectoryPath, recursive: true);
                }
            }
        }
    }
}