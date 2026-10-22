// <copyright file="FakeReportService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock report service.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeReportService : IReportService
    {
        /// <summary>
        /// Gets a value indicating whether the service has been called.
        /// </summary>
        public bool WasCalled { get; private set; }

        /// <summary>
        /// Gets a value indicating the path to the project.
        /// </summary>
        public string? ProjectPath { get; private set; }

        /// <summary>
        /// Gets a list of packages for the report.
        /// </summary>
        public IReadOnlyCollection<PackageInfo>? Packages { get; private set; }

        /// <summary>
        /// Records the provided project path and package collection for verification
        /// during testing and marks the report service as having been invoked.
        /// </summary>
        /// <param name="projectPath">The path of the project for which the report is generated.</param>
        /// <param name="packages">The collection of packages included in the report.</param>
        public void WriteUnusedPackageReport(string projectPath, IReadOnlyCollection<PackageInfo> packages)
        {
            this.WasCalled = true;
            this.ProjectPath = projectPath;
            this.Packages = packages;
        }
    }
}