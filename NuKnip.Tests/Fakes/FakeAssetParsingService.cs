// <copyright file="FakeAssetParsingService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IAssetParsingService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeAssetParsingService : IAssetParsingService
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public JsonDocument Result { get; set; } = JsonDocument.Parse("{}");

        /// <summary>
        /// Returns the parsed assets document configured for the test.
        /// </summary>
        /// <param name="assetsFilePath">The path to the assets file to parse.</param>
        /// <returns>
        /// A <see cref="JsonDocument"/> representing the parsed contents of the
        /// specified assets file.
        /// </returns>
        public JsonDocument Parse(string assetsFilePath)
        {
            return this.Result;
        }
    }
}