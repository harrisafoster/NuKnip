// <copyright file="AssetAnalysisServiceTests.cs" company="N/A">
// Copyright (c) 2026 Harris Foster
// </copyright>

namespace NuKnip.Tests.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using NuKnip.Models;
    using NuKnip.Services;

    /// <summary>
    /// Test class for <see cref="AssetAnalysisService"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AssetAnalysisServiceTests
    {
        /// <summary>
        /// Verifies that <c>GetTopLevelPackages</c> returns the expected top-level package
        /// information from the project assets file.
        /// </summary>
        [Fact]
        public void GetTopLevelPackages_ReturnsPackages()
        {
            AssetAnalysisService sut = new();
            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "project": {
                        "frameworks": {
                          "net8.0": {
                            "dependencies": {
                              "Newtonsoft.Json": {
                                "target": "13.0.3"
                              }
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetTopLevelPackages(assets);

            PackageInfo package = Assert.Single(result);
            Assert.Equal("Newtonsoft.Json", package.Name);
            Assert.Equal("13.0.3", package.Version);
            Assert.Empty(package.Assemblies);
        }

        /// <summary>
        /// Verifies that <c>GetTopLevelPackages</c> assigns the value <c>"unknown"</c>
        /// when a top-level package dependency has a null target version.
        /// </summary>
        [Fact]
        public void GetTopLevelPackages_WhenTargetIsNull_ReturnsUnknown()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "project": {
                        "frameworks": {
                          "net8.0": {
                            "dependencies": {
                              "Newtonsoft.Json": {
                                "target": null
                              }
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetTopLevelPackages(assets);

            PackageInfo package = Assert.Single(result);
            Assert.Equal("unknown", package.Version);
        }

        /// <summary>
        /// Verifies that <c>GetTopLevelPackages</c> returns all top-level package dependencies
        /// defined in the project assets file when multiple packages are present.
        /// </summary>
        [Fact]
        public void GetTopLevelPackages_WithMultiplePackages_ReturnsAllPackages()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "project": {
                        "frameworks": {
                          "net8.0": {
                            "dependencies": {
                              "Newtonsoft.Json": {
                                "target": "13.0.3"
                              },
                              "Serilog": {
                                "target": "4.0.0"
                              }
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetTopLevelPackages(assets);

            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Verifies that <c>GetPackageAssemblies</c> returns the expected package information
        /// and associated assembly names when compile-time assemblies are defined in the assets file.
        /// </summary>
        [Fact]
        public void GetPackageAssemblies_WhenCompileExists_ReturnsAssemblies()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "targets": {
                        "net8.0": {
                          "Newtonsoft.Json/13.0.3": {
                            "compile": {
                              "lib/net8.0/Newtonsoft.Json.dll": {},
                              "lib/net8.0/Newtonsoft.Helper.dll": {}
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetPackageAssemblies(assets);

            PackageInfo package = Assert.Single(result);

            Assert.Equal("Newtonsoft.Json", package.Name);
            Assert.Equal("13.0.3", package.Version);
            Assert.Equal(2, package.Assemblies.Count);
            Assert.Contains("Newtonsoft.Json", package.Assemblies);
            Assert.Contains("Newtonsoft.Helper", package.Assemblies);
        }

        /// <summary>
        /// Verifies that <c>GetPackageAssemblies</c> returns package information with an
        /// empty assembly collection when no compile-time assemblies are defined in the assets file.
        /// </summary>
        [Fact]
        public void GetPackageAssemblies_WhenCompileMissing_ReturnsPackageWithNoAssemblies()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "targets": {
                        "net8.0": {
                          "Newtonsoft.Json/13.0.3": {
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetPackageAssemblies(assets);

            PackageInfo package = Assert.Single(result);
            Assert.Empty(package.Assemblies);
        }

        /// <summary>
        /// Verifies that <c>GetPackageAssemblies</c> ignores entries with an invalid package
        /// identifier format and does not include them in the returned collection.
        /// </summary>
        [Fact]
        public void GetPackageAssemblies_WhenPackageNameIsInvalid_SkipsPackage()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "targets": {
                        "net8.0": {
                          "InvalidPackageName": {
                            "compile": {
                              "Invalid.dll": {}
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetPackageAssemblies(assets);

            Assert.Empty(result);
        }

        /// <summary>
        /// Verifies that <c>GetPackageAssemblies</c> returns only packages with a valid
        /// package/version identifier and ignores invalid entries in the assets file.
        /// </summary>
        [Fact]
        public void GetPackageAssemblies_WithMixedPackages_OnlyReturnsValidOnes()
        {
            AssetAnalysisService sut = new();

            using JsonDocument assets =
                JsonDocument.Parse(
                    """
                    {
                      "targets": {
                        "net8.0": {
                          "InvalidPackage": {},
                          "Serilog/4.0.0": {
                            "compile": {
                              "lib/net8.0/Serilog.dll": {}
                            }
                          }
                        }
                      }
                    }
                    """);

            IReadOnlyCollection<PackageInfo> result = sut.GetPackageAssemblies(assets);

            PackageInfo package = Assert.Single(result);

            Assert.Equal("Serilog", package.Name);
            Assert.Equal("4.0.0", package.Version);
            Assert.Contains("Serilog", package.Assemblies);
        }
    }
}