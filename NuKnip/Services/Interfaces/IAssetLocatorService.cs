// <copyright file="IAssetLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    /// <summary>
    /// Provides helper methods for locating generated NuGet assets files.
    /// </summary>
    public interface IAssetLocatorService
    {
        /// <summary>
        /// Retrieves the path to the project's assets file and validates that it exists.
        /// </summary>
        /// <param name="projectPath">Path to the project file.</param>
        /// <returns>The full path to the project's <c>project.assets.json</c> file.</returns>
        public string GetAssetsFile(string projectPath);
    }
}