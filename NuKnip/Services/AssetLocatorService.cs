// <copyright file="AssetLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for locating generated NuGet assets files.
    /// </summary>
    public sealed class AssetLocatorService : IAssetLocatorService
    {
        /// <inheritdoc/>
        public string GetAssetsFile(string projectPath)
        {
            string projectDirectory = Path.GetDirectoryName(projectPath)!;
            string assetsFilePath = Path.Combine(projectDirectory, "obj", "project.assets.json");

            if (!File.Exists(assetsFilePath))
            {
                throw new FileNotFoundException("project.assets.json not found", assetsFilePath);
            }

            return assetsFilePath;
        }
    }
}