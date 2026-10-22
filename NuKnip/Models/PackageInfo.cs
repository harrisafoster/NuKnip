// <copyright file="PackageInfo.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Models
{
    /// <summary>
    /// Information about a package and its assemblies.
    /// </summary>
    /// <param name="Name">Name of package.</param>
    /// <param name="Version">Version of package.</param>
    /// <param name="Assemblies">Assemblies associated with a package.</param>
    public sealed record PackageInfo(string Name, string Version, IReadOnlyCollection<string> Assemblies)
    {
        /// <summary>
        /// Gets a value indicating whether package has associated assemblies.
        /// </summary>
        public bool HasAssemblies => this.Assemblies.Count > 0;
    }
}