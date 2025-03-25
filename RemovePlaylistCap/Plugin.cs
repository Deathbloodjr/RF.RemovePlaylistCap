using BepInEx.Unity.IL2CPP.Utils;
using BepInEx.Unity.IL2CPP;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BepInEx.Configuration;
using RemovePlaylistCap.Plugins;
using UnityEngine;
using System.Collections;
using SaveProfileManager.Plugins;
using System.Reflection;

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

            SetupConfig(Config, Path.Combine("BepInEx", "data", ModName));
            SetupHarmony();

            var isSaveManagerLoaded = IsSaveManagerLoaded();
            if (isSaveManagerLoaded)
            {
                AddToSaveManager();
            }
        }

        private void SetupConfig(ConfigFile config, string saveFolder, bool isSaveManager = false)
        {
            var dataFolder = Path.Combine("BepInEx", "data", ModName);

            if (!isSaveManager)
            {
                ConfigEnabled = config.Bind("General",
                   "Enabled",
                   true,
                   "Enables the mod.");
            }

            ConfigPlaylist1 = config.Bind("General",
                "Playlist 1",
                "Playlist 1",
                "Playlist 1's new name.");

            ConfigPlaylist2 = config.Bind("General",
                "Playlist 2",
                "Playlist 2",
                "Playlist 2's new name.");

            ConfigPlaylist3 = config.Bind("General",
                "Playlist 3",
                "Playlist 3",
                "Playlist 3's new name.");

            ConfigPlaylist4 = config.Bind("General",
                "Playlist 4",
                "Playlist 4",
                "Playlist 4's new name.");

            ConfigPlaylist5 = config.Bind("General",
                "Playlist 5",
                "Playlist 5",
                "Playlist 5's new name.");
        }

        private void SetupHarmony()
        {
            // Patch methods
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            LoadPlugin(ConfigEnabled.Value);
        }

        public static void LoadPlugin(bool enabled)
        {
            if (enabled)
            {
                bool result = true;
                // If any PatchFile fails, result will become false
                result &= Instance.PatchFile(typeof(RemovePlaylistCapPatch));
                result &= Instance.PatchFile(typeof(RenamePlaylistsPatch));
                if (result)
                {
                    Logger.Log($"Plugin {MyPluginInfo.PLUGIN_NAME} is loaded!");
                }
                else
                {
                    Logger.Log($"Plugin {MyPluginInfo.PLUGIN_GUID} failed to load.", LogType.Error);
                    Instance._harmony.UnpatchSelf();
                }
            }
            else
            {
                Logger.Log($"Plugin {MyPluginInfo.PLUGIN_NAME} is disabled.");
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
                Logger.Log("File patched: " + type.FullName);
#endif
                return true;
            }
            catch (Exception e)
            {
                Logger.Log("Failed to patch file: " + type.FullName);
                Logger.Log(e.Message);
                return false;
            }
        }

        public static void UnloadPlugin()
        {
            Instance._harmony.UnpatchSelf();
            Logger.Log($"Plugin {MyPluginInfo.PLUGIN_NAME} has been unpatched.");
        }

        public static void ReloadPlugin()
        {
            // Reloading will always be completely different per mod
            // You'll want to reload any config file or save data that may be specific per profile
            // If there's nothing to reload, don't put anything here, and keep it commented in AddToSaveManager
            //SwapSongLanguagesPatch.InitializeOverrideLanguages();
            //TaikoSingletonMonoBehaviour<CommonObjects>.Instance.MyDataManager.MusicData.Reload();
        }

        public void AddToSaveManager()
        {
            // Add SaveDataManager dll path to your csproj.user file
            // https://github.com/Deathbloodjr/RF.SaveProfileManager
            var plugin = new PluginSaveDataInterface(MyPluginInfo.PLUGIN_GUID);
            plugin.AssignLoadFunction(LoadPlugin);
            plugin.AssignUnloadFunction(UnloadPlugin);

            // Reloading will always be completely different per mod
            // You'll want to reload any config file or save data that may be specific per profile
            // If there's nothing to reload, don't put anything here, and keep it commented in AddToSaveManager
            //plugin.AssignReloadSaveFunction(ReloadPlugin);

            // Uncomment this if there are more config options than just ConfigEnabled
            //plugin.AssignConfigSetupFunction(SetupConfig);
            plugin.AddToManager(ConfigEnabled.Value);
        }

        private bool IsSaveManagerLoaded()
        {
            try
            {
                Assembly loadedAssembly = Assembly.Load("com.DB.RF.SaveProfileManager");
                return loadedAssembly != null;
            }
            catch
            {
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
