# AI Working State

Updated: 2026-09-06

## Current Truth

- Product: VictusX, a .NET 8 Windows utility currently targeting HP Victus 16-s0035nt / SKU `7Z5Z2EA#AB8` / BIOS `F.31` / Thermal Policy V1.
- HP mode uses the compact inherited shell. Unsupported controls are visible but disabled/read-only. Diagnostic opens as a read-only owned side panel.
- Read-only status currently includes CPU load, battery/AC/charging, device detection, NVIDIA GPU temperature when NVAPI is available, internal-display refresh rate when Windows identifies it safely, and named HP battery-care setting status when the read-only Instrumented BIOS inventory is accessible. Numeric charge-limit values remain unavailable.
- CPU package temperature and V1 fan RPM remain unavailable. `FanGetLevel` is raw-only and must never be shown as RPM or percent. `FanMaxGet` is inconclusive.
- Developer-only four-byte SetFanMax pulse/hold is operational behind explicit CLI gates. Hold seconds mean bounded pre-restore wait, not physical fan-duration control.
- SetFanLevel percentage dry-run and first-write preflight are hardware/WMI-inert. First-write readiness is NO-GO; no value is selected.
- `DeviceValidatedInputLength` is null/unset. Normal/user-facing fan control is NO-GO.
- Source-only release preparation is GO. Preview publishing is NO-GO pending icon, notices/license review, NU1900 disposition, signing/checksums, clean-machine validation, and final artifact inspection.

## Verification Baseline

- Branch: `main` tracking `origin/main`.
- Last verified: `dotnet build VictusX.sln` passed with four recurring NU1900 audit-source warnings; `dotnet test VictusX.sln` passed 354/354.
- NU1900 is not suppressed and remains open for preview release evidence.

## Context Routing

Load one domain pack from `docs/context-packs/`. Use `docs/reference-index.md` only for reference comparisons. Use historical decision documents only when the task touches that decision.
