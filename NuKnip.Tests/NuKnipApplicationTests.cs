// <copyright file="NuKnipApplicationTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using NuKnip;
    using NuKnip.Models;
    using NuKnip.Tests.Fakes;

    /// <summary>
    /// Test class for <see cref="NuKnipApplication"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class NuKnipApplicationTests
    {
        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>0</c> when invoked with the
        /// help option.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>0</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WithHelp_ReturnsZero()
        {
            NuKnipApplication sut = CreateSut();

            int result = await sut.RunAsync(["--help"]);

            Assert.Equal(0, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>0</c> when invoked with the
        /// <c>help</c> command.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>0</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WithHelpKeyword_ReturnsZero()
        {
            NuKnipApplication sut = CreateSut();

            int result = await sut.RunAsync(["help"]);

            Assert.Equal(0, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>1</c> when invoked without any
        /// command-line arguments.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>1</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WithNoArguments_ReturnsOne()
        {
            NuKnipApplication sut = CreateSut();

            int result = await sut.RunAsync([]);

            Assert.Equal(1, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>1</c> when the specified project
        /// file does not exist.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>1</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenProjectDoesNotExist_ReturnsOne()
        {
            FakeFileSystemService files = new() { FileExistsResult = false, };

            NuKnipApplication sut = CreateSut(files: files);
            int result = await sut.RunAsync(["test.csproj"]);

            Assert.Equal(1, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>0</c> when the specified project
        /// exists and is built successfully.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>0</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenBuildSucceeds_ReturnsZero()
        {
            FakeBuildService build = new() { Result = true, };
            FakeFileSystemService files = new() { FileExistsResult = true, };
            NuKnipApplication sut = CreateSut(build: build, files: files);

            int result = await sut.RunAsync(["test.csproj"]);

            Assert.Equal(0, result);
            Assert.Equal(1, build.CallCount);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>1</c> when the specified project
        /// exists but the build operation fails.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>1</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenBuildFails_ReturnsOne()
        {
            FakeBuildService build = new() { Result = false, };
            FakeFileSystemService files = new() { FileExistsResult = true, };
            NuKnipApplication sut = CreateSut(build: build, files: files);

            int result = await sut.RunAsync(["test.csproj"]);

            Assert.Equal(1, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> skips the build operation when invoked with
        /// the <c>--no-build</c> option.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code.
        /// </returns>
        [Fact]
        public async Task RunAsync_WithNoBuild_SkipsBuild()
        {
            FakeBuildService build = new() { Result = true, };
            FakeFileSystemService files = new() { FileExistsResult = true, };
            NuKnipApplication sut = CreateSut(build: build, files: files);

            await sut.RunAsync(["test.csproj", "--no-build"]);

            Assert.Equal(0, build.CallCount);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>0</c> when package analysis
        /// identifies only used and unknown packages, with no unused packages found.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>0</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenUsedAndUnknownPackagesExist_ReturnsZero()
        {
            FakeFileSystemService files = new() { FileExistsResult = true, };
            FakeAssetAnalysisService assetAnalysis =
                new()
                {
                    TopLevelPackages =
                    [
                        new PackageInfo(
                            "UsedPackage",
                            "1.0.0",
                            []),

                        new PackageInfo(
                            "UnknownPackage",
                            "1.0.0",
                            []),
                    ],

                    PackageAssemblies =
                    [
                        new PackageInfo(
                            "UsedPackage",
                            "1.0.0",
                            ["UsedAssembly"]),

                        new PackageInfo(
                            "UnknownPackage",
                            "1.0.0",
                            []),
                    ],
                };

            FakePackageUsageAnalysisService usage =
                new()
                {
                    Result =
                        new PackageAnalysisResult
                        {
                            Used =
                            [
                                new PackageInfo(
                            "UsedPackage",
                            "1.0.0",
                            ["UsedAssembly"]),
                            ],

                            Unknown =
                            [
                                new PackageInfo(
                            "UnknownPackage",
                            "1.0.0",
                            []),
                            ],
                        },
                };

            NuKnipApplication sut = CreateSut(files: files, assetAnalysis: assetAnalysis, usage: usage);

            int result = await sut.RunAsync(["test.csproj", "--no-build"]);

            Assert.Equal(0, result);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>1</c> and generates a report when
        /// package analysis identifies one or more unused packages.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>1</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenUnusedPackagesExist_ReturnsOne()
        {
            FakeReportService report = new();
            FakeFileSystemService files = new() { FileExistsResult = true, };
            FakePackageUsageAnalysisService usage =
                new()
                {
                    Result =
                        new PackageAnalysisResult
                        {
                            Unused =
                            [
                                new PackageInfo(
                                "Newtonsoft.Json",
                                "13.0.0",
                                [])
                            ],
                        },
                };

            NuKnipApplication sut = CreateSut(files: files, report: report, usage: usage);

            int result = await sut.RunAsync(["test.csproj", "--no-build"]);

            Assert.Equal(1, result);
            Assert.True(report.WasCalled);
        }

        /// <summary>
        /// Verifies that <c>RunAsync</c> returns <c>0</c> when package analysis
        /// identifies no unused packages.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous application execution. The task
        /// result contains the application's exit code, which is expected to be
        /// <c>0</c>.
        /// </returns>
        [Fact]
        public async Task RunAsync_WhenNoUnusedPackagesExist_ReturnsZero()
        {
            FakeFileSystemService files = new() { FileExistsResult = true, };
            FakePackageUsageAnalysisService usage = new() { Result = new PackageAnalysisResult(), };

            NuKnipApplication sut = CreateSut(files: files, usage: usage);

            int result = await sut.RunAsync(["test.csproj", "--no-build"]);

            Assert.Equal(0, result);
        }

        private static NuKnipApplication CreateSut(
            FakeBuildService? build = null,
            FakeFileSystemService? files = null,
            FakeReportService? report = null,
            FakePackageUsageAnalysisService? usage = null,
            FakeAssetAnalysisService? assetAnalysis = null)
        {
            return new NuKnipApplication(
                build ?? new FakeBuildService(),
                new FakeAssetLocatorService(),
                new FakeAssetParsingService(),
                new FakeAssemblyLocatorService(),
                new FakeAssemblyReferenceReaderService(),
                new FakeConsoleService(),
                report ?? new FakeReportService(),
                assetAnalysis ?? new FakeAssetAnalysisService(),
                usage ?? new FakePackageUsageAnalysisService(),
                files ?? new FakeFileSystemService());
        }
    }
}