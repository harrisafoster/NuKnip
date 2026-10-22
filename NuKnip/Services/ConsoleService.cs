// <copyright file="ConsoleService.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Services
{
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// Provides helper methods for writing text to the console.
    /// </summary>
    public sealed class ConsoleService : IConsoleService
    {
        /// <inheritdoc/>
        public void WriteColoredLine(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = originalColor;
        }

        /// <inheritdoc/>
        public void WriteError(string text)
        {
            Console.Error.WriteLine(text);
        }

        /// <inheritdoc/>
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}