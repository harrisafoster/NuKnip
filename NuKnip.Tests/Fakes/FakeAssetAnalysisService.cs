// <copyright file="FakeAssetAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IAssetAnalysisService" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeAssetAnalysisService : IAssetAnalysisService
    {
        /// <summary>
        /// Gets or sets the top level packages.
        /// </summary>
        public IReadOnlyCollection<PackageInfo> TopLevelPackages { get; set; } = [];

        /// <summary>
        /// Gets or sets the package assemblies.
        /// </summary>
        public IReadOnlyCollection<PackageInfo> PackageAssemblies { get; set; } = [];

        /// <summary>
        /// Returns the collection of top-level packages configured for the test.
        /// </summary>
        /// <param name="assets">The parsed project assets document.</param>
        /// <returns>
        /// A read-only collection containing the top-level package information.
        /// </returns>
        public IReadOnlyCollection<PackageInfo> GetTopLevelPackages(JsonDocument assets)
        {
            return this.TopLevelPackages;
        }

        /// <summary>
        /// Returns the collection of package assembly information configured for the test.
        /// </summary>
        /// <param name="assets">The parsed project assets document.</param>
        /// <returns>
        /// A read-only collection containing package information and their associated
        /// assemblies.
        /// </returns>
        public IReadOnlyCollection<PackageInfo> GetPackageAssemblies(JsonDocument assets)
        {
            return this.PackageAssemblies;
        }
    }
}