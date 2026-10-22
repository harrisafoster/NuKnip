// <copyright file="AssemblyLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for locating compiled project assemblies.
    /// </summary>
    public sealed class AssemblyLocatorService : IAssemblyLocatorService
    {
        /// <inheritdoc/>
        public string GetAssemblyPath(string projectPath)
        {
            string projectDirectory = Path.GetDirectoryName(projectPath)!;
            string projectName = Path.GetFileNameWithoutExtension(projectPath);
            string binDirectory = Path.Combine(projectDirectory, "bin");

            if (!Directory.Exists(binDirectory))
            {
                throw new DirectoryNotFoundException($"Bin directory not found: {binDirectory}");
            }

            string? assemblyPath = Directory.EnumerateFiles(binDirectory, $"{projectName}.dll", SearchOption.AllDirectories).FirstOrDefault()
                ?? throw new FileNotFoundException($"Assembly not found for project {projectName}");
            return assemblyPath;
        }
    }
}