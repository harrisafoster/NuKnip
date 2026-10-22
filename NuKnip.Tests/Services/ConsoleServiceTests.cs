// <copyright file="ConsoleServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="ConsoleService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class ConsoleServiceTests
    {
        /// <summary>
        /// Verifies that <c>WriteLine</c> writes the specified message followed by a
        /// newline to the console output.
        /// </summary>
        [Fact]
        public void WriteLine_WritesToConsoleOutput()
        {
            ConsoleService sut = new();
            using StringWriter writer = new();
            TextWriter originalOut = Console.Out;

            try
            {
                Console.SetOut(writer);
                sut.WriteLine("Hello");
                Assert.Equal($"Hello{Environment.NewLine}", writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Verifies that <c>WriteError</c> writes the specified message followed by a
        /// newline to the standard error output stream.
        /// </summary>
        [Fact]
        public void WriteError_WritesToConsoleError()
        {
            ConsoleService sut = new();
            using StringWriter writer = new();
            TextWriter originalError = Console.Error;

            try
            {
                Console.SetError(writer);
                sut.WriteError("Error");
                Assert.Equal($"Error{Environment.NewLine}", writer.ToString());
            }
            finally
            {
                Console.SetError(originalError);
            }
        }

        /// <summary>
        /// Verifies that <c>WriteColoredLine</c> writes the specified message to the
        /// console output and restores the original foreground color after writing.
        /// </summary>
        [Fact]
        public void WriteColoredLine_WritesTextAndRestoresOriginalColor()
        {
            ConsoleService sut = new();
            using StringWriter writer = new();
            TextWriter originalOut = Console.Out;
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(writer);
                sut.WriteColoredLine(ConsoleColor.Red, "Hello");

                Assert.Equal($"Hello{Environment.NewLine}", writer.ToString());
                Assert.Equal(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
                Console.SetOut(originalOut);
            }
        }
    }
}