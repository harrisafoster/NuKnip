// <copyright file="PackageAnalysisResult.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Models
{
    /// <summary>
    /// Result of package analysis.
    /// </summary>
    public sealed class PackageAnalysisResult
    {
        /// <summary>
        /// Gets or initializes used package list.
        /// </summary>
        public IReadOnlyCollection<PackageInfo> Used { get; init; } = [];

        /// <summary>
        /// Gets or initializes unused package list.
        /// </summary>
        public IReadOnlyCollection<PackageInfo> Unused { get; init; } = [];

        /// <summary>
        /// Gets or initializes unknown usage package list.
        /// </summary>
        public IReadOnlyCollection<PackageInfo> Unknown { get; init; } = [];
    }
}