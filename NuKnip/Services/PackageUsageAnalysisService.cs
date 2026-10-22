// <copyright file="PackageUsageAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Package usage analysis service implementation.
    /// </summary>
    public sealed class PackageUsageAnalysisService : IPackageUsageAnalysisService
    {
        /// <inheritdoc/>
        public PackageAnalysisResult Analyze(IReadOnlyCollection<PackageInfo> packages, IReadOnlyCollection<string> referencedAssemblies)
        {
            List<PackageInfo> used = [];

            List<PackageInfo> unused = [];

            List<PackageInfo> unknown = [];

            foreach (PackageInfo package in packages)
            {
                if (package.Assemblies.Count == 0)
                {
                    unknown.Add(package);
                    continue;
                }

                if (package.Assemblies.Any(referencedAssemblies.Contains))
                {
                    used.Add(package);
                }
                else
                {
                    unused.Add(package);
                }
            }

            return new PackageAnalysisResult
            {
                Used = used,
                Unused = unused,
                Unknown = unknown,
            };
        }
    }
}