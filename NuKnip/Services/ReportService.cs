// <copyright file="ReportService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Implementation of report service.
    /// </summary>
    public sealed class ReportService : IReportService
    {
        /// <inheritdoc/>
        public void WriteUnusedPackageReport(string projectPath, IReadOnlyCollection<PackageInfo> packages)
        {
            File.WriteAllLines(
                "nuknip-report.txt",
                [
                    "NuKnip Report",
                    string.Empty,
                    $"Project: {Path.GetFileName(projectPath)}",
                    string.Empty,
                    "Unused Packages:",
                    string.Empty,
                    .. packages.Select(x => x.Name),
                ]);
        }
    }
}