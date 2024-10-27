# RF.RemovePlaylistCap
 A Rhythm Festival mod template for developing new mods.
 
# Requirements
 Visual Studio 2022 or newer\
 Taiko no Tatsujin: Rhythm Festival
 


# Build
 Install [BepInEx be 697](https://builds.bepinex.dev/projects/bepinex_be) or [BepInEx 6.0.0-pre.2](https://github.com/BepInEx/BepInEx/releases/tag/v6.0.0-pre.2) into your Rhythm Festival directory and launch the game.\
 This will generate all the dummy dlls in the interop folder that will be used as references.\
 Make sure you install the Unity.IL2CPP-win-x64 version.\
 Newer versions of BepInEx could have breaking API changes until the first stable v6 release, so those are not recommended at this time.
 
 Attempt to build the project, or copy the .csproj.user file from the Resources file to the same directory as the .csproj file.\
 Edit the .csproj.user file and place your Rhythm Festival file location in the "GameDir" variable.
