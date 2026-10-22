// <copyright file="Program.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

using System.Diagnostics.CodeAnalysis;
using NuKnip;
using NuKnip.Services;

NuKnipApplication app =
    new(
        new BuildService(),
        new AssetLocatorService(),
        new AssetParsingService(),
        new AssemblyLocatorService(),
        new AssemblyReferenceReaderService(),
        new ConsoleService(),
        new ReportService(),
        new AssetAnalysisService(),
        new PackageUsageAnalysisService(),
        new FileSystemService());

return await app.RunAsync(args);

/// <summary>
/// Dummy program to allow decorator access.
/// </summary>
[ExcludeFromCodeCoverage]
public partial class Program
{
}