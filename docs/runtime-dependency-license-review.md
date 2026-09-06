# Runtime Dependency License Review

Reviewed: 2026-09-06

## Scope And Result

This source-level review covers every package in the current `app/VictusX.csproj` restore graph. Package identities, versions, and license sources are evidenced. The review does not approve publication: final artifact presence, required license-text bundling, and one restrictive runtime-license disposition remain open.

Evidence was taken from `app/VictusX.csproj`, `app/obj/project.assets.json`, package `.nuspec` and license/readme files restored from `https://api.nuget.org/v3/index.json`, and the identified upstream repositories. The local restore hashes matched the `project.assets.json` content hashes.

## Runtime Packages

| Package | Relationship | License evidence | Notice / attribution requirement | Review status |
| --- | --- | --- | --- | --- |
| FftSharp 2.2.0 | Direct | `MIT` expression in the [2.2.0 NuGet package metadata](https://www.nuget.org/packages/FftSharp/2.2.0); repository commit `3f5158f7ab146c8fb651028e8dce67407b3ded81` | Preserve the MIT copyright and permission notice in distributions containing the library. | Reviewed |
| HidSharpCore 1.3.0 | Direct | Package commit `bca4aee1c349985b0119eed24aba83e2dd63492d`; authoritative upstream [Apache-2.0 license](https://github.com/OpenTabletDriver/HIDSharpCore/blob/bca4aee1c349985b0119eed24aba83e2dd63492d/LICENSE) and [NOTICE](https://github.com/OpenTabletDriver/HIDSharpCore/blob/bca4aee1c349985b0119eed24aba83e2dd63492d/NOTICE.txt) | Bundle Apache-2.0 terms and retain the upstream HIDSharp NOTICE attribution. The package itself omits both files, so VictusX must supply them if the assembly is distributed. | Reviewed; release action required |
| Microsoft.Management.Infrastructure 3.0.0 | Direct | `MIT` expression in the [3.0.0 NuGet package metadata](https://www.nuget.org/packages/Microsoft.Management.Infrastructure/3.0.0) | Preserve the MIT copyright and permission notice. Also review the platform runtime package below. | Reviewed |
| NAudio.Wasapi 2.3.0 | Direct | `MIT` expression in the [2.3.0 NuGet package metadata](https://www.nuget.org/packages/NAudio.Wasapi/2.3.0); repository commit `c89fee940ee6f8d7374d18714a6b85d8b7a18ab0` | Preserve the MIT copyright and permission notice. | Reviewed |
| NvAPIWrapper.Net 0.8.1.101 | Direct | Package `readme.txt`, package license URL, and upstream [LGPL-3.0 license](https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE) | Prominently identify use of the library; bundle the LGPL-3.0 and incorporated GPL-3.0 texts; retain copyright/repository attribution; satisfy LGPL combined-work and relinking requirements. Single-file packaging needs specific review. | Reviewed; release action required |
| System.Management 10.0.10 | Direct | `MIT` expression in the [10.0.10 NuGet package metadata](https://www.nuget.org/packages/System.Management/10.0.10); repository commit `f7d90799ce4ef09a0bb257852a57248d2a8fb8dd` | Preserve the MIT copyright and permission notice. | Reviewed |
| TaskScheduler 2.12.2 | Direct | `MIT` expression in the [2.12.2 NuGet package metadata](https://www.nuget.org/packages/TaskScheduler/2.12.2); repository commit `8f4803cf060b35f8299db26b45bfd6ff0f599c3c` | Preserve the MIT copyright and permission notice. | Reviewed |
| WinForms.DataVisualization 1.10.2 | Direct | `MIT` expression in the [1.10.2 NuGet package metadata](https://www.nuget.org/packages/WinForms.DataVisualization/1.10.2); repository commit `063510db7fa1e7fafbb19a6b1c79a8f25112a700` | Preserve the MIT copyright and permission notice. | Reviewed |
| Microsoft.Management.Infrastructure.Runtime.Unix 3.0.0 | Transitive | `MIT` expression in the [3.0.0 NuGet package metadata](https://www.nuget.org/packages/Microsoft.Management.Infrastructure.Runtime.Unix/3.0.0) | Preserve the MIT copyright and permission notice if included. Its Unix runtime asset should not be assumed present in a Windows package until artifact inspection. | Reviewed; artifact presence pending |
| Microsoft.Management.Infrastructure.Runtime.Win 3.0.0 | Transitive, Windows runtime assets | Package-supplied `LICENSE.txt` referenced by the [3.0.0 NuGet metadata](https://www.nuget.org/packages/Microsoft.Management.Infrastructure.Runtime.Win/3.0.0); custom Microsoft Software License Terms, not an SPDX license | The terms state use is solely with Microsoft PowerShell and prohibit sharing, publishing, or distributing the software. Current evidence does not clear this runtime for VictusX use or redistribution. It must be excluded/replaced or separately cleared before packaging. | Reviewed; release blocker |
| NAudio.Core 2.3.0 | Transitive through NAudio.Wasapi | `MIT` expression in the [2.3.0 NuGet package metadata](https://www.nuget.org/packages/NAudio.Core/2.3.0); repository commit `c89fee940ee6f8d7374d18714a6b85d8b7a18ab0` | Preserve the MIT copyright and permission notice. | Reviewed |

## Framework And Test Separation

`Microsoft.NETCore.App` and `Microsoft.WindowsDesktop.App.WindowsForms` are framework references, not application `PackageReference` dependencies. A framework-dependent preview relies on the installed .NET runtime. A self-contained preview would distribute runtime components and therefore requires the applicable .NET license/notices to be identified during final package inspection.

The test project directly references `Microsoft.NET.Test.Sdk` 18.0.1, `xunit` 2.9.3, and `xunit.runner.visualstudio` 3.1.5. Its resolved tooling graph also contains Microsoft.CodeCoverage, Microsoft.TestPlatform components, Newtonsoft.Json, and xUnit support packages. These are test-only and are excluded from runtime notices unless a future distributed artifact actually contains them.

## Reconciliation Decision

- Runtime dependency license identity review: **Complete for the current restore graph**.
- Runtime package release clearance: **Blocked** by `Microsoft.Management.Infrastructure.Runtime.Win` 3.0.0 terms.
- Required notice/license-text assembly: **Pending** for MIT components, HidSharpCore Apache-2.0/NOTICE, and NvAPIWrapper.Net LGPL-3.0/GPL-3.0 obligations.
- Final artifact match: **Pending** because no preview artifact exists.
- `THIRD-PARTY-NOTICES.md`: remains a source-level draft, not release-ready.

This record is an engineering evidence review, not legal advice. If the restrictive runtime package remains in the application graph, obtain a maintainer/legal disposition before any distribution.
