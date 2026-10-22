// <copyright file="IReportService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    using NuKnip.Models;

    /// <summary>
    /// Interface for report writing service.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Writes unused package report to console and artifact.
        /// </summary>
        /// <param name="projectPath">Path to csproj file.</param>
        /// <param name="packages">Package information.</param>
        void WriteUnusedPackageReport(string projectPath, IReadOnlyCollection<PackageInfo> packages);
    }
}