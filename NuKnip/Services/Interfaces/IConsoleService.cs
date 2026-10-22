// <copyright file="IConsoleService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services.Interfaces
{
    /// <summary>
    /// Interface for console writing service.
    /// </summary>
    public interface IConsoleService
    {
        /// <summary>
        /// Write a given line of text in the console.
        /// </summary>
        /// <param name="text">Text to write.</param>
        void WriteLine(string text);

        /// <summary>
        /// Write a given line with a given color in the console.
        /// </summary>
        /// <param name="color">Color of text.</param>
        /// <param name="text">Text to write.</param>
        void WriteColoredLine(ConsoleColor color, string text);

        /// <summary>
        /// Write an error message to console.
        /// </summary>
        /// <param name="text">Text to write.</param>
        void WriteError(string text);
    }
}