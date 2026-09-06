# Preview Release Current Blockers

Updated: 2026-09-06

This is a concise current snapshot for the VictusX HP Diagnostic preview. It does not authorize publishing, signing, checksum generation, dependency changes, package creation, or runtime behavior changes.

## Current Decisions

- Source-only release preparation: **Done / GO**
- Preview package publish: **Blocked / NO-GO**
- Normal/user-facing fan control: **Blocked / NO-GO**
- Developer-only four-byte SetFanMax pulse/hold: operational under explicit CLI gates only; not part of preview UI release readiness.

## Blocker Snapshot

| Item | Status | Current blocker / evidence needed | Primary reference |
| --- | --- | --- | --- |
| HP inherited-shell UI visual state | Done | Manual confirmation is recorded: compact inherited shell, readable footer labels/icons, aligned disabled captions, and read-only Diagnostic side panel. Preserve during release prep. | [HP inherited shell UI checkpoint](hp-inherited-shell-ui-final-checkpoint.md) |
| Final `VictusX.ico` asset | Blocked | `app/Assets/VictusX.ico` is still missing. Final asset must be original or properly licensed, include provenance/attribution decision, and pass executable/window/tray/Explorer verification. | [Icon wiring checkpoint](victusx-icon-wiring-checkpoint.md), [Icon asset requirements](victusx-icon-asset-requirements.md) |
| Icon/app identity wiring | Pending | Conditional build wiring exists and the HP publish profile now fails closed while the reviewed icon is absent. Final identity cannot be verified until that asset and a package candidate exist. | [Icon implementation plan](victusx-icon-app-identity-implementation-plan.md) |
| Third-party/license notices status | Blocked | Package-library MIT, Apache-2.0/NOTICE, and LGPL-3.0/GPL-3.0 materials are assembled and publish-wired. The self-contained .NET runtime notice match and NvAPIWrapper LGPL single-file/relinking disposition remain open. | [Runtime dependency review](runtime-dependency-license-review.md), [Package notices](../THIRD-PARTY-NOTICES.md) |
| Runtime dependency license review | Done | Source-level identity/version/license evidence is recorded for all seven direct packages and the one transitive package in the current restore graph. Artifact presence remains a separate final inspection step. | [Runtime dependency review](runtime-dependency-license-review.md) |
| MMI runtime release disposition | Done | The sole MMI use was a duplicate read-only readiness probe. It and the direct package reference were removed; restored assets contain no MMI package. Final artifact inspection must confirm absence. | [Runtime dependency review](runtime-dependency-license-review.md) |
| `THIRD-PARTY-NOTICES.md` release readiness | Blocked | Package-library texts and attribution are assembled. Runtime-pack notices, NvAPIWrapper packaging compliance, icon attribution, and final artifact matching remain open. | [Third-Party Notices](../THIRD-PARTY-NOTICES.md) |
| NvAPIWrapper LGPL packaging disposition | Blocked | Current source profile is self-contained single-file. LGPL texts and attribution are assembled, but release must choose and verify one path: distribute NvAPIWrapper in replaceable external form, obtain documented approval for single-file LGPL compliance, or remove/replace the NvAPIWrapper-dependent GPU telemetry path before distribution. | [Runtime dependency review](runtime-dependency-license-review.md), [Package contents inspection](preview-package-contents-inspection.md) |
| `NU1900` disposition | Done for current source | Network-capable restore retrieved NuGet's vulnerability index/base/update data; both project graphs reported no vulnerable packages, and restore/build/test completed without `NU1900`. Repeat against the exact release candidate. | [NU1900 checkpoint](nu1900-warning-checkpoint.md) |
| Signing/checksum evidence | Blocked | No final artifact exists, nothing is signed, and no final SHA-256 checksums exist. Evidence must identify artifact, version, commit, signing status, hash, reviewer/date, and validation linkage. | [Signing/checksum evidence plan](signing-checksum-evidence-plan.md) |
| Clean-machine validation | Blocked | No final package has been validated on a clean Windows machine/VM. Requires artifact-specific launch, UI, no-control, Quit/process, path, crash, and reviewer/date evidence. | [Clean-machine evidence plan](clean-machine-validation-evidence-plan.md) |
| Final package contents inspection | Blocked | No artifact exists to inspect. Future inspection must verify executable identity, final icon, expected runtime files, notices/license, launcher arguments, absence of test/source/reference artifacts, absence of developer-only logs/device captures, and signing/checksum evidence. | [Package contents inspection](preview-package-contents-inspection.md), [Packaging readiness audit](windows-packaging-readiness-audit.md) |
| HP publish profile source | Done | Profile is `Release`, `net10.0-windows`, `win-x64`, self-contained, single-file, untrimmed, native self-extracting, and symbol-free. It requires the final icon and packages the license, notices, and safe HP launcher as external files. | [Package contents inspection](preview-package-contents-inspection.md) |
| Localized inherited branding review | Done | All localized `Strings*.resx` display values use VictusX instead of inherited G-Helper wording. Resource keys and compatibility/internal identifiers remain unchanged, with a focused regression test covering display values. | [Visible branding audit](victusx-visible-branding-audit.md) |
| Dedicated preview package artifact | Pending | Profile and launcher packaging are source-wired, but no release artifact should be created yet. Final artifact name/version and contents still need release-candidate evidence. | [Packaging readiness audit](windows-packaging-readiness-audit.md) |
| HP telemetry usefulness gaps | Not required for preview | Remaining unavailable telemetry does not block source-only release prep if fail-closed wording remains clear. It may continue as safe read-only product work. | [HP UI usefulness gap audit](hp-victus-ui-usefulness-gap-audit.md) |
| Normal fan-control enablement | Not required for preview | Normal fan control must remain blocked for preview. No sliders, toggles, curves, pulse/run buttons, background writes, EC/PawnIO fallback, or normal fan-control UI are required or allowed. | [Fan write blocker summary](fan-write-blocker-summary.md) |

## Remaining Preview-Only Blockers

- Reviewed final icon asset at `app/Assets/VictusX.ico`.
- Self-contained .NET runtime notices matched to the release candidate and NvAPIWrapper LGPL packaging/relinking disposition completed.
- NvAPIWrapper release path chosen and verified: external replaceable library, approved single-file compliance, or removal/replacement of the NvAPIWrapper-dependent runtime feature.
- `THIRD-PARTY-NOTICES.md` promoted from source-assembled status to reviewed release evidence after icon and final artifact matching.
- Final signing decision and SHA-256 checksum evidence.
- Clean-machine validation against the exact package candidate.
- Final package contents inspection.

## Current Release Answer

Source-only work may continue. Preview package publishing remains blocked until the package-specific evidence above is complete.
