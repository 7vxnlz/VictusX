# AI Working State

Updated: 2026-09-06

## Current Truth

- Product: VictusX, a .NET 10 Windows utility currently targeting HP Victus 16-s0035nt / SKU `7Z5Z2EA#AB8` / BIOS `F.31` / Thermal Policy V1.
- HP mode uses the compact inherited shell. Unsupported controls are visible but disabled/read-only. Diagnostic opens as a read-only owned side panel.
- Read-only status currently includes CPU load, battery/AC/charging, device detection, NVIDIA GPU temperature when NVAPI is available, internal-display refresh rate when Windows identifies it safely, named HP battery-care setting status when the read-only Instrumented BIOS inventory is accessible, exact-SKU keyboard-backlight capability evidence, and exact-device GPU-switch capability from cached SystemDesignData. Keyboard state/levels, numeric charge limits, and current GPU mode remain unavailable.
- CPU package temperature and V1 fan RPM remain unavailable. `FanGetLevel` is raw-only and must never be shown as RPM or percent. `FanMaxGet` is inconclusive.
- Developer-only four-byte SetFanMax pulse/hold is operational behind explicit CLI gates. Hold seconds mean bounded pre-restore wait, not physical fan-duration control.
- SetFanLevel percentage dry-run and first-write preflight are hardware/WMI-inert. First-write readiness is NO-GO; no value is selected.
- `DeviceValidatedInputLength` is null/unset. Normal/user-facing fan control is NO-GO.
- Source-only release preparation is GO. Preview publishing is NO-GO pending icon, remaining notice/package compliance, signing/checksums, clean-machine validation, and final artifact inspection.
- The isolated `Microsoft.Management.Infrastructure`/`CimSession` readiness probe and package graph were removed. HP namespace/class/method readiness remains read-only through the existing `System.Management` WMI path; the `Runtime.Win` distribution blocker is resolved at source/restore-graph level pending final artifact confirmation.
- `app/Assets/VictusX.ico` is currently absent. Conditional executable and HP tray icon wiring is ready; the inherited icon remains the fallback until a reviewed asset is added.
- Localized `Strings*.resx` display values have been cleared of inherited G-Helper product wording. Compatibility resource keys and internal identifiers remain unchanged.
- Runtime package-library license and NOTICE texts are assembled under `app/Assets/Licenses` from recorded authoritative sources and wired as external publish content. The NvAPIWrapper LGPL source architecture is resolved by excluding `NvAPIWrapper.dll` from the single-file bundle as a replaceable sidecar; release-candidate layout verification remains pending. Self-contained .NET runtime notice matching remains open.
- The HP preview profile is source-configured for Release `win-x64`, self-contained single-file, untrimmed and symbol-free output. It packages `LICENSE`, notices, the safe `--hp-victus` launcher, and the replaceable NvAPIWrapper assembly externally, and fails closed while the final icon or resolved NvAPIWrapper publish item is absent.
- First preview publish source readiness remains NO-GO until the final icon exists and `THIRD-PARTY-NOTICES.md` is promoted from source-assembled status after runtime-pack, external NvAPIWrapper layout, icon, and final artifact checks.
- `docs/preview-release-current-blockers.md` is the concise current snapshot for preview release blockers; deeper evidence remains in the packaging docs.

## Verification Baseline

- Branch: `main` tracking `origin/main`.
- Last verified: `dotnet restore VictusX.sln` and `dotnet build VictusX.sln` passed with zero warnings; `dotnet test VictusX.sln` passed 379/379.
- NU1900 is not suppressed. Current-source audit evidence is cleared: a network-capable restore retrieved official NuGet vulnerability data, both project graphs reported no vulnerable packages, and required restore/build/test completed without NU1900. Repeat on the exact release candidate.

## Context Routing

Load one domain pack from `docs/context-packs/`. Use `docs/reference-index.md` only for reference comparisons. Use historical decision documents only when the task touches that decision.
