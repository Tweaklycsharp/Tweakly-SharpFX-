# WeAnim - WPF Animation Showcase

A modern WPF application that demonstrates fluid animations and impressive visual effects.

## ✨ Features

- **Modern Interface** with Material Design theme
- **Smooth Animations** (fade, slide, scale, rotate)
- **Impressive Visual Effects** (particles, waves, neon)
- **Intuitive Navigation** between different sections
- **Interactive Particle System** responding to mouse clicks and movements
- **Automatic Demo Mode** showcasing all features

## 🛠️ Technologies Used

- WPF (Windows Presentation Foundation)
- C# .NET 6
- Material Design for WPF
- Microsoft XAML Behaviors

## 📋 Requirements

- Visual Studio 2022 (or later)
- .NET 6 SDK

## 🚀 Installation

1. Clone the repository:
   ```
   git clone https://github.com/your-username/WeAnim.git
   ```
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build and run the application

## 📝 User Guide

1. **Home Page**: Interact with the particle system by clicking and dragging your mouse
2. **Animations Page**: Try different animations (fade, slide, scale, rotate)
3. **Visual Effects Page**: Explore visual effects (particles, waves, neon)
4. **About Page**: Information about the project and contact
5. **Demo Mode**: Click the "Start Demo" button to see an automatic presentation of all features

## 🔨 Building the Executable

To build a standalone executable (.exe) file using command line:

```cmd
cd path\to\WeAnim
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=None -p:DebugSymbols=false
```

The executable will be located in:
```
bin\Release\net6.0-windows\win-x64\publish\WeAnim.exe
```

## 🎨 Customization

You can customize the application by modifying:
- Color themes in `App.xaml`
- Animation parameters in `MainWindow.xaml.cs`
- UI layout in `MainWindow.xaml`

---

⭐ Don't forget to star this project if you found it useful! ⭐