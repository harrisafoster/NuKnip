// <copyright file="IAssemblyReferenceReaderService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    /// <summary>
    /// Provides helper methods for reading assembly reference metadata from .NET assemblies.
    /// </summary>
    public interface IAssemblyReferenceReaderService
    {
        /// <summary>
        /// Retrieves the names of assemblies referenced by the specified assembly.
        /// </summary>
        /// <param name="assemblyPath">Path to the assembly file to inspect.</param>
        /// <returns>A collection containing the names of referenced assemblies.</returns>
        public IReadOnlyCollection<string> GetReferencedAssemblies(string assemblyPath);
    }
}