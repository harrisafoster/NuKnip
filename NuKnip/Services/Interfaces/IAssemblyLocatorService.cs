// <copyright file="IAssemblyLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    /// <summary>
    /// Provides helper methods for locating compiled project assemblies.
    /// </summary>
    public interface IAssemblyLocatorService
    {
        /// <summary>
        /// Retrieves the path to the compiled assembly for the specified project.
        /// </summary>
        /// <param name="projectPath">Path to the project file.</param>
        /// <returns>The full path to the project's compiled assembly.</returns>
        public string GetAssemblyPath(string projectPath);
    }
}