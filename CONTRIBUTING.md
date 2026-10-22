# Contributing to NuKnip

Thanks for taking the time to contribute to NuKnip.

NuKnip is intended to remain a small, predictable, and CI-friendly tool for identifying potentially unused top-level NuGet packages. Contributions should preserve that focus: accurate analysis, deterministic output, clear failure behavior, and maintainable code.

> This contributing guide is a draft. Repository links, supported SDK versions, and release procedures can be refined as the project evolves.

## Ways to Contribute

You can help by:

- Reporting reproducible bugs
- Improving package-usage detection
- Adding support for package types that are difficult to classify
- Improving diagnostics, reports, or CI integration
- Adding or improving tests
- Improving documentation
- Proposing focused enhancements

For substantial changes, open an issue before starting implementation so the problem and proposed direction can be discussed first.

## Before You Start

Before opening an issue or pull request:

1. Search the existing issues and pull requests for related work.
2. Verify that the behavior is reproducible with the latest version of NuKnip.
3. Reduce the problem to the smallest practical example.
4. Remove confidential source code, credentials, internal package names, and private feed details from logs and examples.

## Development Requirements

You will need:

- A compatible .NET SDK
- Git
- A restorable .NET project for end-to-end testing

Check the repository configuration, such as `global.json` and target framework declarations, for the exact SDK requirements.

## Local Setup

Clone the repository, then restore and build it:

```bash
git clone <repository-url>
cd NuKnip
dotnet restore
dotnet build --no-restore
```

Run the test suite:

```bash
dotnet test --no-build
```

Run NuKnip against a test project:

```bash
dotnet run --project <NuKnip-project-path> -- <target-project.csproj>
```

If the target project has already been built:

```bash
dotnet run --project <NuKnip-project-path> -- <target-project.csproj> --no-build
```

Replace the placeholders with paths appropriate for your local checkout.

## Reporting a Bug

A useful bug report includes:

- A concise description of the problem
- The expected behavior
- The actual behavior
- Reproduction steps
- The NuKnip version or commit
- The .NET SDK version from `dotnet --info`
- The operating system and architecture
- A minimal project or sanitized extract of `project.assets.json`, when relevant
- Console output and stack traces with sensitive information removed
- Whether the issue occurs with and without `--no-build`

For incorrect package classifications, also include:

- The package name and version
- The package's role in the project
- Whether it supplies managed assemblies, analyzers, source generators, build targets, content files, or native assets
- Why the package should be considered used, unused, or unknown

## Proposing an Enhancement

Enhancement proposals should explain:

- The problem being solved
- The intended users or workflow
- The proposed behavior
- Any CLI, output, report, or exit-code changes
- Compatibility implications
- Alternatives considered

Prefer focused additions over broad changes that expand NuKnip beyond package-usage analysis.

## Branches and Commits

Create a focused branch from the repository's default branch. Keep each commit limited to one logical change where practical.

Use clear, imperative commit messages, for example:

```text
Handle packages containing analyzer assets
```

Avoid mixing unrelated refactoring, formatting, and functional changes in the same pull request.

## Coding Guidelines

Contributions should follow the conventions already present in the repository.

In particular:

- Keep services focused on a single responsibility.
- Program against the existing interfaces where appropriate.
- Preserve dependency injection and testability.
- Prefer explicit, readable code over unnecessary abstraction.
- Keep nullable-reference-type warnings resolved.
- Avoid introducing new dependencies unless they provide clear value.
- Keep console output deterministic and suitable for CI logs.
- Do not rely on emoji glyphs or terminal-specific rendering.
- Preserve cross-platform behavior and path handling.
- Add concise XML documentation to public APIs.
- Treat warnings as issues to resolve, not output to ignore.

If a change affects package classification, document the reasoning and account for packages that do not expose ordinary managed assembly references.

## Tests

All behavior changes should include tests.

Cover, as applicable:

- The expected success path
- Invalid inputs
- Missing files or assets
- Build failures
- Used, unused, and unknown classifications
- Empty collections
- Case sensitivity and path behavior
- Deterministic ordering
- Exit-code behavior
- Report generation

Bug fixes should include a regression test that fails before the fix and passes afterward.

Before submitting a pull request, run:

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```

If the repository provides formatting, coverage, or static-analysis commands, run those as well.

## CLI and Exit-Code Compatibility

NuKnip is designed for scripts and CI pipelines. Changes to command-line arguments, console output, generated reports, or exit codes can therefore be breaking changes.

When modifying those areas:

- Preserve existing behavior unless the change is intentional and documented.
- Keep standard output and error output purposeful.
- Do not add prompts that require interactive input.
- Ensure failures return a non-zero exit code.
- Update the README and relevant tests.

## Pull Requests

A pull request should:

- Address one well-defined problem
- Explain what changed and why
- Link to the related issue, when one exists
- Include tests for new or changed behavior
- Update documentation when user-visible behavior changes
- Pass the complete build and test suite
- Avoid unrelated generated files or formatting changes

A helpful pull request description includes:

```markdown
## Summary

Describe the change and its purpose.

## Testing

List the commands and scenarios used to validate it.

## Compatibility

Describe any CLI, output, report, or exit-code impact.

## Checklist

- [ ] The solution builds without warnings.
- [ ] Existing tests pass.
- [ ] New or changed behavior is tested.
- [ ] User-facing documentation is updated.
- [ ] No confidential information is included.
```

Maintainers may request changes to keep the project focused, preserve compatibility, or improve test coverage.

## Security Issues

Do not publicly disclose a vulnerability before the maintainer has had a reasonable opportunity to investigate it.

Use the repository's private security-reporting channel if one is available. Otherwise, contact the maintainer privately through the contact method published on the repository profile. Include reproduction details, affected versions, potential impact, and any known mitigation.

Do not include real credentials, private package-feed tokens, or proprietary project files in a report.

## Documentation

Documentation changes should be concise and task-oriented. Commands should be directly executable after documented placeholders are replaced.

When changing behavior, update all affected documentation in the same pull request, including examples, CLI usage, exit codes, and limitations.

## Licensing

By submitting a contribution, you agree that your contribution will be licensed under the MIT License used by this repository. You also confirm that you have the right to submit the contribution.

## Code of Conduct

Be respectful, constructive, and focused on the technical work. Harassment, personal attacks, and discriminatory behavior are not acceptable.

A dedicated `CODE_OF_CONDUCT.md` may be added as the contributor community grows.
