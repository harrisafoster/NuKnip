// <copyright file="IPackageUsageAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    using NuKnip.Models;

    /// <summary>
    /// Package usage analysis service interface.
    /// </summary>
    public interface IPackageUsageAnalysisService
    {
        /// <summary>
        /// Analyze package usage.
        /// </summary>
        /// <param name="packages">Package information.</param>
        /// <param name="referencedAssemblies">Referenced assemblies.</param>
        /// <returns>Results of analysis.</returns>
        public PackageAnalysisResult Analyze(IReadOnlyCollection<PackageInfo> packages, IReadOnlyCollection<string> referencedAssemblies);
    }
}