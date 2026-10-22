// <copyright file="NuKnipApplication.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip
{
    using System.Text.Json;
    using NuKnip.Models;
    using NuKnip.Services.Interfaces;

    /// <summary>
    /// NuKnip application workflow.
    /// </summary>
    /// <param name="buildService">Build service.</param>
    /// <param name="assetLocatorService">Asset location service.</param>
    /// <param name="assetParsingService">Asset parsing service.</param>
    /// <param name="assemblyLocatorService">Assembly location service.</param>
    /// <param name="assemblyReferenceReaderService">Assembly reference reader service.</param>
    /// <param name="consoleService">Console service.</param>
    /// <param name="reportService">Report service.</param>
    /// <param name="assetAnalysisService">Asset analysis service.</param>
    /// <param name="packageUsageAnalysisService">Package usage analysis service.</param>
    /// <param name="fileSystemService">File system service.</param>
    public sealed class NuKnipApplication(
        IBuildService buildService,
        IAssetLocatorService assetLocatorService,
        IAssetParsingService assetParsingService,
        IAssemblyLocatorService assemblyLocatorService,
        IAssemblyReferenceReaderService assemblyReferenceReaderService,
        IConsoleService consoleService,
        IReportService reportService,
        IAssetAnalysisService assetAnalysisService,
        IPackageUsageAnalysisService packageUsageAnalysisService,
        IFileSystemService fileSystemService)
    {
        private readonly IBuildService buildService = buildService;
        private readonly IAssetLocatorService assetLocatorService = assetLocatorService;
        private readonly IAssetParsingService assetParsingService = assetParsingService;
        private readonly IAssemblyLocatorService assemblyLocatorService = assemblyLocatorService;
        private readonly IAssemblyReferenceReaderService assemblyReferenceReaderService = assemblyReferenceReaderService;
        private readonly IConsoleService consoleService = consoleService;
        private readonly IReportService reportService = reportService;
        private readonly IAssetAnalysisService assetAnalysisService = assetAnalysisService;
        private readonly IPackageUsageAnalysisService packageUsageAnalysisService = packageUsageAnalysisService;
        private readonly IFileSystemService fileSystemService = fileSystemService;

        /// <summary>
        /// Run NuKnip.
        /// </summary>
        /// <param name="args">CLI args.</param>
        /// <returns>Exit code.</returns>
        public async Task<int> RunAsync(string[] args)
        {
            if (args.Contains("--help") || args.Contains("help"))
            {
                this.consoleService.WriteLine("Usage: nuknip <project.csproj> [--no-build]");
                return 0;
            }

            if (args.Length == 0)
            {
                this.consoleService.WriteError("Usage: nuknip <project.csproj> [--no-build]");
                return 1;
            }

            string projectPath = Path.GetFullPath(args[0]);

            if (!this.fileSystemService.FileExists(projectPath))
            {
                this.consoleService.WriteError($"Project not found: {projectPath}");
                return 1;
            }

            bool noBuild = args.Contains("--no-build");

            if (!noBuild)
            {
                this.consoleService.WriteLine("Building...");

                bool success = await this.buildService.BuildAsync(projectPath);

                if (!success)
                {
                    this.consoleService.WriteError("Build failed.");

                    return 1;
                }

                this.consoleService.WriteLine("Build succeeded.");
            }

            string assetsFilePath = this.assetLocatorService.GetAssetsFile(projectPath);
            JsonDocument assets = this.assetParsingService.Parse(assetsFilePath);
            IReadOnlyCollection<PackageInfo> packages = this.assetAnalysisService.GetTopLevelPackages(assets);
            string assemblyPath = this.assemblyLocatorService.GetAssemblyPath(projectPath);
            IReadOnlyCollection<string> referencedAssemblies = this.assemblyReferenceReaderService.GetReferencedAssemblies(assemblyPath);
            IReadOnlyCollection<PackageInfo> packageAssemblies = this.assetAnalysisService.GetPackageAssemblies(assets);

            HashSet<string> topLevelPackageNames =
            [
                .. packages.Select(x => x.Name),
            ];

            List<PackageInfo> topLevelPackageAssemblies =
            [
                .. packageAssemblies.Where(x => topLevelPackageNames.Contains(x.Name)),
            ];

            PackageAnalysisResult analysisResult =
                this.packageUsageAnalysisService.Analyze(
                    topLevelPackageAssemblies,
                    referencedAssemblies);

            this.consoleService.WriteLine(string.Empty);
            this.consoleService.WriteLine($"Used packages ({analysisResult.Used.Count})");

            foreach (PackageInfo package in analysisResult.Used.OrderBy(x => x.Name))
            {
                this.consoleService.WriteColoredLine(ConsoleColor.Green, $"    [^_^] {package.Name}");
            }

            this.consoleService.WriteLine(string.Empty);
            this.consoleService.WriteLine($"Unused packages ({analysisResult.Unused.Count})");

            foreach (PackageInfo package in analysisResult.Unused.OrderBy(x => x.Name))
            {
                this.consoleService.WriteColoredLine(ConsoleColor.Red, $"    [-_-] {package.Name}");
            }

            this.consoleService.WriteLine(string.Empty);
            this.consoleService.WriteLine($"Unknown ({analysisResult.Unknown.Count})");

            foreach (PackageInfo package in analysisResult.Unknown.OrderBy(x => x.Name))
            {
                this.consoleService.WriteColoredLine(ConsoleColor.Yellow, $"    [ ? ] {package.Name}");
            }

            if (analysisResult.Unused.Count != 0)
            {
                this.reportService.WriteUnusedPackageReport(
                    projectPath,
                    analysisResult.Unused);

                return 1;
            }

            return 0;
        }
    }
}