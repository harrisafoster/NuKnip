#!/usr/bin/env bash
set -euo pipefail

COVERAGE_ROOT="./coverage"
COVERAGE_DIR="${COVERAGE_ROOT}/Coverage"
REPORT_DIR="${COVERAGE_ROOT}/CoverageReport"
JUNIT_LOG="${COVERAGE_ROOT}/dotnet-unit-test-results.xml"

mkdir -p "$COVERAGE_DIR" "$REPORT_DIR"

TMP_OUTPUT="$(mktemp)"

cleanup() {
  rm -f "$TMP_OUTPUT"
}

trap cleanup EXIT

echo "Running tests + coverage..."

set +e
dotnet test \
  --results-directory "$COVERAGE_DIR" \
  --collect "Code Coverage;Format=cobertura" \
  --verbosity normal \
  >"$TMP_OUTPUT" 2>&1

TEST_EXIT_CODE=$?
set -e

cat "$TMP_OUTPUT"

XML_FILE="$(
  find "$COVERAGE_DIR" \
    -type f \
    -iname '*cobertura.xml' \
    -printf '%T@ %p\n' |
    sort -nr |
    head -n1 |
    cut -d' ' -f2-
)"

if [[ -n "$XML_FILE" ]]; then
  echo "Cobertura file: ${XML_FILE}"
  echo "Generating HTML report (Html_Dark) into: ${REPORT_DIR}"

  reportgenerator \
    --riskHotspotsAnalysisThresholds:metricThresholdForCyclomaticComplexity=30 \
    -assemblyfilters:"+NuKnip;-xunit*;-Spectre.Console*" \
    -reports:"${XML_FILE}" \
    -targetdir:"${REPORT_DIR}" \
    -reporttypes:"Html_Dark;JsonSummary"

  echo "Coverage report generated at: ${REPORT_DIR}/index.html"

  SUMMARY_FILE="${REPORT_DIR}/Summary.json"

  branch_coverage="$(
    grep -m1 -oP '"branchcoverage":\s*\K[0-9.]+' "$SUMMARY_FILE"
  )"

  echo "cSharp coverage: ${branch_coverage}%"
else
  echo "Warning: Could not locate a Cobertura XML file in ${COVERAGE_DIR}"
fi

if [[ "$TEST_EXIT_CODE" -ne 0 ]]; then
  echo "ERROR: dotnet test failed with exit code ${TEST_EXIT_CODE}"
  exit "$TEST_EXIT_CODE"
fi

echo "All tests completed successfully."
