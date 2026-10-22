// <copyright file="BuildService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using System.Diagnostics;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for building .NET projects.
    /// </summary>
    public sealed class BuildService : IBuildService
    {
        /// <inheritdoc/>
        public async Task<bool> BuildAsync(string projectPath)
        {
            Process process = new()
            {
                StartInfo =
                    new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"build \"{projectPath}\" --nologo",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                    },
            };

            process.Start();

            await process.WaitForExitAsync();

            return process.ExitCode == 0;
        }
    }
}