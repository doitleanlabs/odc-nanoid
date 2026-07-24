# NanoID — OutSystems ODC External Library

This document is for OutSystems developers **consuming** the NanoID External Library in an ODC
app. For the library's source code / maintenance, see [README.md](README.md).

## What it does

Generates short, URL-safe, random unique IDs (NanoID), similar in purpose to a UUID but shorter
(21 characters by default) and safe to use in URLs.

## Installing

1. Download the packaged asset: `Dist/ODC-NanoID.zip` from this repository (built via
   `generate_upload_package.ps1`, or downloaded from a GitHub Release of this repo).
2. In OutSystems ODC Portal (or Service Studio, depending on your ODC workflow), add a new
   **External Library** and upload the `.zip`.
3. Once published, the library exposes a **NanoID** interface with the actions below. Add it as
   a dependency to your OutSystems module to use them.

## Actions

### `Generate`

Generates a cryptographically secure random NanoID. Use this for anything where you need
actual uniqueness guarantees (e.g. record identifiers).

**Inputs**

| Name | Type | Required | Default | Description |
|---|---|---|---|---|
| `Size` | Integer | No | `21` | Length of the generated ID. Lower size = shorter ID, but higher collision probability. Must be greater than 0. |
| `CustomAlphabet` | Text | No | `_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ` | Characters allowed in the generated ID. Must be non-empty and at most 256 characters. |

**Outputs**

| Name | Type | Description |
|---|---|---|
| `result` | Text | The generated ID. |

**Example**

Call `Generate()` with defaults:

```
result = "V1StGXR8_Z5jdHi6B-myT"
```

Call `Generate(Size: 10)`:

```
result = "IRFa-VaY2b"
```

**Errors**

- `Size <= 0` → the action throws (`ArgumentOutOfRangeException`); OutSystems surfaces this as a
  runtime error. Validate `Size > 0` before calling if the value comes from user input.
- `CustomAlphabet` empty/null or longer than 256 characters → throws
  (`ArgumentException`). Validate before calling if the alphabet is user-provided.

### `GenerateDeterministic`

Generates a **deterministic** NanoID from a seed: calling it again with the same `Seed`, `Size`
and `CustomAlphabet` always returns the same ID. **Not cryptographically random** — do not use
for scenarios requiring uniqueness guarantees (e.g. primary keys). Intended for reproducible
scenarios such as tests or fixtures.

**Inputs**

| Name | Type | Required | Default | Description |
|---|---|---|---|---|
| `Seed` | Integer | Yes | — | Seed for the pseudo-random generator. Same seed ⇒ same output. |
| `Size` | Integer | No | `21` | Length of the generated ID. Must be greater than 0. |
| `CustomAlphabet` | Text | No | `_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ` | Characters allowed in the generated ID. Must be non-empty and at most 256 characters. |

**Outputs**

| Name | Type | Description |
|---|---|---|
| `result` | Text | The generated ID. |

**Example**

Call `GenerateDeterministic(Seed: 42)` twice — both calls return the same value:

```
result = "3F6vQmZ_9pLxKd0Yh2Ntq"
```

**Errors**: same validation rules as `Generate` (`Size`, `CustomAlphabet`).

### `GenerateWithCustomRandomBytesGenerator` (legacy)

Same behavior as `GenerateDeterministic` (deterministic, seeded, not cryptographically secure).
Kept for backward compatibility with existing consumers. **New integrations should use
`GenerateDeterministic`** — same behavior, clearer name.

**Inputs**

| Name | Type | Required | Default | Description |
|---|---|---|---|---|
| `RandomSize` | Integer | Yes | — | Seed for the pseudo-random generator. Same value ⇒ same output. (Named `RandomSize` for backward compatibility; behaves identically to `Seed` on `GenerateDeterministic`.) |
| `Size` | Integer | No | `21` | Length of the generated ID. Must be greater than 0. |
| `CustomAlphabet` | Text | No | `_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ` | Characters allowed in the generated ID. Must be non-empty and at most 256 characters. |

**Outputs**

| Name | Type | Description |
|---|---|---|
| `result` | Text | The generated ID. |

**Errors**: same validation rules as `Generate` (`Size`, `CustomAlphabet`).

## Error handling in OutSystems

All three actions throw plain .NET exceptions on invalid input (`Size <= 0`, or an invalid
`CustomAlphabet`). In OutSystems, this surfaces as a runtime exception you can catch with an
**Exception Handler** in your logic flow. If `Size` or `CustomAlphabet` come from user input or
external data, validate them in OutSystems logic before calling these actions to avoid runtime
errors reaching end users.

## Breaking changes / migration notes

- **v1 → v2 (this release):** the output parameter of `Generate` and
  `GenerateWithCustomRandomBytesGenerator` was renamed from `NanoID` to `result` (to avoid
  clashing with the interface/class name `NanoID`). If you reference the old output name
  (`NanoID`) anywhere in OutSystems logic (e.g. an assign expression using the structure field
  name), you'll need to update it to `result` after upgrading the library version.
  `GenerateWithCustomRandomBytesGenerator`'s input name (`RandomSize`) was left unchanged for
  backward compatibility, even though it's actually a seed — use the new `GenerateDeterministic`
  action (parameter named `Seed`) for clearer naming going forward; behavior is identical.
