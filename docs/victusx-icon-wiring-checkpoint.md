# VictusX Icon Wiring Checkpoint

## Current Wiring

- `app/VictusX.csproj` conditionally embeds `app/Assets/VictusX.ico` when it exists and otherwise uses `app/favicon.ico` as the compatible build fallback.
- HP-mode `NotifyIcon` uses the executable associated icon in `app/Program.cs`, while non-HP mode continues to start with inherited `Properties.Resources.standard`.
- `Settings.VisualiseIcon` preserves inherited GPU-mode tray resource swapping outside HP mode and intentionally skips it in HP mode.
- `app/UI/IconHelper.cs` obtains the large window icon from the executable associated icon. Therefore the executable icon will also cover the large form/window surface after `ApplicationIcon` is updated.

## Final Asset Contract

The approved final multi-resolution asset should be added at exactly:

```text
app/Assets/VictusX.ico
```

The reviewed asset is currently absent. The project now uses conditional wiring so dropping the approved file at the required path and rebuilding is sufficient:

1. `app/VictusX.csproj` selects `Assets\\VictusX.ico` only when that file exists; otherwise it retains `favicon.ico` as the build fallback.
2. `Program.GetTrayIcon()` extracts the executable icon in `--hp-victus` mode, so the same embedded asset supplies the HP tray icon without adding a generated resource entry.
3. `Settings.VisualiseIcon` returns immediately in HP mode, preventing inherited GPU-mode resources from replacing the HP tray icon. Default-mode tray behavior remains unchanged.

This gives the executable, taskbar, Explorer, Alt-Tab, and HP tray identity one approved source asset after it is supplied, while preserving inherited default-mode icon behavior outside HP mode.

## Metadata And Remaining Inheritance

`AssemblyName`, `Product`, description, company, authors, and version metadata already identify VictusX. The default launch profile has been renamed from `GHelper` to `VictusX`; no runtime behavior changes. `RootNamespace=GHelper`, `StartupObject=GHelper.Program`, resource logical names, `favicon.ico`, and the inherited tray/GPU icon resources remain for compatibility until the reviewed asset integration task.

No placeholder icon, generated resource entry, artwork, or package artifact was added. The final asset must satisfy the provenance, licensing, size, and dark/light contrast requirements in [VictusX Icon Asset Requirements](victusx-icon-asset-requirements.md) before it is supplied at the expected path.
