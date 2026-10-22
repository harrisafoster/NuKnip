// <copyright file="FakeAssemblyReferenceReaderService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IAssemblyReferenceReaderService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeAssemblyReferenceReaderService : IAssemblyReferenceReaderService
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public IReadOnlyCollection<string> Result { get; set; } = [];

        /// <summary>
        /// Returns the collection of referenced assembly names configured for the test.
        /// </summary>
        /// <param name="assemblyPath">The path to the assembly whose references are requested.</param>
        /// <returns>
        /// A read-only collection containing the names of the referenced assemblies.
        /// </returns>
        public IReadOnlyCollection<string> GetReferencedAssemblies(string assemblyPath)
        {
            return this.Result;
        }
    }
}