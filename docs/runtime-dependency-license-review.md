# Runtime Dependency License Review

Reviewed: 2026-09-06

## Scope And Result

This source-level review covers every package in the current `app/VictusX.csproj` restore graph. Package identities, versions, license sources, and package-library license/notice files are evidenced. The review does not approve publication: runtime-pack and final-artifact matching remain open.

Evidence was taken from `app/VictusX.csproj`, `app/obj/project.assets.json`, package `.nuspec` and license/readme files restored from `https://api.nuget.org/v3/index.json`, and the identified upstream repositories. The local restore hashes matched the `project.assets.json` content hashes.

## Runtime Packages

| Package | Relationship | License evidence | Notice / attribution requirement | Review status |
| --- | --- | --- | --- | --- |
| FftSharp 2.2.0 | Direct | `MIT` expression in the [2.2.0 NuGet package metadata](https://www.nuget.org/packages/FftSharp/2.2.0); repository commit `3f5158f7ab146c8fb651028e8dce67407b3ded81` | Preserve the MIT copyright and permission notice in distributions containing the library. | Reviewed |
| HidSharpCore 1.3.0 | Direct | Package commit `bca4aee1c349985b0119eed24aba83e2dd63492d`; authoritative upstream [Apache-2.0 license](https://github.com/OpenTabletDriver/HIDSharpCore/blob/bca4aee1c349985b0119eed24aba83e2dd63492d/LICENSE) and [NOTICE](https://github.com/OpenTabletDriver/HIDSharpCore/blob/bca4aee1c349985b0119eed24aba83e2dd63492d/NOTICE.txt) | Bundle Apache-2.0 terms and retain the upstream HIDSharp NOTICE attribution. The package itself omits both files, so VictusX supplies them in `app/Assets/Licenses`. | Reviewed; files assembled |
| NAudio.Wasapi 2.3.0 | Direct | `MIT` expression in the [2.3.0 NuGet package metadata](https://www.nuget.org/packages/NAudio.Wasapi/2.3.0); repository commit `c89fee940ee6f8d7374d18714a6b85d8b7a18ab0` | Preserve the MIT copyright and permission notice. | Reviewed |
| NvAPIWrapper.Net 0.8.1.101 | Direct | Package `readme.txt`, package license URL, and tag `v0.8.1.101` [LGPL-3.0 license](https://github.com/falahati/NvAPIWrapper/blob/v0.8.1.101/LICENSE) | Prominently identify use of the library; bundle the LGPL-3.0 and incorporated GPL-3.0 texts; retain copyright/repository attribution; satisfy LGPL combined-work and relinking/replacement requirements. Self-contained single-file packaging has not been approved. | Reviewed; texts assembled, release disposition unresolved |
| System.Management 10.0.10 | Direct | `MIT` expression in the [10.0.10 NuGet package metadata](https://www.nuget.org/packages/System.Management/10.0.10); repository commit `f7d90799ce4ef09a0bb257852a57248d2a8fb8dd` | Preserve the MIT copyright and permission notice. | Reviewed |
| TaskScheduler 2.12.2 | Direct | `MIT` expression in the [2.12.2 NuGet package metadata](https://www.nuget.org/packages/TaskScheduler/2.12.2); repository commit `8f4803cf060b35f8299db26b45bfd6ff0f599c3c` | Preserve the MIT copyright and permission notice. | Reviewed |
| WinForms.DataVisualization 1.10.2 | Direct | `MIT` expression in the [1.10.2 NuGet package metadata](https://www.nuget.org/packages/WinForms.DataVisualization/1.10.2); repository commit `063510db7fa1e7fafbb19a6b1c79a8f25112a700` | Preserve the MIT copyright and permission notice. | Reviewed |
| NAudio.Core 2.3.0 | Transitive through NAudio.Wasapi | `MIT` expression in the [2.3.0 NuGet package metadata](https://www.nuget.org/packages/NAudio.Core/2.3.0); repository commit `c89fee940ee6f8d7374d18714a6b85d8b7a18ab0` | Preserve the MIT copyright and permission notice. | Reviewed |

## Microsoft.Management.Infrastructure Disposition

The only application use of `Microsoft.Management.Infrastructure` was the supplementary `HpCimReadinessProbe`: it created a `CimSession` and read class, instance, and method metadata. It did not invoke a CIM method and was not part of the HP command transport. The existing `System.Management` path already reads `root\wmi`, `hpqBIntM`, `hpqBDataIn`, and method metadata for the capability report and Diagnostic panel.

The direct MMI reference was therefore removed together with that duplicate probe. A clean restore during `dotnet build` confirms that `Microsoft.Management.Infrastructure`, `Microsoft.Management.Infrastructure.Runtime.Win`, and `Microsoft.Management.Infrastructure.Runtime.Unix` are absent from `app/obj/project.assets.json`. The current graph contains seven direct packages and one transitive package (`NAudio.Core`). Final package inspection must still confirm that no stale MMI binary or native asset is distributed.

## Framework And Test Separation

`Microsoft.NETCore.App` and `Microsoft.WindowsDesktop.App.WindowsForms` are framework references, not application `PackageReference` dependencies. A framework-dependent preview relies on the installed .NET runtime. A self-contained preview would distribute runtime components and therefore requires the applicable .NET license/notices to be identified during final package inspection.

The test project directly references `Microsoft.NET.Test.Sdk` 18.0.1, `xunit` 2.9.3, and `xunit.runner.visualstudio` 3.1.5. Its resolved tooling graph also contains Microsoft.CodeCoverage, Microsoft.TestPlatform components, Newtonsoft.Json, and xUnit support packages. These are test-only and are excluded from runtime notices unless a future distributed artifact actually contains them.

## NvAPIWrapper.Net LGPL Release Disposition

Current profile context: the HP preview publish profile is source-configured as self-contained single-file. The project also sets the assembled license directory, `LICENSE`, `THIRD-PARTY-NOTICES.md`, and the HP launcher to publish as external files rather than bundling those notices inside the executable.

Current obligation evidence: NvAPIWrapper.Net 0.8.1.101 is licensed under LGPL-3.0, with the upstream package readme and license evidence assembled in `app/Assets/Licenses`. The recorded obligations for a distributed preview are prominent identification of the library, the LGPL-3.0 and incorporated GPL-3.0 texts, copyright/repository attribution, and a packaging approach that satisfies LGPL combined-work relinking or replacement expectations.

Current disposition: **Still unresolved for the current self-contained single-file profile**. The assembled texts and attribution are necessary but are not enough to approve a single-file distribution. The remaining release decision is whether the preview will:

- change the publish profile so NvAPIWrapper is distributed in a replaceable external form, with clear license/notices and artifact inspection evidence;
- keep single-file packaging only after a documented legal/release review concludes the artifact still satisfies LGPL relinking/replacement expectations; or
- remove/replace the NvAPIWrapper-dependent GPU temperature path before distribution, with source, behavior, and notice review.

Until one of those decisions is made and verified against an actual release candidate, NvAPIWrapper.Net remains a preview publish blocker. This is not a product-code blocker for source-only work, and it does not change the existing read-only HP telemetry behavior.

## Reconciliation Decision

- Runtime dependency license identity review: **Complete for the current restore graph**.
- MMI runtime release disposition: **Resolved at source/restore-graph level** by removing the unused duplicate CIM transport and its package graph.
- Required package-library notice/license-text assembly: **Complete** under `app/Assets/Licenses`; source revisions and hashes are recorded in `LICENSE-SOURCES.md`.
- NvAPIWrapper.Net distribution method: **Unresolved for self-contained single-file**. The LGPL/GPL texts and attribution are present, but the current single-file profile has not been approved for LGPL combined-work relinking/replacement compliance.
- Self-contained .NET runtime notices: **Pending release-candidate evidence** because the final runtime pack has not been materialized or inspected.
- Final artifact match: **Pending** because no preview artifact exists.
- `THIRD-PARTY-NOTICES.md`: source-assembled, but not release-ready until the pending packaging and artifact checks are resolved.

This record is an engineering evidence review, not legal advice. Final artifact inspection remains required before any distribution claim.
