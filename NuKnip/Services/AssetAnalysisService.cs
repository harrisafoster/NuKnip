// <copyright file="AssetAnalysisService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using System.Text.Json;
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for extracting package information from NuGet assets files.
    /// </summary>
    public sealed class AssetAnalysisService : IAssetAnalysisService
    {
        /// <inheritdoc/>
        public IReadOnlyCollection<PackageInfo> GetTopLevelPackages(JsonDocument assets)
        {
            JsonProperty framework = assets.RootElement.GetProperty("project").GetProperty("frameworks").EnumerateObject().First();
            JsonElement dependencies = framework.Value.GetProperty("dependencies");
            List<PackageInfo> packages = [];

            foreach (JsonProperty dependency in dependencies.EnumerateObject())
            {
                string name = dependency.Name;

                string version = dependency.Value.GetProperty("target").GetString() ?? "unknown";

                packages.Add(new PackageInfo(name, version, []));
            }

            return packages;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<PackageInfo> GetPackageAssemblies(JsonDocument assets)
        {
            List<PackageInfo> result = [];
            JsonElement targets = assets.RootElement.GetProperty("targets");
            JsonProperty framework = targets.EnumerateObject().First();

            foreach (JsonProperty package in framework.Value.EnumerateObject())
            {
                string[] parts = package.Name.Split('/');

                if (parts.Length != 2)
                {
                    continue;
                }

                string packageName = parts[0];
                string packageVersion = parts[1];
                List<string> assemblies = [];

                if (package.Value.TryGetProperty("compile", out JsonElement compile))
                {
                    foreach (JsonProperty file in compile.EnumerateObject())
                    {
                        string assemblyName = Path.GetFileNameWithoutExtension(file.Name);
                        assemblies.Add(assemblyName);
                    }
                }

                result.Add(new PackageInfo(packageName, packageVersion, assemblies));
            }

            return result;
        }
    }
}