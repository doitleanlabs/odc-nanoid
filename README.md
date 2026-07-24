# odc-nanoid

Source code for the **NanoID** OutSystems External Library, exposed to OutSystems Developer
Cloud (ODC) apps via External Logic.

Nano ID is a library for generating random IDs. Like UUID, there is a probability of duplicate
IDs, but that probability is extremely small.

- **Safe.** Uses a cryptographically strong random generator by default.
- **Compact.** Uses more symbols than UUID (`A-Za-z0-9_-`) and reaches the same number of unique
  options in just 21 symbols instead of 36.
- **Fast.** Nanoid is as fast as UUID generation but URL-safe.

This README targets people building/maintaining the `.cs` code. For OutSystems consumers of the
published library, see [DOC.md](DOC.md).

## Requirements

- .NET SDK 10.0 or later (`dotnet --list-sdks` to check installed SDKs)
- On Linux, publishing targets `linux-x64` (see `generate_upload_package.ps1`); adjust the `-r`
  runtime identifier if you need a different target.

## Repository structure

```
DoiTLean.NanoID/                 Library project (the External Library source)
  INanoID.cs                     Public interface, annotated with OutSystems OSInterface attribute
  NanoID.cs                      Implementation
  resources/icon.png             Icon embedded in the library, shown in OutSystems Service Studio
  generate_upload_package.ps1    Publish + zip packaging script
  Dist/                          Packaging output (zip uploaded to OutSystems), not committed
DoiTLean.NanoID.Tests/           xUnit test project
DoiTLean.NanoID.sln              Solution (both projects)
```

## Build

```bash
dotnet build DoiTLean.NanoID/DoiTLean.NanoID.sln
```

## Test

```bash
dotnet test DoiTLean.NanoID/DoiTLean.NanoID.sln
```

All public methods on `INanoID`/`NanoID` should have test coverage in
`DoiTLean.NanoID.Tests/NanoIDTests.cs`, including validation/error paths.

## Package / publish

`generate_upload_package.ps1` (PowerShell) publishes the library and zips the output for upload
to OutSystems ODC as an External Library:

```powershell
cd DoiTLean.NanoID
./generate_upload_package.ps1
```

Equivalent commands, if PowerShell (`pwsh`) isn't available:

```bash
cd DoiTLean.NanoID
dotnet publish -c Release -r linux-x64 --self-contained false
mkdir -p Dist
cd bin/Release/net10.0/linux-x64/publish
zip -r ../../../../../Dist/ODC-NanoID.zip .
```

The resulting `Dist/ODC-NanoID.zip` is what gets uploaded as the External Library asset in
OutSystems Service Studio / ODC Portal.

## Main concepts

- **`INanoID`** is the contract exposed to OutSystems. It's annotated with
  `[OSInterface(...)]` (from `OutSystems.ExternalLibraries.SDK`), which is what makes OutSystems
  recognize this as an External Logic interface, and generates the corresponding OutSystems
  action names, inputs and outputs from the C# method signatures.
- Method parameters use `out` parameters for return values (`out string result`) because that's
  how the OutSystems External Libraries SDK maps a C# method to an OutSystems action with
  outputs.
- Public method/parameter names and types are part of the OutSystems contract: renaming or
  changing them changes the generated OutSystems action/structure, which can break existing
  consumers. Treat any such change as a breaking change (see `INanoID.cs` and `NanoID.cs` XML
  doc comments for details on current behavior guarantees).
- `Generate` produces cryptographically secure IDs (default behavior of the underlying `Nanoid`
  package). `GenerateWithCustomRandomBytesGenerator` / `GenerateDeterministic` are deterministic
  (seeded) variants — same seed always produces the same ID — intended for reproducible
  scenarios such as tests, not for uniqueness-sensitive use cases like primary keys.

## Known limitations

- Deterministic methods (`GenerateWithCustomRandomBytesGenerator`, `GenerateDeterministic`) use
  `System.Random`, which is not cryptographically secure. This is intentional (they exist for
  reproducibility), but they must not be used where collision resistance matters.
- No test project existed prior to the .NET 10 migration; test coverage is limited to what was
  added during that migration.
