// <copyright file="AssemblyReferenceReaderServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="AssemblyReferenceReaderService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AssemblyReferenceReaderServiceTests
    {
        /// <summary>
        /// Verifies that <c>GetReferencedAssemblies</c> throws a <see cref="FileNotFoundException"/>
        /// when the specified assembly file does not exist.
        /// </summary>
        [Fact]
        public void GetReferencedAssemblies_WhenFileDoesNotExist_Throws()
        {
            AssemblyReferenceReaderService sut = new();

            Assert.Throws<FileNotFoundException>(() => sut.GetReferencedAssemblies(Guid.NewGuid() + ".dll"));
        }

        /// <summary>
        /// Verifies that <c>GetReferencedAssemblies</c> returns the referenced assembly names
        /// for a valid assembly file.
        /// </summary>
        [Fact]
        public void GetReferencedAssemblies_WhenAssemblyIsValid_ReturnsReferences()
        {
            AssemblyReferenceReaderService sut = new();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            IReadOnlyCollection<string> result = sut.GetReferencedAssemblies(assemblyPath);

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Verifies that <c>GetReferencedAssemblies</c> includes a known framework dependency
        /// in the collection of referenced assemblies for a valid assembly.
        /// </summary>
        [Fact]
        public void GetReferencedAssemblies_ReturnsKnownReference()
        {
            AssemblyReferenceReaderService sut = new();
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            IReadOnlyCollection<string> result = sut.GetReferencedAssemblies(assemblyPath);

            Assert.Contains(result, x => x.Equals("System.Runtime", StringComparison.OrdinalIgnoreCase));
        }
    }
}