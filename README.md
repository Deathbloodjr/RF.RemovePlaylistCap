# RF.ModTemplate
 A Rhythm Festival mod template for developing new mods.
 
# Requirements
 Visual Studio 2022 or newer\
 Taiko no Tatsujin: Rhythm Festival
 
 
# How to use
 On github, click the "Use this template" button -> Create a new repository\
 Check "Include all branches", which is just a "main" and "dev" branch\
 Give a Repository name\
 Select Private or Public repository\
 Clone the repo locally

 Change a few instances of "ModTemplate" to whatever you will name your mod <ModName>\
 That would be:
 - The main directory of this
 - ModTemplate.sln
    - Also open the .sln file in a text editor to change "ModTemplate", "ModTemplate\ModTemplate.csproj", replacing ModTemplate with <ModName>
 - ModTemplate folder
 - Within that folder, ModTemplate.csproj
 
 Now open the solution itself\
 In the .csproj file (double click the <ModName> project in solution explorer)
 - Change ModName to your ModName
 - Change the description to whatever your mod does (Or remember to do this later)
 - Change ModVersion to a fitting version in the format of x.x.x (or whatever your preference is)

 In Plugin.cs
 - Change the namespace to what you selected as RootNamespace earlier
    - In Visual Studio, you can highlight the namespace, hit Ctrl + R, Ctrl + R, and enter your new namespace to change all instances of it. 
    - You can change the namespaces in the 2 example patches as well, to RootNamespace.Patches
 - Change public const string ModName = your ModName

# Build
 Install [BepInEx be 697](https://builds.bepinex.dev/projects/bepinex_be) or [BepInEx 6.0.0-pre.2](https://github.com/BepInEx/BepInEx/releases/tag/v6.0.0-pre.2) into your Rhythm Festival directory and launch the game.\
 This will generate all the dummy dlls in the interop folder that will be used as references.\
 Make sure you install the Unity.IL2CPP-win-x64 version.\
 Newer versions of BepInEx could have breaking API changes until the first stable v6 release, so those are not recommended at this time.
 
 Attempt to build the project, or copy the .csproj.user file from the Resources file to the same directory as the .csproj file.\
 Edit the .csproj.user file and place your Rhythm Festival file location in the "GameDir" variable.
