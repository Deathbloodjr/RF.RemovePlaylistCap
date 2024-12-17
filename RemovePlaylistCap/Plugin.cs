using BepInEx.Unity.IL2CPP.Utils;
using BepInEx.Unity.IL2CPP;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BepInEx.Configuration;
using RemovePlaylistCap.Plugins;
using UnityEngine;
using System.Collections;

namespace RemovePlaylistCap
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, ModName, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public const string ModName = "RemovePlaylistCap";

        public static Plugin Instance;
        private Harmony _harmony = null;
        public new static ManualLogSource Log;


        public ConfigEntry<bool> ConfigEnabled;
        public ConfigEntry<string> ConfigPlaylist1;
        public ConfigEntry<string> ConfigPlaylist2;
        public ConfigEntry<string> ConfigPlaylist3;
        public ConfigEntry<string> ConfigPlaylist4;
        public ConfigEntry<string> ConfigPlaylist5;


        public override void Load()
        {
            Instance = this;

            Log = base.Log;

            SetupConfig();
            SetupHarmony();
        }

        private void SetupConfig()
        {
            var dataFolder = Path.Combine("BepInEx", "data", ModName);

            ConfigEnabled = Config.Bind("General",
                "Enabled",
                true,
                "Enables the mod.");

            ConfigPlaylist1 = Config.Bind("General",
                "Playlist 1",
                "Playlist 1",
                "Playlist 1's new name.");

            ConfigPlaylist2 = Config.Bind("General",
                "Playlist 2",
                "Playlist 2",
                "Playlist 2's new name.");

            ConfigPlaylist3 = Config.Bind("General",
                "Playlist 3",
                "Playlist 3",
                "Playlist 3's new name.");

            ConfigPlaylist4 = Config.Bind("General",
                "Playlist 4",
                "Playlist 4",
                "Playlist 4's new name.");

            ConfigPlaylist5 = Config.Bind("General",
                "Playlist 5",
                "Playlist 5",
                "Playlist 5's new name.");
        }

        private void SetupHarmony()
        {
            // Patch methods
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            if (ConfigEnabled.Value)
            {
                bool result = true;
                // If any PatchFile fails, result will become false
                result &= PatchFile(typeof(RemovePlaylistCapPatch));
                result &= PatchFile(typeof(RenamePlaylistsPatch));
                if (result)
                {
                    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} is loaded!");
                }
                else
                {
                    Log.LogError($"Plugin {MyPluginInfo.PLUGIN_GUID} failed to load.");
                    _harmony.UnpatchSelf();
                }
            }
            else
            {
                Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} is disabled.");
            }
        }

        private bool PatchFile(Type type)
        {
            if (_harmony == null)
            {
                _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            }
            try
            {
                _harmony.PatchAll(type);
#if DEBUG
                Log.LogInfo("File patched: " + type.FullName);
#endif
                return true;
            }
            catch (Exception e)
            {
                Log.LogInfo("Failed to patch file: " + type.FullName);
                Log.LogInfo(e.Message);
                return false;
            }
        }

        public static MonoBehaviour GetMonoBehaviour() => TaikoSingletonMonoBehaviour<CommonObjects>.Instance;
        public void StartCoroutine(IEnumerator enumerator)
        {
            GetMonoBehaviour().StartCoroutine(enumerator);
        }
    }
}
