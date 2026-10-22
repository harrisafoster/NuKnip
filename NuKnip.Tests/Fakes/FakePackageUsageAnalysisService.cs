// <copyright file="FakePackageUsageAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock version of <see cref="IPackageUsageAnalysisService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakePackageUsageAnalysisService : IPackageUsageAnalysisService
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public PackageAnalysisResult Result { get; set; } = new();

        /// <summary>
        /// Returns the predefined package analysis result configured for the test.
        /// </summary>
        /// <param name="packages">The packages to analyze.</param>
        /// <param name="referencedAssemblies">The assembly names referenced by the project.</param>
        /// <returns>
        /// A <see cref="PackageAnalysisResult"/> containing the package classification
        /// result for the analysis operation.
        /// </returns>
        public PackageAnalysisResult Analyze(IReadOnlyCollection<PackageInfo> packages, IReadOnlyCollection<string> referencedAssemblies)
        {
            return this.Result;
        }
    }
}