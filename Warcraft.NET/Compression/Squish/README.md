https://github.com/MaxxWyndham/LibSquishNet

**LibSquishNet**

![Nuget](https://img.shields.io/nuget/v/LibSquishNet)

A partial port of Simon Brown's v1.11 libsquish to .NET Framework 4.6

Original project is available here https://code.google.com/p/libsquish/

**Changelog**

**v2.0.0**  
Parallism!  A basic implementation using Parallel.For on the primary loop.  Reduces compression time considerably.  Pass true into `CompressImage` for the new `parallel` parameter.  Default is false.  
Code-style.  The code is more modern C# style now.

**v1.11.0**  
Initial release