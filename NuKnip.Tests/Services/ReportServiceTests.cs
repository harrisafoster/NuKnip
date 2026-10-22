// <copyright file="ReportServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Models;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="ReportService" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ReportServiceTests
    {
        /// <summary>
        /// Verifies that <c>WriteUnusedPackageReport</c> creates a report file
        /// containing the expected project name and unused package entries.
        /// </summary>
        [Fact]
        public void WriteUnusedPackageReport_WhenPackagesExist_WritesReport()
        {
            using TempDirectory tempDirectory = new();
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(tempDirectory.DirectoryPath);
                ReportService sut = new();
                IReadOnlyCollection<PackageInfo> packages =
                [
                    new(
                        "Newtonsoft.Json",
                        "13.0.3",
                        []),

                    new(
                        "Serilog",
                        "4.0.0",
                        []),
                ];

                sut.WriteUnusedPackageReport(@"C:\Projects\MyProject.csproj", packages);
                string reportPath = Path.Combine(tempDirectory.DirectoryPath, "nuknip-report.txt");

                Assert.True(File.Exists(reportPath));
                string[] lines = File.ReadAllLines(reportPath);
                Assert.Contains("NuKnip Report", lines);
                Assert.Contains("Project: MyProject.csproj", lines);
                Assert.Contains("Unused Packages:", lines);
                Assert.Contains("Newtonsoft.Json", lines);
                Assert.Contains("Serilog", lines);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectory);
            }
        }

        /// <summary>
        /// Verifies that <c>WriteUnusedPackageReport</c> creates a report file when no
        /// unused packages are provided and includes the expected report metadata.
        /// </summary>
        [Fact]
        public void WriteUnusedPackageReport_WhenPackageListIsEmpty_WritesReport()
        {
            using TempDirectory tempDirectory = new();
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(tempDirectory.DirectoryPath);
                ReportService sut = new();

                sut.WriteUnusedPackageReport(
                    @"C:\Projects\MyProject.csproj",
                    []);
                string reportPath = Path.Combine(tempDirectory.DirectoryPath, "nuknip-report.txt");

                Assert.True(File.Exists(reportPath));
                string[] lines = File.ReadAllLines(reportPath);
                Assert.Contains("NuKnip Report", lines);
                Assert.Contains("Project: MyProject.csproj", lines);
                Assert.Contains("Unused Packages:", lines);
                Assert.DoesNotContain("Newtonsoft.Json", lines);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectory);
            }
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