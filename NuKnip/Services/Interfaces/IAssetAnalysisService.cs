// <copyright file="IAssetAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    using System.Text.Json;
    using NuKnip.Models;

    /// <summary>
    /// Provides helper methods for extracting package information from NuGet assets files.
    /// </summary>
    public interface IAssetAnalysisService
    {
        /// <summary>
        /// Retrieves the top-level package dependencies defined in the assets file.
        /// </summary>
        /// <param name="assets">JSON document representing the assets file.</param>
        /// <returns>A collection of top-level package information entries.</returns>
        public IReadOnlyCollection<PackageInfo> GetTopLevelPackages(JsonDocument assets);

        /// <summary>
        /// Extracts package assemblies from the assets file target definitions.
        /// </summary>
        /// <param name="assets">JSON document representing the assets file.</param>
        /// <returns>A collection of package assembly information entries.</returns>
        public IReadOnlyCollection<PackageInfo> GetPackageAssemblies(JsonDocument assets);
    }
}