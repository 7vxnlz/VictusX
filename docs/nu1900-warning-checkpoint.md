# NU1900 Warning Checkpoint

## Current Disposition

`dotnet build VictusX.sln` and `dotnet test VictusX.sln` pass, but emit four recurring `NU1900` warnings because this SDK environment cannot retrieve NuGet vulnerability data from `https://api.nuget.org/v3/index.json`. The warning is non-fatal for local source compilation and test verification; it does not prove a dependency is vulnerable.

It does mean vulnerability-audit confidence is incomplete. A passing build/test result is not release-candidate audit evidence.

## Preview Acceptance Criteria

The warning is cleared for a preview candidate only when the same source revision has recorded evidence of either:

- clean `dotnet restore --force --no-cache`, build, and test runs without `NU1900`, plus interpretable direct/transitive vulnerability-list results; or
- an explicit maintainer alternate vulnerability-review disposition, its authoritative evidence, reviewer/date, and any separate handling of actual findings.

If `NU1900` still appears in release-candidate verification without that documented alternate disposition, preview publishing remains **NO-GO**.

Do not suppress `NU1900` blindly or disable package vulnerability auditing as a workaround. See the full [NU1900 Audit-Source Warning Disposition Plan](nu1900-audit-source-warning-disposition-plan.md).
