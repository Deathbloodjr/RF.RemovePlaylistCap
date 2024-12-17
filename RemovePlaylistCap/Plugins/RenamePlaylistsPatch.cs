using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemovePlaylistCap.Plugins
{
    internal class RenamePlaylistsPatch
    {
        static Dictionary<string, ConfigEntry<string>> KeyConfigs = new Dictionary<string, ConfigEntry<string>>()
        {
            { "category_playlist_1", Plugin.Instance.ConfigPlaylist1 },
            { "category_playlist_2", Plugin.Instance.ConfigPlaylist2 },
            { "category_playlist_3", Plugin.Instance.ConfigPlaylist3 },
            { "category_playlist_4", Plugin.Instance.ConfigPlaylist4 },
            { "category_playlist_5", Plugin.Instance.ConfigPlaylist5 },
        };

        static Dictionary<string, string> KeyReplacements = new Dictionary<string, string>();

        static void InitializeKeyReplacements()
        {
            if (KeyReplacements.Count > 0)
            {
                return;
            }

            foreach (var pair in KeyConfigs)
            {
                if (pair.Value.Value != (string)pair.Value.DefaultValue &&
                    pair.Value.Value != "")
                {
                    KeyReplacements.Add(pair.Key, pair.Value.Value);
                }
            }
        }

        [HarmonyPatch(typeof(WordDataManager))]
        [HarmonyPatch(nameof(WordDataManager.GetWordListInfo))]
        [HarmonyPatch(new Type[] { typeof(string) })]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void WordDataManager_GetWordListInfo_Postfix(WordDataManager __instance, ref WordDataManager.WordListKeysInfo __result, string key)
        {
            InitializeKeyReplacements();

            if (key != null)
            {
                if (KeyReplacements.ContainsKey(key))
                {
                    __result.Text = KeyReplacements[key];
                }
            }
        }
    }
}
