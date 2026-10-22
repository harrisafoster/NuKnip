// <copyright file="IAssetParsingService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    using System.Text.Json;

    /// <summary>
    /// Provides helper methods for parsing NuGet assets files.
    /// </summary>
    public interface IAssetParsingService
    {
        /// <summary>
        /// Reads and parses the specified assets file into a JSON document.
        /// </summary>
        /// <param name="assetsFilePath">Path to the assets file to parse.</param>
        /// <returns>A <see cref="JsonDocument"/> representing the parsed file contents.</returns>
        public JsonDocument Parse(string assetsFilePath);
    }
}