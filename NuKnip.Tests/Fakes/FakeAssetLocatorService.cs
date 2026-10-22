// <copyright file="FakeAssetLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IAssetLocatorService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeAssetLocatorService : IAssetLocatorService
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; } = "assets.json";

        /// <summary>
        /// Returns the path to the assets file associated with the specified project.
        /// </summary>
        /// <param name="projectPath">The path to the project file.</param>
        /// <returns>
        /// The path to the project's assets file.
        /// </returns>
        public string GetAssetsFile(string projectPath)
        {
            return this.Result;
        }
    }
}