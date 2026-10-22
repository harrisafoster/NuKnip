// <copyright file="FakeAssemblyLocatorService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IAssemblyLocatorService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeAssemblyLocatorService : IAssemblyLocatorService
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; } = "test.dll";

        /// <summary>
        /// Returns the assembly path configured for the test.
        /// </summary>
        /// <param name="projectPath">The path to the project file.</param>
        /// <returns>
        /// The path to the assembly associated with the specified project.
        /// </returns>
        public string GetAssemblyPath(string projectPath)
        {
            return this.Result;
        }
    }
}