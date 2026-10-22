// <copyright file="BuildServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="BuildService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class BuildServiceTests
    {
        /// <summary>
        /// Verifies that <c>BuildAsync</c> returns <c>false</c> when the specified
        /// project file does not exist.
        /// </summary>
        /// <returns>
        /// A task containing the build result, which is expected to be <c>false</c>
        /// for a nonexistent project file.
        /// </returns>
        [Fact]
        public async Task BuildAsync_WhenProjectDoesNotExist_ReturnsFalse()
        {
            BuildService sut = new();

            bool result = await sut.BuildAsync(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.csproj"));

            Assert.False(result);
        }

        /// <summary>
        /// Verifies that <c>BuildAsync</c> returns <c>true</c> when the specified
        /// project is valid and builds successfully.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous build operation. The task result
        /// contains <c>true</c> when the project is built successfully.
        /// </returns>
        [Fact]
        public async Task BuildAsync_WhenProjectIsValid_ReturnsTrue()
        {
            using TempDirectory tempDirectory = new();
            BuildService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "TestProject.csproj");
#pragma warning disable SA1118 // Parameter should not span multiple lines. For readability, keeping on multiple lines.
            File.WriteAllText(
                projectPath,
                """
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <TargetFramework>net8.0</TargetFramework>
                    <OutputType>Exe</OutputType>
                    <ImplicitUsings>enable</ImplicitUsings>
                    <Nullable>enable</Nullable>
                  </PropertyGroup>
                </Project>
                """);
#pragma warning restore SA1118 // Parameter should not span multiple lines

            File.WriteAllText(Path.Combine(tempDirectory.DirectoryPath, "Program.cs"), """Console.WriteLine("Hello World");""");
            bool result = await sut.BuildAsync(projectPath);

            Assert.True(result);
        }

        /// <summary>
        /// Verifies that <c>BuildAsync</c> returns <c>false</c> when the specified
        /// project contains compilation errors and cannot be built successfully.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous build operation. The task result
        /// contains <c>false</c> when the project build fails.
        /// </returns>
        [Fact]
        public async Task BuildAsync_WhenProjectContainsCompileErrors_ReturnsFalse()
        {
            using TempDirectory tempDirectory = new();
            BuildService sut = new();
            string projectPath = Path.Combine(tempDirectory.DirectoryPath, "TestProject.csproj");

#pragma warning disable SA1118 // Parameter should not span multiple lines. Keeping on multiple lines for readability.
            File.WriteAllText(
                projectPath,
                """
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <TargetFramework>net8.0</TargetFramework>
                    <OutputType>Exe</OutputType>
                  </PropertyGroup>
                </Project>
                """);
#pragma warning restore SA1118 // Parameter should not span multiple lines

            File.WriteAllText(Path.Combine(tempDirectory.DirectoryPath, "Program.cs"), """this is not valid csharp""");
            bool result = await sut.BuildAsync(projectPath);

            Assert.False(result);
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