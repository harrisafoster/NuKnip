// <copyright file="FakeConsoleService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Mock implementation of <see cref="IConsoleService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FakeConsoleService : IConsoleService
    {
        /// <summary>
        /// Gets the lines.
        /// </summary>
        public List<string> Lines { get; } = [];

        /// <summary>
        /// Gets the list of errors.
        /// </summary>
        public List<string> Errors { get; } = [];

        /// <summary>
        /// Writes a line.
        /// </summary>
        /// <param name="text">Text to write.</param>
        public void WriteLine(string text)
        {
            this.Lines.Add(text);
        }

        /// <summary>
        /// Writes an error line.
        /// </summary>
        /// <param name="text">Text to write.</param>
        public void WriteError(string text)
        {
            this.Errors.Add(text);
        }

        /// <summary>
        /// Writes a colored text line.
        /// </summary>
        /// <param name="color">Color of the text tio write.</param>
        /// <param name="text">Text to write.</param>
        public void WriteColoredLine(ConsoleColor color, string text)
        {
            this.Lines.Add(text);
        }
    }
}