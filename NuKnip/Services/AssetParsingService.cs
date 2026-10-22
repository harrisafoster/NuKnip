// <copyright file="AssetParsingService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using System.Text.Json;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for parsing NuGet assets files.
    /// </summary>
    public sealed class AssetParsingService : IAssetParsingService
    {
        /// <inheritdoc/>
        public JsonDocument Parse(string assetsFilePath)
        {
            string json = File.ReadAllText(assetsFilePath);
            return JsonDocument.Parse(json);
        }
    }
}