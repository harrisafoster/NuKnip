// <copyright file="AssemblyLocatorServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services;

    /// <summary>
    /// Test class for the <see cref="AssemblyLocatorService"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AssemblyLocatorServiceTests
    {
        /// <summary>
        /// Verifies that <c>GetAssemblyPath</c> throws a <see cref="DirectoryNotFoundException"/>
        /// when the project's bin directory does not exist.
        /// </summary>
        [Fact]
        public void GetAssemblyPath_WhenBinDirectoryDoesNotExist_Throws()
        {
            using TempDirectory tempDirectory = new();
            AssemblyLocatorService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "MyProject.csproj");

            File.WriteAllText(projectPath, string.Empty);

            DirectoryNotFoundException exception = Assert.Throws<DirectoryNotFoundException>(() => sut.GetAssemblyPath(projectPath));
            Assert.Contains("Bin directory not found", exception.Message);
        }

        /// <summary>
        /// Verifies that <c>GetAssemblyPath</c> throws a <see cref="FileNotFoundException"/>
        /// when the project exists but no assembly can be found in the output directory.
        /// </summary>
        [Fact]
        public void GetAssemblyPath_WhenAssemblyDoesNotExist_Throws()
        {
            using TempDirectory tempDirectory = new();
            AssemblyLocatorService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "MyProject.csproj");

            File.WriteAllText(projectPath, string.Empty);
            Directory.CreateDirectory(Path.Combine(tempDirectory.DirectoryPath, "bin"));

            FileNotFoundException exception = Assert.Throws<FileNotFoundException>(() => sut.GetAssemblyPath(projectPath));
            Assert.Contains("Assembly not found", exception.Message);
        }

        /// <summary>
        /// Verifies that <c>GetAssemblyPath</c> returns the expected assembly path
        /// when a matching assembly exists in the project's output directory.
        /// </summary>
        [Fact]
        public void GetAssemblyPath_WhenAssemblyExists_ReturnsPath()
        {
            using TempDirectory tempDirectory = new();
            AssemblyLocatorService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "MyProject.csproj");
            File.WriteAllText(projectPath, string.Empty);
            string assemblyDirectory = Path.Combine(tempDirectory.DirectoryPath, "bin", "Debug", "net8.0");
            Directory.CreateDirectory(assemblyDirectory);
            string assemblyPath = Path.Combine(assemblyDirectory, "MyProject.dll");

            File.WriteAllText(assemblyPath, string.Empty);
            string result = sut.GetAssemblyPath(projectPath);

            Assert.Equal(assemblyPath, result);
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