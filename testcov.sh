#!/usr/bin/env bash

set -euo pipefail

COVERAGE_ROOT="./coverage"
COVERAGE_DIR="${COVERAGE_ROOT}/Coverage"
REPORT_DIR="${COVERAGE_ROOT}/CoverageReport"

rm -rf "$COVERAGE_ROOT"
mkdir -p "$COVERAGE_DIR" "$REPORT_DIR"

TMP_OUTPUT="$(mktemp)"

cleanup() {
  rm -f "$TMP_OUTPUT"
}

trap cleanup EXIT

echo "Running tests with coverage..."

set +e

dotnet test NuKnip.Tests/NuKnip.Tests.csproj \
  --configuration Release \
  --no-build \
  --no-restore \
  --results-directory "$COVERAGE_DIR" \
  --collect "Code Coverage;Format=cobertura" \
  --verbosity normal \
  >"$TMP_OUTPUT" 2>&1

TEST_EXIT_CODE=$?

set -e

cat "$TMP_OUTPUT"

if [[ "$TEST_EXIT_CODE" -ne 0 ]]; then
  echo "ERROR: dotnet test failed with exit code ${TEST_EXIT_CODE}"
  exit "$TEST_EXIT_CODE"
fi

XML_FILE="$(
  find "$COVERAGE_DIR" \
    -type f \
    -iname '*cobertura.xml' \
    -printf '%T@ %p\n' |
    sort -nr |
    head -n1 |
    cut -d' ' -f2-
)"

if [[ -z "$XML_FILE" ]]; then
  echo "ERROR: Could not locate a Cobertura XML file in ${COVERAGE_DIR}"
  exit 1
fi

echo "Cobertura file: ${XML_FILE}"
echo "Generating coverage report in: ${REPORT_DIR}"

reportgenerator \
  --riskHotspotsAnalysisThresholds:metricThresholdForCyclomaticComplexity=30 \
  -assemblyfilters:"+NuKnip;-xunit*;-Spectre.Console*" \
  -reports:"${XML_FILE}" \
  -targetdir:"${REPORT_DIR}" \
  -reporttypes:"Html_Dark;JsonSummary;Cobertura;Badges"

echo "Coverage report generated at: ${REPORT_DIR}/index.html"

SUMMARY_FILE="${REPORT_DIR}/Summary.json"

if [[ ! -f "$SUMMARY_FILE" ]]; then
  echo "ERROR: ReportGenerator did not create ${SUMMARY_FILE}"
  exit 1
fi

branch_coverage="$(
  sed -n \
    's/.*"branchcoverage"[[:space:]]*:[[:space:]]*\([0-9.]*\).*/\1/p' \
    "$SUMMARY_FILE" |
    head -n1
)"

if [[ -z "$branch_coverage" ]]; then
  echo "ERROR: Could not read branch coverage from ${SUMMARY_FILE}"
  exit 1
fi

echo "C# branch coverage: ${branch_coverage}%"
echo "All tests and coverage checks completed successfully."