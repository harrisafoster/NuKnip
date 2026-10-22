// <copyright file="AssetParsingServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="AssetParsingService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AssetParsingServiceTests
    {
        /// <summary>
        /// Verifies that <c>Parse</c> throws a <see cref="FileNotFoundException"/>
        /// when the specified assets file does not exist.
        /// </summary>
        [Fact]
        public void Parse_WhenFileDoesNotExist_Throws()
        {
            AssetParsingService sut = new();

            Assert.Throws<FileNotFoundException>(() => sut.Parse(Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json")));
        }

        /// <summary>
        /// Verifies that <c>Parse</c> throws a <see cref="JsonException"/> when the
        /// specified assets file contains invalid JSON.
        /// </summary>
        [Fact]
        public void Parse_WhenJsonIsInvalid_Throws()
        {
            using TempDirectory tempDirectory = new();
            AssetParsingService sut = new();
            string jsonPath = Path.Combine(tempDirectory.DirectoryPath, "project.assets.json");

            File.WriteAllText(jsonPath, "{ invalid json }");

            Assert.ThrowsAny<JsonException>(() => sut.Parse(jsonPath));
        }

        /// <summary>
        /// Verifies that <c>Parse</c> returns a <see cref="JsonDocument"/> containing
        /// the expected data when the specified assets file contains valid JSON.
        /// </summary>
        [Fact]
        public void Parse_WhenJsonIsValid_ReturnsDocument()
        {
            using TempDirectory tempDirectory = new();
            AssetParsingService sut = new();
            string jsonPath = Path.Combine(tempDirectory.DirectoryPath, "project.assets.json");
            File.WriteAllText(jsonPath, """{"hello": "world"}""");

            using JsonDocument result = sut.Parse(jsonPath);

            Assert.Equal("world", result.RootElement.GetProperty("hello").GetString());
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