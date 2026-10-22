// <copyright file="PackageUsageAnalysisServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Models;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="PackageUsageAnalysisService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PackageUsageAnalysisServiceTests
    {
        /// <summary>
        /// Verifies that <c>Analyze</c> classifies a package with no associated
        /// assemblies as unknown and does not mark it as used or unused.
        /// </summary>
        [Fact]
        public void Analyze_WhenPackageHasNoAssemblies_ReturnsUnknownPackage()
        {
            PackageUsageAnalysisService sut = new();

            IReadOnlyCollection<PackageInfo> packages =
            [
                new(
                    "PackageA",
                    "1.0.0",
                    []),
            ];

            PackageAnalysisResult result = sut.Analyze(
                packages,
                []);

            PackageInfo package = Assert.Single(result.Unknown);
            Assert.Equal("PackageA", package.Name);
            Assert.Empty(result.Used);
            Assert.Empty(result.Unused);
        }

        /// <summary>
        /// Verifies that <c>Analyze</c> classifies a package as used when one of its
        /// assemblies is present in the referenced assembly collection.
        /// </summary>
        [Fact]
        public void Analyze_WhenPackageAssemblyIsReferenced_ReturnsUsedPackage()
        {
            PackageUsageAnalysisService sut = new();

            IReadOnlyCollection<PackageInfo> packages =
            [
                new(
                    "PackageA",
                    "1.0.0",
                    ["AssemblyA"]),
            ];

            PackageAnalysisResult result =
                sut.Analyze(
                    packages,
                    ["AssemblyA"]);

            PackageInfo package = Assert.Single(result.Used);

            Assert.Equal("PackageA", package.Name);
            Assert.True(package.HasAssemblies);
            Assert.Empty(result.Unused);
            Assert.Empty(result.Unknown);
        }

        /// <summary>
        /// Verifies that <c>Analyze</c> classifies a package as unused when none of
        /// its assemblies are present in the referenced assembly collection.
        /// </summary>
        [Fact]
        public void Analyze_WhenPackageAssemblyIsNotReferenced_ReturnsUnusedPackage()
        {
            PackageUsageAnalysisService sut = new();

            IReadOnlyCollection<PackageInfo> packages =
            [
                new(
                    "PackageA",
                    "1.0.0",
                    ["AssemblyA"]),
            ];

            PackageAnalysisResult result =
                sut.Analyze(
                    packages,
                    ["AssemblyB"]);

            PackageInfo package = Assert.Single(result.Unused);
            Assert.Equal("PackageA", package.Name);
            Assert.Empty(result.Used);
            Assert.Empty(result.Unknown);
        }

        /// <summary>
        /// Verifies that <c>Analyze</c> correctly classifies packages as used, unused,
        /// or unknown based on the presence of their assemblies in the referenced
        /// assembly collection.
        /// </summary>
        [Fact]
        public void Analyze_WhenPackagesContainUsedUnusedAndUnknown_ReturnsExpectedCollections()
        {
            PackageUsageAnalysisService sut = new();

            IReadOnlyCollection<PackageInfo> packages =
            [
                new(
                    "UsedPackage",
                    "1.0.0",
                    ["UsedAssembly"]),

                new(
                    "UnusedPackage",
                    "1.0.0",
                    ["UnusedAssembly"]),

                new(
                    "UnknownPackage",
                    "1.0.0",
                    []),
            ];

            PackageAnalysisResult result =
                sut.Analyze(
                    packages,
                    ["UsedAssembly"]);

            Assert.Single(result.Used);
            Assert.Single(result.Unused);
            Assert.Single(result.Unknown);
            Assert.Equal("UsedPackage", result.Used.Single().Name);
            Assert.Equal("UnusedPackage", result.Unused.Single().Name);
            Assert.Equal("UnknownPackage", result.Unknown.Single().Name);
        }
    }
}