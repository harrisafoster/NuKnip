// <copyright file="IFileSystemService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    /// <summary>
    /// Interface for the file system service.
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Determines the existence of a file.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>True/False - file exists.</returns>
        bool FileExists(string path);
    }
}