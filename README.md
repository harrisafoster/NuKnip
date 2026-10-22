# NuKnip

> Find potentially unused top-level NuGet packages before they become permanent residents of your project.

NuKnip is a lightweight command-line tool that analyzes a .NET project and reports which top-level NuGet packages appear to be:

- **Used**
- **Unused**
- **Unknown**

It compares the assemblies supplied by your top-level NuGet packages with the assemblies referenced by the compiled project.

The result is a fast, CI-friendly dependency check with readable console output and meaningful exit codes.

---

## Features

- Analyzes top-level NuGet package references
- Distinguishes used, unused, and unknown packages
- Builds the target project automatically
- Supports analysis without rebuilding
- Generates a report when unused packages are detected
- Returns non-zero exit codes for CI pipelines
- Uses color-coded, ASCII-only console output
- Keeps filesystem, build, parsing, and analysis behavior independently testable

---

## How It Works

NuKnip performs the following steps:

1. Resolves the supplied project path.
2. Builds the project unless `--no-build` is specified.
3. Locates the generated `project.assets.json` file.
4. Reads the project's top-level NuGet packages.
5. Locates the compiled project assembly.
6. Reads the assemblies referenced by that output.
7. Maps package-provided assemblies to the project's assembly references.
8. Classifies each top-level package as used, unused, or unknown.
9. Writes an unused-package report when necessary.
10. Returns an exit code suitable for local scripts and CI pipelines.

---

## Requirements

- A supported .NET SDK
- A restorable .NET project
- Generated NuGet restore assets
- Compiled project output when using `--no-build`

The target project must be restorable and buildable before NuKnip can analyze its assembly references.

---

## Usage

```text
nuknip <project.csproj> [--no-build]
```
