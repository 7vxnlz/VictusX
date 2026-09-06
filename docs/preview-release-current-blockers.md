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
| Icon/app identity wiring | Pending | Conditional source wiring exists, but final identity cannot be verified until the reviewed icon asset is present and a package candidate exists. | [Icon implementation plan](victusx-icon-app-identity-implementation-plan.md) |
| Third-party/license notices status | Blocked | Project license, G-Helper modified-project attribution, HP/OMEN wording, and package notice contents need maintainer review against final package contents. | [Third-party notices audit](third-party-notices-audit.md), [Package notices plan](package-license-third-party-notices-completion-plan.md) |
| Runtime dependency license review | Blocked | Direct and transitive runtime dependencies still need authoritative identity/version/license/notice/bundling/attribution evidence and final artifact presence checks. | [Runtime dependency checklist](runtime-dependency-license-review-evidence-checklist.md) |
| `THIRD-PARTY-NOTICES.md` release readiness | Blocked | The source-level draft has been tightened to mark unresolved dependency, package, and icon reviews as pending. It is not release-ready until dependency notices, upstream attribution, icon attribution, and final artifact matching are reviewed. | [Third-Party Notices draft](../THIRD-PARTY-NOTICES.md) |
| `NU1900` disposition | Pending | Build/test warnings are currently non-fatal, but vulnerability-audit confidence remains incomplete. Need clean restore/build/test plus vulnerability-list evidence, or documented maintainer alternate review. | [NU1900 checkpoint](nu1900-warning-checkpoint.md) |
| Signing/checksum evidence | Blocked | No final artifact exists, nothing is signed, and no final SHA-256 checksums exist. Evidence must identify artifact, version, commit, signing status, hash, reviewer/date, and validation linkage. | [Signing/checksum evidence plan](signing-checksum-evidence-plan.md) |
| Clean-machine validation | Blocked | No final package has been validated on a clean Windows machine/VM. Requires artifact-specific launch, UI, no-control, Quit/process, path, crash, and reviewer/date evidence. | [Clean-machine evidence plan](clean-machine-validation-evidence-plan.md) |
| Final package contents inspection | Blocked | No artifact exists to inspect. Future inspection must verify executable identity, final icon, expected runtime files, notices/license, launcher arguments, absence of test/source/reference artifacts, absence of developer-only logs/device captures, and signing/checksum evidence. | [Package contents inspection](preview-package-contents-inspection.md), [Packaging readiness audit](windows-packaging-readiness-audit.md) |
| Dedicated preview package artifact | Pending | Publish profile and launcher source exist, but no release artifact should be created yet. Final artifact name/version/runtime choice still need release-candidate evidence. | [Packaging readiness audit](windows-packaging-readiness-audit.md) |
| HP telemetry usefulness gaps | Not required for preview | Remaining unavailable telemetry does not block source-only release prep if fail-closed wording remains clear. It may continue as safe read-only product work. | [HP UI usefulness gap audit](hp-victus-ui-usefulness-gap-audit.md) |
| Normal fan-control enablement | Not required for preview | Normal fan control must remain blocked for preview. No sliders, toggles, curves, pulse/run buttons, background writes, EC/PawnIO fallback, or normal fan-control UI are required or allowed. | [Fan write blocker summary](fan-write-blocker-summary.md) |

## Remaining Preview-Only Blockers

- Reviewed final icon asset at `app/Assets/VictusX.ico`.
- Reviewed package license and third-party notices.
- Runtime dependency license evidence for every runtime package candidate.
- `THIRD-PARTY-NOTICES.md` promoted from draft to reviewed release evidence.
- `NU1900` warning disposition evidence.
- Final signing decision and SHA-256 checksum evidence.
- Clean-machine validation against the exact package candidate.
- Final package contents inspection.

## Current Release Answer

Source-only work may continue. Preview package publishing remains blocked until the package-specific evidence above is complete.
