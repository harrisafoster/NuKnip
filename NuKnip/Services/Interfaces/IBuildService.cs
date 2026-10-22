// <copyright file="IBuildService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    using System.Diagnostics;

    /// <summary>
    /// Provides helper methods for building .NET projects.
    /// </summary>
    public interface IBuildService
    {
        /// <summary>
        /// Builds the specified .NET project and indicates whether the build completed successfully.
        /// </summary>
        /// <param name="projectPath">Path to the project file or directory to build.</param>
        /// <returns><c>true</c> if the build succeeds; otherwise, <c>false</c>.</returns>
        public Task<bool> BuildAsync(string projectPath);
    }
}