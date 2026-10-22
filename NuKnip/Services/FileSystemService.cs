// <copyright file="FileSystemService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Implementation of file system service.
    /// </summary>
    public sealed class FileSystemService : IFileSystemService
    {
        /// <inheritdoc/>
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}