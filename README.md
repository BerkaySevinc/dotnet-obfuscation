# .NET Obfuscation

**.NET Obfuscation** library written in C#.</br>
A library designed to obfuscate and deobfuscate .NET assemblies for code protection purposes.</br>
The core obfuscation logic is separated into a reusable library, with a demo console application included for testing and demonstration.
</br>
</br>

# Details
- Written in C#.
- Targets **.NET assemblies**.
- Structured as multiple focused projects: a shared core, an obfuscator library, a deobfuscator library, and a demo application.
- Supports **obfuscation and deobfuscation** operations.
- Comes with a **demo console application** to test functionality.
- Uses **dnlib** for reading, modifying, and writing .NET assemblies.
</br>

# Features
- **Name obfuscation** — renames assemblies, modules, types, methods, fields, properties, events, and parameters.
- **String value obfuscation** — encodes string literals in method bodies using Base64 + UTF-8 decoding at runtime.
- **Junk field generation** — injects meaningless fields into types to increase complexity.
- **Multiple name generators:**
  - `ComplexNameGenerator` — generates random names using Unicode and ASCII characters with optional embedded signatures.
  - `OneLetterNameGenerator` — generates minimal binary-style names using a single repeated letter.
  - `SimpleNameGenerator` — generates readable names based on the member type (e.g. `int1`, `Type2`).
- **Configurable options** — each obfuscation step can be individually enabled or disabled via `ObfuscatorOptions`.
</br>

# Project Structure
| Project | Role |
|---|---|
| `DotNetAssembly.Core` | Shared abstractions: base classes, enums, and event args. |
| `DotNetAssembly.Obfuscator` | Obfuscation logic: name, value, and junk obfuscation. |
| `DotNetAssembly.Deobfuscator` | Deobfuscation logic. |
| `DotNetAssembly.Obfuscator.Demo` | Console application demonstrating the obfuscator. |
</br>

# Media
![Screenshot 1](Introduction%20Media/Screenshot%201.png)
![Screenshot 2](Introduction%20Media/Screenshot%202.png)
