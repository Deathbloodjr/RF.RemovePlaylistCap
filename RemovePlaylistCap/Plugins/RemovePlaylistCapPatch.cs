using HarmonyLib;
using Scripts.OutGame.SongSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemovePlaylistCap.Plugins
{
    internal class RemovePlaylistCapPatch
    {
        [HarmonyPatch(typeof(PlaylistRegister.__c__DisplayClass12_0))]
        [HarmonyPatch(nameof(PlaylistRegister.__c__DisplayClass12_0._CheckPlayListMax_b__0))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void PlaylistRegister___c__DisplayClass12_0__CheckPlayListMax_b__0_Prefix(ref bool __result)
        {
            __result = false;
        }
    }
}
