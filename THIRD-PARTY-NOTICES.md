# Third-Party Notices (Source-Level Draft)

## Draft Status

Status: **Source-level dependency evidence reviewed; release packaging pending**.

This is a source-level draft for a future VictusX HP Diagnostic preview. It is not a completed package notice file and does not authorize publishing. License identities for the current runtime dependency graph have been reviewed, but required license-text bundling, restrictive-license disposition, and final package-content matching remain pending.

## VictusX Project Notice

Status: **Pending package review**.

VictusX is a modified project based on G-Helper. The repository includes the GNU General Public License version 3 text in `LICENSE`. A future package must include the applicable project license text, the source location and revision used for the package, and this notice after review against the final package contents.

## G-Helper Attribution

Status: **Present in draft; pending final package review**.

VictusX uses [G-Helper](https://github.com/seerge/g-helper) by seerge as its original application base. The project retains inherited application structure, including the `GHelper` root namespace and resource naming. VictusX is a modified project and is not affiliated with, authorized by, or endorsed by G-Helper.

The reviewed upstream source reference recorded by this repository is commit `5c26f5ac970dab9e26347d80976ebf1eece91b1e`. This attribution must remain in future package notices, but it does not replace review of applicable upstream license and notice requirements before distribution.

## Runtime Dependency Notice Review

The following dependencies are recorded by `app/VictusX.csproj` and the local resolved dependency inventory. License evidence is recorded in the [Runtime Dependency License Review](docs/runtime-dependency-license-review.md). Inclusion in a future artifact is not yet known.

| Package | Version | Relationship | License evidence | Release status |
| --- | --- | --- | --- | --- |
| FftSharp | 2.2.0 | Direct | MIT | License notice bundling pending |
| HidSharpCore | 1.3.0 | Direct | Apache-2.0 plus upstream NOTICE | Apache license and NOTICE bundling pending |
| Microsoft.Management.Infrastructure | 3.0.0 | Direct | MIT | License notice bundling pending; runtime package disposition below applies |
| NAudio.Wasapi | 2.3.0 | Direct | MIT | License notice bundling pending |
| NvAPIWrapper.Net | 0.8.1.101 | Direct | LGPL-3.0 | LGPL/GPL texts, prominent notice, attribution, and packaging-method review pending |
| System.Management | 10.0.10 | Direct | MIT | License notice bundling pending |
| TaskScheduler | 2.12.2 | Direct | MIT | License notice bundling pending |
| WinForms.DataVisualization | 1.10.2 | Direct | MIT | License notice bundling pending |
| Microsoft.Management.Infrastructure.Runtime.Unix | 3.0.0 | Transitive | MIT | Artifact presence and license notice bundling pending |
| Microsoft.Management.Infrastructure.Runtime.Win | 3.0.0 | Transitive Windows runtime | Custom Microsoft Software License Terms | **Release blocker:** current terms restrict use to PowerShell and prohibit redistribution; exclude/replace or obtain clearance |
| NAudio.Core | 2.3.0 | Transitive | MIT | License notice bundling pending |

Before distribution, assemble the required license and notice texts, resolve the Microsoft.Management.Infrastructure.Runtime.Win restriction, and compare this list with the final ZIP or installer contents. See [Dependency Notice Inventory](docs/dependency-notice-inventory.md) and [Package License Review Workflow](docs/package-license-review-workflow.md).

The [Runtime Dependency License Review Evidence Checklist](docs/runtime-dependency-license-review-evidence-checklist.md) now tracks the remaining release-candidate and artifact checks; the source-level findings are in the completed [Runtime Dependency License Review](docs/runtime-dependency-license-review.md).

## Test-Only Dependency Notice Review (Separate)

Status: **Pending only if distributed**.

The following packages are recorded only in the test project or its resolved test tooling graph. They are not assumed to be part of an HP Diagnostic preview package. Review them separately only if test tooling, a developer bundle, or another artifact containing them is distributed.

| Package | Version | Status |
| --- | --- | --- |
| Microsoft.NET.Test.Sdk | 18.0.1 | Direct test dependency; **Pending review if distributed** |
| xunit | 2.9.3 | Direct test dependency; **Pending review if distributed** |
| xunit.runner.visualstudio | 3.1.5 | Direct test dependency; **Pending review if distributed** |
| Microsoft.CodeCoverage | 18.0.1 | Resolved test transitive; **Pending review if distributed** |
| Microsoft.TestPlatform.ObjectModel | 18.0.1 | Resolved test transitive; **Pending review if distributed** |
| Microsoft.TestPlatform.TestHost | 18.0.1 | Resolved test transitive; **Pending review if distributed** |
| Newtonsoft.Json | 13.0.3 | Resolved test transitive; **Pending review if distributed** |
| xunit.abstractions | 2.0.3 | Resolved test transitive; **Pending review if distributed** |
| xunit.analyzers | 1.18.0 | Resolved test transitive; **Pending review if distributed** |
| xunit.assert | 2.9.3 | Resolved test transitive; **Pending review if distributed** |
| xunit.core | 2.9.3 | Resolved test transitive; **Pending review if distributed** |
| xunit.extensibility.core | 2.9.3 | Resolved test transitive; **Pending review if distributed** |
| xunit.extensibility.execution | 2.9.3 | Resolved test transitive; **Pending review if distributed** |

## Icon Attribution (Pending)

Status: **Pending asset**.

The current inherited icon is not approved for the future VictusX preview. `app/Assets/VictusX.ico` is not present. A replacement icon's ownership, license, provenance, and any required attribution must be recorded before it is included in a package. See [VictusX Icon Asset Requirements](docs/victusx-icon-asset-requirements.md).

## Trademark Notice

HP, OMEN, and Victus names are used only to identify compatibility targets and research context. VictusX is not affiliated with, authorized by, endorsed by, or certified by HP Inc. No statement in this draft implies an HP or OMEN endorsement.

## Preview Release Blocker

Status: **Blocked for release use**.

This document is a draft, not completed release evidence. A preview package remains blocked until the restrictive Microsoft.Management.Infrastructure.Runtime.Win disposition, required dependency license/notice bundling, final artifact-content comparison, upstream attribution review, icon attribution, signing/checksum evidence, and clean-machine validation are complete. Normal/user-facing fan control also remains NO-GO.
