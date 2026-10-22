// <copyright file="AssetLocatorServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="AssetLocatorService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AssetLocatorServiceTests
    {
        /// <summary>
        /// Verifies that <c>GetAssetsFile</c> throws a <see cref="FileNotFoundException"/>
        /// when the project's assets file cannot be found.
        /// </summary>
        [Fact]
        public void GetAssetsFile_WhenAssetsFileDoesNotExist_Throws()
        {
            using TempDirectory tempDirectory = new();
            AssetLocatorService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "MyProject.csproj");

            File.WriteAllText(projectPath, string.Empty);

            FileNotFoundException exception = Assert.Throws<FileNotFoundException>(() => sut.GetAssetsFile(projectPath));
            Assert.Equal("project.assets.json not found", exception.Message.Split(Environment.NewLine)[0]);
        }

        /// <summary>
        /// Verifies that <c>GetAssetsFile</c> returns the path to the project assets file
        /// when <c>project.assets.json</c> exists in the project's <c>obj</c> directory.
        /// </summary>
        [Fact]
        public void GetAssetsFile_WhenAssetsFileExists_ReturnsPath()
        {
            using TempDirectory tempDirectory = new();
            AssetLocatorService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "MyProject.csproj");
            File.WriteAllText(projectPath, string.Empty);
            string objDirectory = Path.Combine(tempDirectory.DirectoryPath, "obj");
            Directory.CreateDirectory(objDirectory);
            string assetsPath = Path.Combine(objDirectory, "project.assets.json");

            File.WriteAllText(assetsPath, "{}");
            string result = sut.GetAssetsFile(projectPath);

            Assert.Equal(assetsPath, result);
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