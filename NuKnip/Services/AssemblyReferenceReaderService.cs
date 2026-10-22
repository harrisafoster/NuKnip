// <copyright file="AssemblyReferenceReaderService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using System.Reflection.Metadata;
    using System.Reflection.PortableExecutable;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for reading assembly reference metadata from .NET assemblies.
    /// </summary>
    public sealed class AssemblyReferenceReaderService : IAssemblyReferenceReaderService
    {
        /// <inheritdoc/>
        public IReadOnlyCollection<string> GetReferencedAssemblies(string assemblyPath)
        {
            using FileStream stream = File.OpenRead(assemblyPath);
            using PEReader peReader = new(stream);
            MetadataReader metadataReader = peReader.GetMetadataReader();
            HashSet<string> assemblies = new(StringComparer.OrdinalIgnoreCase);

            foreach (AssemblyReferenceHandle handle in metadataReader.AssemblyReferences)
            {
                AssemblyReference reference = metadataReader.GetAssemblyReference(handle);
                string name = metadataReader.GetString(reference.Name);
                assemblies.Add(name);
            }

            return assemblies;
        }
    }
}