// <copyright file="FakeFileSystemService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IFileSystemService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeFileSystemService : IFileSystemService
    {
        /// <summary>
        /// Gets or sets a value indicating whether the file exists.
        /// </summary>
        public bool FileExistsResult { get; set; }

        /// <summary>
        /// Returns a value indicating whether the specified file exists.
        /// </summary>
        /// <param name="path">The path of the file to check.</param>
        /// <returns>
        /// <c>true</c> if the file exists; otherwise, <c>false</c>.
        /// </returns>
        public bool FileExists(string path)
        {
            return this.FileExistsResult;
        }
    }
}