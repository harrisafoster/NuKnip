// <copyright file="FakeBuildService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IBuildService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeBuildService : IBuildService
    {
        /// <summary>
        /// Gets or sets a value indicating whether build succeeded.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Gets number of calls to this service.
        /// </summary>
        public int CallCount { get; private set; }

        /// <summary>
        /// Simulates building the specified project, records the invocation count, and
        /// returns the configured build result.
        /// </summary>
        /// <param name="projectPath">The path of the project to build.</param>
        /// <returns>
        /// A task that represents the asynchronous build operation. The task result
        /// contains <c>true</c> if the build succeeds; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> BuildAsync(string projectPath)
        {
            this.CallCount++;

            return Task.FromResult(this.Result);
        }
    }
}