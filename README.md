# Advanced Volume Control - Reimagined

This is an attempt to re-implement and reimagine goals of the original
[Advanced Volume Control](https://www.reddit.com/r/ProgrammerHumor/comments/6f2c4v/advanced_volume_control/)
app, using modern technologies, such as [.NET Core 6](https://dotnet.microsoft.com/en-us/) and [Avalonia UI](https://avaloniaui.net/).

<details>
<summary>Images with implemenation</summary>

### Original idea and implementation:
![image info](./Pictures/OG.png)
### Implementation of this project:
![image info](./Pictures/New.png)

 </details>

> **Why?**
> 
"Science isn't about WHY. It's about WHY NOT. Why is so much of our science dangerous? Why not marry safe science if you love it so much. In fact, why not invent a special safety door that won't hit you on the butt on the way out, because you are fired." — Cave Johnson (Portal 2)

Serious answer: it was part of my learning process of Avalonia UI, .NET and other technologies.

## Supported audio backends
- [x] Pipewire (via `pipewire-pulse`)
- [x] PulseAudio
- [x] ALSA (via `pipewire-alsa` OR `pulseaudio-alsa`)
- [x] JACK (via `pipewire-jack` OR `pulseaudio-jack`)
- [x] Windows Audio
- [ ] Core Audio

## Building project

- Get a [.NET Core 6](https://dotnet.microsoft.com/en-us/) package for your system first;
- Clone this repository to the directory of your choice;
- Open terminal, or PowerShell window in directory with project
and run `dotnet build`
- Finally, navigate to `bin/Debug/net6.0/` and run executable file `FunnyVolumeApp`


Currently only Linux and Windows platforms are supported. <br>
I can't support Mac's audio backend due to lack of hardware
and software development tools. PR's welcome!
